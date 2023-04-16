using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : SingletonMonoBehaviour<CameraController>
{
    public enum State
    {
        Player,
        Zoom,
        Vista
    }
    [SerializeField] private float playerSmooth, sitSmooth, attractSmooth;
    [SerializeField] private Camera cam;
    public State state { get; private set; }
    private Transform target;
    private Transform playerCameraTarget, sitCameraTarget;
    private Vector3 initialPlayerCameraTargetPosition;
    private float smooth;
    private Vector3 velocity = Vector3.zero;

    private PlayerController player;
    private bool isSnapping;

    private bool isFrozen;

    private DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> smoothTween;

    protected override void Awake()
    {
        base.Awake();
        state = State.Player;
    }

    private void Start()
    {
        player = PlayerController.instance;
        playerCameraTarget = player.cameraTarget;
        sitCameraTarget = player.sitCameraTarget;
        initialPlayerCameraTargetPosition = playerCameraTarget.localPosition;
        target = playerCameraTarget;
        smooth = playerSmooth;
    }

    void FixedUpdate()
    {
        if (isSnapping)
        {
            if ((transform.position - target.position).magnitude > 2f) transform.position = target.position;
            isSnapping = false;
        }
        if (!isFrozen)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smooth);
        }
    }

    public void ActivateVista(Transform newTarget, float vistaSmooth)
    {
        if (state == State.Zoom) return;

        state = State.Vista;
        target = newTarget;
        smoothTween.Kill();
        smooth = vistaSmooth;
    }

    public void ZoomTo(Transform newTarget, float height = 0f, float zoom = 2f)
    {
        target = playerCameraTarget;
        playerCameraTarget.position = newTarget.position + initialPlayerCameraTargetPosition / zoom + Vector3.up * height + Vector3.forward * height;
        smoothTween.Kill();
        smooth = sitSmooth;
        state = State.Zoom;
    }

    public void ResetToPlayer()
    {
        playerCameraTarget.localPosition = initialPlayerCameraTargetPosition;
        target = playerCameraTarget;
        state = State.Player;
        smoothSmooth(2f);
    }

    public void CancelVista()
    {
        if(state != State.Vista) return;

        target = playerCameraTarget;
        smoothTween.Kill();
        smooth = playerSmooth;
    }

    public void AttractTo(Vector3 target, float lerpFactor)
    {
        if (isFrozen) return;
        if (state == State.Zoom) return;

        if (isSnapping)
        {
            transform.position = Vector3.Lerp(player.transform.position + initialPlayerCameraTargetPosition, target, lerpFactor);
            isSnapping = false;
        }
        playerCameraTarget.position = Vector3.Lerp(player.transform.position + initialPlayerCameraTargetPosition, target, lerpFactor);
        smooth = Mathf.Lerp(playerSmooth, attractSmooth, lerpFactor);
    }

    public void Shake(float strenght = 0.15f)
    {
        cam.DOKill();
        cam.DOShakePosition(0.4f, strenght, 10);
    }

    // Instantly move camera toward its target
    public void Snap()
    {
        isSnapping = true;
    }

    public void SitZoom(bool active)
    {
        if (active)
        {
            smooth = sitSmooth;
            target = sitCameraTarget;
            smoothTween.Kill();
            state = State.Zoom;
            sitCameraTarget.DOKill();
            sitCameraTarget.DOMove(sitCameraTarget.position + Vector3.up * 0.05f + Vector3.forward * 0.05f, 3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            smoothSmooth(2f);
            target = playerCameraTarget;
            state = State.Player;
            sitCameraTarget.DORestart();
            sitCameraTarget.DOKill();
        }
    }

    private void smoothSmooth(float duration)
    {
        smoothTween.Kill();
        smoothTween = DOTween.To(() => smooth, (s) => smooth = s, playerSmooth, duration).SetEase(Ease.OutSine);
    }

    public Camera GetCamera()
    {
        return cam;
    }

    public void Freeze(bool freeze)
    {
        isFrozen = freeze;
    }
}
