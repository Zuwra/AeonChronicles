﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ErrorPrompt : MonoBehaviour {

	private AudioSource myAudio;
	public AudioClip errorSound;
	public static ErrorPrompt instance;


	float lastErrorTime;
	public void showError(string err)
	{

		if (lastErrorTime < Time.time - 2.5f) {
		
		

			this.gameObject.GetComponent<Text> ().text = err;
			this.gameObject.GetComponent<Text> ().enabled = true;
			myAudio.PlayOneShot (errorSound);
			StopCoroutine (MyCoroutine ());
			StartCoroutine (MyCoroutine ());

			lastErrorTime = Time.time;
		}
	}


	//Reserved for thing like building construction completion
	public void showMessage(string err, AudioClip clip)
	{
		this.gameObject.GetComponent<Text> ().text = err;
		this.gameObject.GetComponent<Text> ().enabled = true;
		myAudio.PlayOneShot (clip);
		StopCoroutine (MyCoroutine ());
		StartCoroutine (MyCoroutine ());

		lastErrorTime = Time.time;

	}
	
	IEnumerator MyCoroutine ()
	{

		yield return new WaitForSeconds(3f);
		this.gameObject.GetComponent<Text> ().enabled = false;

	}//
	

	public void notEnoughResource()
	{showError( "Not Enough Ore");
	}

	public void notEnoughSupply()
	{showError( "Not enough Supply, Build more Aether Cores");
		}

	public void notEnoughEnergy()
	{showError(  "Not enough Energy");
		}

	public void populationcapHit()
	{
		showError( "Population Limited Reached");

	}

	public void invalidGroundLocation()
	{
		showError( "Invalid Location");

	}

	public void invalidTarget()
	{showError( "Invalid target.");
		}

	public void onCooldown()
	{showError( "Ability on Cooldown");
	}
	
	
	



	// Use this for initialization
	void Start () {
		instance = this;
		myAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	


}
