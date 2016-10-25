using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData  {

	public static int currentLevel =0;
	public static int totalXP = 0;

	public static List<levelInfo> myLevels;
	public static bool easyMode = true;
	public static bool ComingFromLevel;
	public static levelInfo lastInfo;
	public static List<Upgrade> purchasedUpgrades;
	public static Dictionary<string,int> appliedUpgrades;

	public static List<VeteranStats> myVets;

	public static int UltOneLevel = 0;
	public static int UltTwoLevel = 0;
	public static int UltThreeLevel = 0;
	public static int UltFourLevel = 0;


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
		totalXP += Tech;
		if (currentLevel < levelN + 1) {
			currentLevel = levelN +1; 
		}
	
		lastInfo = myL;
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
		Debug.Log ("Adding upgrade " + s + "  " + u);
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
		Debug.Log ("Resetting");
	currentLevel =0;
		totalXP = 0;
		if (myLevels != null) {
			myLevels.Clear ();
		}
		easyMode = true;
		ComingFromLevel = false;
	}



}
