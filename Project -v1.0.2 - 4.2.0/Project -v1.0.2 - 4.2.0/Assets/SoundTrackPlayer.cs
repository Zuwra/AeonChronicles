using UnityEngine;
using System.Collections;
using DigitalRuby.SoundManagerNamespace;

public class SoundTrackPlayer : MonoBehaviour {


	public SoundTrackPlayList myPlayList;
	AudioSource mySrc;

	int currentIndex = 0;

	float nextPlayTime;

	void Start () {
		currentIndex = Random.Range (0, myPlayList.myTracks.Count - 1);
		mySrc = GetComponent<AudioSource> ();
		playNextTrack ();
	}

	void playNextTrack()
	{
		currentIndex++;
		if (currentIndex >= myPlayList.myTracks.Count) {
			currentIndex = 0;}
		mySrc.clip = myPlayList.myTracks [currentIndex];
		mySrc.Play ();

		nextPlayTime = System.DateTime.Now.Second + (int)mySrc.clip.length - 1;
		//Invoke ("playNextTrack", mySrc.clip.length -1.5f);

	}

	void Update(){
	
		if (System.DateTime.Now.Second > nextPlayTime) {
			playNextTrack ();
		}


	}



}
