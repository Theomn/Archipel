using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomScaler : MonoBehaviour
{
    [SerializeField] private float randomScaleMultiplierRange;
    [SerializeField] private float punchForce;

    private Quaternion startRotation;
    private void Awake()
    {
        transform.localScale *= 1 + Random.Range(-randomScaleMultiplierRange, +randomScaleMultiplierRange);
        startRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer != Layer.player) return;

        var punchDirection = Mathf.Sign(other.transform.position.x - transform.position.x);
        transform.DOKill();
        transform.rotation = startRotation;
        transform.DOPunchRotation(Vector3.forward * punchForce * punchDirection, 0.6f, 0, 0);
    }
}
