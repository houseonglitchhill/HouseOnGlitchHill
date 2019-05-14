using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEvent : MonoBehaviour {

    public GameObject staticCanvas;
    public AudioSource[] allAudioInScene;

    private void Start()
    {
        staticCanvas.SetActive(false);
    }

    public IEnumerator playStaticEvent()
    {
        foreach (AudioSource audioSource in allAudioInScene)
        {
            audioSource.enabled = !audioSource.enabled;
        }

        staticCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        staticCanvas.SetActive(false);

        foreach (AudioSource audioSource in allAudioInScene)
        {
            audioSource.enabled = !audioSource.enabled;
        }
    }
}
