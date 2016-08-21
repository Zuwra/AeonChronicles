using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


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
	public bool isDisabled = false;

	public Canvas racetipMenu;
	public Canvas OptionMenu;
	public Canvas hotkeyMenu;
	public Canvas soundMenu;
	public Canvas gameplayMenu;
	public Canvas graphicsMenu;

	public Canvas missionLog;
	public Canvas otherHotkeys;

	private Canvas currentMenu;

	private UIManager uimanage;

	//to be deactivated when the game is paused to halt their inputs.
	private List<MonoBehaviour> disableScripts = new List<MonoBehaviour>();

	public static GameMenu main;

	void Awake()
	{
		main = this;
	}

	// Use this for initialization
	void Start () {
		
		uimanage = (UIManager)FindObjectOfType<UIManager>();
		if (GameSettings.gameSpeed < 0) {
			GameSettings.gameSpeed = 1;
		} 
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Backspace)) {
			
			openMenu ();
			CursorManager.main.normalMode ();
		}

	}

	public void addDisableScript(MonoBehaviour m)
	{
		disableScripts.Add (m);
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
		pause ();
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
	//	Debug.Log ("Pauseing");
		ispaused = true;

			Time.timeScale = 0;
		foreach (MonoBehaviour m in disableScripts) {
			m.enabled = false;
		}

		
	}

	public void disableInput()
	{foreach (MonoBehaviour m in disableScripts) {
			m.enabled = false;
		}

	}

	public void EnableInput()
	{foreach (MonoBehaviour m in disableScripts) {
			m.enabled =true;
		}
	}

	public void unpause()
	{
		//	Debug.Log ("Pauseing");
		ispaused = false;
	
		Time.timeScale = GameSettings.gameSpeed;
		foreach (MonoBehaviour m in disableScripts) {
			m.enabled = true;
		}
	}

	public void openSoundMenu()
	{setMenu (soundMenu);
	}

	public void returnToGame()
	{setMenu (null);
		uimanage.SwitchToModeNormal ();
		unpause ();
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



	public void opeMissionLog(){
		setMenu (missionLog);
	}


}
