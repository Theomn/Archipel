using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController>
{
    public Localization localization;
    protected override void Awake()
    {
        base.Awake();
        localization = new Localization();
        DontDestroyOnLoad(this);
    }
}
