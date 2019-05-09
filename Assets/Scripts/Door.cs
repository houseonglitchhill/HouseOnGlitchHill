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
    private AudioSource audioSource;

    public AudioClip doorLocked;
    public AudioClip doorUnlocked;
    public AudioClip doorOpen;
    public AudioClip doorClosed;
    public AudioClip doorCreak;

    [SerializeField] private bool locked;
    private bool brickingDoor = false;

	// Use this for initialization
	void Start () {
        hinge = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();

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
                audioSource.clip = doorUnlocked;
                audioSource.Play();
                locked = false;
                player.HasMasterKey = false;
            }
            else
            {
                audioSource.clip = doorLocked;
                audioSource.Play();
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
        if(targetPosition == 100 && !brickingDoor)
        {
            audioSource.clip = doorOpen;
            audioSource.Play();
        } else if(targetPosition == 0 && !brickingDoor)
        {
            audioSource.clip = doorCreak;
            audioSource.Play();
        }
        while (hinge.angle <= targetPosition-0.5f || hinge.angle >= targetPosition + 0.5f)
        {
            yield return new WaitForSeconds(0.02f);
        }
        if (targetPosition == 0 && !brickingDoor)
        {
            audioSource.clip = doorClosed;
            audioSource.Play();
        }
        rb.isKinematic = true;
    }

    public IEnumerator BrickTheDoor()
    {
        joint.spring = 45;
        joint.damper = 2;
        brickingDoor = true;
        OpenOrCloseDoor();
        while (!rb.isKinematic)
        {
            yield return new WaitForSeconds(0.1f);
        }
        brickingDoor = false;
        joint.spring = 8;
        joint.damper = 3;
        FindObjectOfType<BrickedDoor>().ActivateBricks();
    }

    public bool getOpen()
    {
        return open;
    }
}
