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

	// Use this for initialization
	void Start () {
        hinge = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();

        hinge.useSpring = true;
        joint = hinge.spring;
        joint.spring = 8;
        joint.damper = 3;

        open = false;
	}

    public void OpenOrCloseDoor()
    {
        open = !open;
        if (open)
        {   // open door
            joint.targetPosition = 0;
            StartCoroutine("DoorSwinging");
        }
        else
        {   // closes door
            joint.targetPosition = 100;
            StartCoroutine("DoorSwinging");
        }
        hinge.spring = joint;
    }

    IEnumerator DoorSwinging()
    {
        rb.isKinematic = false;
        yield return new WaitForSeconds(1.5f);
        rb.isKinematic = true;
    }
}
