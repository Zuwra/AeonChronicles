using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CampUpgradePurchase : MonoBehaviour {


	public Upgrade myUpgrade;
	public GameObject Costobject;
	public int myCost;
	public bool purchased;

	private CampUpgradeManager manager;
	public CampaignUpgrade.upgradeType myType;
	public List<CampUpgradePurchase> enables = new List<CampUpgradePurchase>();
	// Use this for initialization
	void Start () {
		manager = GameObject.FindObjectOfType<CampUpgradeManager> ();

		if(LevelData.purchasedUpgrades != null && myUpgrade != null){
			foreach (Upgrade up in LevelData.purchasedUpgrades) {
				if (up.GetType () == myUpgrade.GetType ()) {
					psuedoPurchase ();
			
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Initialize()
	{
		
		GetComponent<Button> ().interactable = (myCost <= LevelData.getMoney());

	}

	public void purchase()
	{
		if (myCost <= LevelData.getMoney()) {
			manager.changeMoney (-myCost);
			GetComponent<Image> ().color = Color.green;
			GetComponent<Button> ().interactable = false;
			LevelData.addUpgrade (myUpgrade);

			Costobject.SetActive (false);
			foreach (CampUpgradePurchase up in enables) {
				up.activate ();
			}

			if (myUpgrade) {
				GameObject.FindObjectOfType<TrueUpgradeManager> ().upgradeBought (myUpgrade, myType);
				}
		}
		
	}

	private void psuedoPurchase()
	{
		GetComponent<Image> ().color = Color.green;
		GetComponent<Button> ().interactable = false;
		Costobject.SetActive (false);
		foreach (CampUpgradePurchase up in enables) {
			up.activate ();
		}

		if (myUpgrade) {
			GameObject.FindObjectOfType<TrueUpgradeManager> ().upgradeBought (myUpgrade, myType);
		}

	}


	public void activate()
	{if (!purchased) {
			purchased = true;
			Costobject.GetComponent<Image> ().color = Color.blue;
			GetComponent<Button> ().interactable = true;
			GetComponent<Image> ().color = Color.blue;
		}
	}
}
