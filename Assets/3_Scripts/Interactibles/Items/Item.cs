using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour, Grabbable
{
    [SerializeField] public string identifier;

    private Collider coll;
    protected SpriteRenderer sprite;
    private float phaseTimer;
    private bool isSolid;
    public bool isUseable {get; protected set;}

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isSolid = !coll.isTrigger;
        isUseable = false;
    }

    protected virtual void Start()
    {
        
    }

    public virtual Item Grab()
    {
        return this;
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
        Utils.SetHighSprite(this, heightFromGround);
    }

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        sprite.transform.DORestart();
        sprite.transform.DOKill();
        phaseTimer = 0.2f;
        Utils.ResetHighSprite(this);
    }

    public void NegativeFeedback()
    {
        sprite.color = new Color(1f, 0.5f, 0.5f, 1);
        sprite.DOKill();
        sprite.DOColor(Color.white, 1f);
        sprite.transform.DORestart();
        sprite.transform.DOKill();
        sprite.transform.DOPunchPosition(Vector3.down*0.2f, 0.5f);
    } 
}
