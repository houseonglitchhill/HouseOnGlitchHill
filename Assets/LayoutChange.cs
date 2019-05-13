using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class LayoutChange : MonoBehaviour {
    public GameObject doorBlocker1, doorBlocker2, doorBlocker3;
    public OffMeshLink Link1, Link2, Link3;
    private bool primed;

    // Use this for initialization
    void Start() {
        doorBlocker1.SetActive(false);
        doorBlocker2.SetActive(false);
        doorBlocker3.SetActive(true);
        primed = false;
        Link1.activated = true;
        Link2.activated = true;
        Link3.activated = false;
    }

    // Update is called once per frame
    void Update() {

    }
    private void ChangeLayout() {
        doorBlocker1.SetActive(true);
        doorBlocker2.SetActive(true);
        doorBlocker3.SetActive(false);
        Link1.activated = false;
        Link2.activated = false;
        Link3.activated = true;
    }

    public void RegisterEntry() {
        Debug.Log("Entry Registered");
        if (primed) {
            ChangeLayout();
        }
    }
    public void RegisterExit() {
        Debug.Log("Exit Registered");
        if (!primed) {
            primed = true;
        }
    }

    public void ResetGlitch() {
        doorBlocker1.SetActive(false);
        doorBlocker2.SetActive(false);
        doorBlocker3.SetActive(true);
        primed = false;
        Link1.activated = true;
        Link2.activated = true;
        Link3.activated = false;
    }

}
