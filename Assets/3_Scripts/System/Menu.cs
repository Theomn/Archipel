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
    public TextMeshProUGUI theText;
    public UnityEngine.UI.Image black;
    Color blue = new Color(0.04705882f, 0.2431373f, 0.3294118f);
    [SerializeField] GameObject options;
    [SerializeField] GameObject start;
    [SerializeField] GameObject quit;
    [SerializeField] GameObject optionButton;
    [SerializeField] GameObject firstSelected;
    [SerializeField] GameObject back;
    public void PlayGame()
    {
        black.gameObject.SetActive(true);
        SceneManager.LoadScene("Main");

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonEnter()
    {
        theText.color = Color.white;
    }
    public void ButtonExit()
    {
        theText.color = blue ;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OptionMenu()
    {
        options.SetActive(true);
        start.SetActive(false);
        quit.SetActive(false);
        optionButton.SetActive(false);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(back, new BaseEventData(eventSystem));
    }
    public void OptionClose()
    {
        options.SetActive(false);
        start.SetActive(true);
        quit.SetActive(true);
        optionButton.SetActive(true);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(start, new BaseEventData(eventSystem));
    }
}
