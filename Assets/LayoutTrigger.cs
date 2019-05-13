using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutTrigger : MonoBehaviour {
    public LayoutChange LayoutChanger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            LayoutChanger.RegisterEntry();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            LayoutChanger.RegisterExit();
        }
    }
}
