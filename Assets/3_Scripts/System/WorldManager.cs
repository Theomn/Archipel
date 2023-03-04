using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private List<string> scenes;
    // Start is called before the first frame update
    void Start()
    {
        foreach(string scene in scenes)
        {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
