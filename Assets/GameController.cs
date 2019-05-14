using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    //prefabs
    public GameObject ghostPrefab, playerPrefab, keyPrefab;
    public Transform ghostSpawnPoint, playerSpawnPoint;
    public List<Transform> keySpawns, teleportSpots;

    //canvas elements
    public RawImage keyImage;
    private float alpha;
    public GameObject pauseMenu;

    //hold references to key objects
    private GameObject ghost, player, key;

    [SerializeField]
    private bool keyGrabbed;

    PlayerController pc;

    // Use this for initialization
    void Start() {
        SpawnPlayer();
        SpawnKey();
        SpawnGhost();

        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        keyGrabbed = false;

        //set key canvas image invisible
        alpha = 0.0f;
        keyImage.CrossFadeAlpha(alpha, 0, true);

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        if (keyGrabbed)
        {
            alpha = 1.0f;
            keyImage.CrossFadeAlpha(alpha, 1, true);
        }
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

    public void GrabKey()
    {
        Debug.Log("Key Grabbed");
        keyGrabbed = true;
    }

    public void PauseGame()
    {
        GetComponent<AudioSource>().Pause();
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        pc.enabled = false;
    }

    public void ResumeGame()
    {
        GetComponent<AudioSource>().Play(); Time.timeScale = 1f;
        Cursor.visible = false;
        pc.enabled = true;
    }

    public bool KeyGrabbed { get; set; }

    // end of class
}