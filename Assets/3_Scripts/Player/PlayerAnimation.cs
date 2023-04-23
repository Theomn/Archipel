using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector3 input;
    private SpriteRenderer sprite;
    private int facing;
    //private bool bath;
    private bool isHolding;
    private PlayerItem playerItem;
    private bool stateChange;

    
    void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        facing = 4;
        isHolding = false;
        stateChange = isHolding;
    }
    private void Update()
    {
        if(isHolding != stateChange)
        {
            if(input.x == 0 && input.y == 0)
            {
                Idle();
                stateChange = isHolding;
            }
            else
            {
                Walk();
                stateChange = isHolding;
            }
        }
    }


    public void SetInput(Vector3 input)
    {
        this.input = input;
        // animator.SetFloat("moveX", input.x);
        // animator.SetFloat("moveY", input.y);
    }
    public void SetHolding(bool isHolding)
    {
        this.isHolding = isHolding; 
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Bath>() != null)
        {
            //bath = true;
        }
        else
        {
            //bath = false;
        }
    }

    
    public void Idle()
    {
        Vector2 direction = new Vector2();
        sprite.sortingOrder = 0;
     //   isHolding = playerItem.isHolding;
        if (isHolding)
        {
            if (facing == 1)
            {
                direction = new Vector2(1, -11);
            }
            if (facing == 2)
            {
                direction = new Vector2(2, -9);
            }
            if (facing == 3)
            {
                direction = new Vector2(3, -11);
            }
            if (facing == 4)
            {
                direction = new Vector2(2, -13);
            }
        }
        else
        {
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
        }
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    public void Jump()
    {
       
        Vector2 direction = new Vector2();

        if (isHolding)
        {
            if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
            {

                direction = new Vector2(-12, 9);
            }
            if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
            {

                direction = new Vector2(-16, 9);
            }
            if (input.z > 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
            {

                direction = new Vector2(-14, 11);
            }
            if (input.z < 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
            {

                direction = new Vector2(-14, 5);
            }
            if (input.z > 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
            {
                direction = new Vector2(-14, 11);
            }
            if (input.z < 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
            {
                direction = new Vector2(-14, 5);
            }
            if (input.z == 0 & input.x == 0)
            {
                if (facing == 1)
                {
                    direction = new Vector2(-16, 9);
                }
                if (facing == 2)
                {
                    direction = new Vector2(-14, 11);
                }
                if (facing == 3)
                {
                    direction = new Vector2(-12, 9);
                }
                if (facing == 4)
                {
                    direction = new Vector2(-14, 5);
                }
            }
        }
        else
        {
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
        }

        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        //  animator.SetBool("Jump", true);
        // animator.SetBool("Fall", false);
    }

    public void Fall()
    {

        Vector2 direction = new Vector2();

        if (isHolding)
        {
            if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
            {
                direction = new Vector2(17, 14);
               // direction = new Vector2(-14, -15);
            }
            if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
            {
                direction = new Vector2(13, 14);
                // direction = new Vector2(-19, -15);
            }
            if (input.z > 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
            {
                direction = new Vector2(15, 17);
                // direction = new Vector2(-17, -13);
            }
            if (input.z < 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
            {
                direction = new Vector2(15, 10);
                // direction = new Vector2(-17, -17);
            }
            if (input.z > 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
            {
                direction = new Vector2(15, 17);
                // direction = new Vector2(-17, -13);
            }
            if (input.z < 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
            {
                direction = new Vector2(15, 10);
                // direction = new Vector2(-17, -17);
            }
            if (input.z == 0 & input.x == 0)
            {
                if (facing == 1)
                {
                    direction = new Vector2(13, 14);
                    //  direction = new Vector2(-19, -15);
                }
                if (facing == 2)
                {
                    direction = new Vector2(15, 17);
                    //  direction = new Vector2(-17, -13);
                }
                if (facing == 3)
                {
                    direction = new Vector2(17, 14);
                    //  direction = new Vector2(-14, -15);
                }
                if (facing == 4)
                {
                    direction = new Vector2(15, 10);
                    // direction = new Vector2(-17, -17);
                }
            }
        }
        else
        {
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
      //  isHolding = playerItem.isHolding;
        if (isHolding)
        {
            if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
            {
                facing = 3;
                direction = new Vector2(3, 7);
            }
            if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.z))
            {
                facing = 1;
                direction = new Vector2(1, 7);
            }
            if (input.z > 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
            {
                facing = 2;
                direction = new Vector2(2, 9);
            }
            if (input.z < 0f && Mathf.Abs(input.z) > Mathf.Abs(input.x))
            {
                facing = 4;
                direction = new Vector2(2, 5);
            }
            if (input.z > 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
            {
                facing = 2;
                direction = new Vector2(2, 9);
            }
            if (input.z < 0f && Mathf.Abs(input.z) == Mathf.Abs(input.x))
            {
                facing = 4;
                direction = new Vector2(2, 5);
            }
        }
        if(!isHolding) 
        {
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
        }
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);

        
    }
    public void SetFacing (int facing)
    {
        this.facing = facing;
    }

    public void Step()
    {

    }
}
