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

	public AudioClip victoryLine;
	public AudioClip DefeatLine;
	bool hasFinished;

	public static VictoryTrigger instance;
	// Use this for initialization
	void Awake () {
		instance = this;
		PlayerPrefs.SetInt ("RecentLevel", levelNumber);
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
			ObjectiveManager.instance.completeBonus (obj);

		} else {

			if (obj.UltimateObjective) {
				Win ();
				return;
			}
			ObjectiveManager.instance.completeMain (obj);

			//foreach (Objective o in mainObjective) {
				//if (!o.completed) {
				//	return;}
			//}
			//Win ();
		}
	}

	public void unComplete(Objective obj)
	{TechCredits -= obj.reward;
		if (obj.bonus) {
			Debug.Log ("Completed bonus");
			completeBonusObj--;
			ObjectiveManager.instance.unCompleteBonus (obj);

		} else {
			ObjectiveManager.instance.UnCompleteMain(obj);
		}
	}


	public void FailObjective(Objective ob)
	{
		ObjectiveManager.instance.failObjective(ob);
	}


	public void UpdateObjective(Objective ob)
	{
		ObjectiveManager.instance.updateObjective(ob);
	}



	public void Win()
	{if (!hasFinished) {
			hasFinished = true;
			VictoryScreen.enabled = true;
			PlayerPrefs.SetInt ("L" + levelNumber+"Win", PlayerPrefs.GetInt ("L" + levelNumber+"Win") + 1);

			int diff = LevelData.getDifficulty ()-1;
			if (diff > PlayerPrefs.GetInt ("L" + levelNumber + "Dif", -1)) {
		
				PlayerPrefs.SetInt ("L" + levelNumber + "Dif", diff);
			}
			GameObject.FindObjectOfType<MainCamera> ().DisableScrolling ();
			GetComponent<AchievementChecker> ().EndLevel ();

			StartCoroutine (WinLevel ());
		}
	}


	public void Lose()
	{if (!hasFinished) {
			hasFinished = true;
			Debug.Log ("Lost");
			DefeatScreen.enabled = true;
			GameObject.FindObjectOfType<MainCamera> ().DisableScrolling ();
			StartCoroutine (LoseLevel ());
		}
	}

	IEnumerator WinLevel ()
	{LevelData.getsaveInfo().ComingFromLevel = true;
		ExpositionDisplayer.instance.displayText (6, victoryLine, 1);
	
		yield return new WaitForSeconds (2.5f);

		int bonusTech =LevelData.getDifficulty ();
		if (bonusTech == 1) {
			bonusTech = 0;
		} else if (bonusTech == 3) {
			bonusTech = 5;}
		//Set my victory screen
		//LevelData.loadVetStats (GameManager.main.playerList [0].getUnitStats());
		LevelData.levelInfo Ldata = createLevelInfo(levelNumber , GameManager.main.playerList [1].UnitsLost(),GameManager.main.playerList [0].UnitsLost(), GameManager.main.playerList [0].totalResO() +  GameManager.main.playerList [0].totalResT(),
			Clock.main.getTime(), TechCredits + techRewards + bonusTech, completeBonusObj + "/" + totalBonusObj);
		foreach (VictoryScreen vs in GameObject.FindObjectsOfType<VictoryScreen> ()) {
			vs.SetResults (Ldata, true);
		}

		LevelData.addLevelInfo (levelNumber , GameManager.main.playerList [1].UnitsLost(),GameManager.main.playerList [0].UnitsLost(), GameManager.main.playerList [0].totalResO() +  GameManager.main.playerList [0].totalResT(),
			Clock.main.getTime(), TechCredits + techRewards + bonusTech, completeBonusObj + "/" + totalBonusObj);
		


	}

	IEnumerator LoseLevel ()
	{
		yield return new WaitForSeconds (1);
		ExpositionDisplayer.instance.displayText (6, DefeatLine, 1);
		yield return new WaitForSeconds (2.5f);

		LevelData.levelInfo Ldata = createLevelInfo(levelNumber , GameManager.main.playerList [1].UnitsLost(),GameManager.main.playerList [0].UnitsLost(), GameManager.main.playerList [0].totalResO() +  GameManager.main.playerList [0].totalResT(),
			Clock.main.getTime(), TechCredits + techRewards, completeBonusObj + "/" + totalBonusObj);
		foreach (VictoryScreen vs in GameObject.FindObjectsOfType<VictoryScreen> ()) {
			vs.SetResults (Ldata, false);
		}

	//	DefeatScreen.enabled = false;
	//	GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
	//	DefeatScreen.enabled = false;
		//SceneManager.LoadScene (3);
	}



	public LevelData.levelInfo createLevelInfo(int levelN, int EnemiesD, int UnitsL, int Res, string timer,
		int Tech, string bonus)
	{
		LevelData.levelInfo myL = new LevelData.levelInfo ();
		myL.levelNumber = levelN;
		myL.EnemiesDest = EnemiesD;
		myL.unitsLost = UnitsL;
		myL.Resources = Res;
		myL.time = timer;
		myL.TechCredits = Tech;
		myL.bonusObj = bonus;
		return myL;
		//	Debug.Log ("Cureent Level " + currentLevel);
	}

	public void replay()
	{
		VictoryScreen.enabled = false;
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

	}

	public void endLevel ()
	{
		VictoryScreen.enabled = false;
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (3);

	}



}
