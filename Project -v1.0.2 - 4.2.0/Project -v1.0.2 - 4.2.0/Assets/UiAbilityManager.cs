using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class UiAbilityManager : MonoBehaviour {



	public GameObject UITemplate;


	List<GameObject> myTemplates = new List<GameObject>();

	private int currentX;
	private int currentY;
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
	}


	public void addUnit(GameObject obj)
	{UnitManager man = obj.GetComponent<UnitManager> ();
							 
		GameObject template = (GameObject)Instantiate(UITemplate, this.gameObject.transform.position, Quaternion.identity );
		myTemplates.Add (template);
		template.transform.parent = this.gameObject.transform.FindChild("Panel");
		template.transform.position = this.gameObject.transform.FindChild ("Panel").position;
		float yTotal = template.GetComponent<RectTransform> ().position.y + 55;
		float xTotal = template.GetComponent<RectTransform> ().position.x - 70;
		Vector3 location = new Vector3 (xTotal, yTotal, 0);
		template.GetComponent<RectTransform> ().position = location;


		template.GetComponent<Text>().text = obj.name;


		if (man.QAbility == null) {
			Destroy (template.transform.FindChild ("QButton").gameObject);
		} else {
			template.transform.FindChild ("QButton").FindChild("Text").GetComponent<Text>().text = man.QAbility.Name + " (Q)";

		}

		if (man.WAbility == null) {
			Destroy (template.transform.FindChild ("WButton").gameObject);
		} else {
			template.transform.FindChild ("WButton").FindChild("Text").GetComponent<Text>().text = man.WAbility.Name+ " (W)";
		}

		if (man.EAbility == null) {
			Destroy (template.transform.FindChild ("EButton").gameObject);
		} else {
			template.transform.FindChild ("EButton").FindChild("Text").GetComponent<Text>().text = man.EAbility.Name+ " (E)";
		}

		if (man.RAbility == null) {
			Destroy (template.transform.FindChild ("RButton").gameObject);
		} else {
			template.transform.FindChild ("RButton").FindChild("Text").GetComponent<Text>().text = man.RAbility.Name+ " (R)";
		}

	}
	// these are seperate methods  from the update key input so the buttons can also call the same functions.





}
