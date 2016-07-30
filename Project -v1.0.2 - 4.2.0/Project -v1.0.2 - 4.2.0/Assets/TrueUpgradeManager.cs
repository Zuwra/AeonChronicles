using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrueUpgradeManager : MonoBehaviour {
	public List<CampaignUpgrade.UpgradesPiece> myUpgrades= new List<CampaignUpgrade.UpgradesPiece>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void upgradeBought(Upgrade upg, CampaignUpgrade.upgradeType t)
	{
		CampaignUpgrade.UpgradesPiece cpu = new CampaignUpgrade.UpgradesPiece ();
		cpu.pointer = upg;
		cpu.name = upg.Name;
		cpu.description = upg.Descripton;
		cpu.unlocked = true;
		cpu.pic = upg.iconPic;
		cpu.myType = t;


		myUpgrades.Add (cpu);
	/*	for (int i = 0; i < myUpgrades.Count; i++) {
			Debug.Log ("checking " + myUpgrades [i].pointer + "   " + upg);
			if (myUpgrades[i].pointer &&  myUpgrades [i].pointer.Equals(upg)) {
				myUpgrades [i].unlock ();

			}
			//Debug.Log ("It is now " + myUpgrades[i].unlocked);
		}
*/
		foreach (CampaignUpgrade cu in GameObject.FindObjectsOfType<CampaignUpgrade>()) {
			cu.reInitialize ();
		}
		

	}


}
