using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum InputType
{
    Keyboard,
    Gamepad
}

public class GameController : SingletonMonoBehaviour<GameController>
{
    public Localization localization;
    public InputType inputType {get; private set;}

    [SerializeField] private Texture2D mouseTexture;

    public AK.Wwise.Event uiHoverEvent;

    private List<InputTypeSwitch> inputTypeListeners;
    protected override void Awake()
    {
        base.Awake();
        inputTypeListeners = new List<InputTypeSwitch>();
        localization = new Localization();
        DOTween.SetTweensCapacity(1250, 50);
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        ControlToggle.Update();
        
        var joysticks = Input.GetJoystickNames();
        InputType newType = inputType;
        if (joysticks.Length > 0)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                newType = InputType.Gamepad;
            }
        }
        if(Input.anyKeyDown)
        {
            newType = InputType.Keyboard;
        }
        if(newType != inputType)
        {
            NotifyInputSwitches(newType);
        }
        inputType = newType;
    }

    public void ShowCursor(bool show)
    {
        if (show)
        {
            Cursor.SetCursor(mouseTexture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.visible = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void RegisterInputSwitch(InputTypeSwitch switsh)
    {
        if (inputTypeListeners.Contains(switsh)) return;
        inputTypeListeners.Add(switsh);
    }

    public void RemoveInputSwitch(InputTypeSwitch switsh)
    {
        if (inputTypeListeners.Contains(switsh)) return;
        inputTypeListeners.Remove(switsh);
    }

    private void NotifyInputSwitches(InputType type)
    {
        foreach(var switsh in inputTypeListeners)
        {
            switsh.Notify(type);
        }
    }
}
