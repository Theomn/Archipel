using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Animate(Vector3 input, bool isGrounded, bool isSitting)
    {

    }


    public void Jump(Vector3 input)
    {

    }

    public void Sit(Vector3 input)
    {

    }

    public void Walk(Vector3 input, bool isJumping, bool isFalling)
    {
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

        Vector2 direction = Vector2.zero;
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
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }
}
