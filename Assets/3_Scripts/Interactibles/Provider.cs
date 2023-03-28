using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provider : MonoBehaviour, Grabbable
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float cooldown;
    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public Item Grab()
    {
        if(cooldownTimer > 0)
        {
            return null;
        }
        cooldownTimer = cooldown;
        return Instantiate(itemPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Item>();
    }
}
