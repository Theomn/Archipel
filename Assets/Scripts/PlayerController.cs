using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField] private float speed;
    [SerializeField] private float sitWaitTime;

    private Rigidbody rb;

    public bool isSitting {get; private set;}
    private float sitTimer;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var input = Vector3.forward * vertical + Vector3.right * horizontal;
        if (input.sqrMagnitude > 0.2f)
        {
            sitTimer = 0;
            isSitting = false;
        }
        else
        {
            sitTimer += Time.deltaTime;
            if (sitTimer > sitWaitTime)
            {
                isSitting = true;
            }
        }

        rb.velocity = input * speed;
    }

}
