using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttractor : MonoBehaviour
{
    [SerializeField] private float strenght, innerRadius;
    [SerializeField] private Transform cameraTarget;

    private Transform player;

    private SphereCollider coll;

    public Vector3 target
    {
        get
        {
            return cameraTarget.position;
        }
        private set { }
    }

    private CameraController cam;

    private void Start()
    {
        cam = CameraController.instance;
        player = PlayerController.instance.transform;
        coll = GetComponent<SphereCollider>();
    }

    private void Attract()
    {
        float distanceToPlayer = (transform.position - player.position).magnitude;
        float lerpFactor = (coll.radius - distanceToPlayer) / (coll.radius - innerRadius);
        lerpFactor = Mathf.Clamp(lerpFactor, 0f, 1f);
        lerpFactor *= strenght;
        cam.AttractTo(target, lerpFactor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        Attract();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;
        if (cam.state == CameraController.State.Zoom) return;

        Attract();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        cam.ResetToPlayer();
    }

    
}
