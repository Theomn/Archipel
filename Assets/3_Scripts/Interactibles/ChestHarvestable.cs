using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHarvestable : MonoBehaviour, Harvestable
{
    [SerializeField] private string harvestToolIdentifier;
    [SerializeField] private GameObject openChest;

    private void Start() {
        openChest.SetActive(false);
    }
    public void Harvest(Item tool)
    {
        if (!tool.identifier.Equals(harvestToolIdentifier)) return;

        openChest.transform.parent = transform.parent;
        openChest.SetActive(true);
        gameObject.SetActive(false);
    }
}
