using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField] private float speed;
    [SerializeField] private float sitWaitTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded;
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
        var velocity = new Vector3(input.x * speed, rb.velocity.y, input.z * speed);
        rb.velocity = velocity;


        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        if(movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }



        var origin = transform.position;
        var radius = 1.2f;
        bool collided = Physics.CheckSphere(origin, radius, 1 << Layer.ground);
        if(collided)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }
    private void FixedUpdate()
    {
        var gravity = 30f;
        rb.AddForce(0, -gravity, 0);
    }



}
