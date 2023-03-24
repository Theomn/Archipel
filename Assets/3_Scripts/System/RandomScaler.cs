using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScaler : MonoBehaviour
{
    [SerializeField] private float randomScaleMultiplierRange;
    private void Awake()
    {
        transform.localScale *= 1 + Random.Range(-randomScaleMultiplierRange, +randomScaleMultiplierRange);
    }
}
