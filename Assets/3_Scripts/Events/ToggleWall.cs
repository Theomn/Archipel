using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToggleWall : MonoBehaviour
{
    [Tooltip("The collider will activate itself if the player has that modifier.")]
    [SerializeField] private string activationModifier;

    [SerializeField] AK.Wwise.Event contactEvent, leaveContactEvent;
    private Collider coll;
    private Material mat;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    private void Awake()
    {
        coll = GetComponentInChildren<Collider>();
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        mat.DOFade(0f, 0f);
    }

    void Update()
    {
        if (PlayerModifiers.instance.ContainsModifier(activationModifier))
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != Layer.player)
        {
            return;
        }
        mat.DOKill();
        mat.DOFade(0.4f, 0.2f);
        contactEvent.Post(gameObject);
        animator.SetTrigger("Open");
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer != Layer.player)
        {
            return;
        }
        mat.DOKill();
        mat.DOFade(0f, 4f);
        leaveContactEvent.Post(gameObject);
        animator.SetTrigger("Close");
    }
}
