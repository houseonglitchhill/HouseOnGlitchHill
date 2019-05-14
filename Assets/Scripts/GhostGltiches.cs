using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGltiches : MonoBehaviour {
    public GhostAI ghost;
    public List<Transform> teleports;
    public GameObject player;
    public float effectLength;
    private List<Action> glitches;
    private Vector3 originalSize;

    public StaticEvent staticEvent;

	// Use this for initialization
	void Start () {
        glitches = new List<Action>();
        effectLength = 5f;
        glitches.Add(TeleportPlayer);
        glitches.Add(ShrinkPlayer);
        glitches.Add(ReverseControls);
        staticEvent = FindObjectOfType<StaticEvent>();

        originalSize = new Vector3(1.0f, 1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
		if(staticEvent == null)
        {
            Debug.Log("Static Event not assigned");
        }
	}
    public void SelectRandomGlitch() {
        //This will select a possible glitch at random
        glitches[UnityEngine.Random.Range(0, glitches.Count)]();
        GameController.glitchesActivated++;
    }
    private void TeleportPlayer() {
        player.transform.position = teleports[UnityEngine.Random.Range(0, teleports.Count)].position;
        Debug.Log("Teleporting Player");
        StartCoroutine(staticEvent.playStaticEvent());
    }

    private void ShrinkPlayer() {
        Debug.Log("Shrinking Player");
        StartCoroutine(ShrinkPlayerRevert());
        player.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        StartCoroutine(staticEvent.playStaticEvent());
    }

    private void ReverseControls() {
        Debug.Log("Reversing Player Controls");
        player.GetComponent<PlayerController>().reversed = true;
        StartCoroutine(ReverseControlsRevert());
        StartCoroutine(staticEvent.playStaticEvent());
    }

    private IEnumerator ReverseControlsRevert() {
        yield return new WaitForSeconds(effectLength);
        player.GetComponent<PlayerController>().reversed = false;
        StartCoroutine(staticEvent.playStaticEvent());
    }

    private IEnumerator ShrinkPlayerRevert() {
        yield return new WaitForSeconds(effectLength);
        player.transform.localScale = originalSize;
        StartCoroutine(staticEvent.playStaticEvent());
    }



    //end of class
}
