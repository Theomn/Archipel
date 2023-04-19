using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTypeSwitch : MonoBehaviour
{
    [SerializeField] private GameObject keyboardObject, gamepadObject;

    private void Start()
    {
        GameController.instance.RegisterInputSwitch(this);
        Notify(InputType.Gamepad);
    }

    private void OnDestroy()
    {
        GameController.instance.RemoveInputSwitch(this);
    }

    public void Notify(InputType input)
    {
        switch (input)
        {
            case InputType.Keyboard:
                keyboardObject.SetActive(true);
                gamepadObject.SetActive(false);
                break;
            case InputType.Gamepad:
                gamepadObject.SetActive(true);
                keyboardObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
