using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomScaler : MonoBehaviour
{
    [SerializeField] private float randomScaleMultiplierRange;
    [SerializeField] private float punchForce;
    [SerializeField] private float windForce;

    private Collider coll;

    private Quaternion startRotation;
    private void Awake()
    {
        transform.localScale *= 1 + Random.Range(-randomScaleMultiplierRange, +randomScaleMultiplierRange);
        startRotation = transform.rotation;
        coll = GetComponent<Collider>();
        if (windForce > 0 && (!coll || coll.isTrigger == false))
        {
            windForce = Random.value > 0.5f ? windForce : -windForce;
            transform.Rotate(Vector3.forward * -windForce / 2);
            var period = Random.Range(2f,4f);
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
}
