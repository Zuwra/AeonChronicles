using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
		if (Time.time > nextActionTime) {
			nextActionTime += .5f;

			foreach (KeyValuePair<string, List<UnitProduction>> pair in abilityList) {
				Transform t = iconList [pair.Key].transform.FindChild ("Percent");
				t.GetComponent<Text>().text = 
					"" +(int)(pair.Value [0].getProgress()*100) + "%";


			}

		}

	}


	public void updateUnits( UnitProduction producer)
	{
		UnitManager manage = producer.GetComponent<UnitManager> ();

		if (abilityList.ContainsKey (manage.UnitName)) {

			Debug.Log ("bulding " + producer);
			abilityList [manage.UnitName].Add (producer);
			StartCoroutine (addNUmber (manage));

		} 
		else {

			List<UnitProduction> list = new List<UnitProduction> ();
			list.Add (producer);
			abilityList.Add (manage.UnitName, list);
			unitCount++;


			StartCoroutine (createIcon (producer));

		}

	}

	IEnumerator addNUmber(UnitManager manage)
	{
		yield return new WaitForSeconds(0);

		iconList [manage.UnitName].transform.FindChild("Text").GetComponent<Text> ().text
		= ""+abilityList [manage.UnitName].Count;
	}




	public void unitLost(UnitProduction produce)
	{UnitManager manage = produce.GetComponent<UnitManager> ();

		abilityList [manage.UnitName].Remove (produce);

		if (abilityList [manage.UnitName].Count == 0) {
			GameObject obj = iconList [manage.UnitName];
			iconList.Remove (manage.UnitName);
			Destroy (obj);
			abilityList.Remove (manage.UnitName);
		} else {
			addNUmber (manage);
		}

	}


	IEnumerator createIcon(UnitProduction produce)
	{		yield return new WaitForSeconds(0);

		UnitManager manage = produce.GetComponent<UnitManager> ();
		UnitStats theStats = manage.gameObject.GetComponent<UnitStats> ();

		GameObject icon = (GameObject)Instantiate (template, unitPanel.transform.position, Quaternion.identity);

		icon.transform.rotation = unitPanel.transform.rotation;
	
		icon.transform.SetParent (unitPanel.transform);
		icon.GetComponent<Image> ().material = produce.iconPic;


		if (!theStats.isUnitType (UnitTypes.UnitTypeTag.structure)) {
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
		iconList.Add (manage.UnitName, icon);

	}



}
