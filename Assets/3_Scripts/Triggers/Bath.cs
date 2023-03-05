using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bath : MonoBehaviour
{
    [SerializeField] private string fruitModifier;
    [SerializeField] private string jumpModifier;
    [SerializeField] private float jumpModifierDuration;
    [SerializeField] private List<string> thoughtKeys;
    private Animator animator;
    private SpriteRenderer sprite;

    private PlayerController player;
    private PlayerModifiers mods;

    // How many conditions did the player satisfy the best time he sat in the bath?
    private int maxRevealedLevel = -1;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        player = PlayerController.instance;
        mods = PlayerModifiers.instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != Layer.player)
        {
            return;
        }

        if (player.state == PlayerController.State.Jumping)
        {
            mods.AddModifier(jumpModifier, jumpModifierDuration);
        }

        if (player.state == PlayerController.State.Sitting)
        {
            var level = DetermineLevel();
            if (level > maxRevealedLevel)
            {
                maxRevealedLevel = level;
                for (int i = 0; i < level; i++)
                {
                    ThoughtScreen.instance.RemoveThought(thoughtKeys[i]);
                }
                ThoughtScreen.instance.AddThought(thoughtKeys[level]);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            animator.SetBool("Ripple", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            animator.SetBool("Ripple", false);
        }
    }

    private int DetermineLevel()
    {
        int level = 0;
        if (mods.ContainsModifier(jumpModifier))
        {
            level++;
        }
        if (mods.ContainsModifier(fruitModifier))
        {
            level++;
        }
        return level;
    }
}
