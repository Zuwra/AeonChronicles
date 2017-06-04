using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelCompilation : MonoBehaviour {

	[SerializeField]
		public List<LevelInfo> MyLevels = new List<LevelInfo>();


	public void saveGame()
	{
		LevelList ll = new LevelList (MyLevels);
		string jsonString = JsonUtility.ToJson(ll);
		Debug.Log ("Saving game " + jsonString );
		System.IO.File.WriteAllText ("BattleMoreLevelInfo", jsonString);
	}


	public static void saveGameStatic()
	{

		string jsonString = JsonUtility.ToJson(myComp);
		Debug.Log ("Saving game " + jsonString );
		System.IO.File.WriteAllText ("BattleMoreLevelInfo", jsonString);
	}

	public LevelList loadGame()
	{

		LevelList ll = JsonUtility.FromJson<LevelList>(System.IO.File.ReadAllText("BattleMoreLevelInfo"));
		return ll;
		//Resources.Load<GameObject> ("LevelEditor").GetComponent<LevelCompilation> ().MyLevels = ll.ls;

	}
	public static LevelList loadGameStatic()
	{

		LevelList ll = JsonUtility.FromJson<LevelList>(System.IO.File.ReadAllText("BattleMoreLevelInfo"));
		return ll;
		//Resources.Load<GameObject> ("LevelEditor").GetComponent<LevelCompilation> ().MyLevels = ll.ls;

	}

	static LevelList myComp;

	public static LevelList getLevelInfo()
	{
		if (myComp == null) {
			myComp = loadGameStatic ();
			//myComp = Resources.Load<GameObject> ("LevelEditor").GetComponent<LevelCompilation>();
		}
		return myComp;

	}
		


}

[System.Serializable]
public class LevelList
{
	[SerializeField]
	public List<LevelInfo> ls;

	public LevelList(List<LevelInfo> list)
	{
		ls = list;
	}
}
