using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI theText;
    public UnityEngine.UI.Image black;
    Color blue = new Color(0.04705882f, 0.2431373f, 0.3294118f);
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
}
