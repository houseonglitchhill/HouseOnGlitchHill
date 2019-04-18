using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour {

    public Text titleText;
    public Button startButton;
    public Button quitButton;
    public Text loadText;

	// Use this for initialization
	void Awake () {
        //set text fields
        titleText.text = "House on Glitch Hill";
        loadText.text = "";

        //create on listeners for buttons
        startButton.onClick.AddListener(delegate { StartGame(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
	}
	
    public void StartGame()
    {
        loadText.text = "Loading . . .";
    }

    public void QuitGame()
    {

    }

}
