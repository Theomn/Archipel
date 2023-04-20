using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPop : MonoBehaviour
{
    private float timer = -1;
    [SerializeField] GameObject title;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            timer = 1.5f;
            //   animator.SetTrigger("Activate");
        }



    }
    void Update()
    {
        // animator.SetTrigger("Activate");

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (timer <=0)
            {
                animator.SetTrigger("Activate");
            }

        }
        
    }
}
