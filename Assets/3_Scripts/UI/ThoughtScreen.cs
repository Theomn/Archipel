using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThoughtScreen : SingletonMonoBehaviour<ThoughtScreen>
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float backgroundOpacity;
    [SerializeField] private GameObject thoughtPrefab;
    [SerializeField] private Transform thoughtRoot;
    [SerializeField] private RawImage background;
    private Dictionary<string, GameObject> activeThoughts;

    private bool isActive;

    protected override void Awake()
    {
        base.Awake();
        activeThoughts = new Dictionary<string, GameObject>();
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Sit"))
        {
            Close(); 
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
        if(isActive)
        {
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
        OpenThoughts();
        background.DOKill();
        background.DOFade(backgroundOpacity, fadeSpeed);
    }

    public void Close()
    {
        isActive = false;
        CloseThoughts();
        background.DOKill();
        background.DOFade(0f, fadeSpeed);
    }

    private void OpenThoughts()
    {
        float y = 100;
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
