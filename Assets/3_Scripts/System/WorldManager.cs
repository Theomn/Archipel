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

    void Start()
    {
        if (!loadScenes) return;
        foreach(string scene in scenes)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
