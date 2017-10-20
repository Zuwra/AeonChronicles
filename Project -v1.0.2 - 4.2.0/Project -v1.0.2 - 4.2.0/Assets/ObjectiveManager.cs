using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectiveManager : MonoBehaviour {



	public GameObject ObjectiveText;

	private Dictionary<Objective, GameObject> mainObjectives = new Dictionary<Objective, GameObject> ();
	private Dictionary<Objective, GameObject> bonusObjectives = new Dictionary<Objective, GameObject> ();

	public Text bonusTitle;
	public Text mainTitle;
	public GameObject highlighter;

	public static ObjectiveManager instance;

	//private float turnOffTime;
	//private bool blinking;

	// Use this for initialization
	void Awake () {
		instance = this;
		mainTitle.gameObject.SetActive (false);
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
		GameObject obj = (GameObject)Instantiate (ObjectiveText, this.transform);
		highlighter.SetActive (true);
		obj.GetComponentInChildren<Text> ().text ="" +  input.description;

		if (!bonusObjectives.ContainsKey (input)) {
			bonusObjectives.Add (input, obj);
		}
		Vector3 pos = obj.transform.position;
		pos.z = 0;
		obj.transform.position = pos;
		bonusTitle.transform.SetSiblingIndex (mainObjectives.Count + bonusObjectives.Count+1);
		obj.transform.SetSiblingIndex (mainObjectives.Count + bonusObjectives.Count+1);
		blink (true);
		obj.transform.localScale = new Vector3 (1, 1, 1);
		UIHighLight.main.highLight (bonusTitle.gameObject, 2);

	}

	public void setObjective(Objective input)
	{//Debug.Log ("adding objective");
		mainTitle.gameObject.SetActive (true);
		highlighter.SetActive (true);
		GameObject obj = (GameObject)Instantiate (ObjectiveText, this.transform);

		obj.transform.SetAsFirstSibling ();
		Vector3 pos = obj.transform.position;
		pos.z = 0;
		obj.transform.position = pos;
		obj.GetComponentInChildren<Text> ().text = "" + input.description;
		if (!mainObjectives.ContainsKey (input)) {
			mainObjectives.Add (input, obj);
			obj.transform.SetSiblingIndex (mainObjectives.Count);
			obj.transform.localScale = new Vector3 (1, 1, 1);
			blink (false);

			UIHighLight.main.highLight (obj, 2);
		}
		obj.transform.SetAsFirstSibling ();

	}

	public void updateObjective(Objective obj)
	{try{
		bonusObjectives [obj].GetComponentInChildren<Text> ().text =  "" + obj.description;
		}
		catch(Exception ){
			mainObjectives[obj].GetComponentInChildren<Text> ().text =  "" + obj.description;
		}
	}

	public bool hasObjective(Objective obj)
	{
		return (bonusObjectives.ContainsKey (obj) || mainObjectives.ContainsKey (obj));
			
	}



	public void completeBonus(Objective obj)
		{
		bonusObjectives [obj].GetComponentInChildren<Toggle> ().isOn = true;
		bonusObjectives [obj].GetComponentInChildren<Text> ().fontSize = 7;
		bonusObjectives [obj].GetComponentInChildren<Text> ().color = new Color (.6f,1,.74f,.5f);
		blink (true);
	}


	public void unCompleteBonus(Objective obj)
	{
		

		bonusObjectives [obj].GetComponentInChildren<Toggle> ().isOn = false;
		bonusObjectives [obj].GetComponentInChildren<Text> ().fontSize = 12;
		bonusObjectives [obj].GetComponentInChildren<Text> ().color = Color.green;
		blink (true);
	}


	public void failObjective(Objective obj)
	{

		bonusObjectives [obj].GetComponentInChildren<Toggle> ().isOn = false;
		bonusObjectives [obj].GetComponentInChildren<Text> ().fontSize = 7;
		bonusObjectives [obj].GetComponentInChildren<Text> ().color = Color.red;

	}



	public void completeMain(Objective obj)
		{
		mainObjectives [obj].GetComponentInChildren<Toggle> ().isOn = true;
		mainObjectives [obj].GetComponentInChildren<Text> ().fontSize = 7;
		mainObjectives [obj].GetComponentInChildren<Text> ().color = new Color (.6f,1,.74f,.5f);
		blink (true);
	}


	public void UnCompleteMain(Objective obj)
	{
		mainObjectives [obj].GetComponentInChildren<Toggle> ().isOn =false;
		mainObjectives [obj].GetComponentInChildren<Text> ().fontSize = 12;
		mainObjectives [obj].GetComponentInChildren<Text> ().color = Color.green;
		blink (true);
	}



}
