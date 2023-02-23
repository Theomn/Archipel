using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Thought : MonoBehaviour
{
    private TMP_Text textComponent;

    private void Start()
    {
        transform.DOShakePosition(10, 1, 1, 90, false, false, ShakeRandomnessMode.Harmonic).SetLoops(-1, LoopType.Restart);
        transform.DOShakeRotation(10, 1, 1, 90, false, ShakeRandomnessMode.Harmonic).SetLoops(-1, LoopType.Restart);
    }

    public void SetText(string text)
    {
        GetComponent<TMP_Text>().text = text;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
