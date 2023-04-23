using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPop : MonoBehaviour
{
    [SerializeField] private float titleAppearDelay, pauseDuration;
    [SerializeField] private GameObject craterAttractor, musicTrigger;
    [SerializeField] private Animator animator;
    [SerializeField] private AK.Wwise.Event musicStart;
    private float timer;

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
                musicStart.Post(gameObject);
            }
        }
    }

    private void Close()
    {
        craterAttractor.SetActive(true);
    }
}
