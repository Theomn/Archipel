using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingletonMonoBehaviour<CameraController>
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float smoothness;
    private Vector3 velocity;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(targetTransform.position, transform.position, smoothness);
    }
}
