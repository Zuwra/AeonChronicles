using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public Canvas MainMenu;
	public Canvas multiplayer;
	public Canvas campaign;
	public Canvas Credits;
	public Canvas loadCanvas;
	public Canvas OptionsCan;

	public AudioSource audioSource;
	private Canvas currentScreen;

	// Use this for initialization
	void Start () {
		currentScreen = MainMenu;
	
	}

	public void loadScreen(Canvas can)
	{currentScreen.enabled = false;
		currentScreen = can;
		can.enabled = true;
	}

	public void toMultiplayer()
	{
		multiplayer.GetComponent<RaceSelectionManger> ().initialize ();
		loadScreen (multiplayer);
		multiplayer.GetComponent<RaceSelectionManger> ().initialize ();

	}


	public void toPrologue()
	{
		SceneManager.LoadScene (2);
	}

	public void startMatch(Dropdown dropper)
	{
		setDifficulty (dropper);
		resetProgress ();
		audioSource.Stop ();
		SceneManager.LoadScene(5);
	}

	public void toCampaignLevelSelect()
	{
		SceneManager.LoadScene (1);
	}

	public void toCampaign()
		{loadScreen (campaign);
		audioSource.Play ();
	
	}

	public void Exit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
		}


	public void loadOption()
	{
		OptionsCan.enabled = !OptionsCan.enabled;

	}

	public void LoadLevel(int n)
	{//LevelData.currentLevel = n;
		LevelData.getsaveInfo().ComingFromLevel = false;
		SceneManager.LoadScene (3);
	}

	public void toCredits()
	{loadScreen (Credits);}

	public void toMain()
	{loadScreen (MainMenu);
		audioSource.Stop ();
	}


	public void resetProgress()
	{PlayerPrefs.DeleteAll ();

		LevelData.reset ();


	}


	public void setDifficulty(Dropdown dropper)
	{
		LevelData.setDifficulty (dropper.value + 1);

	}


}
