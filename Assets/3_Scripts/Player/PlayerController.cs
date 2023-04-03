using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private Transform visual;
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] ParticleSystem dustParticles;
    public Transform cameraTarget, sitCameraTarget;

    [Header("Wwise")]
    [SerializeField] private AK.Wwise.Event jumpUpEvent;
    [SerializeField] private AK.Wwise.Event jumpDownEvent, walkEvent, sitDownEvent, sitUpEvent;


    public enum State
    {
        Walking,
        Idle,
        Jumping,
        Falling,
        Sitting
    }

    public State state { get; private set; }
    private bool isPaused;
    private bool unpauseFlag;
    private bool isSpeedCheat;
    private float initialSpeed;

    // used to grab/drop objects
    public Vector3 forward { get; private set; }
    private Vector3 input;

    private bool isGrounded;
    private Rigidbody rb;

    private Localization loc;
    private HUDController hud;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        initialSpeed = speed;
    }

    private void Start()
    {
        loc = GameController.instance.localization;
        hud = HUDController.instance;
        hud.sit.Show(true, loc.GetText("action_sit"));
        hud.jump.Show(true, loc.GetText("action_jump"));
    }

    void Update()
    {
        if (isPaused)
        {
            if (unpauseFlag)
            {
                isPaused = false;
                unpauseFlag = false;
            }
            return;
        }
        if (state == State.Sitting)
        {
            if (Input.GetButtonDown(Button.sit) || Input.GetButtonDown(Button.jump) || Input.GetButtonDown(Button.use) || Input.GetButtonDown(Button.grab))
            {
                LeaveSitting();
                SetIdle();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isSpeedCheat)
            {
                speed = initialSpeed;
                isSpeedCheat = false;
            }
            else
            {
                speed = initialSpeed * 2f;
                isSpeedCheat = true;
            }
        }

        // Input
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        input = Vector3.forward * vertical + Vector3.right * horizontal;
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
            if (Input.GetButtonDown("Sit") && !PlayerItem.instance.isHoldingItem)
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
                jumpDownEvent.Post(gameObject);
                SetIdle();
            }
        }
    }

    private void FixedUpdate()
    {
        // Groundcheck
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * 0.1f, 0.2f, 1 << Layer.ground);
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        if(isGrounded && !dustParticles.isEmitting)
        {
            dustParticles.Play();
        }
        else if (!isGrounded)
        {
            dustParticles.Stop();
        }

        if (isPaused || state == State.Sitting || input.sqrMagnitude < 0.1f)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            SetIdle();
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            isPaused = true;
            unpauseFlag = false;
            hud.jump.Show(false);
            hud.sit.Show(false);
        }
        else
        {
            unpauseFlag = true;
            hud.jump.Show(true, loc.GetText("action_jump"));
            hud.sit.Show(true, loc.GetText("action_sit"));
        }
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
        sitDownEvent.Post(gameObject);
        ThoughtScreen.instance.Open();
        PlayerItem.instance.Pause(true);
        hud.BackInput(true);
        CameraController.instance.SitZoom(true);
    }

    private void LeaveSitting()
    {
        ThoughtScreen.instance.Close();
        sitUpEvent.Post(gameObject);
        PlayerItem.instance.Pause(false);
        hud.BackInput(false);
        hud.sit.Show(true, loc.GetText("action_sit"));
        hud.jump.Show(true, loc.GetText("action_jump"));
        CameraController.instance.SitZoom(false);
    }
    private void SetJumping()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        state = State.Jumping;
        anim.Jump();
        jumpUpEvent.Post(gameObject);
    }
    private void SetFalling()
    {
        state = State.Falling;
        anim.Fall();
    }
}
