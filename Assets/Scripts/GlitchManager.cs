using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchManager : MonoBehaviour {
    public GameObject bsodEvent, crashEvent, windowsEvent, staticEvent;
    public float intialDelay, interGlitchDelay, intialRandomOffset, interRandomOffset;
    private float intialTimer, nextGlitchTimer, timeToFirstGlitch, timeToNextGlitch;
    private bool bsod, crash, windows, intialStart, firstGlitch;

    // Use this for initialization
    void Start() {
        bsod = crash = windows = intialStart = firstGlitch = false;
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

    #region EventCalling
    private void SelectAndTrigger() {
        Debug.Log("Glitch");
        if (!bsod && !crash && !windows) {
            chooseFromFourEvents();
        } else if ((bsod && !crash && !windows) || (!bsod && crash && !windows) || (!bsod && !crash && windows)) {
            chooseFromThreeEvents();
        } else if((bsod && !crash && windows) || (!bsod && crash && !windows) || (bsod && !crash && !windows)) {
            chooseFromTwoEvents();
        } else
        {
            callStaticEvent();
        }
    }

    //This function randomly picks one of any of the events
    private void chooseFromFourEvents()
    {
        int randomRoll = Random.Range(0, 100);
        if (randomRoll > 90)
        {
            callBsodEvent();
        }
        else if (randomRoll > 80)
        {
            callCrashEvent();
        }
        else if (randomRoll > 70)
        {
            callWindowsEvent();
        }
        else
        {
            callStaticEvent();
        }
    }

    //This function chooses an event dependent on which event has already been activated
    private void chooseFromThreeEvents()
    {
        int randomRoll = Random.Range(0, 90);
        if(randomRoll > 70)
        {
            if (bsod)
            {
                if(randomRoll > 80)
                {
                    callCrashEvent();
                }
                else
                {
                    callWindowsEvent();
                }
            } else if (crash)
            {
                if (randomRoll > 80)
                {
                    callBsodEvent();
                }
                else
                {
                    callWindowsEvent();
                }
            }
            else {
                if (randomRoll > 80)
                {
                    callBsodEvent();
                }
                else
                {
                    callCrashEvent();
                }
            }
        } else
        {
            callStaticEvent();
        }

    }

    //This function chooses an event dependent on which event hasn't been activated
    private void chooseFromTwoEvents()
    {
        int randomRoll = Random.Range(0, 80);
        if (randomRoll > 70)
        {
            if (!bsod)
            {
                callBsodEvent();
            }
            else if (!crash)
            {
                callCrashEvent();
            }
            else
            {
                callWindowsEvent();
            }
        }
        else
        {
            callStaticEvent();
        }
    }
    #endregion

    #region EventCalls
    private void callBsodEvent()
    {
        bsod = true;
    }

    private void callCrashEvent()
    {
        crash = true;
    }

    private void callWindowsEvent()
    {
        windows = true;
    }

    private void callStaticEvent()
    {

    }
    #endregion

    public void BeginGlitchTimer() {
        intialStart = true;
    }

}
