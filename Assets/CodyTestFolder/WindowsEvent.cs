using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsEvent : MonoBehaviour {

    public GameObject windowsCanvas;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            windowsCanvas.SetActive(true);
            StartCoroutine(playWindowsEvent());
        }
    }

    IEnumerator playWindowsEvent()
    {
        windowsCanvas.SetActive(true);
        Time.timeScale = 0.01f;
        yield return new WaitForSeconds(0.03f);
        Time.timeScale = 1f;
        windowsCanvas.SetActive(false);
    }
}
