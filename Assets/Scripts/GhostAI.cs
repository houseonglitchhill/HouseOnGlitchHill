using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    //References to other objects
    public GameObject Ghost, Player, OrbOne, OrbTwo, OrbThree;
    public float coolOffTime, closeDistance; //Cooldown timer and how close the ghost should be to the player before disengaging
    public float musicDistance, fadeTime; //how close the ghost should be when ghost music starts playing
    public GhostGltiches glitchList;
    public AudioClip ghostMusic;
    public AudioSource audioS;
    public enum GhostMood { Idle, Enraged, Search, Cowardly};
    public List<Transform> ghostSpawns;
    public Color enraged, search, coward;

    //Behavior variables
    private GhostMood ghostMood;
    private bool handOff, warned;
    //Idle Variables
    //Enraged Variables
    private bool nearTarget;
    private float enragedTime, enrageTimer;
    //Search Variables
    private Vector3 lastPostion;

    //Navmesh agent stuff
    private NavMeshAgent agent;
    private Transform target;
    private float distanceToTarget, coolOffTimer;
    private GhostOrbController orbControllOne, orbControllTwo, orbControllThree; // References to each orbcontroller script

    // Use this for initialization
    void Start()
    {
        Ghost = this.gameObject;
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
        //Get references to the color control on orbs
        orbControllOne = OrbOne.GetComponent<GhostOrbController>();
        orbControllTwo = OrbTwo.GetComponent<GhostOrbController>();
        orbControllThree = OrbThree.GetComponent<GhostOrbController>();
        handOff = true;
        warned = false;
        enragedTime = 10f;
        enrageTimer = 0f;
        ghostMood = GhostMood.Idle;
    }

    // Update is called once per frame
    void Update()
    {
       
        agent.SetDestination(target.position); // Sets nav agent target to player position
        distanceToTarget = FindTargetDistance(); //Finds distance to player
        if (!warned) {
            RaycastHit hit;
            Vector3 f = Player.GetComponent<PlayerController>().mainCamera.transform.forward;
            if (Physics.SphereCast(Player.transform.position, 2f, f, out hit, 4f)) {
                if (hit.transform.tag == "Enemy") {
                    audioS.PlayOneShot(ghostMusic);
                    warned = true;
                }
            }
        }
        //handle moods
        if (ghostMood == GhostMood.Idle) {
            if (handOff) {
                Ghost.transform.position = SpawnRandom();
                agent.isStopped = true; // Stops the nav agent
                agent.velocity = Vector3.zero;
                ChangeOrbColor(Color.white);
                orbControllOne.SetSpeed("Low");
                orbControllTwo.SetSpeed("Low");
                orbControllThree.SetSpeed("Low");
                handOff = false;
                coolOffTimer = 0;
            }
            Idle();
        }
        if (ghostMood == GhostMood.Enraged) {
            if (handOff) {
                Ghost.transform.position = SpawnRandom();
                agent.isStopped = false; // Starts the nav agent
                ChangeOrbColor(enraged);
                orbControllOne.SetSpeed("High");
                orbControllTwo.SetSpeed("High");
                orbControllThree.SetSpeed("High");
                handOff = false;
                nearTarget = false;
                warned = false;
            }
            Enraged();
        }
        if (ghostMood == GhostMood.Search) {
            if (handOff) {
                Ghost.transform.position = SpawnRandom();
                agent.isStopped = false; // Starts the nav agent
                ChangeOrbColor(search);
                orbControllOne.SetSpeed("High");
                orbControllTwo.SetSpeed("High");
                orbControllThree.SetSpeed("High");
                lastPostion = target.position;
                handOff = false;
                nearTarget = false;
                warned = false;
            }
            Search();
        }
        if (ghostMood == GhostMood.Cowardly) {
            if (handOff) {
                Ghost.transform.position = SpawnRandom();
                agent.isStopped = false; // Starts the nav agent
                ChangeOrbColor(coward);
                orbControllOne.SetSpeed("High");
                orbControllTwo.SetSpeed("High");
                orbControllThree.SetSpeed("High");
                lastPostion = target.position;
                handOff = false;
                nearTarget = false;
                warned = false;
            }
            Cower();
        }
        //end of Update
    }
    private void Idle() {
        coolOffTimer += Time.deltaTime;
        if (coolOffTimer > coolOffTime) {
            coolOffTimer -= coolOffTime;
            ghostMood = FindMood();
            handOff = true;
        }
    }
    private void Enraged() {
        if (distanceToTarget < closeDistance * 4) {
            nearTarget = true;
        }
        if (nearTarget) {
            enrageTimer += Time.deltaTime;
            if (!audioS.isPlaying && !warned) {
                audioS.PlayOneShot(ghostMusic);
                warned = true;
            }
        }
        if(enrageTimer > enragedTime) {
            enrageTimer -= enragedTime;
            ghostMood = GhostMood.Idle;
            handOff = true;
        }
        if (distanceToTarget < closeDistance) {
            glitchList.SelectRandomGlitch();
            ghostMood = GhostMood.Idle;
            handOff = true;
        }
    }
    private void Search() {
        if (distanceToTarget < closeDistance * 2.5 && !nearTarget) {
            nearTarget = true;
            lastPostion = target.position;
        }
        if( distanceToTarget > closeDistance * 3) {
            nearTarget = false;
        }
        if (!audioS.isPlaying && !warned && nearTarget) {
            audioS.PlayOneShot(ghostMusic);
            warned = true;
        }
        if (distanceToTarget < closeDistance) {
            if (Vector3.Distance(target.position, lastPostion) < 0.05f && !Player.GetComponent<PlayerController>().flashlight.enabled) {
                ghostMood = GhostMood.Idle;
                handOff = true;
            } else {
                glitchList.SelectRandomGlitch();
                ghostMood = GhostMood.Idle;
                handOff = true;
            }
        }
    }
    private void Cower() {
        if (Player.GetComponent<PlayerController>().flashlight.enabled) {
            RaycastHit hit;
            Vector3 f = Player.GetComponent<PlayerController>().mainCamera.transform.forward;
            if (Physics.SphereCast(Player.transform.position, 2f, f, out hit, 4f)) {
                if (hit.transform.tag == "Enemy") {
                    ghostMood = GhostMood.Idle;
                    handOff = true;
                }
            }
        }
        if (distanceToTarget < closeDistance * 1.5) {
            nearTarget = true;
        }
        if (!audioS.isPlaying && !warned && nearTarget) {
            audioS.PlayOneShot(ghostMusic);
            warned = true;
        }
        if (distanceToTarget < closeDistance) {
                glitchList.SelectRandomGlitch();
                ghostMood = GhostMood.Idle;
                handOff = true;
        }
    }
    private GhostMood FindMood() {
        int roll = Random.Range(0, 3);
        if(roll == 0) {
            return GhostMood.Enraged;
        } else if(roll == 1) {
            return GhostMood.Search;
        } else {
            return GhostMood.Cowardly;
        }
       
    }

    private void ChangeOrbColor(Color c) {
        orbControllOne.baseColor = c;
        orbControllTwo.baseColor = c;
        orbControllThree.baseColor = c;
    }

    private Vector3 SpawnRandom() {
        if(ghostSpawns.Count == 0) {
            return Player.transform.position + new Vector3(3, 0, 0);
        } else {
            return ghostSpawns[Random.Range(0, ghostSpawns.Count)].position;
        }
    }
    
    private float FindTargetDistance() {
        if (target.position.y > transform.position.y +4f || target.position.y < transform.position.y - 4f) {
            return 1000000f;
        } else {
            return Vector3.Distance(target.position, transform.position);
        }
    }
    //end of class
}
