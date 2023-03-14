using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Grabbable
{
    [SerializeField] public string identifier;

    private Collider coll;
    private float phaseTimer;
    private bool isSolid;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        isSolid = !coll.isTrigger;
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
        Utils.LiftSprite(this, heightFromGround);
    }

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        phaseTimer = 0.2f;
        Utils.ResetSpriteLift(this);
    }
}
