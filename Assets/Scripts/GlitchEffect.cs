using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour {
    public AudioClip glitchEffect;
    public AudioSource audioS;

    public void Glitch()
    {
        audioS.PlayOneShot(glitchEffect);
    }
}
