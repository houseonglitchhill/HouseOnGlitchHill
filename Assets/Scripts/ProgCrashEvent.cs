using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgCrashEvent : MonoBehaviour {

    public GameObject whiteCanvas;
    public GameObject crashWindow;
    public FillBar fillBar;
    public AudioSource[] allAudioInScene;
    private PlayerController pc;

    // Use this for initialization
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        whiteCanvas.SetActive(false);
        crashWindow.SetActive(false);
    }

    private void Update()
    {
        if(pc == null)
        {
            pc = FindObjectOfType<PlayerController>();
        }
    }

    public IEnumerator TriggerCrashSequence()
    {
        Time.timeScale = 0.01f;
        pc.enabled = false;
        toggleWhiteCanvas();
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(openCrashWindow());
    }

    public void disableCrashSequence()
    {
        crashWindow.SetActive(false);
        toggleWhiteCanvas();
        pc.enabled = true;
        Time.timeScale = 1f;
    }

	private void toggleWhiteCanvas()
    {
        Debug.Log("Toggling the white canvas");
        whiteCanvas.active = !whiteCanvas.active;

        foreach (AudioSource audioSource in allAudioInScene)
        {
            audioSource.enabled = !audioSource.enabled;
        }
    }

    IEnumerator openCrashWindow()
    {
        crashWindow.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        fillBar.startFixingProcess();
    }
}
