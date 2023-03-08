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
    private bool isHeldSpriteLoaded;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (phaseTimer > 0)
        {
            phaseTimer -= Time.deltaTime;
            if (phaseTimer <= 0)
            {
                Physics.IgnoreCollision(coll, PlayerController.instance.GetComponent<Collider>(), false);
                //coll.isTrigger = false;
            }
        }
    }

    public virtual void Take()
    {
        if (!isHeldSpriteLoaded)
        {
            LoadHeldSprite();
        }
        Physics.IgnoreCollision(coll, PlayerController.instance.GetComponent<Collider>(), true);
        //coll.isTrigger = true;
        phaseTimer = 0f;
        spriteRenderer.sprite = heldSprite;
        //transform.localPosition -= hands;
        spriteRenderer.transform.position -= hands;
    }

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        phaseTimer = 0.2f;
        spriteRenderer.sprite = originalSprite;
        //transform.localPosition += hands;
        spriteRenderer.transform.position += hands;
    }

    private void LoadHeldSprite()
    {
        // Switching sprites when item is taken fixes a bug where sprites do not display correctly over other sprites when tilted.
        originalSprite = spriteRenderer.sprite;
        hands = PlayerItem.instance.initialHandsPosition;
        float yPivot = -hands.magnitude;
        yPivot /= spriteRenderer.transform.lossyScale.x;
        yPivot /= originalSprite.texture.height / 100f;
        var newPivot = new Vector2(0.5f, yPivot);
        heldSprite = Sprite.Create(originalSprite.texture, originalSprite.rect, newPivot);
        isHeldSpriteLoaded = true;
    }
}
