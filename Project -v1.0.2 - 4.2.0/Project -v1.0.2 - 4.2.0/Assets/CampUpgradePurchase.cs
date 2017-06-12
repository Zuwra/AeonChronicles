using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CampUpgradePurchase : MonoBehaviour {


	public SpecificUpgrade myUpgrade;
	public GameObject Costobject;
	public int myCost;
	public bool purchased;

	private LevelManager manager;
	public CampaignUpgrade.upgradeType myType;
	public List<CampUpgradePurchase> enables = new List<CampUpgradePurchase>();
	// Use this for initialization
	void Start () {
		StartCoroutine (DelayedStarted ());
	}

	IEnumerator DelayedStarted()
	{

		yield return null;
		manager = GameObject.FindObjectOfType<LevelManager> ();

		if( myUpgrade != null){

			foreach (string up in LevelData.getsaveInfo().purchasedUpgrades) {
				//Debug.Log ("Checking " + up + "   " + myUpgrade.GetType());
				if (up == myUpgrade.Name) {
					psuedoPurchase ();

				}
			}
		}
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
		purchased = true;
		foreach (CampUpgradePurchase up in enables) {
			up.activate ();
		}

		if (myUpgrade) {
			
			GameObject.FindObjectOfType<TrueUpgradeManager> ().upgradeBought (myUpgrade, myType);
		}

		GetComponent<Image> ().color = Color.green;
		GetComponent<Button> ().interactable = false;
		Costobject.SetActive (false);

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
