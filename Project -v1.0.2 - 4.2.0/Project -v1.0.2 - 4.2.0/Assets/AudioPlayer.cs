using UnityEngine;
using System.Collections;
using DigitalRuby.SoundManagerNamespace;
public class AudioPlayer : MonoBehaviour {

	public AudioClip myClip;

	// Use this for initialization
	public void Start () {

		if (myClip) { // this is here to make sure you dont' play too many of the same sound, takes place of playOnAwake of AudiOSource
			SoundManager.PlayOneShotSound (GetComponent<AudioSource> (), myClip);
		}
	
	}

}
