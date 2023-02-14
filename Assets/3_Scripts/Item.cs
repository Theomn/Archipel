using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string identifier;

    private Collider coll;
    private float phaseTimer;

    void Awake()
    {
        coll = GetComponent<Collider>();
    }

    void Start()
    {

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
    }

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        phaseTimer = 0.2f;
    }
}
