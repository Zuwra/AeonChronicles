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
