using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : SingletonMonoBehaviour<CameraController>
{
    public enum State
    {
        Player,
        Vista
    }
    [SerializeField] private float playerSmooth;
    [SerializeField] private float vistaSmooth;
    [SerializeField] private Transform camTransform;
    private State state;
    private Vector3 target;
    private Transform playerCamera;
    private Vector3 velocity = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        state = State.Player;
    }

    private void Start() 
    {
        playerCamera = PlayerController.instance.cameraTarget;
    }

    void FixedUpdate()
    {
        if (state == State.Player)
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerCamera.position, ref velocity, playerSmooth);
        }
        else if (state == State.Vista)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, vistaSmooth);
        }
    }

    public void ActivateVista(Vector3 newTarget)
    {
        target = newTarget;
        state = State.Vista;
    }

    public void DeactivateVista()
    {
        state = State.Player;
    }

    public void Shake()
    {
        camTransform.DOKill();
        camTransform.DOShakePosition(0.4f, 0.15f, 10);
    }
}
