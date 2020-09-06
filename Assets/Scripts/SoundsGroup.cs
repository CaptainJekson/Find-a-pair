using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsGroup : MonoBehaviour
{
    private AudioSource[] _sounds;

    private void Awake()
    {
        _sounds = GetComponentsInChildren<AudioSource>();
    }

    public void EnableSounds(bool enabled)
    {
        foreach (var sound in _sounds)
        {
            sound.enabled = enabled;
        }
    }
}
