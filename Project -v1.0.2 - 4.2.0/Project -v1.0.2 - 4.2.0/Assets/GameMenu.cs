using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameMenu : MonoBehaviour {


	public Canvas myCanvas;

	public Button quitB;
	public Button optionsB;
	public Button hotkeysB;
	public Button raceTipsB;
	public Button pauseB;
	public Button returnToGameB;

	public bool ispaused = false;


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Backspace)) {
			openMenu ();
		}
	
	}



	public void openMenu()
	{if(myCanvas.enabled == true)
		{returnToGame();}
	else
	{myCanvas.enabled = true;}
	}

	public void quitGame()
	{}

	public void openOptions()
	{
	}

	public void openHotkeys()
	{}

	public void openRaceTips()
	{}
	public void pause()
	{
		ispaused = !ispaused;
		if (ispaused) {
			pauseB.GetComponentInChildren<Text>().text = "Resume";
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
			pauseB.GetComponentInChildren<Text>().text = "Pause";
		}

	}

	public void returnToGame()
	{myCanvas.enabled = false;
	}





}
