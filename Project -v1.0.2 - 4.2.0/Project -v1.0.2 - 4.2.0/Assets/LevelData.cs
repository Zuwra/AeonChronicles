using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;

using System.IO;
public class LevelData  {


	public static saveInfo currentInfo;

	[Serializable]
	public class saveInfo
	{
		public  List<levelInfo> myLevels = new List<levelInfo>();
		public  bool ComingFromLevel;

		public  List<string> purchasedUpgrades = new List<string>();
	
		public  List<keyValue> appliedUpgrades  = new List<keyValue>();

		public List<VeteranStats> myVets;

	}

	[Serializable]
	public class keyValue
	{
		public string theName;
		public int index;
	}


	[Serializable]
	public struct levelInfo{
		public int levelNumber;
		public int unitsLost;
		public int EnemiesDest;
		public int Resources;
		public string time;
		public int TechCredits;
		public string bonusObj;

	}

	public static saveInfo getsaveInfo()
	{
		loadGame ();

		return currentInfo;
	}

	public static void addLevelInfo(int levelN, int EnemiesD, int UnitsL, int Res, string timer,
		int Tech, string bonus)
	{
		loadGame ();

		levelInfo myL = new levelInfo ();
		myL.levelNumber = levelN;
		myL.EnemiesDest = EnemiesD;
		myL.unitsLost = UnitsL;
		myL.Resources = Res;
		myL.time = timer;
		myL.TechCredits = Tech;
		myL.bonusObj = bonus;

		//TO DO ADD IN STUFF TO REPLACE PAST LEVEL's INFO
		if (currentInfo.myLevels == null) {
			currentInfo.myLevels = new List<levelInfo> ();
		}
		currentInfo.myLevels.Add (myL);
		addMoney (Tech);

		setHighestLevel (levelN + 1);

		saveGame ();

	//	Debug.Log ("Cureent Level " + currentLevel);
	}

	public static void loadVetStats(List<VeteranStats> theStats)
	{loadGame ();
		currentInfo.myVets = theStats;
		saveGame ();
	}

	public static void addUpgrade(Upgrade up)
	{loadGame ();

		if (up) {
			currentInfo.purchasedUpgrades.Add (up.Name);
		}
		saveGame ();
	}

	//Used for keeping track of which guys have upgrades applied to them
	public static void applyUpgrade(string s, int u )
	{
		loadGame ();
	

		//Debug.Log ("Applying Upgrade " + s + "  " + u);
		bool hitSomething = false;
		foreach (keyValue kv in currentInfo.appliedUpgrades) {
			if (kv.theName == s) {
				kv.index = u;
				hitSomething = true;
			}
		}
		if (!hitSomething) {
			keyValue newKV = new keyValue ();
			newKV.theName = s;
			newKV.index = u;
			currentInfo.appliedUpgrades.Add (newKV);
		}
			
		saveGame ();
	}



	public static void reset()
	{
		loadGame ();
		PlayerPrefs.DeleteAll ();

		PlayerPrefs.SetInt ("HighestLevel", 0);
		//Debug.Log ("Hieghest level is 0");
		setMoney (0);
		currentInfo = new saveInfo ();
		saveGame ();
		PlayerPrefs.SetInt ("VoicePack", 0);
		foreach (Achievement Ach in ((GameObject)Resources.Load("Achievements")).GetComponents<Achievement>()) {
			Ach.Reset ();
		}

		for (int i = 0; i < 12; i++) {
			PlayerPrefs.SetInt ("L" + i + "Dif", -1);
			PlayerPrefs.SetInt ("L" + i + "Win", 0);
		}
	}


	public static void setHighestLevel(int levelNum)
	{
		if(levelNum > PlayerPrefs.GetInt("HighestLevel", 0)){
			PlayerPrefs.SetInt ("HighestLevel", levelNum);
		}

	}

	public static int getHighestLevel()
	{
		if (PlayerPrefs.GetInt ("HighestLevel", -1) == -1) {
			//Debug.Log ("its zero");
			return 0;
		}
		return PlayerPrefs.GetInt ("HighestLevel", -1);
	}


	public static void loadGame()
	{
		if (currentInfo == null) {
			try{
			string inputJson = System.IO.File.ReadAllText("AeonSaveFile.txt");
			currentInfo = JsonUtility.FromJson<saveInfo> (inputJson);
			//	Debug.Log ("Loading info " + inputJson);
			}
			catch(Exception) {
				
			}
		}
		if (currentInfo == null) {
			currentInfo = new saveInfo ();
		}
	}


	public static void saveGame()
	{
		
		string jsonString = JsonUtility.ToJson(currentInfo);
		//Debug.Log ("Saving game " + jsonString);
		System.IO.File.WriteAllText ("AeonSaveFile.txt", jsonString);

	}



	public static void setMoney(int amount)
	{
		PlayerPrefs.SetInt ("Money", amount);
	}

	public static int getMoney()
	{
	//	Debug.Log ("Money left" + PlayerPrefs.GetInt ("Money"));
		return PlayerPrefs.GetInt ("Money");
	}

	public static int getArbitronium()
	{
		return PlayerPrefs.GetInt ("Arbitronium",0);
	}
		

	public static void addMoney(int amount)
	{
		PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + amount);
	}
	public static void addArbitronium(int amount)
	{
		PlayerPrefs.SetInt ("Arbitronium", PlayerPrefs.GetInt ("Arbitronium") + amount);
	}


	public static int getDifficulty()
	{
		return PlayerPrefs.GetInt ("Difficulty", 1);
	}

	public static void setDifficulty(int n)
	{
		PlayerPrefs.SetInt ("Difficulty", n);
	}




	public static void setUltLevel(int ultNum, int rank)
	{
		PlayerPrefs.SetInt ("Ult" + ultNum, rank);
	}

	public static int getUltLevel(int ultNum)
	{
		return PlayerPrefs.GetInt("Ult" + ultNum);
	}

}
