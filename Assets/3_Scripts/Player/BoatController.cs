using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatController : SingletonMonoBehaviour<BoatController>
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

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
        Vector3 movementDirection = Vector3.forward * vertical + Vector3.right * horizontal;
        
        if (movementDirection != Vector3.zero)
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
