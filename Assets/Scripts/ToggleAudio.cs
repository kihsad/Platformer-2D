using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic, _toggleSounds;

    public void Toggle()
    {
        if (_toggleSounds) SoundManager.Instance.ToggleSounds();
        if (_toggleMusic) SoundManager.Instance.ToggleMusic();
    }
}
