using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TempleDoor : MonoBehaviour
{
    [SerializeField] private string keyIdentifier;
    [SerializeField] private Receptacle firstReceptacle;
    [SerializeField] private Receptacle secondReceptacle;
    [SerializeField] private GameObject doorObject;

    [SerializeField] private AK.Wwise.Event openEvent, closeEvent;

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
        if (firstReceptacle.isHoldingItem && firstReceptacle.heldItem.identifier.Equals(keyIdentifier) &&
            secondReceptacle.isHoldingItem && secondReceptacle.heldItem.identifier.Equals(keyIdentifier))
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
    }

    private void Close()
    {
        isOpen = false;
        doorCollider.enabled = true;
        doorMaterial.DOKill();
        doorMaterial.DOFade(1, 1f);
        closeEvent.Post(gameObject);
    }
}
