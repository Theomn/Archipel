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

    [Header("Wwise")]
    [SerializeField] AK.Wwise.Event openEvent;
    [SerializeField] AK.Wwise.Event closeEvent;
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
        TimelineArray = new GameObject[7] { event1, event2, event3, event4, event5, event6, event7 };
        foreach (GameObject timeline in TimelineArray)
        {
            timeline.SetActive(false);
        }
        HUDController.instance.diary.Show(true);
        HUDController.instance.diary.Show(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ControlToggle.isActive && (PlayerController.instance.state != PlayerController.State.Sitting) && !PlayerItem.instance.isHoldingItem)
        {
            if (Input.GetButtonDown("Diary"))
            {
                if (isAccessible)
                {
                    showOrHideDiary();
                    //ControlToggle.TakeControl(showOrHideDiary);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            revealText(5);
        }

    }

    public void showOrHideDiary()
    {
        if (!isVisible)
        {
            notes.SetActive(true);
            back.SetActive(true);
            background.DOFade(1, 0.3f);
            HUDController.instance.BackInput(true);
            PlayerController.instance.Pause(true);
            PlayerItem.instance.Pause(true);

        }
        else
        {
            notes.SetActive(false);
            back.SetActive(false);
            background.DOFade(0, 0.3f);
            PlayerController.instance.Pause(false);
            PlayerItem.instance.Pause(false);
            HUDController.instance.BackInput(false);
        }
        isVisible = !isVisible;
    }

    public void revealText(int eventNumber)
    {
        if (eventNumber > 0 && eventNumber < 8)
        {
            TimelineArray[eventNumber - 1].SetActive(true);
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

    public bool DiaryIsAccessible()
    {
        return isAccessible;
    }

}
