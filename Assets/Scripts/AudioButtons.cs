using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButtons : MonoBehaviour
{
    public AudioClip hover;
    public AudioClip click;
    public AudioClip parameter;

    public void HoverSound()
    {
        SoundManager.Instance.PlaySound(hover);
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlaySound(click);
    }

    public void ParameterSound()
    {
        SoundManager.Instance.PlaySound(parameter);
    }

}
