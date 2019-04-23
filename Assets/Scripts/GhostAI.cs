using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour {
    private NavMeshAgent agent;
    public GameObject Player, OrbOne, OrbTwo, OrbThree;
    private GhostOrbController orbControllOne, orbControllTwo, orbControllThree;
    private Transform target;
    private float distanceToTarget, coolOffTimer;
    public bool engaging;
    public float coolOffTime, closeDistance;
    
    // Use this for initialization
    void Start () {
        if (Player == null) {
            GameObject[] t = GameObject.FindGameObjectsWithTag("Player");
            target = t[0].transform;
        } else {
            target = Player.transform;
        }
        agent = GetComponent<NavMeshAgent>();
        engaging = true;
        orbControllOne = OrbOne.GetComponent<GhostOrbController>();
        orbControllTwo = OrbTwo.GetComponent<GhostOrbController>();
        orbControllThree = OrbThree.GetComponent<GhostOrbController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (engaging) {
            agent.isStopped = false;
            orbControllOne.baseColor = Color.red;
            orbControllTwo.baseColor = Color.red;
            orbControllThree.baseColor = Color.red;
            orbControllOne.SetSpeed("High");
            orbControllTwo.SetSpeed("High");
            orbControllThree.SetSpeed("High");


        }
        else {
            coolOffTimer += Time.deltaTime;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            orbControllOne.baseColor = Color.white;
            orbControllTwo.baseColor = Color.white;
            orbControllThree.baseColor = Color.white;
            orbControllOne.SetSpeed("Low");
            orbControllTwo.SetSpeed("Low");
            orbControllThree.SetSpeed("Low");
        }
        agent.SetDestination(target.position);
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget < closeDistance) {
            engaging = false;
            coolOffTimer = 0;
        }
        if(coolOffTimer > coolOffTime) {
            engaging = true;
        }
    }
}
