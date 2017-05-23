using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelCompilation : MonoBehaviour {

	[SerializeField]
		public List<LevelInfo> MyLevels = new List<LevelInfo>();
		public int HighestId;



	public void saveGame()
	{
		LevelList ll = new LevelList (MyLevels);
		string jsonString = JsonUtility.ToJson(ll);
		Debug.Log ("Saving game " + jsonString );
		System.IO.File.WriteAllText ("BattleMoreLevelInfo", jsonString);
	}

	public void loadGame()
	{

		LevelList ll = JsonUtility.FromJson<LevelList>(System.IO.File.ReadAllText("BattleMoreLevelInfo"));
		Resources.Load<GameObject> ("LevelEditor").GetComponent<LevelCompilation> ().MyLevels = ll.ls;

	}

	static LevelCompilation myComp;

	public static LevelCompilation getLevelInfo()
	{
		if (myComp == null) {
			myComp = Resources.Load<GameObject> ("LevelEditor").GetComponent<LevelCompilation>();
		}
		return myComp;

	}
		


}

[System.Serializable]
class LevelList
{
	[SerializeField]
	public List<LevelInfo> ls;

	public LevelList(List<LevelInfo> list)
	{
		ls = list;
	}
}
