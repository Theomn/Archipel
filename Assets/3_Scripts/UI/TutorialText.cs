using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private string localizationKey;
    [SerializeField] private float textSway;
    [SerializeField] private float arrowSway;
    [SerializeField] private TMP_Text TMPText;
    [SerializeField] private Image arrow;

    public bool isActive { get; private set; }

    private Vector3 upFwd = Vector3.up + Vector3.forward;
    private Transform target;

    private Vector3 initialArrowScale;

    private void Awake()
    {
        Hide(true);
        initialArrowScale = arrow.transform.localScale;
    }

    void Start()
    {
        TMPText.text = GameController.instance.localization.GetText(localizationKey);
        TMPText.outlineWidth = 0.2f;
        var darkBlue = Swatches.HexToColor(Swatches.darkBlue);
        float intensity = 0.2f;
        TMPText.outlineColor = new Color(darkBlue.r * intensity, darkBlue.g * intensity, darkBlue.b * intensity, 1);
        TMPText.transform.localPosition += Vector3.up * arrowSway; // avoid arrow overlap
        TMPText.transform.DOLocalMoveY(TMPText.transform.localPosition.y + textSway, Random.Range(1.5f, 2.5f)).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        var arrowSequence = DOTween.Sequence();
        arrowSequence.Append(arrow.transform.DOLocalMoveY(arrow.transform.localPosition.y + arrowSway, 1f).SetEase(Ease.OutSine));
        arrowSequence.Join(arrow.transform.DOPunchScale(-Vector3.up * arrow.transform.localScale.y * 0.2f, 0.6f, 0, 0));
        //arrowSequence.Append(arrow.transform.DOLocalMoveY(arrow.transform.localPosition.y - arrowSway, 0.2f).SetEase(Ease.InSine));
        //arrowSequence.Insert(1, arrow.transform.DOScaleX(initialArrowScale.x * 0.9f, 0.2f).SetEase(Ease.InSine)
        //                    .OnComplete(() => arrow.transform.localScale = initialArrowScale));
        arrowSequence.SetLoops(-1, LoopType.Restart);
    }

    private void FixedUpdate()
    {
        if (!target) return;

        transform.position = Vector3.Lerp(transform.position, target.position + upFwd * 0.5f, 0.1f);
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
