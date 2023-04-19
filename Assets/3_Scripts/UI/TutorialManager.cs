using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : SingletonMonoBehaviour<TutorialManager>
{
    [SerializeField] private TutorialText move, grab, drop, pick, eat, think, read;
    [SerializeField] private string grabIdentifier, pickIdentifier, eatModifier;

    private PlayerController playerController;
    private PlayerItem playerItem;

    private void Start()
    {
        playerController = PlayerController.instance;
        playerItem = PlayerItem.instance;
        move.AttachToPlayer();
    }

    public void Begin()
    {
        move.Show();
        drop.AttachToPlayer();
        eat.AttachToPlayer();
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
    }
}
