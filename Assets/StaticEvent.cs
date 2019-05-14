using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEvent : MonoBehaviour {

    public GameObject staticCanvas;
    public AudioSource[] allAudioInScene;

    public float timeForEvent = 0.5f;

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
        yield return new WaitForSeconds(timeForEvent);
        staticCanvas.SetActive(false);

        foreach (AudioSource audioSource in allAudioInScene)
        {
            audioSource.enabled = !audioSource.enabled;
        }
    }
}
