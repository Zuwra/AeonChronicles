using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveManager : MonoBehaviour {


	public Text myText;
	public static ObjectiveManager instance;
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setObjective(string input)
	{

		myText.text = input;

	}
}
