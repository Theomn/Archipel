using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RainZone : MonoBehaviour
{
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] RawImage grayFilter;

    private float initialAlpha;

    private readonly Vector3 upFwd = Vector3.up + Vector3.forward;

    private void Awake()
    {
        initialAlpha = grayFilter.color.a;
        grayFilter.color = new Color(grayFilter.color.r, grayFilter.color.g, grayFilter.color.b, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        grayFilter.DOFade(initialAlpha, 5);
        rainParticles.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        rainParticles.transform.position = other.transform.position + upFwd * 20f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        grayFilter.DOFade(0, 5);
        rainParticles.Stop();
    }
}
