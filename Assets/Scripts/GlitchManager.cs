using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchManager : MonoBehaviour {
    public float intialDelay, interGlitchDelay, intialRandomOffset, interRandomOffset;
    private float intialTimer, nextGlitchTimer, timeToFirstGlitch, timeToNextGlitch;
    private bool bsod, crash, intialStart, firstGlitch;
    // Use this for initialization
    void Start() {
        bsod = crash = intialStart = firstGlitch = false;
        intialTimer = nextGlitchTimer = 0f;
        timeToFirstGlitch = intialDelay + Random.Range(-intialRandomOffset, intialRandomOffset);
        timeToNextGlitch = 50f;

        //for testing only
        intialStart = true;
    }

    // Update is called once per frame
    void Update() {
        if (intialStart && !firstGlitch) {
            intialTimer += Time.deltaTime;
        }
        if (intialStart && firstGlitch) {
            nextGlitchTimer += Time.deltaTime;
        }

        if (intialTimer >= timeToFirstGlitch && !firstGlitch) {
            //Initial Delay over, trigger a glitch
            SelectAndTrigger();
            firstGlitch = true;
            timeToNextGlitch = interGlitchDelay + Random.Range(-interRandomOffset, interRandomOffset);
        }
        if (nextGlitchTimer >= timeToNextGlitch) {
            SelectAndTrigger();
            nextGlitchTimer -= timeToNextGlitch;
            timeToNextGlitch = interGlitchDelay + Random.Range(-interRandomOffset, interRandomOffset);
        }
    }
    private void SelectAndTrigger() {
        Debug.Log("Glitch");
        int randomRoll;
        if (!bsod && !crash) {
            randomRoll = Random.Range(0, 100);
        } else if (!bsod) {
            randomRoll = Random.Range(0, 90);
        } else {
            randomRoll = Random.Range(0, 80);
        }

        if (randomRoll > 90) {
            // Start Crash glitch
        } else if (randomRoll > 80) {
            //Start BSOD glitch
        } else {
            //Start Static Glitch
        }
    }
    public void BeginGlitchTimer() {
        intialStart = true;
    }
}
