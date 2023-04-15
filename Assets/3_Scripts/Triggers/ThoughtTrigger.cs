using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    [SerializeField] private List<string> addThoughtKeys;
    [SerializeField] private List<string> removeThoughtKeys;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        foreach(string key in addThoughtKeys)
        {
            ThoughtScreen.instance.AddThought(key);
        }
        foreach(string key in removeThoughtKeys)
        {
            ThoughtScreen.instance.RemoveThought(key);
        }
        Destroy(gameObject);
    }
}
