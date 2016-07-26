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

		masterSLider.value = AudioListener.volume;
		musicSlider.value = music.volume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void MusicVolumeChange()
	{
		music.volume = musicSlider.value;
	
	}


	public void masterVolumeChange()
	{
		AudioListener.volume = masterSLider.value;
	
	}


}
