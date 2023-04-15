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
    public void PlayGame()
    {
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
}
