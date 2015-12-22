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

	public Canvas racetipMenu;
	public Canvas OptionMenu;
	public Canvas hotkeyMenu;
	public Canvas soundMenu;
	public Canvas gameplayMenu;
	public Canvas graphicsMenu;

	private Canvas currentMenu;


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Backspace)) {
			openMenu ();
		}
	
	}




	private void setMenu(Canvas can)
	{if (currentMenu != null) {
			currentMenu.enabled = false;
		}
		currentMenu = can;
		if (can != null) {
			can.enabled = true;
		}
		
	}



	public void openMenu()
	{if(myCanvas.enabled == true)
		{returnToGame();}
	else
	{setMenu (myCanvas);}
	}

	public void quitGame()
	{}

	public void openOptions()
	{setMenu (OptionMenu);
	}

	public void openHotkeys()
	{
		setMenu (hotkeyMenu);
	}

	public void openRaceTips()
	{setMenu (racetipMenu);}


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

	public void openSoundMenu()
	{setMenu (soundMenu);
	}

	public void returnToGame()
	{setMenu (null);
	}



	public void openGamePlayMenu()
	{setMenu (gameplayMenu);
	}

	public void openGraphicsMenu()
	{
		setMenu (graphicsMenu);
	}



}
