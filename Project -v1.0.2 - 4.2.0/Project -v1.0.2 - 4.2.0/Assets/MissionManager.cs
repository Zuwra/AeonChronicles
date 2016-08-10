using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {


	public Canvas Intelligence;
	public Canvas Victoryscreen;
	public GameObject loadingScreen;
	public Canvas myCanvas;
	public static MissionManager main;

	void Awake()
	{main = this;
		myCanvas = GetComponent<Canvas> ();}

	// Use this for initialization
	void Start () {
		

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void ToggleIntelligence()
	{
		Intelligence.enabled = !Intelligence.enabled;

	}




	public void toggleVictory()
	{
		Victoryscreen.enabled = !Victoryscreen.enabled;
		myCanvas.enabled = !myCanvas.enabled;

	}

	public void nextLevel(){
		Victoryscreen.enabled = !Victoryscreen.enabled;
		myCanvas.enabled = !myCanvas.enabled;
	
			LevelManager.main.nextLevel ();

	}


	public void StartMission(int levelNum)
	{
		loadingScreen.SetActive (true);
		SceneManager.LoadScene (levelNum);
	}

	public void QuitCampaign()
	{
		SceneManager.LoadScene (0);
	}



}
