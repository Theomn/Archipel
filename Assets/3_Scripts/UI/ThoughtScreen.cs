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
        float y = 110;
        foreach (GameObject thoughtObject in activeThoughts.Values)
        {
            thoughtObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -y, 0);
            thoughtObject.GetComponent<Thought>().Open();
            y += 50;
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
