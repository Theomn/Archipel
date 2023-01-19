using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : SingletonMonoBehaviour<CharacterController>
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        
    }
}
