using UnityEngine;
using System.Collections;
using DigitalRuby.SoundManagerNamespace;

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

		//Invoke ("playNextTrack", mySrc.clip.length -1.5f);

	}

	float updateTime;

	void Update(){

		if(!mySrc.isPlaying){
			if(mySrc.time > mySrc.clip.length -1){
		//if (Time.unscaledTime > nextPlayTime) {
				playNextTrack ();
			}
		//}
		}

	}

	public void crossFadeTrack(AudioClip clip)
	{
		StartCoroutine (crossFade(3,clip));
	}

	public void crossFadeTrack(float fadeTime, AudioClip clip)
	{
		StartCoroutine (crossFade(fadeTime,clip));
	}

	IEnumerator crossFade(float fadeTime, AudioClip clip)
	{
		float startVolume = mySrc.volume;
		float FadeRate = startVolume / (fadeTime * .5f);
		for (float i = 0; i < fadeTime / 2; i += Time.deltaTime) {
			mySrc.volume -= FadeRate * Time.deltaTime;
			yield return null;
		}
		mySrc.Stop ();
		mySrc.PlayOneShot (clip);
		for (float i = 0; i < fadeTime / 5; i += Time.deltaTime) {
			mySrc.volume += FadeRate * Time.deltaTime;
			yield return null;
		}


	}



}
