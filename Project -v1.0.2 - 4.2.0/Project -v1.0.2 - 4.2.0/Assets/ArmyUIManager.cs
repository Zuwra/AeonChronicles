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


	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void updateUnits(GameObject unit)
		{
		UnitManager manage = unit.GetComponent<UnitManager> ();

		if (unitList.ContainsKey (manage.UnitName)) {
			
			unitList [manage.UnitName].Add (unit);
			if (this.gameObject.activeSelf) {
				StartCoroutine (addNUmber (manage));
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

	IEnumerator addNUmber(UnitManager manage)
	{
		yield return new WaitForSeconds(0);

		iconList [manage.UnitName].transform.FindChild("Text").GetComponent<Text> ().text
		= ""+unitList [manage.UnitName].Count;
	}




	public void unitLost(GameObject unit)
	{UnitManager manage = unit.GetComponent<UnitManager> ();


		if (unitList.ContainsKey (manage.UnitName)) {

			unitList [manage.UnitName].Remove (unit);

			if (unitList [manage.UnitName].Count == 0) {
	
				GameObject obj = iconList [manage.UnitName];
				iconList.Remove (manage.UnitName);
				Destroy (obj);
				unitList.Remove (manage.UnitName);
			} else {
				if (this.gameObject.activeSelf) {
					StartCoroutine (addNUmber (manage));
				}
			}
		}
	}


	IEnumerator createIcon(GameObject unit)
	{		yield return new WaitForSeconds(0);
		
		UnitManager manage = unit.GetComponent<UnitManager> ();

		GameObject icon = (GameObject)Instantiate (template, unitPanel.transform.position, Quaternion.identity);

		icon.transform.rotation = unitPanel.transform.rotation;

		icon.transform.SetParent (unitPanel.transform);
	

		icon.transform.SetParent (unitPanel.transform);
		icon.GetComponent<Image> ().sprite = manage.myStats.Icon;
		if (!manage.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			icon.transform.SetAsFirstSibling ();
		}
		/*
		RectTransform trans = unitPanel.GetComponent<RectTransform> ();

		if (iconList.Count > 8) {
			trans.sizeDelta = new Vector2(trans.rect.width, 135);
			//unitPanel.transform.Translate (0, -20, 0);
			//unitPanel.transform.position = new Vector3 (unitPanel.transform.position.x, (yPosition -20) * unitPanel.transform.localScale.x, unitPanel.transform.position.z);
			//trans.localPosition = new Vector3 (trans.position.x, trans.position.y, trans.position.z);
		} else if (iconList.Count > 12) {
			trans.sizeDelta = new Vector2(trans.rect.width, 180);
			//unitPanel.transform.Translate (0, -40, 0);
			//unitPanel.transform.position = new Vector3 (unitPanel.transform.position.x, (yPosition - 40) * unitPanel.transform.localScale.x, unitPanel.transform.position.z);
			//trans.localPosition = new Vector3 (trans.position.x, trans.position.y, trans.position.z);
		} else {
			trans.sizeDelta = new Vector2(trans.rect.width, 95);
			//unitPanel.transform.position = new Vector3 (unitPanel.transform.position.x, unitPanel.transform.position.y, unitPanel.transform.position.z);
			//trans.localPosition = new Vector3 (trans.position.x, trans.position.y, trans.position.z);
		}
*/
		icon.transform.localScale = unitPanel.transform.localScale;
		iconList.Add (manage.UnitName, icon);

	}











}
