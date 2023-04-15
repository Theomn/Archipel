using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ButtonsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonEnter()
    {
        //text.fontSize = 80;
        text.DOFontSize(80, 0.1f);
    }

    public void ButtonExit()
    {
       // text.fontSize = 60;
        text.DOFontSize(60, 0.1f);
    }
}
