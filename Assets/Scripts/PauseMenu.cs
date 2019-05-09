using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Text pauseText;
    public Button continueBtn;
    public Button mmBtn;
    public Button quitBtn;

    GameController gc;

	// Use this for initialization
	void Awake() {
        gc = GameController.FindObjectOfType<GameController>();

        continueBtn.GetComponent<Button>().onClick.AddListener(delegate { ContinueGame(); } );
        mmBtn.GetComponent<Button>().onClick.AddListener(delegate { MainMenu(); } );
        quitBtn.GetComponent<Button>().onClick.AddListener(delegate { QuitGame(); } );

    }

    //Continue the game
    void ContinueGame()
    {
        gameObject.SetActive(false);  //Set pause menu canvas to false
        gc.ResumeGame();
    }

    //Load Main Menu
    void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    //Exit the game
    void QuitGame()
    {
        Application.Quit();
    }
}
