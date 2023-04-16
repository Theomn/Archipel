using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField] private bool loadScenes;
    [SerializeField] private List<string> scenes;
    [SerializeField] public Volume postProcessVolume;
    [SerializeField] AK.Wwise.Event mapLoadedEvent;
    // Start is called before the first frame update
    void Start()
    {
        mapLoadedEvent.Post(gameObject);
        if (!loadScenes) return;
        foreach(string scene in scenes)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
