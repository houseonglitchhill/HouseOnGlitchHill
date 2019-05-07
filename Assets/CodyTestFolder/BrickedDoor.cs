using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickedDoor : MonoBehaviour {

    [SerializeField] private GameObject brickWall;
    public BrickTrigger[] triggers;
    

    // Use this for initialization
    void Start() {
        brickWall = GameObject.Find("BrickWall");
        brickWall.SetActive(false);
        triggers = GetComponentsInChildren<BrickTrigger>();
    }

    public void ActivateBricks()
    {
        brickWall.SetActive(true);
        foreach(BrickTrigger trigger in triggers)
        {
            trigger.gameObject.SetActive(false);
        }
    }

}
