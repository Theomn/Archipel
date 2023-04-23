using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plank : MonoBehaviour
{
    [SerializeField] private float depth;
    private Transform visual;
    private float initialY;
    void Start()
    {
        visual = GetComponentInChildren<SpriteRenderer>().transform;
        if (Random.value > 0.5f) visual.localScale = new Vector3(visual.localScale.x, -visual.localScale.y, visual.localScale.z);
        initialY = visual.transform.position.y;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        visual.DOKill();
        visual.DOMoveY(initialY - depth, 0.4f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        visual.DOKill();
        visual.DOMoveY(initialY, 0.4f);
    }
}
