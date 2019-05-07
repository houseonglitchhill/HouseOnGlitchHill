using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    // door hinge
    public HingeJoint hinge;

    // for swinging door open and closed
    private JointSpring joint;
    private Rigidbody rb;
    private bool open;
    private PlayerController player;

    [SerializeField] private bool locked;

	// Use this for initialization
	void Start () {
        hinge = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerController>();

        hinge.useSpring = true;
        joint = hinge.spring;
        joint.spring = 8;
        joint.damper = 3;

        open = false;
	}

    public void OpenOrCloseDoor()
    {
        if (locked)
        {
            if (player.HasMasterKey)
            {
                //Play sound effect to unlock the door.
                locked = false;
                player.HasMasterKey = false;
            }
            else
            {
                //Play sound effect for locked door;
                return;
            }
        }

        open = !open;
        if (open)
        {   // open door
            joint.targetPosition = 100;
            StartCoroutine(DoorSwinging(100));
        }
        else
        {   // closes door
            joint.targetPosition = 0;
            StartCoroutine(DoorSwinging(0));
        }
        hinge.spring = joint;
    }

    IEnumerator DoorSwinging(float targetPosition)
    {
        rb.isKinematic = false;
        while (hinge.angle <= targetPosition-0.5f || hinge.angle >= targetPosition + 0.5f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        rb.isKinematic = true;
    }

    public IEnumerator BrickTheDoor()
    {
        joint.spring = 16;
        joint.damper = 5;
        OpenOrCloseDoor();
        while (!rb.isKinematic)
        {
            yield return new WaitForSeconds(0.1f);
        }
        joint.spring = 8;
        joint.damper = 3;
        FindObjectOfType<BrickedDoor>().ActivateBricks();
    }

    public bool getOpen()
    {
        return open;
    }
}
