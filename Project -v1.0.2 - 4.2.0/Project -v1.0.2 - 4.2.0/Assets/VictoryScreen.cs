using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour {


	public Text timeDisplay;
	public Text enemyDisplay;
	public Text allyDisplay;
	public Text objDisplay;
	public Text techDisplay;
	public Text ResourceDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void SetResults(victoryInfo info)
	{
		timeDisplay.text = ""+info.totalTime;
		enemyDisplay.text =""+ info.enemyDeaths;
		allyDisplay.text = ""+info.allyLost;
		objDisplay.text = ""+info.bonusObjectives;
		techDisplay.text = ""+info.techEarned;
		ResourceDisplay.text = ""+info.resourceCollected;
	}

	public class victoryInfo
	{
		public string totalTime;
		public int enemyDeaths;
		public int allyLost;
		public string bonusObjectives;
		public int techEarned;
		public int resourceCollected;



		public victoryInfo(string totalT, int enemyD,  int allyL, int numObj, int totalObj, int techE, int collect)
		{
			totalTime = totalT;
			enemyDeaths =enemyD;
			allyLost = allyL;
			bonusObjectives = numObj + "/" + totalObj;
			techEarned = techE;
			resourceCollected = collect;
		}
	}


}
