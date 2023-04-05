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
    [SerializeField] private AK.Wwise.Event pickupEvent, dropGroundEvent, dropReceptacleEvent, cannotDropEvent;

    private struct DropData
    {
        public Receptacle receptacle;
        public Vector3 target;
    }

    public Vector3 initialHandsPosition { get; private set; }
    public bool isHoldingItem { get; private set; }
    private Item heldItem;
    private PlayerController controller;
    private HUDController hud;
    private bool isPaused;
    private bool unpauseFlag;
    private Localization loc;


    protected override void Awake()
    {
        base.Awake();
        initialHandsPosition = hands.localPosition;
    }

    void Start()
    {
        controller = PlayerController.instance;
        hud = HUDController.instance;
        loc = GameController.instance.localization;
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

        UpdateUI();

        if (isHoldingItem)
        {
            if (Input.GetButtonDown("Use"))
            {
                heldItem.Use();
            }
            else if (Input.GetButtonDown("Grab"))
            {
                var interactible = FindClosestInteractible(out var pos);
                var canDrop = CanDropItem(out var dropData);
                if (interactible is Grabbable)
                {
                    // Potential swap situation
                    if (interactible is Receptacle)
                    {
                        var receptacle = interactible as Receptacle;
                        dropData.receptacle = receptacle;
                        if (!receptacle.isBlocked && receptacle.isHoldingItem)
                        {
                            var item = receptacle.Grab();
                            DropItem(dropData);
                            TakeItem(item);
                        }
                        else if (!receptacle.isBlocked && !receptacle.isHoldingItem)
                        {
                            DropItem(dropData);
                        }
                        else
                        {
                            heldItem.NegativeFeedback();
                            cannotDropEvent.Post(gameObject);
                        }
                    }
                    else if (canDrop)
                    {
                        DropItem(dropData);
                        TakeItem((interactible as Grabbable).Grab());
                    }
                    else
                    {
                        heldItem.NegativeFeedback();
                        cannotDropEvent.Post(gameObject);
                    }
                }
                else
                {
                    // Drop situation
                    if (canDrop)
                    {
                        DropItem(dropData);
                    }
                    else
                    {
                        heldItem.NegativeFeedback();
                        cannotDropEvent.Post(gameObject);
                    }
                }
            }

        }

        else if (!isHoldingItem)
        {
            var interactible = FindClosestInteractible(out var pos);
            if (interactible is Grabbable && interactible is Useable)
            {
                if (Input.GetButtonDown("Grab"))
                {
                    TakeItem((interactible as Grabbable).Grab());
                }
                if (Input.GetButtonDown("Use"))
                {
                    (interactible as Useable).Use();
                }
            }
            else if (interactible is Grabbable)
            {
                if (Input.GetButtonDown("Grab") || Input.GetButtonDown("Use"))
                {
                    TakeItem((interactible as Grabbable).Grab());
                }
            }
            else if (interactible is Useable)
            {
                if (Input.GetButtonDown("Use"))
                {
                    (interactible as Useable).Use();
                }
            }
        }
    }

    private void UpdateUI()
    {
        if (isHoldingItem)
        {
            // use
            if (heldItem.isUseable)
            {
                hud.use.Show(true, loc.GetText("action_use"));
            }
            else
            {
                hud.use.Show(false);
            }

            // grab
            var interactible = FindClosestInteractible(out var pos);
            var canDrop = CanDropItem(out var dropData);
            if (interactible is Grabbable)
            {
                if (interactible is Receptacle)
                {
                    var receptacle = interactible as Receptacle;
                    if (!receptacle.isBlocked && receptacle.isHoldingItem)
                    {
                        Debug.Log("receptacle and is holding item");
                        hud.grab.Show(true, loc.GetText("action_swap"));
                        hud.ShowHighlightParticles(pos);
                    }
                    else if (!receptacle.isBlocked && !receptacle.isHoldingItem)
                    {
                        hud.grab.Show(true, loc.GetText("action_place"));
                        hud.ShowHighlightParticles(pos);
                    }
                    else
                    {
                        hud.grab.Show(false, loc.GetText("action_drop"));
                        hud.HideHighlightParticles();
                    }

                }
                else if (canDrop)
                {
                    Debug.Log("grabbable not receptacle and can drop");
                    hud.grab.Show(true, loc.GetText("action_swap"));
                    hud.ShowHighlightParticles(pos);
                }
                else
                {
                    hud.grab.Show(false, loc.GetText("action_drop"));
                    hud.HideHighlightParticles();
                }
            }
            else if (canDrop)
            {
                if (dropData.receptacle != null)
                {
                    hud.grab.Show(true, loc.GetText("action_place"));
                    hud.ShowHighlightParticles(dropData.receptacle.transform.position);
                }
                else
                {
                    hud.grab.Show(true, loc.GetText("action_drop"));
                    hud.HideHighlightParticles();
                }
            }
            else
            {
                hud.grab.Show(false, loc.GetText("action_drop"));
                hud.HideHighlightParticles();
            }
            hud.sit.Show(false);
        }

        else if (!isHoldingItem)
        {
            var interactible = FindClosestInteractible(out var pos);

            if (interactible == null) hud.HideHighlightParticles();
            else hud.ShowHighlightParticles(pos);

            if (interactible is Grabbable && interactible is Useable)
            {
                if (interactible is Receptacle && (!(interactible as Receptacle).isHoldingItem || (interactible as Receptacle).isBlocked))
                {
                    hud.grab.Show(false);
                }
                else
                {
                    hud.grab.Show(true, loc.GetText("action_grab"));
                }
                hud.use.Show(true, loc.GetText("action_use"));
            }
            else if (interactible is Grabbable)
            {
                if (interactible is Receptacle && (!(interactible as Receptacle).isHoldingItem || (interactible as Receptacle).isBlocked))
                {
                    hud.grab.Show(false);
                    hud.use.Show(false);
                }
                else
                {
                    hud.grab.Show(true, loc.GetText("action_grab"));
                    hud.use.Show(true, loc.GetText("action_grab"));
                }
            }
            else if (interactible is Useable)
            {
                hud.use.Show(true, loc.GetText("action_use"));
                hud.grab.Show(false);
            }
            else
            {
                hud.grab.Show(false);
                hud.use.Show(false);
            }
            hud.sit.Show(true, loc.GetText("action_sit"));
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
        item.transform.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.OutSine);
        isHoldingItem = true;
        heldItem = item;
        pickupEvent.Post(gameObject);
    }

    private Interactible FindClosestInteractible(out Vector3 position)
    {
        position = Vector3.zero;
        var colliders = Physics.OverlapSphere(transform.position + controller.forward * 0.9f, 1f, 1 << Layer.interactible);
        if (colliders.Length == 0) return null;

        Collider[] filtered = colliders;
        if (heldItem != null)
        {
            filtered = colliders.Where(c => c.gameObject.GetInstanceID() != heldItem.gameObject.GetInstanceID()).ToArray();
            if (filtered.Length == 0) return null;
        }

        var ordered = filtered.OrderBy(c => distanceToPlayer(c.transform));
        var closest = ordered.ElementAt(0);
        position = closest.transform.position;
        return closest.GetComponent<Interactible>();
    }

    private void DropItem(DropData data)
    {
        heldItem.Drop();
        heldItem.transform.DOKill();

        if (data.receptacle != null)
        {
            var target = data.receptacle.Place(heldItem);
            DropAnimation(target);
            dropReceptacleEvent.Post(gameObject);
        }
        else
        {
            DropAnimation(data.target);
            heldItem.transform.parent = null;
            dropGroundEvent.Post(gameObject);
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
            if ((data.receptacle = collider.GetComponent<Receptacle>()) != null)
            {
                if (!data.receptacle.isBlocked && !data.receptacle.isHoldingItem)
                {
                    return true;
                }
                else return false;
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

    private void DropAnimation(Vector3 target)
    {
        heldItem.transform.DOMoveX(target.x, 0.3f).SetEase(Ease.Linear);
        heldItem.transform.DOMoveZ(target.z, 0.3f).SetEase(Ease.Linear);
        heldItem.transform.DOMoveY(target.y, 0.25f).SetEase(Ease.InBack);
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
            hud.HideHighlightParticles();
            hud.use.Show(false);
            hud.grab.Show(false);
        }
        else
        {
            unpauseFlag = true;
        }
    }

    private float distanceToPlayer(Transform t)
    {
        var flatPlayerPos = new Vector2(transform.position.x, transform.position.z);
        var flatPos = new Vector2(t.position.x, t.position.z);
        return (flatPos - flatPlayerPos).sqrMagnitude;
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
