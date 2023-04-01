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
    [SerializeField] private float startHeightOffset, spacePerLine, width;

    [Header("Wwise")]
    [SerializeField] private AK.Wwise.Event openEvent;
    [SerializeField] private AK.Wwise.Event closeEvent, newThoughtEvent;


    private Dictionary<string, GameObject> activeThoughts;

    private bool isActive = false;

    private bool isAlien;

    private AlienVision alienVision;

    protected override void Awake()
    {
        base.Awake();
        activeThoughts = new Dictionary<string, GameObject>();
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        alienVision = GetComponent<AlienVision>();
    }

    private void Start() {
        for (int i = 0; i < 10; i++)
        {
            AddThought("s a f s d g f d s g f d g f d a g d f"+i);
            //AddThought("Long texte pour que ça prenne plusieurs lignes et en meme temps tester si les pensées se rentrent pas l'une dans l'autre"+i);
        }
    }

    public void AddThought(string key)
    {
        if (activeThoughts.ContainsKey(key))
        {
            return;
        }
        var thoughtObject = Instantiate(thoughtPrefab, thoughtRoot);
        var thought = thoughtObject.GetComponent<Thought>();

        thought.SetText(GameController.instance.localization.GetText(key));
        thought.fadeSpeed = fadeSpeed;
        activeThoughts.Add(key, thoughtObject);

        newThoughtEvent.Post(gameObject);

        if (isActive)
        {
            //Refresh view
            Open();
        }
    }

    public void RemoveThought(string key)
    {
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
        isActive = true;
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
        isActive = false;
        closeEvent.Post(gameObject);
        if (PlayerModifiers.instance.ContainsModifier(bathModifier))
        {
            alienVision.Close();
            return;
        }
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
}
