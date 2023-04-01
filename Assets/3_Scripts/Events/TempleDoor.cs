using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TempleDoor : MonoBehaviour
{
    [SerializeField] private List<string> keyIdentifiers;
    [SerializeField] private Receptacle firstReceptacle;
    [SerializeField] private Receptacle secondReceptacle;
    [SerializeField] private GameObject doorObject;

    [SerializeField] private AK.Wwise.Event openEvent, closeEvent;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;


    private Material doorMaterial;
    private Collider doorCollider;
    private bool isOpen;
    private void Awake()
    {
        doorMaterial = doorObject.GetComponent<MeshRenderer>().material;
        doorCollider = doorObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstReceptacle.isHoldingItem && keyIdentifiers.Contains(firstReceptacle.heldItem.identifier) &&
            secondReceptacle.isHoldingItem && keyIdentifiers.Contains(secondReceptacle.heldItem.identifier))
        {
            if (!isOpen)
            {
                Open();
            }
        }
        else if (isOpen)
        {
            Close();
        }
    }

    private void Open()
    {
        isOpen = true;
        doorCollider.enabled = false;
        doorMaterial.DOKill();
        doorMaterial.DOFade(0, 1f);
        openEvent.Post(gameObject);
        animator.SetTrigger("Open");
    }

    private void Close()
    {
        isOpen = false;
        doorCollider.enabled = true;
        doorMaterial.DOKill();
        doorMaterial.DOFade(1, 1f);
        closeEvent.Post(gameObject);
        animator.SetTrigger("Close");
    }
}
