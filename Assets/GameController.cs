using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    //prefabs
    public GameObject ghostPrefab, playerPrefab, keyPrefab;
    public Transform playerSpawnPoint;
    public List<Transform> keySpawns, teleportSpots, ghostSpawnPoints;

    //canvas elements
    public RawImage keyImage;
    public Text endGameText;
    public GameObject pauseMenu;

    //hold references to key objects
    private GameObject ghost, player, key;

    [SerializeField]
    private bool keyGrabbed;

    [SerializeField]
    private bool tutorialFinished;

    public static int glitchesActivated = 0;

    PlayerController pc;

    // Use this for initialization
    void Start() {
        SpawnPlayer();
        SpawnKey();
        SpawnGhost();

        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        keyGrabbed = false;
        tutorialFinished = false;

        //set key canvas image and end game text invisible
        keyImage.CrossFadeAlpha(0.0f, 0, true);
        endGameText.text = "";

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        if (keyGrabbed)
        {
            keyImage.CrossFadeAlpha(1.0f, 1, true);
        }
        else
        {
            keyImage.CrossFadeAlpha(0.0f, 0, true);
        }

        if(glitchesActivated >= 15)
        {
            Debug.Log("Game Over");
            EndGame(false);
            //load game over
        }
    }

    void SpawnGhost() {
        ghost = Object.Instantiate(ghostPrefab, ghostSpawnPoints[0].position, Quaternion.identity);
        ghost.GetComponent<GhostAI>().Player = player;
        ghost.GetComponent<GhostAI>().ghostSpawns = ghostSpawnPoints;
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

    public void EndGame(bool wonGame)
    {
        if (wonGame)
        {
            Debug.Log("Win Game");
            endGameText.text = "You've Escaped!";
        }
        else
        {
            endGameText.text = "You Died...";
        }
        StartCoroutine(EndGameWait());
    }

    IEnumerator EndGameWait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main Menu");
    }

    public bool KeyGrabbed { get; set; }

    public bool TutorialFinished { get; set; }

    // end of class
}