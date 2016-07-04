using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour {



	public GameObject ObjectiveText;

	private Dictionary<Objective, GameObject> mainObjectives = new Dictionary<Objective, GameObject> ();
	private Dictionary<Objective, GameObject> bonusObjectives = new Dictionary<Objective, GameObject> ();

	public Text bonusTitle;


	public static ObjectiveManager instance;

	//private float turnOffTime;
	//private bool blinking;

	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {

		//if (blinking) {
			//if (Time.time > turnOffTime) {
			
			//}
		//}
	
	}


	public void blink(bool input)
	{
		//if (input) {
			
		//}
		//blinking = input;
		//if (input) {
		//	blinking = true;}

	}

	public void setBonusObjectives(Objective input)
	{bonusTitle.gameObject.SetActive (true);
		GameObject obj = (GameObject)Instantiate (ObjectiveText);
		obj.transform.SetParent (this.transform);
		obj.GetComponentInChildren<Text> ().text ="" +  input.description;
		bonusObjectives.Add (input, obj);
		Vector3 pos = obj.transform.position;
		pos.z = 0;
		obj.transform.position = pos;
		bonusTitle.transform.SetSiblingIndex (mainObjectives.Count + bonusObjectives.Count+1);
		obj.transform.SetSiblingIndex (mainObjectives.Count + bonusObjectives.Count+1);
		blink (true);
		UIHighLight.main.highLight (bonusTitle.gameObject, 2);

	}

	public void setObjective(Objective input)
	{

		GameObject obj = (GameObject)Instantiate (ObjectiveText);
		obj.transform.SetParent (this.transform);
		Vector3 pos = obj.transform.position;
		pos.z = 0;
		obj.transform.position = pos;
		obj.GetComponentInChildren<Text> ().text = "" + input.description;
		mainObjectives.Add (input, obj);
		obj.transform.SetSiblingIndex (mainObjectives.Count);
		blink (false);

		UIHighLight.main.highLight (obj, 2);

	}

	public void completeBonus(Objective obj)
		{
		mainObjectives [obj].GetComponentInChildren<Toggle> ().isOn = true;
		blink (true);
	}

	public void completeMain(Objective obj)
		{
		bonusObjectives [obj].GetComponentInChildren<Toggle> ().isOn = true;
		blink (true);
	}

}
