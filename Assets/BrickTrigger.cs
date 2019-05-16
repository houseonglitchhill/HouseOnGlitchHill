using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTrigger : MonoBehaviour {

    [SerializeField] private Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && door.getOpen())
        {
            Debug.Log("Briiiickk");
            StartCoroutine(door.BrickTheDoor());
        }
    }
}
