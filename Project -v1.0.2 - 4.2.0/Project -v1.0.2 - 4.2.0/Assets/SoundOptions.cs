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
		music = Object.FindObjectOfType<AudioSource>();
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
