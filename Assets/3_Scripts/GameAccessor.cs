using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAccessor : SingletonMonoBehaviour<GameAccessor>
{
    public Localization localization;
    protected override void Awake()
    {
        base.Awake();
        localization = new Localization();
    }
}
