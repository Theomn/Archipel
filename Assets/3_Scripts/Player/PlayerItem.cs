using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class PlayerItem : SingletonMonoBehaviour<PlayerItem>
{
    [SerializeField] private LayerMask dropLayerMask;
    [SerializeField] private Transform hands;
    [SerializeField] private float handsLenght;
    [SerializeField] public Transform mouth;

    private struct DropData
    {
        public Receptacle receptacle;
        public Vector3 target;
    }

    public Vector3 initialHandsPosition { get; private set; }
    private bool isHoldingItem = false;
    private Item heldItem;
    private PlayerController controller;
    private bool isPaused;
    private bool unpauseFlag;



    protected override void Awake()
    {
        base.Awake();
        initialHandsPosition = hands.localPosition;
        //highlightParticles.Stop();
    }

    void Start()
    {
        controller = PlayerController.instance;
        ThoughtScreen.instance.AddThought("test_bateau");
    }

    void Update()
    {
        hands.localPosition = initialHandsPosition + SnapHandPosition();

        if (isPaused)
        {
            if (unpauseFlag)
            {
                isPaused = false;
                unpauseFlag = false;
            }
            return;
        }

        if (isHoldingItem)
        {
            if (Input.GetButtonDown("Use"))
            {
                heldItem.Use();
            }
            else if (Input.GetButtonDown("Grab") && CanDropItem(out var data))
            {
                DropItem(data);
            }
        }

        else if (!isHoldingItem)
        {
            var interactible = FindClosestInteractible();
            if (Input.GetButtonDown("Grab") && interactible is Grabbable)
            {
                TakeItem((interactible as Grabbable).Grab());
            }
            if (Input.GetButtonDown("Use") && interactible is Useable)
            {
                (interactible as Useable).Use();
            }
        }
    }

    private void TakeItem(Item item)
    {
        if (!item)
        {
            return;
        }
        item.Take(hands.localPosition.y);
        item.transform.parent = hands;
        item.transform.DOKill();
        item.transform.DOLocalMove(Vector3.zero, 0.25f).SetEase(Ease.InOutCirc);
        isHoldingItem = true;
        heldItem = item;
        HUDController.instance.HideHighlightParticles();

    }

    private Interactible FindClosestInteractible()
    {
        var colliders = Physics.OverlapSphere(transform.position + controller.forward * 0.9f, 1f, 1 << Layer.interactible);
        if (colliders.Length == 0)
        {
            HUDController.instance.HideHighlightParticles();

            return null;
        }
        var closest = colliders.OrderBy(c => distanceToPlayer(c.transform)).ElementAt(0);
        HUDController.instance.ShowHighlightParticles(closest.transform.position);
        return closest.GetComponent<Interactible>();
    }

    private void DropItem(DropData data)
    {
        heldItem.Drop();
        heldItem.transform.DOKill();

        if (data.receptacle != null)
        {
            var target = data.receptacle.Place(heldItem);
            heldItem.transform.DOMove(target, 0.25f).SetEase(Ease.InQuad);
            heldItem.transform.parent = data.receptacle.transform;
        }
        else
        {
            heldItem.transform.DOLocalMove(data.target, 0.25f).SetEase(Ease.InQuad);
            heldItem.transform.parent = null;
        }
        isHoldingItem = false;
        heldItem = null;
    }

    private bool CanDropItem(out DropData data)
    {
        data = new DropData();
        var colliders = Physics.OverlapSphere(transform.position + controller.forward * 0.5f + Vector3.up * 0.1f, 0.3f, dropLayerMask);
        foreach (var collider in colliders)
        {
            // Receptacle detected
            if ((data.receptacle = collider.GetComponent<Receptacle>()) != null && !data.receptacle.isBlocked && !data.receptacle.isHoldingItem)
            {
                return true;
            }
            // Cannot drop on any solid collider
            if (!collider.isTrigger)
            {
                return false;
            }
        }
        if (Physics.Raycast(transform.position + controller.forward * 0.5f + Vector3.up, Vector3.down, out var hit, 1.5f, 1 << Layer.ground))
        {
            data.target = hit.point;
            return true;
        }
        return false;
    }

    public void RemoveItem()
    {
        isHoldingItem = false;
        heldItem = null;
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            isPaused = true;
            unpauseFlag = false;
            HUDController.instance.HideHighlightParticles();
        }
        else
        {
            unpauseFlag = true;
        }
    }

    private float distanceToPlayer(Transform t)
    {
        return (t.position - transform.position).sqrMagnitude;
    }

    private Vector3 SnapHandPosition()
    {
        Vector3 snapPosition;
        if (Mathf.Abs(controller.forward.x) > Mathf.Abs(controller.forward.z))
        {
            snapPosition = new Vector3(Mathf.Sign(controller.forward.x), 0, 0);
        }
        else
        {
            snapPosition = new Vector3(0, 0, Mathf.Sign(controller.forward.z));
        }
        return snapPosition * handsLenght + Vector3.back * 0.01f; // little offset backwards to make sure it appears in front of player when sideways
    }
}
