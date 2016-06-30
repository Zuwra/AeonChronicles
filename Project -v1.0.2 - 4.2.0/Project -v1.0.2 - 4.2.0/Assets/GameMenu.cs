using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameMenu : MonoBehaviour {


	public Canvas myCanvas;

	public Button quitB;
	public Button optionsB;
	public Button hotkeysB;
	public Button raceTipsB;
	public Button pauseB;
	public Button returnToGameB;
	public Button MissionLogB;

	public bool ispaused = false;

	public Canvas racetipMenu;
	public Canvas OptionMenu;
	public Canvas hotkeyMenu;
	public Canvas soundMenu;
	public Canvas gameplayMenu;
	public Canvas graphicsMenu;
	public Canvas Objectives;
	public Canvas Victory;
	public Canvas missionLog;
	public Canvas otherHotkeys;

	private Canvas currentMenu;

	private UIManager uimanage;
	// Use this for initialization
	void Start () {
		uimanage = (UIManager)FindObjectOfType<UIManager>();
	
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
		{returnToGame();
			uimanage.SwitchToModeNormal ();
		

		}
	else
	{setMenu (myCanvas);
		uimanage.setToMenu ();

	}
	}

	public void quitGame()
	{SceneManager.LoadScene (0);}

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
		uimanage.SwitchToModeNormal ();
		
	}


	public void openOtherHotkeys()
	{setMenu (otherHotkeys);
	}

	public void openGamePlayMenu()
	{setMenu (gameplayMenu);
	}

	public void openGraphicsMenu()
	{
		setMenu (graphicsMenu);
	}

	public void openObjectives()
	{setMenu (Objectives);
	}

	public void opeMissionLog(){
		setMenu (missionLog);
	}


}
