using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData  {

	public static int currentLevel =0;
	public static int totalXP = 10;

	public static List<levelInfo> myLevels;
	public static bool easyMode = true;
	public static bool ComingFromLevel;

	public static List<VeteranStats> myVets;

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
		currentLevel = levelN +1;
	//	Debug.Log ("Cureent Level " + currentLevel);
	}

	public static void loadVetStats(List<VeteranStats> theStats)
	{
		myVets = theStats;
	}


	public static void reset()
	{

	currentLevel =0;
		totalXP = 0;
		if (myLevels != null) {
			myLevels.Clear ();
		}
		easyMode = true;
		ComingFromLevel = false;
	}



}
