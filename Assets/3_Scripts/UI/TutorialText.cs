using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private string localizationKey;
    [SerializeField] private float swayAmount;
    [SerializeField] private TMP_Text TMPText;
    [SerializeField] private Image arrow;

    void Start()
    {
        TMPText.text = GameController.instance.localization.GetText(localizationKey);
        transform.position += Vector3.down * swayAmount / 2f;
        transform.DOMoveY(transform.position.y + swayAmount, 3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void Close()
    {
        TMPText.DOFade(0, 2f);
        arrow.DOFade(0, 2f);
    }
}
