using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : SingletonMonoBehaviour<CameraController>
{
    public enum State
    {
        Player,
        Vista,
        Thinking
    }
    [SerializeField] private float playerSmooth, vistaSmooth, sitSmooth;
    [SerializeField] private Transform camTransform;
    private State state;
    private Transform target;
    private Transform playerCameraTarget, sitCameraTarget;
    private Vector3 initialPlayerCameraTargetPosition;
    private float smooth;
    private Vector3 velocity = Vector3.zero;

    private DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> smoothTween;

    protected override void Awake()
    {
        base.Awake();
        state = State.Player;
    }

    private void Start()
    {
        playerCameraTarget = PlayerController.instance.cameraTarget;
        sitCameraTarget = PlayerController.instance.sitCameraTarget;
        initialPlayerCameraTargetPosition = playerCameraTarget.localPosition;
        target = playerCameraTarget;
        smooth = playerSmooth;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smooth);
    }

    public void ActivateVista(Transform newTarget)
    {
        target = newTarget;
        state = State.Vista;
        smooth = vistaSmooth;
        smoothTween.Kill();
    }

    public void ZoomTo(Transform newTarget, float height = 0f, float zoom = 2f)
    {
        playerCameraTarget.position = newTarget.position + initialPlayerCameraTargetPosition / zoom + Vector3.up * height + Vector3.forward * height;
        smoothTween.Kill();
        smooth = sitSmooth;
    }

    public void ResetToPlayer()
    {
        playerCameraTarget.localPosition = initialPlayerCameraTargetPosition;
        target = playerCameraTarget;
        state = State.Player;
        smoothSmooth(2f);
    }

    public void Shake(float strenght = 0.15f)
    {
        camTransform.DOKill();
        camTransform.DOShakePosition(0.4f, strenght, 10);
    }

    public void SitZoom(bool active)
    {
        if (active)
        {
            smooth = sitSmooth;
            target = sitCameraTarget;
            state = State.Thinking;
            smoothTween.Kill();
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
}
