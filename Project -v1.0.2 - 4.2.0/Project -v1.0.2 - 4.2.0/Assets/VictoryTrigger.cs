using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class VictoryTrigger : MonoBehaviour {

	public Canvas VictoryScreen;
	public Canvas DefeatScreen;


	public List<Objective> mainObjective = new List<Objective> ();
	public List<Objective> bonusObjective = new List<Objective> ();

	public int techRewards;
	private int totalBonusObj;
	private int completeBonusObj;

	public int levelNumber;
	public int EnemiesDest;
	public int Resources;
	public string time;
	public int TechCredits;


	public static VictoryTrigger instance;
	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Home)) {
			Win ();
		}
	}


	public void addObjective(Objective obj)
	{
		if (obj.bonus) {
			totalBonusObj++;
			bonusObjective.Add (obj);

			ObjectiveManager.instance.setBonusObjectives (obj);
		
		} 
		else {
			mainObjective.Add (obj);
		
			ObjectiveManager.instance.setObjective (obj);
		}
	
	}

	public void CompleteObject(Objective obj)
	{	TechCredits += obj.reward;
		
		if (obj.bonus) {
			Debug.Log ("Completed bonus");
			completeBonusObj++;

			ObjectiveManager.instance.completeMain (obj);
		} else {

			if (obj.UltimateObjective) {
				Win ();
				return;
			}

			ObjectiveManager.instance.completeBonus (obj);
			foreach (Objective o in mainObjective) {
				if (!o.completed) {
					return;}
			}
			Win ();
		}



	}




	public void Win()
	{
		VictoryScreen.enabled = true;
		GameObject.FindObjectOfType<MainCamera> ().DisableScrolling ();
		StartCoroutine(WinLevel ());
	}


	public void Lose()
	{
		DefeatScreen.enabled = true;
		GameObject.FindObjectOfType<MainCamera> ().DisableScrolling ();
		StartCoroutine(LoseLevel ());
	}

	IEnumerator WinLevel ()
	{LevelData.ComingFromLevel = true;
		yield return new WaitForSeconds (4);

		LevelData.addLevelInfo (levelNumber , GameManager.main.playerList [1].UnitsLost(),GameManager.main.playerList [0].UnitsLost(), GameManager.main.playerList [0].totalResO() +  GameManager.main.playerList [0].totalResT(),
			Clock.main.getTime(), TechCredits + techRewards, completeBonusObj + "/" + totalBonusObj);
	
		VictoryScreen.enabled = false;
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (3);
	}

	IEnumerator LoseLevel ()
	{
		yield return new WaitForSeconds (4);
		DefeatScreen.enabled = false;
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (1);
	}



}
