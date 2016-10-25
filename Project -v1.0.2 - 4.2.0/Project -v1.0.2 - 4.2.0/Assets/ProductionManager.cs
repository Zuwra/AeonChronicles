using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class ProductionManager : MonoBehaviour {




	private Dictionary<string,  List<UnitProduction>> abilityList = new Dictionary<string,  List<UnitProduction>> ();
	public GameObject template;
	public GameObject unitPanel;
	private Dictionary<string, GameObject> iconList = new Dictionary<string,GameObject>();
	private int unitCount = 0;

	private float yPosition;

	private float nextActionTime;
	// Use this for initialization
	void Start () {
		nextActionTime = Time.time + .5f;
		yPosition = unitPanel.transform.position.y;


	}

	// Update is called once per frame
	void Update () {
		if (this.gameObject.activeSelf) {
			if (Time.time > nextActionTime) {
				nextActionTime += .5f;

				foreach (KeyValuePair<string, List<UnitProduction>> pair in abilityList) {try{
						
					Transform t = iconList [pair.Key].transform.FindChild ("Percent");

						t.GetComponent<Slider> ().value = pair.Value [0].getProgress ();
							}
					catch(Exception) {
						
						continue;
					}


				}

			}
		}

	}


	public void updateUnits( UnitProduction producer)
	{
		Debug.Log ("Here");
		if (abilityList.ContainsKey (producer.Name)) {

		
			abilityList [producer.Name].Add (producer);

			StartCoroutine (addNUmber (producer, true));


		} 
		else {

			List<UnitProduction> list = new List<UnitProduction> ();
			list.Add (producer);

			abilityList.Add (producer.Name, list);
			unitCount++;


			StartCoroutine (createIcon (producer));


		}

	}

	IEnumerator addNUmber(UnitProduction produce, bool addIt)
	{
		yield return new WaitForSeconds(0);
	
		iconList [produce.Name].transform.FindChild("Text").GetComponent<Text> ().text
		= ""+abilityList [produce.Name].Count;

		if (addIt) {
			iconList [produce.Name].GetComponent<DropDownDudeFinder> ().myProducer.Add (produce.gameObject);
		} else {
			iconList [produce.Name].GetComponent<DropDownDudeFinder> ().myProducer.Remove (produce.gameObject);
		}
	}




	public void unitLost(UnitProduction produce)
	{

		if (abilityList.ContainsKey (produce.Name)) {
			abilityList [produce.Name].Remove (produce);

		
			if (abilityList [produce.Name].Count == 0) {
				if (iconList.ContainsKey (produce.Name)) {
					GameObject obj = iconList [produce.Name];
					iconList.Remove (produce.Name);
					Destroy (obj);
					abilityList.Remove (produce.Name);

				}
			} else {


				StartCoroutine( addNUmber (produce, false));
			}
		}
	}


	IEnumerator createIcon(UnitProduction produce)
	{		
		yield return new WaitForSeconds(0);

	
		UnitManager manage = produce.GetComponent<UnitManager> ();
	
		UnitStats theStats = manage.gameObject.GetComponent<UnitStats> ();

		GameObject icon = (GameObject)Instantiate (template, unitPanel.transform.position, Quaternion.identity);
		if (produce.unitToBuild) {

			icon.transform.FindChild ("ProductionHelp").GetComponentInChildren<Text> ().text = produce.unitToBuild.GetComponent<UnitManager> ().UnitName;
		} else {
			icon.transform.FindChild ("ProductionHelp").GetComponentInChildren<Text> ().text = produce.Name;
		}
		icon.GetComponent<DropDownDudeFinder> ().myProducer.Add (produce.gameObject);
		icon.transform.rotation = unitPanel.transform.rotation;
	
		icon.transform.SetParent (unitPanel.transform);
		icon.GetComponent<Image> ().sprite= produce.iconPic;


		if (!theStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			icon.transform.SetAsFirstSibling ();
		}
		RectTransform trans = unitPanel.GetComponent<RectTransform> ();

		if (iconList.Count > 8) {
			trans.sizeDelta = new Vector2(trans.rect.width, 135);
			unitPanel.transform.position = new Vector3 (unitPanel.transform.position.x, yPosition -20, unitPanel.transform.position.z);

		} else if (iconList.Count > 12) {
			trans.sizeDelta = new Vector2(trans.rect.width, 180);
			unitPanel.transform.position = new Vector3 (unitPanel.transform.position.x, yPosition - 40, unitPanel.transform.position.z);
		
		} else {
			trans.sizeDelta = new Vector2(trans.rect.width, 95);
			unitPanel.transform.position = new Vector3 (unitPanel.transform.position.x, unitPanel.transform.position.y, unitPanel.transform.position.z);

		}
		icon.transform.localScale = unitPanel.transform.localScale;
		iconList.Add (produce.Name, icon);

	}



}
