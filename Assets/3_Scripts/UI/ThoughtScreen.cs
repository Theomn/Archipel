using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ThoughtScreen : SingletonMonoBehaviour<ThoughtScreen>
{
    [SerializeField] private string bathModifier;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float backgroundOpacity;
    [SerializeField] private Transform thoughtRoot;
    [SerializeField] private GameObject thoughtPrefab;
    [SerializeField] private RawImage background;
    [SerializeField] private GameObject notification;
    [SerializeField] private ThoughtNotification thoughtNotification;
    [SerializeField] private float startHeightOffset, spacePerLine, width;

    [Header("Wwise")]
    [SerializeField] private AK.Wwise.Event openEvent;
    [SerializeField] private AK.Wwise.Event closeEvent, newThoughtEvent;


    private Dictionary<string, GameObject> activeThoughts;
    private List<string> removedThoughts;

    private bool isAlien;

    private AlienVision alienVision;

    private Sequence notificationSequence;

    protected override void Awake()
    {
        base.Awake();
        activeThoughts = new Dictionary<string, GameObject>();
        removedThoughts = new List<string>();
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        alienVision = GetComponent<AlienVision>();
        //notification.SetActive(false);
        /*notificationSequence = DOTween.Sequence();
        notificationSequence.Append(notification.transform.DOPunchScale(notification.transform.localScale * 0.5f, 0.25f, 0, 0));
        notificationSequence.AppendInterval(2f);
        notificationSequence.SetLoops(-1, LoopType.Restart);*/
    }

    /*private void Start() {
        for (int i = 0; i < 10; i++)
        {
            AddThought("s a f s d g f d s g f d g f d a g d f"+i);
            //AddThought("Long texte pour que ça prenne plusieurs lignes et en meme temps tester si les pensées se rentrent pas l'une dans l'autre"+i);
        }
    }*/

    public void AddThought(string key)
    {
        if (string.IsNullOrEmpty(key)) return;
        key = key.Trim().ToLower();
        if (removedThoughts.Contains(key)) return;
        if (activeThoughts.ContainsKey(key)) return;
        
        var thoughtObject = Instantiate(thoughtPrefab, thoughtRoot);
        var thought = thoughtObject.GetComponent<Thought>();

        thought.SetText(GameController.instance.localization.GetText(key));
        thought.fadeSpeed = fadeSpeed;
        activeThoughts.Add(key, thoughtObject);
        //notification.SetActive(true);
        thoughtNotification.Play();
        notificationSequence.Restart();
        newThoughtEvent.Post(gameObject);
    }

    public void RemoveThought(string key)
    {
        key = key.Trim().ToLower();
        if (!removedThoughts.Contains(key))
        {
            removedThoughts.Add(key);
        }
        if (!activeThoughts.ContainsKey(key))
        {
            return;
        }
        var thought = activeThoughts[key];
        Destroy(thought);
        activeThoughts.Remove(key);
    }


    public void Open()
    {
        openEvent.Post(gameObject);
        if (PlayerModifiers.instance.ContainsModifier(bathModifier))
        {
            alienVision.Open();
            return;
        }
        OpenThoughts();
        background.DOKill();
        background.DOFade(backgroundOpacity, fadeSpeed);
    }



    public void Close()
    {
        closeEvent.Post(gameObject);
        if (PlayerModifiers.instance.ContainsModifier(bathModifier))
        {
            alienVision.Close();
            return;
        }
        //notification.SetActive(false);
        thoughtNotification.Hide();
        notificationSequence.Restart();
        notificationSequence.Pause();
        CloseThoughts();
        background.DOKill();
        background.DOFade(0f, fadeSpeed);
    }

    private void OpenThoughts()
    {
        float leftY = startHeightOffset, rightY = startHeightOffset;
        float x = -width;
        foreach (GameObject thoughtObject in activeThoughts.Values)
        {
            thoughtObject.GetComponent<RectTransform>().localPosition = new Vector3(x, x > 0 ? -rightY : -leftY, 0);
            var thought = thoughtObject.GetComponent<Thought>();
            thought.Open();
            if (x > 0) rightY += spacePerLine * thought.GetLineCount();
            else leftY += spacePerLine * thought.GetLineCount();
            x = -x;
        }
    }

    private void CloseThoughts()
    {
        foreach (GameObject thoughtObject in activeThoughts.Values)
        {
            thoughtObject.GetComponent<Thought>().Close();
        }
    }

    private void Log()
    {
        string log = "Active thought keys:\n";
        foreach(string key in activeThoughts.Keys)
        {
            log += key + "\n";
        }

        log += "\nRemoved thought keys:\n";
        foreach (string key in removedThoughts)
        {
            log += key + "\n";
        }

        Debug.Log(log);
    }
}
