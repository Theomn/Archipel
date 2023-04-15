using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provider : MonoBehaviour, Grabbable
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float cooldown;
    [SerializeField] private string grabTextKey;

    [SerializeField] private AK.Wwise.Event pickupEvent;
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
        pickupEvent.Post(gameObject);
        cooldownTimer = cooldown;
        return Instantiate(itemPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Item>();
    }

    public virtual Vector3 GetHighlightPosition()
    {
        return spawnLocation.position;
    }

    public string GetGrabTextKey()
    {
        return grabTextKey != "" ? grabTextKey : "action_grab";
    }
}
