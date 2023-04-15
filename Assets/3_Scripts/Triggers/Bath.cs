using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bath : MonoBehaviour
{

    [SerializeField] private string bathModifier;
    [SerializeField] private string fruitModifier;
    [SerializeField] private string jumpModifier;
    [SerializeField] private string coconutModifier;
    [SerializeField] private float jumpModifierDuration;
    [SerializeField] private List<string> thoughtKeys;
    [SerializeField] private List<string> alienVisionsKeys;
    [SerializeField] private List<float> characterAppearInterval;
    [SerializeField] private Animator ripple;
    [SerializeField] private Animator splash;
    [SerializeField] private ParticleSystem dropplet, ripplesBurst, ripplesWalk, splashParticle, splashFront, foam;
    [SerializeField] private GameObject burstSplashAnchor;

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
            // This modifier triggers the vision
            mods.AddModifier(bathModifier);
            if (player.state == PlayerController.State.Sitting)
            {
                maxRevealedLevel = level;
                for (int i = 0; i < level; i++)
                {
                    ThoughtScreen.instance.RemoveThought(thoughtKeys[i]);
                }
                AlienVision.instance.SetCharacterAppearInterval(characterAppearInterval[level]);
                AlienVision.instance.SetText(alienVisionsKeys[level]);
                AlienVision.instance.SetThoughtKey(thoughtKeys[level]);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player)
        {
            return;
        }

        //ripple.SetTrigger("Ripple");
        ripplesWalk.Play();

        if (player.state == PlayerController.State.Falling)
        {
            /*splash.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 0.1f);
            splash.SetTrigger("Splash");*/
            burstSplashAnchor.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 0.1f);
            dropplet.Play();
            ripplesWalk.Play();
            splashParticle.Play();
            splashFront.Play();
            foam.Play();
            // ripplesBurst.Play();
            //dropplet1.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 0.1f);
            // dropplet1.Play();
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
        if (mods.ContainsModifier(fruitModifier))
        {
            level = 1;
        }
        if (mods.ContainsModifier(coconutModifier))
        {
            level = 2;
        }
        return level;
    }
}
