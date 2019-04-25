using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    //References to other objects
    public GameObject Player, OrbOne, OrbTwo, OrbThree;
    private GhostOrbController orbControllOne, orbControllTwo, orbControllThree;

    public Color engagedColor, inactiveColor;

    public bool engaging; // Whether Ghost is pursuing player
    public float coolOffTime, closeDistance; //Cooldown timer and how close the ghost should be to the player before disengaging

    //Navmesh agent stuff
    private NavMeshAgent agent;
    private Transform target;
    private float distanceToTarget, coolOffTimer;


    // Use this for initialization
    void Start()
    {
        // Get a reference to the player object
        if (Player == null)
        {
            GameObject[] t = GameObject.FindGameObjectsWithTag("Player");
            target = t[0].transform;
        }
        else
        {
            target = Player.transform;
        }

        agent = GetComponent<NavMeshAgent>(); //setup the nav agent
        engaging = true;
        //Get references to the color control on orbs
        orbControllOne = OrbOne.GetComponent<GhostOrbController>();
        orbControllTwo = OrbTwo.GetComponent<GhostOrbController>();
        orbControllThree = OrbThree.GetComponent<GhostOrbController>();

        //Define default colors
        engagedColor = Color.red;
        inactiveColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (engaging)
        { // Runs when ghost is pursing player
            agent.isStopped = false; // Starts the nav agent
            orbControllOne.baseColor = engagedColor;
            orbControllTwo.baseColor = engagedColor;
            orbControllThree.baseColor = engagedColor;
            orbControllOne.SetSpeed("High");
            orbControllTwo.SetSpeed("High");
            orbControllThree.SetSpeed("High");


        }
        else
        { // Runs when the ghost is inactive
            coolOffTimer += Time.deltaTime;
            agent.isStopped = true; // Stops the nav agent
            agent.velocity = Vector3.zero;
            orbControllOne.baseColor = inactiveColor;
            orbControllTwo.baseColor = inactiveColor;
            orbControllThree.baseColor = inactiveColor;
            orbControllOne.SetSpeed("Low");
            orbControllTwo.SetSpeed("Low");
            orbControllThree.SetSpeed("Low");
        }

        agent.SetDestination(target.position); // Sets nav agent target to player position
        distanceToTarget = Vector3.Distance(target.position, transform.position); //Store the distance to the target

        //If the ghost has reached the player make it inactive for a peroid
        if (distanceToTarget < closeDistance)
        {
            engaging = false;
            coolOffTimer = 0;
        }
        if (coolOffTimer > coolOffTime)
        {
            engaging = true;
        }
    }
}
