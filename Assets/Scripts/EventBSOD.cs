using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBSOD : MonoBehaviour
{

    public Canvas bsod;
    public Canvas playerUI;
    public AudioSource[] allAudioInScene;

    private void Start()
    {
        bsod = GetComponent<Canvas>();
        bsod.enabled = false;
    }

    public IEnumerator TriggerBSOD()
    {
        toggleBSOD();
        Time.timeScale = 0.01f;
        yield return new WaitForSeconds(0.05f);
        toggleBSOD();
        Time.timeScale = 1f;
    }

    private void toggleBSOD()
    {
        bsod.enabled = !bsod.enabled;
        //playerUI.enabled = !playerUI.enabled;

        foreach (AudioSource audioSource in allAudioInScene)
        {
            audioSource.GetComponent<AudioSource>().enabled = !audioSource.GetComponent<AudioSource>().enabled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Debug.Log("Player Detected");

            StartCoroutine(TriggerBSOD());
        }
    }

}
