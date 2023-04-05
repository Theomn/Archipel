using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DiaryScreen : SingletonMonoBehaviour<DiaryScreen>
{
    [SerializeField] private GameObject event1, event2, event3, event4, event5, event6, event7;
    private GameObject[] TimelineArray;
    [SerializeField] private GameObject notes;
    [SerializeField] private RawImage background;
    [SerializeField] private GameObject back;
    public string buttonDiary = "Journal";

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
        TimelineArray = new GameObject[7] { event1, event2, event3, event4, event5, event6, event7};
        foreach (GameObject timeline in TimelineArray)
        {
            timeline.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Diary"))
        {
            showOrHideDiary();
            //ControlToggle.TakeControl(Close);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            revealText(5);
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
                    background.DOFade(1, 0.3f);
                } else
                {
                    notes.SetActive(false);
                    back.SetActive(false);
                    background.DOFade(0, 0.3f);
            }
                isVisible = !isVisible;
        }       
    }

    public void revealText(int eventNumber)
    {
        if (eventNumber > 0 && eventNumber < 8)
        {
            TimelineArray[eventNumber-1].SetActive(true);
            if (!isAccessible)
            {
                isAccessible = true;
            }
        }
        
    }

    public void Close()
    {
        showOrHideDiary();
    }

}
