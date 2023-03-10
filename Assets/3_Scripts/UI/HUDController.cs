using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUDController : SingletonMonoBehaviour<HUDController>
{
    [SerializeField] public ParticleSystem highlightParticles;

    [Header("Text Popups")]
    [SerializeField] private TextPopup defaultPopup;

    [Header("Subtitle")]
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private RawImage subtitleBackground;

    private Dictionary<TextType, TextPopup> popups;
    private Localization localization;
    private float subtitleTimer;
    protected override void Awake()
    {
        base.Awake();
        HideSubtitle();
        popups = new Dictionary<TextType, TextPopup>();
        popups.Add(TextType.Popup, defaultPopup);
    }

    void Start()
    {
        localization = GameController.instance.localization;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DisplaySubtitle("test_subtitle", 5);
        }

        if (subtitleTimer > 0)
        {
            subtitleTimer -= Time.deltaTime;
            if (subtitleTimer <= 0)
            {
                FadeOutSubtitle();
            }
        }
    }

    public void ShowHighlightParticles(Vector3 position)
    {
        highlightParticles.transform.position = position + Vector3.up * 0.25f + Vector3.forward * 0.2f;
        highlightParticles.Play();
    }

    public void HideHighlightParticles()
    {
        highlightParticles.Stop();
    }

    public void DisplayText(TextType textType, string key)
    {
        popups[textType].Show(key);
    }

    /// <summary>
    ///  Displays a subtitle for duration seconds.
    /// </summary>
    public void DisplaySubtitle(string key, float duration)
    {
        subtitleText.text = localization.GetText(key);
        subtitleText.DOKill();
        subtitleText.DOFade(1, 1);
        subtitleBackground.DOKill();
        subtitleBackground.DOFade(0.8f, 1);
        subtitleTimer = duration;
    }

    private void FadeOutSubtitle()
    {
        subtitleText.DOKill();
        subtitleText.DOFade(0, 1);
        subtitleBackground.DOKill();
        subtitleBackground.DOFade(0, 1);
    }

    private void HideSubtitle()
    {
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
        subtitleBackground.color = new Color(subtitleBackground.color.r, subtitleBackground.color.g, subtitleBackground.color.b, 0);
    }
}
