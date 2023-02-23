using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtScreen : SingletonMonoBehaviour<ThoughtScreen>
{
    [SerializeField] private GameObject thoughtPrefab;
    [SerializeField] private Transform thoughtRoot;
    private Dictionary<string, GameObject> activeThoughts;

    protected override void Awake()
    {
        base.Awake();
        activeThoughts = new Dictionary<string, GameObject>();
        gameObject.SetActive(false);
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
        activeThoughts.Add(key, thoughtObject);
    }

    public void RemoveThought(string key)
    {
        var thought = activeThoughts[key];
        if (!thought)
        {
            return;
        }

        Destroy(thought);
        activeThoughts.Remove(key);
    }

    public void Open()
    {
        PackThoughts();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void PackThoughts()
    {
        float y = 100;
        foreach (GameObject thoughtObject in activeThoughts.Values)
        {
            thoughtObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -y, 0);
            y += 50;
        }
    }
}
