using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialText move, grab, use, think, read;
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerController.instance.state != PlayerController.State.Start)
        {
            move.Close();
        }
    }
}
