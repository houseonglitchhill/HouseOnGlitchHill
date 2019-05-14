using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour {

    GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameController.FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gc.EndGame(true);
        }
    }
}
