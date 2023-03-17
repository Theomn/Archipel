using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bath : MonoBehaviour
{

    [SerializeField] private string bathModifier;
    [SerializeField] private string fruitModifier;
    [SerializeField] private string jumpModifier;
    [SerializeField] private float jumpModifierDuration;
    [SerializeField] private List<string> thoughtKeys;
    [SerializeField] private List<string> alienVisionsKeys;
    [SerializeField] private Animator ripple;
    [SerializeField] private Animator splash;

    private PlayerController player;
    private PlayerModifiers mods;

    // How many conditions did the player satisfy the best time he sat in the bath?
    private int maxRevealedLevel = -1;

    private void Awake()
    {
        // animator = GetComponentInChildren<Animator>();
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

        var level = DetermineLevel();
        if (level > maxRevealedLevel)
        {
            // This modifier trigger the vision
            mods.AddModifier(bathModifier);
            if (player.state == PlayerController.State.Sitting)
            {
                maxRevealedLevel = level;
                for (int i = 0; i < level; i++)
                {
                    ThoughtScreen.instance.RemoveThought(thoughtKeys[i]);
                }
                AlienVision.instance.SetText(alienVisionsKeys[level]);
                ThoughtScreen.instance.AddThought(thoughtKeys[level]);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player)
        {
            return;
        }

        ripple.SetTrigger("Ripple");

        if (player.state == PlayerController.State.Falling)
        {
            splash.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 0.1f);
            splash.SetTrigger("Splash");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.player)
        {
            return;
        }
        mods.RemoveModifier(bathModifier);
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
