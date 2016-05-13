using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

	public Canvas Technology;
	public Canvas TechTree;
	public Canvas Intelligence;
	public Canvas Victoryscreen;
	private Canvas myCanvas;


	// Use this for initialization
	void Start () {
		myCanvas = GetComponent<Canvas> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void ToggleTech()
	{
		Technology.enabled = !Technology.enabled;
		myCanvas.enabled = !myCanvas.enabled;
	}

	public void ToggleIntelligence()
	{
		Intelligence.enabled = !Intelligence.enabled;

	}

	public void toggleTechTree()
	{
		Technology.enabled = !Technology.enabled;
		TechTree.enabled = !TechTree.enabled;
	}


	public void toggleVictory()
	{
		Victoryscreen.enabled = !Victoryscreen.enabled;
		myCanvas.enabled = !myCanvas.enabled;
	}


	public void StartMission()
	{
		SceneManager.LoadScene (1);
	}

	public void QuitCampaign()
	{
		SceneManager.LoadScene (0);
	}



}
