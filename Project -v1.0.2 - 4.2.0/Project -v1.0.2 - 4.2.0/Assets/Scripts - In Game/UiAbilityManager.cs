using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
public class UiAbilityManager : MonoBehaviour {



	public GameObject UITemplate;


	List<GameObject> myTemplates = new List<GameObject>();

	private int currentX;
	private int currentY;
	private string[][] letters = new string[][]{ new string[]{" Q", "W"," E"," R"},
												   new string[]{" A"," S"," D"," F"}, 
													new string[]{" Z"," X"," C"," V"}};

	public List<GameObject> Stats = new List<GameObject> ();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {




	
	}


	public void resetUI()
	{
		foreach (GameObject obj in myTemplates) {
			Destroy(obj);
		}
		foreach (GameObject obj in Stats) {
			obj.GetComponent<StatsUI> ().clear ();
		}
	}


	private void loadUI(List<RTSObject> list, int col)
	{
		UnitManager man = list[0].gameObject.GetComponent<UnitManager> ();

		GameObject template = (GameObject)Instantiate(UITemplate, this.gameObject.transform.position, Quaternion.identity );
		myTemplates.Add (template);
		template.transform.parent = this.gameObject.transform.FindChild("Panel");
		template.transform.position = this.gameObject.transform.FindChild ("Panel").position;
		float yTotal = template.GetComponent<RectTransform> ().position.y + 95 - col*47;
		float xTotal = template.GetComponent<RectTransform> ().position.x - 70;
		Vector3 location = new Vector3 (xTotal, yTotal, 0);
		template.GetComponent<RectTransform> ().position = location;
		
		
		template.GetComponent<Text>().text = list[0].gameObject.name;
		
		
		if (man.QAbility == null) {
			Destroy (template.transform.FindChild ("QButton").gameObject);
		} else {
			template.transform.FindChild ("QButton").FindChild("Text").GetComponent<Text>().text = man.QAbility.Name + letters[col -1][0];
			
		}
		
		if (man.WAbility == null) {
			Destroy (template.transform.FindChild ("WButton").gameObject);
		} else {
			template.transform.FindChild ("WButton").FindChild("Text").GetComponent<Text>().text = man.WAbility.Name+  letters[col -1][1];
		}
		
		if (man.EAbility == null) {
			Destroy (template.transform.FindChild ("EButton").gameObject);
		} else {
			template.transform.FindChild ("EButton").FindChild("Text").GetComponent<Text>().text = man.EAbility.Name+  letters[col -1][2];
		}
		
		if (man.RAbility == null) {
			Destroy (template.transform.FindChild ("RButton").gameObject);
		} else {
			template.transform.FindChild ("RButton").FindChild("Text").GetComponent<Text>().text = man.RAbility.Name+  letters[col -1][3];
		}
		
		Stats[col -1].GetComponent<StatsUI> ().loadUnit (list[0].gameObject.GetComponent<UnitStats> (), list[0].gameObject.GetComponent<IWeapon> (), list.Count);




	}

	// this really needs to be optimized
	public void addUnit(List<RTSObject> list)
	{

		List<string> usedUnits = new List<string> ();
		int rowCounter = 1;

		foreach(RTSObject first in list){

	
			string name = "";
			List<RTSObject> temp = new List<RTSObject>();
			int maxPriority = -1;

			foreach (RTSObject entry in list) {

				UnitManager manage = entry.gameObject.GetComponent<UnitManager>();

				if(!usedUnits.Contains(manage.UnitName)){

				if (manage.UnitName == name)
					{
					temp.Add(entry);
				}
				else{
				
					if(manage.AbilityPriority > maxPriority)
						{temp.Clear();
						temp.Add(entry);
						maxPriority = (int)manage.AbilityPriority;
						name = manage.UnitName;
					

					}
					}
				}
			
			}
			usedUnits.Add(name);
			//only load the first three rows.
			if(rowCounter <4 && temp.Count > 0){
				loadUI(temp, rowCounter);}
			rowCounter ++;
		}


	}


}
