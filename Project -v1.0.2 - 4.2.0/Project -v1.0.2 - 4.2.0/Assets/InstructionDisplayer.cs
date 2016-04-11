using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionDisplayer : MonoBehaviour {


	private Canvas myCanvas;
	private Text myText;
	public static InstructionDisplayer instance;

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

	public void displayText(string input, float duration, AudioClip sound)
	{	this.enabled = true;
		myText.text = input;
		myCanvas.enabled = true;
		turnOffTime = Time.time + duration;
		MissionLogger.instance.AddLog (input);
		if (sound != null) {
			myAudio.PlayOneShot (sound);
		}
	}


}
