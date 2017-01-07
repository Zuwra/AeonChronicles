using UnityEngine;
using System.Collections;
using DigitalRuby.SoundManagerNamespace;
public class AudioPlayer : MonoBehaviour {

	public AudioClip myClip;

	// Use this for initialization
	void Start () {

		if (myClip) {
			SoundManager.PlayOneShotSound (GetComponent<AudioSource> (), myClip);
		}
	
	}

}
