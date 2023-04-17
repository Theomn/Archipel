using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Item, Useable
{

    [SerializeField] private float kickForce;
    private Rigidbody rb;
    
    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }
    void Useable.Use()
    {
        rb.AddForce(Vector3.up * kickForce + PlayerController.instance.forward * kickForce, ForceMode.Impulse);
    }

    public override Item Grab()
    {
        var item = base.Grab();
        rb.isKinematic = true;
        return item;
    }

    public override void Drop()
    {
        base.Drop();
        transform.position += PlayerController.instance.forward;
        rb.isKinematic = false;
        rb.AddForce(Vector3.up * kickForce + PlayerController.instance.forward * kickForce, ForceMode.Impulse);
    }

    bool Useable.IsUseable()
    {
        return true;
    }
}
