using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Thought : MonoBehaviour
{
    private TMP_Text textComponent;

    private bool isNew;
    private bool isAlien;

    [HideInInspector] public float fadeSpeed;


    protected void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
        isNew = true;
        textComponent.fontStyle = FontStyles.Bold;
    }

    public void Open()
    {
        transform.DOKill();
        if (!isAlien)
        {
            textComponent.DOFade(1, fadeSpeed);
            //textComponent.textInfo.characterInfo[0].
            float amplitude = Random.Range(3, 5);
            transform.localPosition += Vector3.down * (amplitude / 2f);
            transform.DOLocalMoveY(transform.localPosition.y + amplitude, Random.Range(2, 4)).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            textComponent.DOFade(0.9f, fadeSpeed * 5);
            transform.DOShakePosition(10, 2, 2, 90, false, false, ShakeRandomnessMode.Harmonic).SetLoops(-1, LoopType.Restart);
        }
    }

    public void Close()
    {
        isNew = false;
        transform.DOKill();
        textComponent.DOKill();
        textComponent.DOFade(0, fadeSpeed).onKill += () => textComponent.fontStyle = FontStyles.Normal;
    }

    public int GetLineCount()
    {
        return textComponent.textInfo.lineCount;
    }

    public void SetText(string text)
    {
        text = ParseTag(text);
        if (!isAlien)
        {
            textComponent.text = text;
            return;
        }

        string alienText = "";
        bool escapeChar = false;
        foreach (char c in text)
        {
            // tags
            if (c == '<')
            {
                escapeChar = true;
            }
            if (escapeChar)
            {
                if (c == '>')
                    escapeChar = false;
                alienText += c;
                continue;
            }

            // not tags
            if (Random.value >= 0.5f)
            {
                alienText += "<font=\"kaerukaeru-Regular SDF\">" + c + "</font>";
            }
            else
            {
                alienText += c;
            }
        }
        textComponent.text = alienText;
    }

    private string ParseTag(string text)
    {
        if (text.Contains("<alien>"))
        {
            isAlien = true;
            return text.Replace("<alien>", "");
        }
        return text;
    }

    private void OnDestroy()
    {
        transform.DOKill();
        textComponent.DOKill();
    }
}
