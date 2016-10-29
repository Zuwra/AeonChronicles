using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour {



	public GameObject masterSLiderO;
	public GameObject musicSliderO;
	private AudioSource music;

	private Slider masterSLider;
	private Slider musicSlider;


	// Use this for initialization
	void Start () {
		

		masterSLider = masterSLiderO.GetComponent<Slider> ();

		musicSlider = musicSliderO.GetComponent<Slider> ();
		MainCamera mc = GameObject.FindObjectOfType<MainCamera> ();
		if (mc) {
			music = mc.GetComponent<AudioSource> ();
		}
		if (!music) {
			music = GameObject.FindObjectOfType<Camera> ().GetComponent<AudioSource> ();
		}


		if (GameSettings.masterVolume > -1) {
			masterSLider.value = GameSettings.masterVolume;
		}
		else{
			masterSLider.value = AudioListener.volume;
			GameSettings.masterVolume = masterSLider.value;
		}


		if (GameSettings.musicVolume > -1) {
			musicSlider.value = GameSettings.musicVolume;
		} else {
			musicSlider.value = music.volume;
			GameSettings.musicVolume = music.volume;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			GameSettings.masterVolume += .15f;
			masterSLider.value = GameSettings.masterVolume;
			AudioListener.volume = masterSLider.value;

		} else if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			GameSettings.masterVolume -= .15f;
			masterSLider.value = GameSettings.masterVolume;
			AudioListener.volume = masterSLider.value;
		}
	
	}


	public void MusicVolumeChange()
	{
		music.volume = musicSlider.value;
		GameSettings.musicVolume = music.volume;
	}


	public void masterVolumeChange()
	{
		AudioListener.volume = masterSLider.value;
		GameSettings.masterVolume = masterSLider.value;
	}


}
