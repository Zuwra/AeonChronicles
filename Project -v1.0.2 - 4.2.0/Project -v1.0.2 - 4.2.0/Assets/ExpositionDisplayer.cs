using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpositionDisplayer : MonoBehaviour {


	private Canvas myCanvas;
	private Text myText;
	public static ExpositionDisplayer instance;

	private float turnOffTime;
	private AudioSource myAudio;
	public Image personPic;
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

	IEnumerator scrollingText(string dialog)
	{

		int i = 0;
		while (i <dialog.Length) {
			i++;
	

			myText.text = dialog.Substring(0,i);

			yield return new WaitForSeconds (.027f);
		}


	}


	public void displayText(string input, float duration, AudioClip sound, float volume, Sprite pic)
	{	

		this.enabled = true;
		StartCoroutine (scrollingText (input));
		//myText.text = input;
		myCanvas.enabled = true;
		turnOffTime = Time.time + duration;
		MissionLogger.instance.AddLog (input);
	
		if (input.Length < 100) {
			myText.fontSize = 26;
		} else {
			myText.fontSize = 20;
		}

		if (pic) {
			personPic.sprite = pic;}
		
		if (sound != null) {
			myAudio.volume = volume;
			myAudio.PlayOneShot (sound,1);
		}
	}

	public void displayText( float duration, AudioClip sound, float volume)
	{	
		
		this.enabled = true;

		turnOffTime = Time.time + duration;

		if (sound != null) {
			myAudio.volume = volume;
			myAudio.PlayOneShot (sound,1);
		}
	}

	public void TurnOff()
	{
		this.enabled = false;
		myCanvas.enabled = false;

	}


}
