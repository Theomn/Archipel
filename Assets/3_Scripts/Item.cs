using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected float weight;

    private Collider coll;
    private float dropTimer;

    void Awake()
    {
        coll = GetComponent<Collider>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (dropTimer > 0)
        {
            dropTimer -= Time.deltaTime;
            if (dropTimer < 0)
            {
                coll.enabled = true;
            }
        }
    }

    public virtual void Take()
    {
        coll.enabled = false;
    }

    public virtual void Use()
    {

    }

    public virtual void Drop()
    {
        dropTimer = 0.2f;
    }
}
