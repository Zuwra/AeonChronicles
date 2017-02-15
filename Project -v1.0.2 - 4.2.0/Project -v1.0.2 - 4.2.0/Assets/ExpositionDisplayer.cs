using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using DigitalRuby.SoundManagerNamespace;
public class ExpositionDisplayer : MonoBehaviour {


	private Canvas myCanvas;
	private Text myText;
	public static ExpositionDisplayer instance;

	private AudioSource myAudio;
	public Image personPic;
	// Use this for initialization

	public List<SoundMessage> messageQueue = new List<SoundMessage> ();
	public SoundMessage currentMessage;

	Coroutine currentScrolling;
	Coroutine currentDuration;

	bool inMessage;


	void Update()
	{	if (inMessage) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				if (inMessage)
					InteruptMessage ();
				if (messageQueue.Count > 0) {
					SoundMessage sm =  messageQueue [0];
					messageQueue.RemoveAt (0);
					playMessage (sm);
			
				}
			}
		}
	}

	[Serializable]
	public class SoundMessage{

		public AudioClip myClip;
		public float duration;
		public Sprite myPic;
		public string myText;
		public int priority; // if it is 2 more than the current one, it will cut it off, else it will wait til its done, if it is 3 less, it wont queue
		public float volume;

		public SoundMessage(AudioClip myCl, float dur,Sprite myP,string myT,int pri, float vol)
		{volume = vol;
			myClip = myCl;
			duration = dur;
			myPic = myP;
			myText = myT;
			priority= pri;
		}


		//5 Level Finish/Objective Achieved
		//4 Objective Prompts
		//3 base alerts
		//2 character expositions
		//0 unit voices
	}





	void Start () {
		myText = GetComponentInChildren<Text> ();
		instance = this;
		myCanvas = GetComponent<Canvas> ();
		myCanvas.enabled = false;
		myAudio = GetComponent<AudioSource> ();

		//Debug.Log (currentMessage + "   hi");
	}



	IEnumerator scrollingText(string dialog)
	{

		int i = 0;
		while (i <dialog.Length) {
			i++;
	

			myText.text = dialog.Substring(0,i);

			yield return new WaitForSeconds (.022f);
		}


	}


	public void displayText(string input, float duration, AudioClip sound, float volume, Sprite pic, int Priority)
	{	
		//Debug.Log ("Displaying " + duration);

		SoundMessage newMessage = new SoundMessage (sound, duration,pic,input, Priority, volume);
		if (inMessage) {

			//Debug.Log ("In a message " + input);
			if (newMessage.priority > currentMessage.priority + 2) {
				Debug.Log ("Interrupting " + input +"   " +  newMessage.priority);
				InteruptMessage ();
				playMessage (newMessage);
			
			} else {
				if (newMessage.priority > 0) {
					bool inserted = false;
					//Debug.Log ("Queeing message " + input);
					for (int i = 0; i < messageQueue.Count; i++) {
						if (newMessage.priority > messageQueue [i].priority ) {
							//Debug.Log ("Inserting at " + i + "  "  + newMessage.myText);
							messageQueue.Insert (i, newMessage);
							inserted = true;
							break;
						}
					}
					if (!inserted) {
						//Debug.Log ("Adding to end " + newMessage.myText);
						messageQueue.Add (newMessage);
					}
				}
				//messageQueue.Sort ();
			}
		} else {
			//Debug.Log ("No message " + input);
			playMessage (newMessage);
		}


	}


	public void InteruptMessage()
	{
		Debug.Log ("Interupting message");
		if (currentScrolling != null) {
			StopCoroutine (currentScrolling);
		}
		if (currentDuration != null) {
			StopCoroutine (currentDuration);}

		this.enabled = false;
		myCanvas.enabled = false;


		if (currentMessage.myClip) {
			myAudio.Stop ();
		}
		currentMessage = null;
		inMessage = false;

	}

	void playMessage(SoundMessage myMess)
	{
		//Debug.Log ("Playing Message " + myMess.myText);
		currentMessage = myMess;
		inMessage = true;
		if (myMess.myPic == null) {
			PlaySound (myMess);
		} else {
		

			this.enabled = true;
			if (currentScrolling != null) {
				StopCoroutine (currentScrolling);}
			currentScrolling = StartCoroutine (scrollingText (myMess.myText));
			myCanvas.enabled = true;
		
			//MissionLogger.instance.AddLog (myMess.myText);

			if (myMess.myText.Length < 100) {
				myText.fontSize = 26;
			} else {
				myText.fontSize = 20;
			}

			personPic.sprite = myMess.myPic;

			if (myMess.myClip != null) {
				myAudio.volume = myMess.volume;
				myAudio.clip = myMess.myClip;
				myAudio.Play ();
			}
		}

		currentDuration = StartCoroutine (playNextSOund(myMess.duration));
	
	}

	void PlaySound(SoundMessage myMess)
	{
		
		//Debug.Log ("In here " + myMess.myClip);
		if (myMess.myClip != null) {
			myAudio.volume = myMess.volume;
			myAudio.clip = myMess.myClip;
			myAudio.Play ();
			currentDuration = StartCoroutine (playNextSOund(myMess.duration));
			//SoundManager.PlayOneShotSound (myAudio, myMess.myClip);
		}
	
	}

	IEnumerator playNextSOund(float Duration)
	{
		yield return new WaitForSeconds (Duration + .2f);
		if (messageQueue.Count > 0) {
			playMessage (messageQueue [0]);
			currentMessage = messageQueue [0];
			inMessage = true;
			messageQueue.RemoveAt (0);
		
		} else {
	
			this.enabled = false;

			myCanvas.enabled =false;
			currentMessage = null;
			inMessage = false;


		}

	
	
	}


	public void displayText( float duration, AudioClip sound, float volume)
	{	
		
		this.enabled = true;

		if (sound != null) {
			myAudio.volume = volume;
			myAudio.PlayOneShot (sound,1);
		}

		StartCoroutine (playNextSOund (duration));
	}



}
