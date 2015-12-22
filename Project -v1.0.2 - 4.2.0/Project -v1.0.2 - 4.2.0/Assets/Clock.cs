using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Clock : MonoBehaviour {



	public Text clockText;


	// Use this for initialization
	void Start () {
	
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
}
