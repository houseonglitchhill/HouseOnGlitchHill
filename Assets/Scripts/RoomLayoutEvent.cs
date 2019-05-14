using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutEvent : MonoBehaviour {

    public GameObject[] layout1;
    public GameObject[] layout2;

    public AudioSource roomShiftSound;

    public void Start()
    {
        foreach(GameObject item in layout2)
        {
            item.SetActive(false);
        }

        roomShiftSound = GetComponent<AudioSource>();
    }

    public void swapLayouts()
    {
        foreach(GameObject item in layout1){
            item.SetActive(false);
        }

        roomShiftSound.Play();

        foreach(GameObject item in layout2)
        {
            item.SetActive(true);
        }
    }
}
