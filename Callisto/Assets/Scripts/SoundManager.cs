using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    //====================================================
    // Awake is called before the first frame update
    //====================================================
    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    //====================================================
    // Plays audio with given input
    //====================================================
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
