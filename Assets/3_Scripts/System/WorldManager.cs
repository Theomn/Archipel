using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class WorldManager : SingletonMonoBehaviour<WorldManager>
{
    [SerializeField] private bool loadScenes;
    [SerializeField] private bool addCheatItems;
    [SerializeField] private List<string> scenes;
    [SerializeField] private AK.Wwise.Event introCinematicEvent;
    [SerializeField] private AK.Wwise.Event startEvent;
    [SerializeField] public Volume postProcessVolume;
    [SerializeField] public AK.Wwise.Event secretRevealedEvent;
    private HUDController hud;
    private PlayerController controller;

    private readonly float fadeInDuration = 5f;

    private readonly string cheatScene = "Items";
    private bool scenesLoaded, cinematicEnded;

    void Start()
    {
        if (!loadScenes)
        {
            scenesLoaded = true;
            cinematicEnded = true;
            startEvent.Post(gameObject);
            //TutorialManager.instance.Begin();
            return;
        }

        if (addCheatItems)
        {
            scenes.Add(cheatScene);
        }
        
        hud = HUDController.instance;
        controller = PlayerController.instance;
        scenesLoaded = false;
        cinematicEnded = false;

        hud.Blackout(true);
        controller.Freeze(true);
        //ControlToggle.TakeControl(duration + fadeInDuration);
        introCinematicEvent.Post(gameObject, (int)AkCallbackType.AK_EndOfEvent, CinematicEnded);
        StartCoroutine(LoadScenesAsync());
    }

    void Update()
    {
        if (!loadScenes) return;

        if (cinematicEnded && scenesLoaded)
        {
            startEvent.Post(gameObject);
            hud.Blackout(false, fadeInDuration);
            controller.Freeze(false);
            ControlToggle.TakeControl(fadeInDuration);
            PlayerController.instance.StartState();
            //TutorialManager.instance.Begin();
            loadScenes = false;
        }

        if (Input.GetButtonDown(Button.jump))
        {
            cinematicEnded = true;
        }
    }

    IEnumerator LoadScenesAsync()
    {
        // Create an array to hold the AsyncOperation objects for each scene
        AsyncOperation[] asyncOps = new AsyncOperation[scenes.Count];

        // Load each scene asynchronously and store the AsyncOperation object
        for (int i = 0; i < scenes.Count; i++)
        {
            asyncOps[i] = SceneManager.LoadSceneAsync(scenes[i], LoadSceneMode.Additive);
            //asyncOps[i].allowSceneActivation = false;
        }
        float[] progress = new float[scenes.Count];

        // Wait for all scenes to finish loading
        bool allScenesLoaded = false;
        while (!allScenesLoaded)
        {
            string log = "Progress: ";
            allScenesLoaded = true;
            for (int i = 0; i < asyncOps.Length; i++)
            {
                progress[i] = asyncOps[i].progress * 100;
                log += progress[i] + "% | ";
                if (!asyncOps[i].isDone)
                {
                    allScenesLoaded = false;
                    //break;
                }
            }
            Debug.Log(log);
            yield return null;
        }

        // Activate all loaded scenes
        for (int i = 0; i < asyncOps.Length; i++)
        {
            //asyncOps[i].allowSceneActivation = true;
        }

        // Set the boolean to true to indicate that all scenes have finished loading
        scenesLoaded = true;
        Debug.Log("All scenes loaded");
    }

    private void CinematicEnded(object in_cookie, AkCallbackType in_type, object in_info)
    {
        // Set the boolean to true when the event ends
        if (in_type == AkCallbackType.AK_EndOfEvent)
        {
            Debug.Log("cinematicEnded");
            cinematicEnded = true;
        }
    }
}
