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
        float y = 160;
        float x = -180;
        foreach (GameObject thoughtObject in activeThoughts.Values)
        {
            thoughtObject.GetComponent<RectTransform>().localPosition = new Vector3(x, -y, 0);
            thoughtObject.GetComponent<Thought>().Open();
            if (x > 0) y += 60;
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
