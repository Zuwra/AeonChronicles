using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public Canvas MainMenu;
	public Canvas multiplayer;
	public Canvas campaign;
	public Canvas Credits;


	private Canvas currentScreen;

	// Use this for initialization
	void Start () {
		currentScreen = MainMenu;
	
	}
	
	// Update is called once per frame
	void Update () {
	
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




	public void startMatch()
	{SceneManager.LoadScene (1);
	}

	public void toCampaign()
	{loadScreen (campaign);}

	public void Exit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
		}


	public void toCredits()
	{loadScreen (Credits);}

	public void toMain()
	{loadScreen (MainMenu);}
}
