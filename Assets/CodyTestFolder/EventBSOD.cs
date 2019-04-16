using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBSOD : MonoBehaviour
{

    public Canvas bsod;
    public Canvas playerUI;
    public AudioSource[] allAudioInScene;



    private void toggleBSOD()
    {
        bsod.enabled = !bsod.enabled;
        playerUI.enabled = !playerUI.enabled;

        foreach (AudioSource audioSource in allAudioInScene)
        {
            audioSource.GetComponent<AudioSource>().enabled = !audioSource.GetComponent<AudioSource>().enabled;
        }
    }

}
