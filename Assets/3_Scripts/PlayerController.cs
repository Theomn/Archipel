using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField] private float speed;
    [SerializeField] private float sitWaitTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private Transform visual;

    [SerializeField] private PlayerAnimation anim;


    public bool isSitting { get; private set; }
    public Vector3 forward { get; private set; }

    private bool isGrounded;
    private Rigidbody rb;
    private float sitTimer;

    public bool isJumping;
    public bool isFalling;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Input
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var input = Vector3.forward * vertical + Vector3.right * horizontal;

        if (input.sqrMagnitude > 1)
        {
            input.Normalize();
        }
    

    
    



        // Move
        var velocity = new Vector3(input.x * speed, rb.velocity.y, input.z * speed);
        rb.velocity = velocity;

        // Jump
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down, 0.2f, 1 << Layer.ground);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Visual
        if (input != Vector3.zero)
        {
            forward = input.normalized;
            sitTimer = 0;
            if (isSitting)
            {
                isSitting = false;
                visual.localScale = new Vector3(visual.localScale.x, 1f, visual.localScale.z);
            }
        }
        else
        {
            sitTimer += Time.deltaTime;
            if (sitTimer > sitWaitTime)
            {
                isSitting = true;
                visual.localScale = new Vector3(visual.localScale.x, 0.5f, visual.localScale.z);
            }
        }
        if(rb.velocity.y > 0.2)
        {
           isJumping = true;
           isFalling = false;
        }
        else if(rb.velocity.y < -0.2)
        {
            isJumping = false;
            isFalling = true;
        }
        else
        {
            isJumping = false;
            isFalling = false;
        }
        
        anim.Walk(input, isJumping, isFalling);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }
}
