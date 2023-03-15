using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Telescope : Receptacle, Useable
{
    [SerializeField] private string lensIdentifier;
    [SerializeField] private float sensitivity;
    [SerializeField] private TMP_Text horizontalText;
    [SerializeField] private TMP_Text verticalText;
    private float horizontal = 36, vertical = 61;

    private bool isActive;
    public void Use()
    {
        if (heldItem == null || !heldItem.identifier.Equals(lensIdentifier))
        {
            return;
        }
        isActive = true;
        PlayerController.instance.Pause(true);
        PlayerItem.instance.Pause(true);
    }

    private void Update()
    {
        horizontalText.text = Mathf.RoundToInt(horizontal).ToString();
        verticalText.text = Mathf.RoundToInt(vertical).ToString();
        
        if (!isActive)
        {
            return;
        }

        var hInput = Input.GetAxisRaw("Horizontal");
        var vInput = Input.GetAxisRaw("Vertical");

        horizontal += hInput * Time.deltaTime * sensitivity;
        vertical += vInput * Time.deltaTime * sensitivity;
        horizontal = Mathf.Clamp(horizontal, 0, 99);
        vertical = Mathf.Clamp(vertical, 0, 99);
        

        if (Input.GetButtonDown("Use") || Input.GetButtonDown("Grab") || Input.GetButtonDown("Jump"))
        {
            isActive = false;
            PlayerController.instance.Pause(false);
            PlayerItem.instance.Pause(false);
        }
    }
}
