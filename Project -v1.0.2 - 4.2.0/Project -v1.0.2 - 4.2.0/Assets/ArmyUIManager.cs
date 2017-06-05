using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ArmyUIManager : MonoBehaviour {


	private Dictionary<string, List<GameObject>> unitList = new Dictionary<string, List<GameObject>>();

	public GameObject template;
	public GameObject unitPanel;
	private Dictionary<string, GameObject> iconList = new Dictionary<string,GameObject>();
	private int unitCount = 0;

	public bool Buildings;
	float startingY;
	RectTransform trans;
	void Start()
	{trans = unitPanel.GetComponent<RectTransform> ();
		startingY = trans.anchoredPosition.y;
	
	}

	public void updateUnits(UnitManager manage)
		{
		//UnitManager manage = unit.GetComponent<UnitManager> ();
		if (!Buildings) {
			if (manage.myStats.otherTags.Contains (UnitTypes.UnitTypeTag.Structure)) {
				return; // I don't display building but this guy is a building
			}
		
		} else if (!manage.myStats.otherTags.Contains (UnitTypes.UnitTypeTag.Structure)) {
			return; // Displays buildings but it isn't one
		}
			

			

		if (!unitList.ContainsKey (manage.UnitName)) {
			unitList.Add (manage.UnitName, new List<GameObject> ());
			unitCount++;
			if (this.gameObject.activeSelf) {
				StartCoroutine (createIcon (manage));
			}
		}
		unitList [manage.UnitName].Add (manage.gameObject);

		if (this.gameObject.activeSelf) {
				StartCoroutine (addNUmber (manage,true));
		} 
	}

	IEnumerator addNUmber(UnitManager manage, bool addIt)
	{
		yield return new WaitForSeconds(0);
		try{
		iconList [manage.UnitName].transform.Find("Text").GetComponent<Text> ().text
		= ""+unitList [manage.UnitName].Count;

			if (addIt) {
				iconList [manage.UnitName].GetComponent<DropDownDudeFinder> ().myProducer.Add (manage.gameObject);
			} 

		}catch(KeyNotFoundException){
		
		}
	}




	public void unitLost(UnitManager  manage)
	{if (!manage) {
			return;}
		

		if (!Buildings) {
			if (manage.myStats.otherTags.Contains (UnitTypes.UnitTypeTag.Structure)) {
				return; // I don't display building but this guy is a building
			}

		} else if (!manage.myStats.otherTags.Contains (UnitTypes.UnitTypeTag.Structure)) {
			return; // Displays buildings but it isn't one
		}

		if (unitList.ContainsKey (manage.UnitName)) {

			unitList [manage.UnitName].Remove (manage.gameObject);

			//if (!unitList.ContainsKey(manage.UnitName)) {
			if(unitList [manage.UnitName].Count == 0){
				//Debug.Log ("Removing icon " + manage.UnitName + "  ");
					GameObject obj = iconList [manage.UnitName];
					iconList.Remove (manage.UnitName);
					Destroy (obj);



					unitList.Remove (manage.UnitName);

				resetSize ();

			} else {
				if (this.gameObject.activeSelf) {
					StartCoroutine (addNUmber (manage, false));
				}
			}
		}
	}

	public void resetSize()
	{
		int rowCount = (iconList.Count) / 3;
		trans.sizeDelta = new Vector2(trans.rect.width, 45 + (46 * rowCount));
		trans.anchoredPosition = new Vector2( trans.anchoredPosition.x, startingY - 23 * rowCount);
	}


	IEnumerator createIcon(UnitManager manage)
	{		yield return new WaitForSeconds(0);
		
	
		GameObject icon = (GameObject)Instantiate (template, unitPanel.transform.position, Quaternion.identity);
		icon.transform.Find ("ProductionHelp").GetComponentInChildren<Text> ().text = manage.UnitName;
		icon.GetComponent<DropDownDudeFinder> ().myProducer.Add (manage.gameObject);
		icon.transform.rotation = unitPanel.transform.rotation;

		icon.transform.SetParent (unitPanel.transform);
	

		icon.transform.SetParent (unitPanel.transform);
		icon.GetComponent<Image> ().sprite = manage.myStats.Icon;
		if (!manage.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			icon.transform.SetAsFirstSibling ();
		}
		resetSize ();

		icon.transform.localScale = unitPanel.transform.localScale;
		//Debug.Log ("Adding icon " + manage.UnitName + "  " + icon);
		iconList.Add (manage.UnitName, icon);

	}


}
