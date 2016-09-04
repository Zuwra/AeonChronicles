using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Clock : MonoBehaviour {



	public Text clockText;

	public static Clock main;
	private float timer = 0;
	private float nextActionTime;
	// Use this for initialization

	void Awake() {
		nextActionTime = Time.time;
		timer = 0;
		main = this;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Time.time > nextActionTime) {
			nextActionTime += 1;
			int time = (int)timer;
			int minutes = time / 60;
			int seconds = time % 60;


			if (seconds < 10) {
				clockText.text = minutes + ":0" + seconds;
			} else {
				clockText.text = minutes + ":" + seconds;
			}

		}
	}

	public float getTotalSecond()
	{return ((int)timer);
	}

	public string getTime()
	{int time = (int)timer;
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

	public static string convertToString(float toCon)
	{int time = (int) toCon;
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
