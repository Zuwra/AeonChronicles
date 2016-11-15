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
	{
			LevelData.setUltLevel (ultNumber, level);
	

	}

	public override void applyUpgrade (GameObject obj){



	}


	public override void unApplyUpgrade (GameObject obj)
	{
		

	}


}
