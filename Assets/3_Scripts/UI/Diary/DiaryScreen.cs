using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DiaryScreen : SingletonMonoBehaviour<DiaryScreen>
{
    [SerializeField] private TMP_Text event1, event2, event3, event4, event5, event6, event7, event8, event9, event10;
    private TMP_Text[] textArray;
    [SerializeField] public GameObject notes;
    [SerializeField] private RawImage background;
    [SerializeField] public GameObject back;
    private bool isAccessible;
    private bool isVisible;


    // Start is called before the first frame update
    void Start()
    {
        notes.SetActive(false);
        back.SetActive(false);
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        isVisible = false;
        isAccessible = false;
        textArray = new TMP_Text[10] { event1, event2, event3, event4, event5, event6, event7, event8, event9, event10 };
        foreach (TMP_Text text in textArray)
        {
            text.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            showOrHideDiary();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            revealText(1);
        }

    }

    public void showOrHideDiary()
    {
        if (isAccessible)
        {
            if (!isVisible)
                    {
                        notes.SetActive(true);
                        back.SetActive(true);
                        background.DOFade(1, 1);
                    } else
                    {
                        notes.SetActive(false);
                        back.SetActive(false);
                        background.DOFade(0, 1);
                    }
                    isVisible = !isVisible;
        }       
    }

    public void revealText(int eventNumber)
    {
        if (eventNumber > 0 && eventNumber < 11)
        {
            textArray[eventNumber-1].enabled = true;
            Debug.Log("You can read" + eventNumber);
            if (!isAccessible)
            {
                isAccessible = true;
            }
        }
        
    }

}
