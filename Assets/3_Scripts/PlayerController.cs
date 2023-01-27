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

    [SerializeField] Animator animator;

    public bool isSitting { get; private set; }
    public Vector3 forward { get; private set; }

    private bool isGrounded;
    private Rigidbody rb;
    private float sitTimer;

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

        Vector2 direction = new Vector2(0, 0);
        if(forward.x > 0f)
        {
            GetComponent<Animator>().SetTrigger("rightMoveTrigger");
            direction = new Vector2(1, 0);
        }
        if (forward.x < 0f)
        {
            GetComponent<Animator>().SetTrigger("leftMoveTrigger");
            direction = new Vector2(-1, 0);
        }
        if (forward.z > 0f)
        {
            GetComponent<Animator>().SetTrigger("upMoveTrigger");
            direction = new Vector2(0, 1);
        }
        if (forward.z < 0f)
        {
            GetComponent<Animator>().SetTrigger("downMoveTrigger");
            direction = new Vector2(0, -1);
        }
        if(input == Vector3.zero)
        {
            GetComponent<Animator>().SetTrigger("idle");
            direction = new Vector2(0, 0);
        }
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);

     
        /*
                if(horizontal > 0.2)
                {
                    GetComponent<Animator>().SetTrigger("rightMoveTrigger");
                }
                if (horizontal < -0.2)
                {
                    GetComponent<Animator>().SetTrigger("leftMoveTrigger");
                }
                if (vertical > 0.2)
                {
                    GetComponent<Animator>().SetTrigger("upMoveTrigger");
                }
                if (vertical < -0.2)
                {
                    GetComponent<Animator>().SetTrigger("downMoveTrigger");
                }
        */
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }
}
