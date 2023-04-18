using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class RandomScaler : MonoBehaviour
{
    [SerializeField] private float randomScaleMultiplierRange;

    [Header("Auto Collider Settings")]
    [Tooltip("Will automatically set the box collider attached to this gameobject to the width of the sprite. If there is no box collider, will create one.")]
    [SerializeField] private bool autoCalculateCollider;
    [Tooltip("If Auto Calculate Collider is ticked, this will override the collider Is Trigger setting")]
    [SerializeField] private bool isTrigger;
    [SerializeField] private float colliderWidthMultiplier = 1f;

    [Header("Decal Settings")]
    [SerializeField] private float decalWidthMultiplier = 1f;
    [SerializeField] private float decalWidthDepthRatio = 0.75f;

    [Header("Movement Settings")]
    [SerializeField] private float punchForce;
    [SerializeField] private float windForce;

    private DecalProjector decal;
    private Collider coll;
    private SpriteRenderer sprite;

    private Quaternion startRotation;
    private void Awake()
    {
        transform.localScale *= 1 + Random.Range(-randomScaleMultiplierRange, +randomScaleMultiplierRange);
        startRotation = transform.rotation;
        coll = GetComponent<Collider>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        decal = GetComponentInChildren<DecalProjector>();
        CalculateDecalDimension();
        if (autoCalculateCollider) CalculateColliderBounds();
        if (windForce > 0 && (!coll || coll.isTrigger == false))
        {
            windForce = Random.value > 0.5f ? windForce : -windForce;
            transform.Rotate(Vector3.forward * -windForce / 2);
            var period = Random.Range(2f, 4f);
            transform.DORotate(Vector3.forward * windForce, period).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;
        if (punchForce <= 0) return;

        var punchDirection = Mathf.Sign(other.transform.position.x - transform.position.x);
        transform.DOKill();
        transform.rotation = startRotation;
        transform.DOPunchRotation(Vector3.forward * punchForce * punchDirection, 0.6f, 0, 0);
    }

    private void CalculateDecalDimension()
    {
        float width = sprite.bounds.size.x;
        float depth = width * decalWidthDepthRatio;
        decal.size = new Vector3(width, depth, decal.size.z);
    }

    private void CalculateColliderBounds()
    {
        BoxCollider box;
        if (!coll)
        {
            box = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        }
        else
        {
            box = coll.GetComponent<BoxCollider>();
            if (!box) box = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;;
        }
        box.isTrigger = isTrigger;
        float width = sprite.bounds.size.x / transform.localScale.x;
        width *= colliderWidthMultiplier;
        float height = Mathf.Max(2f / transform.localScale.y, sprite.bounds.size.y / transform.localScale.y);
        box.size = new Vector3(width, height, width / 5f);
        box.center = new Vector3(box.center.x, box.size.y / 2f, box.size.z / 2f);
    }
}
