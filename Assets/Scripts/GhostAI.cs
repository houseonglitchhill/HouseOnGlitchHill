using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour {
    private NavMeshAgent agent;
    public GameObject Player, OrbOne, OrbTwo, OrbThree;
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
    }
	
	// Update is called once per frame
	void Update () {
        if (engaging) {
            agent.isStopped = false;
            OrbOne.GetComponent<GhostOrbController>().baseColor = Color.red;
            OrbThree.GetComponent<GhostOrbController>().baseColor = Color.red;
            OrbTwo.GetComponent<GhostOrbController>().baseColor = Color.red;
        }
        else {
            coolOffTimer += Time.deltaTime;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            OrbOne.GetComponent<GhostOrbController>().baseColor = Color.white;
            OrbThree.GetComponent<GhostOrbController>().baseColor = Color.white;
            OrbTwo.GetComponent<GhostOrbController>().baseColor = Color.white;
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
