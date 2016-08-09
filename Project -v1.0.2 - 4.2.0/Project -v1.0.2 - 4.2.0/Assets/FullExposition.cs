using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class FullExposition : SceneEventTrigger {

	private Canvas myCanvas;
	private Text myText;
	public static FullExposition instance;

	public Image backOne;
	public Image portraitOne;

	public Image backTwo;
	public Image portraitTwo;

	private float turnOffTime;
	private AudioSource myAudio;


	public List<scene> myScenes = new List<scene> ();
	private int currentScene = -1;
	private int currentShot = 0;
	private float shotChangeTime;
	//private float shotStartTime;

	private float nextDialogue;
	private bool hasNextDialogue;
	public bool openOnStart;


	[System.Serializable]
	public struct scene{
		public List<shot> myShots;
		public List<SceneEventTrigger> nextTrig;



	}
	[System.Serializable]
	public struct shot{

		public float duration;

		public Sprite dialogueImage;
		public float dialogueStartDelay;
		public float dialogueLength;
		public AudioClip dialogueAudio;
		[TextArea(1,10)]
		public string DialogueText;
		public int personNum;

	}



	// Use this for initialization
	void Start () {
		myText = GetComponentInChildren<Text> ();
		instance = this;
		myCanvas = GetComponent<Canvas> ();
	
		myAudio = GetComponent<AudioSource> ();
		if (openOnStart) {
			myCanvas.enabled = true;
			trigger(0, 0, Vector3.zero, null, false);
		} else {
			myCanvas.enabled = false;
		}


	}

	// Update is called once per frame
	void Update ()
	{ if (currentScene > -1) {
		if (Time.time > shotChangeTime) {
			//NEW SHOT

			//shotStartTime = Time.time;
			currentShot++;
			if (currentShot == myScenes [currentScene].myShots.Count) {
				exitScene ();
				return;
			} else {

				shotChangeTime = Time.time + myScenes [currentScene].myShots [currentShot].duration;
				if (myScenes [currentScene].myShots [currentShot].dialogueLength > 0) {
					hasNextDialogue = true;
					nextDialogue = Time.time + myScenes [currentScene].myShots [currentShot].dialogueStartDelay;
				}
			}
		}

		if (hasNextDialogue && Time.time > nextDialogue) {
			hasNextDialogue = false;
				displayText (myScenes [currentScene].myShots [currentShot].DialogueText,myScenes [currentScene].myShots [currentShot].duration
					,myScenes [currentScene].myShots [currentShot].dialogueAudio, .8f,myScenes [currentScene].myShots [currentShot].dialogueImage,
					myScenes [currentScene].myShots [currentShot].personNum);

		}

		
	}


}

	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		
		//Debug.Log ("Index is " + index);
		if (MainCamera.main) {
			MainCamera.main.DisableScrolling ();
		}
	
		currentScene = index;
		shotChangeTime = Time.time + myScenes [currentScene].myShots [currentShot].duration;
		if (myScenes [currentScene].myShots [currentShot].dialogueLength > 0) {
			hasNextDialogue = true;
			nextDialogue = Time.time + myScenes [currentScene].myShots [currentShot].dialogueStartDelay;
		}
	}

	public void exitScene(){
		foreach(SceneEventTrigger trig in myScenes[currentScene].nextTrig){

			trig.trigger (0, 0, Vector3.zero, null, false);
		}
		myCanvas.enabled = false;
		currentScene = -1;
		if (MainCamera.main) {
			MainCamera.main.EnableScrolling ();
		}
	
	}




	public void displayText(string input, float duration, AudioClip sound, float volume, Sprite pic, int personNum)
	{	
		this.enabled = true;
		myText.text = input;
		myCanvas.enabled = true;
		turnOffTime = Time.time + duration;
		if (MissionLogger.instance) {
			MissionLogger.instance.AddLog (input);
		}
	

		if (pic) {
			if (personNum == 0) {

				portraitOne.enabled = true;
				backOne.enabled = true;
				portraitOne.sprite = pic;
				portraitTwo.enabled = false;
				backTwo.enabled = false;
		
			} else {
				portraitOne.enabled = false;
				backOne.enabled = false;
				portraitTwo.sprite = pic;
				portraitTwo.enabled = true;
				backTwo.enabled = true;
			}
			}

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
