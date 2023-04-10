using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{

    [SerializeField] private ParticleSystem splash1, splash2, splash3, splash4, splash5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Vérifie si le bouton gauche de la souris est enfoncé
        {
            splash1.Play();
            splash2.Play();
            splash3.Play();
            splash4.Play();
            splash5.Play();
        }
    }
}
