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

    public bool isActive { get; private set; }

    private Vector3 upFwd = Vector3.up + Vector3.forward;
    private Transform target;

    void Start()
    {
        TMPText.text = GameController.instance.localization.GetText(localizationKey);
        TMPText.transform.DOLocalMoveY(swayAmount, Random.Range(3f, 4f)).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        arrow.transform.localPosition -= Vector3.up * swayAmount;
        arrow.transform.DOLocalMoveY(swayAmount, 1.2f).SetEase(Ease.OutCirc).SetLoops(-1, LoopType.Restart);
        Hide(true);
    }

    private void FixedUpdate()
    {
        if (!target) return;

        transform.position = Vector3.Lerp(transform.position, target.position + upFwd * 0.5f, 0.05f);
    }

    public void Show()
    {
        isActive = true;
        TMPText.DOFade(1, 0.8f);
        arrow.DOFade(1, 0.8f);
    }

    public void Hide(bool instantaneous = false)
    {
        isActive = false;
        if (instantaneous)
        {
            TMPText.color = Utils.ChangeColorAlpha(TMPText.color, 0);
            arrow.color = Utils.ChangeColorAlpha(arrow.color, 0);
            return;
        }
        TMPText.DOFade(0, 1f);
        arrow.DOFade(0, 1f);
    }

    public void AttachToPlayer()
    {
        target = PlayerItem.instance.mouth;
    }
}
