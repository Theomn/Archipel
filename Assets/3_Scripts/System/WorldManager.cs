using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private bool loadScenes;
    [SerializeField] private List<string> scenes;
    [SerializeField] AK.Wwise.Event mapLoadedEvent;
    // Start is called before the first frame update
    void Start()
    {
        mapLoadedEvent.Post(gameObject);
        if (!loadScene) return;
        foreach(string scene in scenes)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
