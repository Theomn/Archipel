using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string identifier;

    private Collider coll;
    private SpriteRenderer spriteRenderer;
    private float phaseTimer;
    private Vector3 hands;
    private Sprite originalSprite;
    private Sprite heldSprite;

    void Awake()
    {
        coll = GetComponent<Collider>();

        // Switching sprites fixes a bug where sprites do not display correctly over other sprites when tilted.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        hands = PlayerItem.instance.hands.localPosition;
        var newPivot = new Vector2(0.5f, -hands.magnitude);
        heldSprite = Sprite.Create(originalSprite.texture, originalSprite.rect, newPivot);
    }

    void Update()
    {
        if (phaseTimer > 0)
        {
            phaseTimer -= Time.deltaTime;
            if (phaseTimer <= 0)
            {
                //Physics.IgnoreCollision(coll, PlayerController.instance.GetComponent<Collider>(), false);
                coll.isTrigger = false;
            }
        }
    }

    public virtual void Take()
    {
        //Physics.IgnoreCollision(coll, PlayerController.instance.GetComponent<Collider>(), true);
        coll.isTrigger = true;
        phaseTimer = 0f;
        spriteRenderer.sprite = heldSprite;
        transform.localPosition -= hands;
    }

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        phaseTimer = 0.2f;
        spriteRenderer.sprite = originalSprite;
        transform.localPosition += hands;
    }
}
