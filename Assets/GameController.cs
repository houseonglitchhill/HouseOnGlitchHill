using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    //prefabs
    public GameObject ghostPrefab, playerPrefab, keyPrefab;
    public Transform ghostSpawnPoint, playerSpawnPoint;
    public List<Transform> keySpawns, teleportSpots;

    //hold references to key objects
    private GameObject ghost, player, key;


    // Use this for initialization
    void Start() {
        SpawnPlayer();
        SpawnGhost();
        SpawnKey();
    }

    // Update is called once per frame
    void Update() {

    }

    void SpawnGhost() {
        ghost = Object.Instantiate(ghostPrefab, ghostSpawnPoint.position, Quaternion.identity);
        ghost.GetComponent<GhostAI>().Player = player;
        ghost.GetComponent<GhostGltiches>().player = player;
        ghost.GetComponent<GhostGltiches>().teleports = teleportSpots;
    }

    void SpawnPlayer() {
        player = Object.Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    void SpawnKey() {
        key = Object.Instantiate(keyPrefab, keySpawns[Random.Range(0, keySpawns.Count)].position, Quaternion.identity);
    }



    // end of class
}