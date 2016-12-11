using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrueUpgradeManager : MonoBehaviour {
	public List<AudioClip> buttonPress;
	public AudioSource mySource;

	public List<CampaignUpgrade> CampUpRef;
	public List<CampaignUpgrade.UpgradesPiece> myUpgrades= new List<CampaignUpgrade.UpgradesPiece>();
	// Use this for initialization


	void Start()
	{
		mySource = GetComponent<AudioSource> ();
	}

	public void playSound ()
	{
		mySource.PlayOneShot (buttonPress[Random.Range(0, buttonPress.Count -1)]);

	}

	public void upgradeBought(Upgrade upg, CampaignUpgrade.upgradeType t)
	{
		//Debug.Log ("Upgrade was bought" +upg.name);
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
		foreach (CampaignUpgrade cu in CampUpRef) {
			cu.reInitialize ();
			//Debug.Log ("Reinitializing " + cu.gameObject);
		}
		

	}


}
