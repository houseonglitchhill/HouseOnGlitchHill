using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickedDoor : MonoBehaviour {

    [SerializeField] private GameObject brickWall;
    private AudioSource audioSource;
    public BrickTrigger[] triggers;
    

    // Use this for initialization
    void Start() {
        brickWall = GameObject.Find("BrickWall");
        brickWall.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        triggers = GetComponentsInChildren<BrickTrigger>();
    }

    public void ActivateBricks()
    {
        brickWall.SetActive(true);
        audioSource.Play();
        foreach(BrickTrigger trigger in triggers)
        {
            trigger.gameObject.SetActive(false);
        }
    }

}
