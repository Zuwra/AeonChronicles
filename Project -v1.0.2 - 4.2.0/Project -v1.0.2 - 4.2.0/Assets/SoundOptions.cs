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


		//if (GameSettings.getMasterVolume()> -1) {
			masterSLider.value = GameSettings.getMasterVolume();
			AudioListener.volume = masterSLider.value;
		//}
		//else{
		//	masterSLider.value = AudioListener.volume;
		//	GameSettings.setMusicVolume (masterSLider.value);
		//}


		//if (GameSettings.getMusicVolume()> -1) {
			musicSlider.value = GameSettings.getMusicVolume ();
		music.volume = musicSlider.value;
		//} else {
		//	musicSlider.value = music.volume;
		//	GameSettings.setMusicVolume (music.volume);
		//}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			GameSettings.setMasterVolume (GameSettings.getMasterVolume () + .15f);
			masterSLider.value = GameSettings.getMasterVolume();
			AudioListener.volume = masterSLider.value;
			Debug.Log ("Volume is " + AudioListener.volume);

		} else if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			GameSettings.setMasterVolume (GameSettings.getMasterVolume () - .15f);
			masterSLider.value = GameSettings.getMasterVolume();
			AudioListener.volume = masterSLider.value;
		}
	
	}


	public void MusicVolumeChange()
	{
		music.volume = musicSlider.value;
		GameSettings.setMusicVolume (music.volume);
	}


	public void masterVolumeChange()
	{
		AudioListener.volume = masterSLider.value;
		GameSettings.setMasterVolume (masterSLider.value);
	}


}
