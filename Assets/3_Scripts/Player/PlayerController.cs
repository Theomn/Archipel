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

    public Transform cameraTarget;

    public enum State
    {
        Walking,
        Idle,
        Jumping,
        Falling,
        Sitting
    }

    private State state;
    private bool paused;

    public bool isSitting
    {
        get
        { return state == State.Sitting; }
    }

    // used to grab/drop objects
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
        if (paused)
        {
            return;
        }
        if (state == State.Sitting)
        {
            if (Input.GetButtonDown("Sit"))
            {
                ThoughtScreen.instance.Close();
                SetIdle();
            }
            return;
        }

        // Input
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var input = Vector3.forward * vertical + Vector3.right * horizontal;
        if (input.sqrMagnitude > 1)
        {
            input.Normalize();
        }
        if (input.sqrMagnitude > 0)
        {
            forward = input.normalized;
        }
        anim.SetInput(input);

        // Move
        var velocity = new Vector3(input.x * speed, rb.velocity.y, input.z * speed);
        rb.velocity = velocity;

        // Ground stuff
        if (state == State.Walking)
        {
            anim.Walk();
            if (rb.velocity.sqrMagnitude < 0.1f)
            {
                SetIdle();
            }
        }

        if (state == State.Walking || state == State.Idle)
        {
            if (Input.GetButtonDown("Sit"))
            {
                SetSitting();
            }
        }

        if (state == State.Idle)
        {
            if (input.sqrMagnitude > 0)
            {
                SetWalking();
            }
        }

        // Air stuff
        if (state == State.Walking || state == State.Idle)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                SetJumping();
            }
            else if (!isGrounded)
            {
                SetFalling();
            }
        }

        if (state == State.Jumping)
        {

            if (rb.velocity.y < 0)
            {
                SetFalling();
            }
        }

        if (state == State.Falling)
        {
            if (isGrounded)
            {
                SetIdle();
            }
        }
    }

    private void FixedUpdate()
    {
        // Groundcheck
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, 1 << Layer.ground);
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    public void Pause(bool pause)
    {
        this.paused = pause;
    }

    private void SetWalking()
    {
        state = State.Walking;
    }

    private void SetIdle()
    {
        state = State.Idle;
        anim.Idle();
    }
    private void SetSitting()
    {
        state = State.Sitting;
        rb.velocity = new Vector3(0, 0, 0);
        anim.Sit();
        ThoughtScreen.instance.AddThought("test_pingu");
        ThoughtScreen.instance.AddThought("test_pingu2");
        ThoughtScreen.instance.Open();
    }
    private void SetJumping()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        state = State.Jumping;
        anim.Jump();
    }
    private void SetFalling()
    {
        state = State.Falling;
        anim.Fall();
    }
}
