using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    const int numOfKey;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Key set to Inactive");
            this.gameObject.SetActive(false);
        }
    }
}
