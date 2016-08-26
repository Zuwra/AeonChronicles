using UnityEngine;
using System.Collections;

public class CampaignUltUpgrade : Upgrade {

	public int level;
	public int ultNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void buyUlt()
	{if(ultNumber ==1){
			LevelData.UltOneLevel = level;
			}
		else if(ultNumber ==2){
			LevelData.UltTwoLevel = level;
		}
		else if(ultNumber ==3){
			LevelData.UltThreeLevel = level;
		}
		else if(ultNumber ==4){
			LevelData.UltFourLevel = level;
		}

	}

	public override void applyUpgrade (GameObject obj){



	}


	public override void unApplyUpgrade (GameObject obj)
	{
		

	}


}
