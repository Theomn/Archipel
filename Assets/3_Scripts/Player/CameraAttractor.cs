using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttractor : MonoBehaviour
{
    [Tooltip("How strong the attractor attracts the camera. Max is 1.")]
    [SerializeField] private float strenght;
    [Tooltip("Defines an inner radius within the attractor within which the strenght is at maximum.")]
    [SerializeField] private float innerRadius;
    [SerializeField] private Transform cameraTarget;
    [Tooltip("If set to 0, this attractor lasts forever")]
    [SerializeField] private float lifetime;

    private float timer;

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

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                cam.ResetToPlayer();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        float distanceToPlayer = (transform.position - player.position).magnitude;
        if (distanceToPlayer < innerRadius)
        {
            cam.Snap();
        }

        if (timer <= 0)
        {
            timer = lifetime;
        }
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
        cam.Snap();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
    }
}
