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

    private float loadPercentage;

    void Start()
    {
        if (!loadScenes)
        {
            scenesLoaded = true;
            cinematicEnded = true;
            startEvent.Post(gameObject);
            if (TutorialManager.instance) TutorialManager.instance.Begin();
            hud.SetLoadingText("");
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

        hud.BlackoutInstant();
        hud.SetLoadingText("Chargement... " + (int)loadPercentage + "%");
        controller.Freeze(true);
        StartCoroutine(LoadScenesAsync());
        introCinematicEvent.Post(gameObject, (int)AkCallbackType.AK_EndOfEvent, CinematicEnded);
    }

    void Update()
    {
        if (!loadScenes) return;

        if (scenesLoaded)
        {
            hud.SetLoadingText("Prêt");
        }
        else
        {
            hud.SetLoadingText("Chargement... " + (int)loadPercentage + "%");
        }
        if (scenesLoaded && Input.GetButtonDown(ButtonName.jump))
        {
            cinematicEnded = true;
        }

        if (cinematicEnded && scenesLoaded)
        {
            startEvent.Post(gameObject);
            hud.Blackout(false, fadeInDuration);
            controller.Freeze(false);
            ControlToggle.TakeControl(fadeInDuration, () => TutorialManager.instance.Begin());
            PlayerController.instance.StartState();
            hud.SetLoadingText("");
            loadScenes = false;
        }
    }

    IEnumerator LoadScenesAsync()
    {
        AsyncOperation[] asyncOps = new AsyncOperation[scenes.Count];
        for (int i = 0; i < scenes.Count; i++)
        {
            asyncOps[i] = SceneManager.LoadSceneAsync(scenes[i], LoadSceneMode.Additive);
        }

        bool allScenesLoaded = false;
        while (!allScenesLoaded)
        {
            allScenesLoaded = true;
            loadPercentage = 0;
            for (int i = 0; i < asyncOps.Length; i++)
            {
                loadPercentage += asyncOps[i].progress * 100;
                if (!asyncOps[i].isDone)
                {
                    allScenesLoaded = false;
                }
            }
            loadPercentage /= scenes.Count;
            yield return null;
        }

        scenesLoaded = true;
    }

    private void CinematicEnded(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (in_type == AkCallbackType.AK_EndOfEvent)
        {
            cinematicEnded = true;
        }
    }
}
