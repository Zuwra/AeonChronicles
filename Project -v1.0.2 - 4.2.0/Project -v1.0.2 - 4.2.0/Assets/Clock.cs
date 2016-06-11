using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Clock : MonoBehaviour {



	public Text clockText;

	public static Clock main;
	// Use this for initialization
	void Start () {
		main = this;
	}
	
	// Update is called once per frame
	void Update () {
		int time = (int)Time.time;
		int minutes = time / 60;
		int seconds = time % 60;


		if (seconds < 10) {
			clockText.text = minutes + ":0" + seconds;
		} else {
			clockText.text = minutes + ":" + seconds;
		}

	
	}

	public string getTime()
	{int time = (int)Time.time;
		int minutes = time / 60;
		int seconds = time % 60;
		string s;

		if (seconds < 10) {
			s = minutes + ":0" + seconds;
		} else {
			s = minutes + ":" + seconds;
		}

		return s;
	}


}
