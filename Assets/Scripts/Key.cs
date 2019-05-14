using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    const int oneKey = 1;
    int numInScene;

    void Start()
    {
        numInScene = GameObject.FindGameObjectsWithTag("Key").Length;

        if (numInScene > oneKey)
        {
            // destroy this key if another key already exists
            GameObject.Destroy(this);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Key set to Inactive");
            this.gameObject.SetActive(false);
        }
    }
}
