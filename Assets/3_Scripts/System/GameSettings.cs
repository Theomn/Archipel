using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : SingletonMonoBehaviour<GameSettings>
{
    public Language language;
    public float masterVolume, musicVolume;
    public InputType inputType;
    public bool shakeText;
}
