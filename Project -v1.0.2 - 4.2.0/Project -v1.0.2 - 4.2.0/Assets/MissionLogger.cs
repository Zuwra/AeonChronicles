using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionLogger : MonoBehaviour {



	public Text myText;
	public static MissionLogger instance;
	// Use this for initialization
	void Start () {
		instance = this;
	}
	


	public void AddLog(string s)
	{
		string n = myText.text;
		n += "\n";
		n += s;
		myText.text = n;

	}
}
