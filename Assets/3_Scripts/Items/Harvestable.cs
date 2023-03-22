using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Harvestable : MonoBehaviour
{
    [SerializeField] private string harvestToolIdentifier;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject harvestedPrefab;
    [SerializeField] private Transform spawn;

    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void Harvest(Item tool)
    {
        if (cooldownTimer > 0) return;
        if (!tool.identifier.Equals(harvestToolIdentifier)) return;

        cooldownTimer = cooldown;
        var spawnedObject = Instantiate(harvestedPrefab);
        spawnedObject.transform.parent = transform;
        spawnedObject.transform.localPosition = spawn.localPosition;
        Utils.SetHighSprite(spawnedObject, spawn.localPosition.y);

        spawnedObject.transform.DOLocalMove(Vector3.back * 0.1f, 1f).SetEase(Ease.OutBounce);
        DOTween.To(() => spawnedObject.transform.localPosition.y, y => Utils.SetHighSprite(spawnedObject, y), 0, 1f).SetEase(Ease.OutBounce);
    }
}
