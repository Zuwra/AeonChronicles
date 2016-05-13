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

	public List<CampUpgradePurchase> enables = new List<CampUpgradePurchase>();
	// Use this for initialization
	void Start () {
		manager = GameObject.FindObjectOfType<CampUpgradeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Initialize()
	{
		
			GetComponent<Button> ().interactable = (myCost <= manager.creditAmount);

	}

	public void purchase()
	{
		if (myCost <= manager.creditAmount) {
			manager.changeMoney (-myCost);
			GetComponent<Image> ().color = Color.green;
			GetComponent<Button> ().interactable = false;
			Costobject.SetActive (false);
			foreach (CampUpgradePurchase up in enables) {
				up.activate ();
			}
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
