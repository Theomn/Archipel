using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoconutTree : MonoBehaviour, Harvestable, Useable
{
    [SerializeField] private Transform highlightLocation;
    [SerializeField] private string harvestToolIdentifier;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject harvestedPrefab;
    [SerializeField] private Transform spawn;

    [SerializeField] AK.Wwise.Event harvestEvent;

    private float cooldownTimer;

    private SpriteRenderer sprite;

    private void Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public Vector3 GetHighlightPosition()
    {
        return highlightLocation ? highlightLocation.position : transform.position;
    }

    public void Use()
    {
        if (cooldownTimer > 0) return;

        sprite.transform.DOShakeRotation(0.3f, 5f, 5);
        CameraController.instance.Shake(0.07f);
        cooldownTimer = cooldown;
        var spawnedObject = Instantiate(harvestedPrefab);
        spawnedObject.transform.parent = transform;
        spawnedObject.transform.localPosition = spawn.localPosition;
        Utils.SetHighSprite(spawnedObject, spawn.localPosition.y);

        spawnedObject.transform.DOLocalMove(Vector3.back * 0.1f, 1f).SetEase(Ease.OutBounce);
        DOTween.To(() => spawnedObject.transform.localPosition.y, y => Utils.SetHighSprite(spawnedObject, y), 0, 1f).SetEase(Ease.OutBounce);

        harvestEvent.Post(gameObject);
    }

    public void Harvest(Item tool)
    {
        if (cooldownTimer > 0) return;
        if (!tool.identifier.Equals(harvestToolIdentifier)) return;

        sprite.transform.DOShakeRotation(0.3f, 5f, 5);
        CameraController.instance.Shake(0.07f);
        cooldownTimer = cooldown;
        var spawnedObject = Instantiate(harvestedPrefab);
        spawnedObject.transform.parent = transform;
        spawnedObject.transform.localPosition = spawn.localPosition;
        Utils.SetHighSprite(spawnedObject, spawn.localPosition.y);

        spawnedObject.transform.DOLocalMove(Vector3.back * 0.1f, 1f).SetEase(Ease.OutBounce);
        DOTween.To(() => spawnedObject.transform.localPosition.y, y => Utils.SetHighSprite(spawnedObject, y), 0, 1f).SetEase(Ease.OutBounce);

        harvestEvent.Post(gameObject);
    }

    public string GetUseTextKey()
    {
        return "action_shake";
    }

    public bool IsUseable()
    {
        return cooldownTimer <= 0;
    }
}
