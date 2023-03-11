using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string identifier;

    private Collider coll;
    private SpriteRenderer spriteRenderer;
    private float phaseTimer;
    private Sprite originalSprite;
    private bool isSolid;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        isSolid = !coll.isTrigger;
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
                if (isSolid)
                {
                    coll.isTrigger = false;
                }
            }
        }
    }

    public virtual void Take(float heightFromGround)
    {
        Physics.IgnoreCollision(coll, PlayerController.instance.GetComponent<Collider>(), true);
        coll.isTrigger = true;
        phaseTimer = 0f;
        LiftSprite(heightFromGround);
    }

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        phaseTimer = 0.2f;
        ResetSprite();
    }

    public void LiftSprite(float height)
    {
        // Switching sprites when item is lifted fixes a bug where sprites do not display correctly over other sprites when tilted.
        ResetSprite();
        Vector3 skewedHeight = new Vector3(0, height, height);
        float yPivot = -Vector3.Distance(Vector3.zero, skewedHeight);
        yPivot /= spriteRenderer.transform.lossyScale.x;
        yPivot /= originalSprite.texture.height / 100f;
        var newPivot = new Vector2(0.5f, yPivot);
        spriteRenderer.sprite = Sprite.Create(originalSprite.texture, originalSprite.rect, newPivot);
        spriteRenderer.transform.position -= skewedHeight;
    }

    public void ResetSprite()
    {
        spriteRenderer.sprite = originalSprite;
        spriteRenderer.transform.localPosition = Vector3.zero;
    }
}
