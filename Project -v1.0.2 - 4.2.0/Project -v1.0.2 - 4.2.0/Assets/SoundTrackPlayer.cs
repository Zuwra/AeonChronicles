using UnityEngine;
using System.Collections;

public class SoundTrackPlayer : MonoBehaviour {


	public SoundTrackPlayList myPlayList;
	AudioSource mySrc;

	int currentIndex = 0;

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
		Invoke ("playNextTrack", mySrc.clip.length);


	}



}
