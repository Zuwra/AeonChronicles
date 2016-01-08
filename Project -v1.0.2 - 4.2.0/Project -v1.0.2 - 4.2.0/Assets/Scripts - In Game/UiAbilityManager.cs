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


	public void loadUI(Page uiPage)
	{resetUI ();
		int n = 0;
		for(int j = 0; j < 3; j ++){
			if (uiPage.rows [j] == null) {
				continue;
			}
	
			n = uiPage.rows[j][0].AbilityStartingRow;
		
			int AbilityX = 0;

			UnitManager man = uiPage.rows[j][0].gameObject.GetComponent<UnitManager> ();


			Stats[n].GetComponent<StatsUI> ().loadUnit (uiPage.rows[j][0].gameObject.GetComponent<UnitStats> (), uiPage.rows[j][0].gameObject.GetComponent<IWeapon> (), 
			uiPage.rows[j].Count, man.UnitName);
			
				for (int i = 0; i < (man.abilityList.Count / 4) + 1; i++) {
				//sets up the exact position of the buttons
					GameObject template = (GameObject)Instantiate (UITemplate, this.gameObject.transform.position, Quaternion.identity);
					myTemplates.Add (template);
					template.transform.SetParent(this.gameObject.transform.FindChild ("TopLeftPanel"));
					template.transform.position = this.gameObject.transform.FindChild ("TopLeftPanel").position;
					float yTotal = template.GetComponent<RectTransform> ().position.y + 3 - n * 52;
					float xTotal = template.GetComponent<RectTransform> ().position.x - 50;
					Vector3 location = new Vector3 (xTotal, yTotal, 0);
					template.GetComponent<RectTransform> ().position = location;

				//deletes buttons if they shouldn't exist or sets their images and hotkeys
					if (man.abilityList.Count <= AbilityX * 4 || man.abilityList [0 + AbilityX * 4] == null) {
						Destroy (template.transform.FindChild ("QButton").gameObject);
					} else {
						template.transform.FindChild ("QButton").GetComponent<Image> ().material = man.abilityList [0].iconPic;
						template.transform.FindChild ("QButton").FindChild ("Text").GetComponent<Text> ().text = letters [n] [0];

					}

					if (man.abilityList.Count <= 1 + AbilityX * 4 || man.abilityList [1 + AbilityX * 4] == null) {
						Destroy (template.transform.FindChild ("WButton").gameObject);
					} else {
						template.transform.FindChild ("WButton").GetComponent<Image> ().material = man.abilityList [1].iconPic;
						template.transform.FindChild ("WButton").FindChild ("Text").GetComponent<Text> ().text = letters [n] [1];
					}

					if (man.abilityList.Count <= 2 + AbilityX * 4 || man.abilityList [2 + AbilityX * 4] == null) {
						Destroy (template.transform.FindChild ("EButton").gameObject);
					} else {
						template.transform.FindChild ("EButton").GetComponent<Image> ().material = man.abilityList [2].iconPic;
						template.transform.FindChild ("EButton").FindChild ("Text").GetComponent<Text> ().text = letters [n] [2];
					}

					if (man.abilityList.Count <= 3 + AbilityX * 4 || man.abilityList [3 + AbilityX * 4] == null) {
						Destroy (template.transform.FindChild ("RButton").gameObject);
					} else {
						template.transform.FindChild ("RButton").GetComponent<Image> ().material = man.abilityList [3].iconPic;
						template.transform.FindChild ("RButton").FindChild ("Text").GetComponent<Text> ().text = letters [n] [3];
					}
					AbilityX++;
					n++;
				}



			}

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
				//loadUI(temp);
			}
			rowCounter ++;
		}


	}


}
