using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThoughtNotification : MonoBehaviour
{
    [SerializeField] private float bubbleLerp;
    [SerializeField] private Image bubble;
    [SerializeField] private Image notificationIcon;
    [SerializeField] private Transform iconStart, iconEnd;

    private Vector3 initialBubbleScale, initialIconScale;
    private Sequence bubbleSequence, notificationAppearSequence, notificationIdleSequence;

    private void Awake()
    {

    }
    void Start()
    {
        initialBubbleScale = bubble.transform.localScale;
        CreateSequences();
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Play();
        }

        var target = CameraController.instance.GetCamera().WorldToScreenPoint(PlayerController.instance.head.position);
        bubble.transform.position = Vector3.Lerp(bubble.transform.position, target, bubbleLerp);
    }

    public void Play()
    {
        notificationIcon.transform.SetParent(iconStart);
        bubble.transform.localScale = initialBubbleScale; // so the next line finds the correct position
        notificationIcon.transform.position = iconStart.position;

        notificationIcon.transform.localScale = Vector3.zero;
        bubble.transform.localScale = Vector3.zero;


        notificationAppearSequence.Restart();
        notificationAppearSequence.onComplete += () =>
        {
            notificationIcon.transform.SetParent(iconEnd);
            notificationIcon.transform.DOJump(iconEnd.position, 350, 1, 0.5f).SetEase(Ease.InOutSine).onComplete += () => 
            //notificationIcon.transform.DOMove(iconEnd.position, 0.8f).SetEase(Ease.InOutSine).onComplete += () => 
            {
                notificationIdleSequence.Restart();
            };
        };
        bubbleSequence.Restart();
        notificationIdleSequence.Pause();
    }

    public void Hide()
    {
        notificationIcon.transform.localScale = Vector3.zero;
        bubble.transform.localScale = Vector3.zero;
        notificationIdleSequence.Restart();
        notificationIdleSequence.Pause();
    }

    private void CreateSequences()
    {
        /*bubbleSequence.Kill();
        notificationAppearSequence.Kill();
        notificationIdleSequence.Kill();*/

        initialIconScale = notificationIcon.transform.localScale;
        bubbleSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        bubbleSequence.Append(bubble.transform.DOScale(initialBubbleScale, 0.4f).SetEase(Ease.OutBack));
        bubbleSequence.AppendInterval(1f);
        bubbleSequence.Append(bubble.transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack));

        notificationIdleSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        notificationIdleSequence.AppendInterval(2f);
        notificationIdleSequence.Append(notificationIcon.transform.DOPunchScale(initialIconScale * 2.5f, 0.25f, 0, 0));
        notificationIdleSequence.SetLoops(-1, LoopType.Restart);

        notificationAppearSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        notificationAppearSequence.AppendInterval(0.4f);
        notificationAppearSequence.Append(notificationIcon.transform.DOScale(initialIconScale * 2f, 0.3f).SetEase(Ease.InCirc));
        notificationAppearSequence.Append(notificationIcon.transform.DOScale(initialIconScale, 0.3f).SetEase(Ease.OutSine));
        notificationAppearSequence.AppendInterval(0.4f);
        /*notificationAppearSequence.Append(notificationIcon.transform.DOMove(iconEnd.position, 0.5f).SetEase(Ease.InOutSine));
        notificationAppearSequence.onComplete += () =>
        {
            notificationIdleSequence.Restart();
        };*/
    }
}
