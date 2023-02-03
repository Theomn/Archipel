using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUDController : SingletonMonoBehaviour<HUDController>
{
    [Header("Subtitle")]
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private RawImage subtitleBackground;

    private Localization localization;
    protected override void Awake()
    {
        base.Awake();
        subtitleText.text = string.Empty;
    }

    void Start()
    {
        localization = GameAccessor.instance.localization;
    }

    public void DisplaySubtitle(string key, float time)
    {
        subtitleText.text = localization.GetText(key);
    }
}
