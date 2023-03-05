using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Thought : MonoBehaviour
{
    private TMP_Text textComponent;

    [HideInInspector] public bool isNew;

    [HideInInspector] public float fadeSpeed;
    [HideInInspector] public float backgroundOpacity;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
    }

    public void FadeIn()
    {
        transform.DOKill();
        textComponent.DOFade(1, fadeSpeed);
        transform.DOShakePosition(10, 1, 1, 90, false, false, ShakeRandomnessMode.Harmonic).SetLoops(-1, LoopType.Restart);
        transform.DOShakeRotation(10, 1, 1, 90, false, ShakeRandomnessMode.Harmonic).SetLoops(-1, LoopType.Restart);
    }

    public void FadeOut()
    {
        transform.DOKill();
        textComponent.DOFade(0, fadeSpeed);
    }

    public void SetText(string text)
    {
        textComponent.text = text;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
