using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector3 input;
    private SpriteRenderer sprite;
    private int facing;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        facing = 4;
    }

    public void SetInput(Vector3 input)
    {
        this.input = input;
        // animator.SetFloat("moveX", input.x);
        // animator.SetFloat("moveY", input.y);
    }

    public void Animate(Vector3 input, bool isGrounded, bool isSitting)
    {

    }
    public void Idle()
    {
        Vector2 direction = new Vector2();
        sprite.sortingOrder = 0;
        if (facing == 1)
        {
            direction = new Vector2(1, -4);
        }
        if (facing == 2)
        {
            direction = new Vector2(2, -3);
        }
        if (facing == 3)
        {
            direction = new Vector2(3, -4);
        }
        if (facing == 4)
        {
            direction = new Vector2(2, -5);
        }

        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    public void Jump()
    {
        Vector2 direction = new Vector2();
        if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
        {

            direction = new Vector2(-3, 3);
        }
        if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
        {

            direction = new Vector2(-5, 3);
        }
        if (input.z > 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
        {

            direction = new Vector2(-4, 5);
        }
        if (input.z < 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
        {

            direction = new Vector2(-4, 2);
        }
        if (input.z > 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
        {
            direction = new Vector2(-4, 5);
        }
        if (input.z < 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
        {
            direction = new Vector2(-4, 2);
        }
        if (input.z == 0 & input.x == 0)
        {
            if (facing == 1)
            {
                direction = new Vector2(-5, 3);
            }
            if (facing == 2)
            {
                direction = new Vector2(-4, 5);
            }
            if (facing == 3)
            {
                direction = new Vector2(-3, 3);
            }
            if (facing == 4)
            {
                direction = new Vector2(-4, 2);
            }

        }

        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        //  animator.SetBool("Jump", true);
        // animator.SetBool("Fall", false);
    }

    public void Fall()
    {

        Vector2 direction = new Vector2();
        if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
        {

            direction = new Vector2(-3, -4);
        }
        if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
        {

            direction = new Vector2(-5, -4);
        }
        if (input.z > 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
        {

            direction = new Vector2(-4, -3);
        }
        if (input.z < 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
        {

            direction = new Vector2(-4, -5);
        }
        if (input.z > 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
        {
            direction = new Vector2(-4, -3);
        }
        if (input.z < 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
        {
            direction = new Vector2(-4, -5);
        }
        if (input.z == 0 & input.x == 0)
        {
            if (facing == 1)
            {
                direction = new Vector2(-5, -4);
            }
            if (facing == 2)
            {
                direction = new Vector2(-4, -3);
            }
            if (facing == 3)
            {
                direction = new Vector2(-3, -4);
            }
            if (facing == 4)
            {
                direction = new Vector2(-4, -5);
            }
        }
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        //  animator.SetBool("Fall", true);
        //  animator.SetBool("Jump", false);
    }

    public void Sit()
    {
        // Display player over everything when sitting
        sprite.sortingOrder = 2;
        animator.SetFloat("moveX", 4);
        animator.SetFloat("moveY", 1);
    }

    public void Walk()
    {
        Vector2 direction = new Vector2();
        if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
        {
            facing = 3;
            direction = new Vector2(1, 0);
        }
        if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
        {
            facing = 1;
            direction = new Vector2(-1, 0);
        }
        if (input.z > 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
        {
            facing = 2;
            direction = new Vector2(0, 1);
        }
        if (input.z < 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
        {
            facing = 4;
            direction = new Vector2(0, -1);
        }
        if (input.z > 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
        {
            facing = 2;
            direction = new Vector2(0, 1);
        }
        if (input.z < 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
        {
            facing = 4;
            direction = new Vector2(0, -1);
        }
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);

        /*if (horizontal > 0.2)
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
        }*/

        /*Vector2 direction = Vector2.zero;
        if (input.x > 0f)
        {
           // GetComponent<Animator>().SetTrigger("rightMoveTrigger");
            direction = new Vector2(1, 0);
        }
        if (input.x < 0f)
        {
           // GetComponent<Animator>().SetTrigger("leftMoveTrigger");
            direction = new Vector2(-1, 0);
        }
        if (input.z > 0f)
        {
           // GetComponent<Animator>().SetTrigger("upMoveTrigger");
            direction = new Vector2(0, 1);
        }
        if (input.z < 0f)
        {
            //GetComponent<Animator>().SetTrigger("downMoveTrigger");
            direction = new Vector2(0, -1);
        }
        if (input == Vector3.zero)
        {
            //GetComponent<Animator>().SetTrigger("idle");
            direction = new Vector2(0, 0);
        }
        if (isJumping)
        {
            direction = new Vector2(1, 1);
        }
        if (isFalling)
        {
            direction = new Vector2(-1, -1);
        }
        */
    }
}
