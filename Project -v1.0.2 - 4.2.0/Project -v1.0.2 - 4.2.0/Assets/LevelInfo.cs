using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelInfo {


	public  string LevelName;
	public int SceneNumber;//????
	public List<LevelDescription> Description = new List<LevelDescription>();
	public Sprite GeneralPic;
	public Sprite ScenaryPic;
	public List<NewThing> newUnits = new List<NewThing>();
	public List<NewThing> newAbilities= new List<NewThing>();
	public List<NewThing> Intelligence= new List<NewThing>();
	public List<int> UnlockOnWin;
	int CompletionCount;
	int HardestDifficulty;
	int ShortestTime;// in seconds
	public bool unlocked;
	public Sprite MinimapPic;
	public TopUIActivations UIBarsNUlts;
	public float startingMoney = 500;
	public EnemyArsenal displayArsenal;
	public int defaultTip;




	[System.Serializable]
	public class TopUIActivations
	{
		public bool UltOneActivated = true;
		public bool UltTwoActivated= true;
		public bool UltThreeActivated= true;
		public bool UltFourActivated= true;
		public bool LeftBarOpen= true;
		public bool RightBarOpen= true;
		public bool resourcesOpen= true;
		public bool CommandsOpen= false;
	}

	public int getCompletionCount()
	{
		return PlayerPrefs.GetInt ("L" + SceneNumber +"Win", 0);

		//LevelCompilation.loadGameStatic ();
		//Resources.Load<LevelCompilation> ("LevelEditor").loadGame ();
		//return CompletionCount;
	}

	public void increaseCompCount()
	{

		PlayerPrefs.SetInt ("L" + SceneNumber +"Win", PlayerPrefs.GetInt ("L" + SceneNumber+"Win") + 1);

		//CompletionCount++;
		//LevelCompilation.saveGameStatic ();
		//Resources.Load<LevelCompilation> ("LevelEditor").saveGame ();
	}


	public int getHighestDiff()
	{
		return PlayerPrefs.GetInt ("L" +SceneNumber + "Dif", 1);
	}

	public void setHighestDiff(int n)
	{

		int diff = LevelData.getDifficulty ()-1;
		if (diff > PlayerPrefs.GetInt ("L" + SceneNumber + "Dif", -1)) {

			PlayerPrefs.SetInt ("L" +SceneNumber + "Dif", diff);
		}

	}

	public void Reset()
	{
		PlayerPrefs.SetInt ("L" + SceneNumber +"Win", 0);
		PlayerPrefs.SetInt ("L" +SceneNumber + "Dif", 1);
	}

	//int diff = LevelData.getDifficulty ()-1;
	//if (diff > PlayerPrefs.GetInt ("L" + SceneNumber + "Dif", -1)) {

	//	PlayerPrefs.SetInt ("L" + SceneNumber + "Dif", diff);
	//}


	[System.Serializable]
	public class NewThing{

		public string thingName;
		[TextArea(2,10)]
		public string thingDescrption;
		public Sprite Icon;
	}

	[System.Serializable]
	public class  LevelDescription
	{
		[TextArea(2,10)]
		public string ShortDescription;
		[TextArea(3,10)]
		public string LongDescription;
	}


}

[System.Serializable]
public class EnemyArsenal
{
	public List<GameObject> tobeSeen = new List<GameObject> ();
}

