using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InputHUDElement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color buttonColor;
    [SerializeField] private TMP_Text TMPtext;
    
    private bool isActive = true;

    private readonly Color grey = new Color(1,1,1,0.3f);

    public void Show(bool activate, string text = "")
    {
        if (activate) Activate();
        else Deactivate();
        TMPtext.text = text;
    }

    private void Activate()
    {
        if (isActive) return;

        isActive = true;
        image.color = buttonColor;
        TMPtext.color = Color.white;
        image.transform.DORestart();
        image.transform.DOKill();
        //image.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 0, 0);
    }

    private void Deactivate()
    {
        if (!isActive) return;
        
        isActive = false;
        image.color = grey;
        TMPtext.color = grey;
    }
}
