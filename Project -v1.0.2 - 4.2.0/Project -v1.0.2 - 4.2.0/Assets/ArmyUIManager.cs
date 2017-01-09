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

	float startingY;
	RectTransform trans;
	void Start()
	{trans = unitPanel.GetComponent<RectTransform> ();
		startingY = trans.anchoredPosition.y;
	
	}

	public void updateUnits(GameObject unit)
		{
		UnitManager manage = unit.GetComponent<UnitManager> ();

		if (unitList.ContainsKey (manage.UnitName)) {
			
			unitList [manage.UnitName].Add (unit);
			if (this.gameObject.activeSelf) {
				StartCoroutine (addNUmber (manage,true));
			}
		} 
		else {
			
				List<GameObject> list = new List<GameObject> ();
				list.Add (unit);
				unitList.Add (manage.UnitName, list);
				unitCount++;

			if (this.gameObject.activeSelf) {
				StartCoroutine (createIcon (unit));
			}
		
			}

	}

	IEnumerator addNUmber(UnitManager manage, bool addIt)
	{
		yield return new WaitForSeconds(0);
		try{
		iconList [manage.UnitName].transform.FindChild("Text").GetComponent<Text> ().text
		= ""+unitList [manage.UnitName].Count;

			if (addIt) {
				iconList [manage.UnitName].GetComponent<DropDownDudeFinder> ().myProducer.Add (manage.gameObject);
			} 

		}catch(KeyNotFoundException){
		
		}
	}




	public void unitLost(GameObject unit)
	{UnitManager manage = unit.GetComponent<UnitManager> ();


		if (unitList.ContainsKey (manage.UnitName)) {

			unitList [manage.UnitName].Remove (unit);

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


	IEnumerator createIcon(GameObject unit)
	{		yield return new WaitForSeconds(0);
		
		UnitManager manage = unit.GetComponent<UnitManager> ();

		GameObject icon = (GameObject)Instantiate (template, unitPanel.transform.position, Quaternion.identity);
		icon.transform.FindChild ("ProductionHelp").GetComponentInChildren<Text> ().text = manage.UnitName;
		icon.GetComponent<DropDownDudeFinder> ().myProducer.Add (unit);
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
