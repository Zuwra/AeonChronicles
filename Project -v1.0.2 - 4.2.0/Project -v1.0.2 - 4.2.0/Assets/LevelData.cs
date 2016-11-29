using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
public class LevelData  {


	public static List<levelInfo> myLevels;
	public static bool ComingFromLevel;

	public static List<Upgrade> purchasedUpgrades;
	public static Dictionary<string,int> appliedUpgrades;

	public static List<VeteranStats> myVets;



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

	public static void addLevelInfo(int levelN, int EnemiesD, int UnitsL, int Res, string timer,
		int Tech, string bonus)
	{
		levelInfo myL = new levelInfo ();
		myL.levelNumber = levelN;
		myL.EnemiesDest = EnemiesD;
		myL.unitsLost = UnitsL;
		myL.Resources = Res;
		myL.time = timer;
		myL.TechCredits = Tech;
		myL.bonusObj = bonus;

		//TO DO ADD IN STUFF TO REPLACE PAST LEVEL's INFO
		if (myLevels == null) {
			myLevels = new List<levelInfo> ();
		}
		myLevels.Add (myL);
		addMoney (Tech);

		setHighestLevel (levelN + 1);

	

	//	Debug.Log ("Cureent Level " + currentLevel);
	}

	public static void loadVetStats(List<VeteranStats> theStats)
	{
		myVets = theStats;
	}

	public static void addUpgrade(Upgrade up)
	{if (purchasedUpgrades == null) {
			purchasedUpgrades = new List<Upgrade> ();
		}
		purchasedUpgrades.Add (up);
	}

	//Used for keeping track of which guys have upgrades applied to them
	public static void applyUpgrade(string s, int u )
	{
	//	Debug.Log ("Adding upgrade " + s + "  " + u);
		if(appliedUpgrades == null) {
			appliedUpgrades = new Dictionary<string, int> ();
		
		}

		if (appliedUpgrades.ContainsKey (s)) {
			appliedUpgrades.Remove (s);
		}

		appliedUpgrades.Add (s, u);
	}



	public static void reset()
	{

		PlayerPrefs.SetInt ("HighestLevel", 0);
		Debug.Log ("Hieghest level is 0");
		setMoney (0);
		if (myLevels != null) {
			myLevels.Clear ();
		}

		ComingFromLevel = false;
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
			Debug.Log ("its zero");
			return 0;
		}
		return PlayerPrefs.GetInt ("HighestLevel", -1);
	}



	public static void setMoney(int amount)
	{
		PlayerPrefs.SetInt ("Money", amount);
	}

	public static int getMoney()
	{
		return PlayerPrefs.GetInt ("Money");
	}


	public static void addMoney(int amount)
	{
		PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt ("Money") + amount);
	}

	public static int getDifficulty()
	{

		return PlayerPrefs.GetInt ("Dificulty", 1);
		
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
