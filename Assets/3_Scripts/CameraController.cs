using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingletonMonoBehaviour<CameraController>
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothness;
    private Vector3 velocity;

    protected override void Awake()
    {
        base.Awake();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(target.position, transform.position, smoothness);
    }
}
