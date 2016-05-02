using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class VictoryTrigger : MonoBehaviour {

	public Canvas VictoryScreen;
	public Canvas DefeatScreen;


	public List<Objective> mainObjective = new List<Objective> ();
	public List<Objective> bonusObjective = new List<Objective> ();

	public static VictoryTrigger instance;
	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void addObjective(Objective obj)
	{
		if (obj.bonus) {
			bonusObjective.Add (obj);

			ObjectiveManager.instance.setBonusObjectives (obj);
		} 
		else {
			mainObjective.Add (obj);
		
			ObjectiveManager.instance.setObjective (obj);
		}
	}

	public void CompleteObject(Objective obj)
	{
		if (obj.bonus) {
			ObjectiveManager.instance.completeMain (obj);
		} else {
			
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
	{
		yield return new WaitForSeconds (6);
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (0);
	}

	IEnumerator LoseLevel ()
	{
		yield return new WaitForSeconds (6);
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (0);
	}



}
