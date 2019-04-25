using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgCrashEvent : MonoBehaviour {

    public GameObject whiteCanvas;
    public GameObject crashWindow;
    public FillBar fillBar;
    public AudioSource[] allAudioInScene;

	// Use this for initialization
	void Start () {
        whiteCanvas.SetActive(false);
        crashWindow.SetActive(false);
        StartCoroutine(TriggerCrashSequence());
	}
	
    IEnumerator TriggerCrashSequence()
    {
        Time.timeScale = 0.001f;

        toggleWhiteCanvas();
        yield return new WaitForSeconds(0.003f);
        StartCoroutine(openCrashWindow());
    }

    public void disableCrashSequence()
    {
        crashWindow.SetActive(false);
        toggleWhiteCanvas();
        Time.timeScale = 1f;
    }

	private void toggleWhiteCanvas()
    {
        Debug.Log("Toggling the white canvas");
        whiteCanvas.active = !whiteCanvas.active;

        foreach (AudioSource audioSource in allAudioInScene)
        {
            audioSource.GetComponent<AudioSource>().enabled = !audioSource.GetComponent<AudioSource>().enabled;
        }
    }

    IEnumerator openCrashWindow()
    {
        crashWindow.SetActive(true);
        yield return new WaitForSeconds(0.003f);
        fillBar.startFixingProcess();
    }
}
