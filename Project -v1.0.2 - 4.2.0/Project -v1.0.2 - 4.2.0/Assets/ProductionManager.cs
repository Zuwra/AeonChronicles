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

	float startingY;
	RectTransform trans;
	void Start()
	{
		nextActionTime = Time.time + .5f;
		trans = unitPanel.GetComponent<RectTransform> ();
		startingY = trans.anchoredPosition.y;

	}
		

	// Update is called once per frame
	void Update () {
		if (this.gameObject.activeSelf) {
			if (Time.time > nextActionTime) {
				nextActionTime += .5f;

				foreach (KeyValuePair<string, List<UnitProduction>> pair in abilityList) {try{
						
					Transform t = iconList [pair.Key].transform.Find ("Percent");

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
		try{
		iconList [produce.Name].transform.Find("Text").GetComponent<Text> ().text
		= ""+abilityList [produce.Name].Count;
		}
		catch{
		}
		if (iconList [produce.Name]) {
			if (addIt) {
				iconList [produce.Name].GetComponent<DropDownDudeFinder> ().myProducer.Add (produce.gameObject);
			} else {
				iconList [produce.Name].GetComponent<DropDownDudeFinder> ().myProducer.Remove (produce.gameObject);
			}
		}// TO DO FIX THIS IN RARE CASES WHERE NOTHING IS IN THERE
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
					resetSize ();

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

			icon.transform.Find ("ProductionHelp").GetComponentInChildren<Text> ().text = produce.unitToBuild.GetComponent<UnitManager> ().UnitName;
		} else {
			icon.transform.Find ("ProductionHelp").GetComponentInChildren<Text> ().text = produce.Name;
		}
		icon.GetComponent<DropDownDudeFinder> ().myProducer.Add (produce.gameObject);
		icon.transform.rotation = unitPanel.transform.rotation;
	
		icon.transform.SetParent (unitPanel.transform);
		icon.GetComponent<Image> ().sprite= produce.iconPic;


		if (!theStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
			icon.transform.SetAsFirstSibling ();
		}
		resetSize ();
		icon.transform.localScale = unitPanel.transform.localScale;
		iconList.Add (produce.Name, icon);

	}
	public void resetSize()
	{
		int rowCount = (iconList.Count) / 3;
		trans.sizeDelta = new Vector2(trans.rect.width, 45 + (46 * rowCount));
		trans.anchoredPosition = new Vector2( trans.anchoredPosition.x, startingY - 23 * rowCount);
	}



}
