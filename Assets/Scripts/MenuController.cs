using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {

    //public Text titleText;
    public Button startButton;
    public Button quitButton;
    public Text houseText;
    public Text onText;
    public Text hillText;
    public Text loadText;

    private int waitSeconds = 3;

	// Use this for initialization
	void Awake () {
        //set text fields
        houseText.text = "House";
        onText.text = "On";
        hillText.text = "Hill";
        loadText.text = "";

        //create on listeners for buttons
        startButton.onClick.AddListener(delegate { StartGame(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
	}
	
    public void StartGame()
    {
        loadText.text = "Loading . . .";
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(waitSeconds);
        SceneManager.LoadScene("EricScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
