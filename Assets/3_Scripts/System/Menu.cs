using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] List<GameObject> selections;
    [SerializeField] private UnityEngine.UI.Image black;
    [SerializeField] private AK.Wwise.Event menuStartEvent, menuEndEvent, clickEvent, hoverEvent, backEvent;

    private void Start()
    {
        GameController.instance.ShowCursor(true);
        menuStartEvent.Post(gameObject);
        EventSystem.current.SetSelectedGameObject(selections[0]);
    }

    private void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            SetInputPrefs(InputType.Gamepad);
        }
        else
        {
            SetInputPrefs(InputType.Keyboard);
        }
    }

    private void SetInputPrefs(InputType inputType)
    {
        var currentInputType = (InputType)PlayerPrefs.GetInt(PauseMenu.inputType);
        if (currentInputType == inputType) return;

        PlayerPrefs.SetInt(PauseMenu.inputType, (int)inputType);
        Debug.Log("HUD Input changed to " + inputType.ToString() + ".");
    }

    public void PlayGame()
    {
        clickEvent.Post(gameObject);
        menuEndEvent.Post(gameObject);
        black.gameObject.SetActive(true);
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        clickEvent.Post(gameObject);
        Application.Quit();
    }

    public void BackToMenu()
    {
        clickEvent.Post(gameObject);
        menuEndEvent.Post(gameObject);
        SceneManager.LoadScene("Menu");
    }
    public void OptionMenu()
    {
        clickEvent.Post(gameObject);
        foreach(var selection in selections)
        {
            selection.SetActive(false);
        }
        PauseMenu.instance.Open();
    }
    public void OptionClose()
    {
        backEvent.Post(gameObject);
        PauseMenu.instance.Close();
        foreach(var selection in selections)
        {
            selection.SetActive(true);
        }
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selections[0], new BaseEventData(eventSystem));
    }
}
