using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpositionDisplayer : MonoBehaviour {


	private Canvas myCanvas;
	private Text myText;
	public static ExpositionDisplayer instance;

	private float turnOffTime;
	private AudioSource myAudio;

	// Use this for initialization
	void Start () {
		myText = GetComponentInChildren<Text> ();
		instance = this;
		myCanvas = GetComponent<Canvas> ();
		myCanvas.enabled = false;
		myAudio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		if (Time.time > turnOffTime) {

			this.enabled = false;
			myCanvas.enabled = false;
		}
	}

	public void displayText(string input, float duration, AudioClip sound, float volume)
	{	this.enabled = true;
		myText.text = input;
		myCanvas.enabled = true;
		turnOffTime = Time.time + duration;
		MissionLogger.instance.AddLog (input);
		if (sound != null) {
			myAudio.volume = volume;
			myAudio.PlayOneShot (sound);
		}
	}

	public void TurnOff()
	{
		this.enabled = false;
		myCanvas.enabled = false;

	}


}
