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
            StartCoroutine(DoorSwinging(0));
        }
        else
        {   // closes door
            joint.targetPosition = 100;
            StartCoroutine(DoorSwinging(100));
        }
        hinge.spring = joint;
    }

    IEnumerator DoorSwinging(float targetPosition)
    {
        rb.isKinematic = false;
        while (hinge.angle <= targetPosition-0.5f || hinge.angle >= targetPosition + 0.5f)
        {
            Debug.Log("Hinge Angle: " + hinge.angle + " Target Angle: " + targetPosition);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Reached our target angle");
        rb.isKinematic = true;
    }
}
