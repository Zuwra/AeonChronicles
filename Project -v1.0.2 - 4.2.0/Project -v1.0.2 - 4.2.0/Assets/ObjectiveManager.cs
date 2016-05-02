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
	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setBonusObjectives(Objective input)
	{bonusTitle.gameObject.SetActive (true);
		GameObject obj = (GameObject)Instantiate (ObjectiveText);
		obj.transform.SetParent (this.transform);
		obj.GetComponentInChildren<Text> ().text ="  " +  input.description;
		bonusObjectives.Add (input, obj);
		Vector3 pos = obj.transform.position;
		pos.z = 0;
		obj.transform.position = pos;
		obj.transform.SetSiblingIndex (mainObjectives.Count + bonusObjectives.Count+1);
	

	}

	public void setObjective(Objective input)
	{

		GameObject obj = (GameObject)Instantiate (ObjectiveText);
		obj.transform.SetParent (this.transform);
		Vector3 pos = obj.transform.position;
		pos.z = 0;
		obj.transform.position = pos;
		obj.GetComponentInChildren<Text> ().text = "  " + input.description;
		mainObjectives.Add (input, obj);
		obj.transform.SetSiblingIndex (mainObjectives.Count);

	}

	public void completeBonus(Objective obj)
		{
		mainObjectives [obj].GetComponentInChildren<Toggle> ().isOn = true;
	}

	public void completeMain(Objective obj)
		{
		bonusObjectives [obj].GetComponentInChildren<Toggle> ().isOn = true;
	}

}
