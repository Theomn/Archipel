using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : SingletonMonoBehaviour<TutorialManager>
{
    [SerializeField] private TutorialText move, grab, drop, pick, eat, think, read;
    [SerializeField] private string grabIdentifier, pickIdentifier, eatModifier;
    [SerializeField] private ReadableStation readStation;

    private PlayerController playerController;
    private PlayerItem playerItem;

    private bool sitDone = false;

    private void Start()
    {
        playerController = PlayerController.instance;
        playerItem = PlayerItem.instance;
        move.AttachToPlayer();
    }

    public void Begin()
    {
        move.Show();
        read.Show();
        drop.AttachToPlayer();
        eat.AttachToPlayer();
        think.AttachToPlayer();
    }

    public void End()
    {
        move.Hide();
        grab.Hide();
        drop.Hide();
        pick.Hide();
        eat.Hide();
        think.Hide();
        read.Hide();
    }

    void Update()
    {
        if (move.isActive && playerController.forward != Vector3.zero)
        {
            move.Hide();
            grab.Show();
            pick.Show();
        }

        if (grab.isActive && playerItem.isHoldingItem && playerItem.heldItem.identifier == grabIdentifier)
        {
            grab.Hide();
            drop.Show();
        }
        if (drop.isActive && !playerItem.isHoldingItem)
        {
            drop.Hide();
        }

        if (pick.isActive && playerItem.isHoldingItem && playerItem.heldItem.identifier == pickIdentifier)
        {
            pick.Hide();
            eat.Show();
        }
        if (eat.isActive && PlayerModifiers.instance.ContainsModifier(eatModifier))
        {
            eat.Hide();
        }

        if (!sitDone && ThoughtScreen.instance.ThoughtCount() > 0)
        {
            sitDone = true;
            Invoke("ShowThink", 2.5f);
        }

        if (think.isActive && PlayerController.instance.state == PlayerController.State.Sitting)
        {
            think.Hide();
        }

        if (read.isActive && readStation.isRead)
        {
            read.Hide();
        }
    }

    private void ShowThink()
    {
        think.Show();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        End();
    }
}
