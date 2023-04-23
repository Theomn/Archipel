using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPop : MonoBehaviour
{
    [SerializeField] private float titleAppearDelay, musicStartOffset, pauseDuration;
    [SerializeField] private GameObject craterAttractor, musicTrigger;
    [SerializeField] private Animator animator;
    [SerializeField] private AK.Wwise.Event musicStart;
    private float timer, timerMusic;

    private void Start() {
        craterAttractor.SetActive(false);
        musicTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            ControlToggle.TakeControl(pauseDuration, Close);
            timer = titleAppearDelay;
            timerMusic = titleAppearDelay + musicStartOffset;
            GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                animator.SetTrigger("Activate");
            }
        }

        if (timerMusic > 0)
        {
            timerMusic -= Time.deltaTime;
            if (timerMusic <= 0)
            {
                musicStart.Post(gameObject);
            }
        }
    }

    private void Close()
    {
        craterAttractor.SetActive(true);
    }
}
