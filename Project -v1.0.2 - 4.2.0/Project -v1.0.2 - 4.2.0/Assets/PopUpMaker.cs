﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpMaker : MonoBehaviour {

	public string mytext;
	public Color textColor;
	public Sprite mySprite;

	//If the text isnt showing up make sure the alpha level is all the way up
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreatePopUp(string input, Color c)
	{Vector3 location = this.transform.position;
		location.y += 5;
		GameObject obj = (GameObject)Instantiate (Resources.Load ("PopUp"), location, Quaternion.identity);
		if (mySprite != null) {
			obj.GetComponentInChildren<Image> ().enabled = true;
			obj.GetComponentInChildren<Image> ().sprite = mySprite;
		} else {
			obj.GetComponentInChildren<Text> ().text = input;
			obj.GetComponentInChildren<Text> ().color = c;
		}


	}

	public void CreatePopUp()
	{Vector3 location = this.transform.position;
		location.y +=5;
		GameObject obj = (GameObject)Instantiate (Resources.Load ("PopUp"), location, Quaternion.identity);
		if (mySprite != null) {
			obj.GetComponentInChildren<Image> ().enabled = true;
			obj.GetComponentInChildren<Image> ().sprite = mySprite;
		} else {
			obj.GetComponentInChildren<Text> ().text = mytext;
			obj.GetComponentInChildren<Text> ().color = textColor;
		}

	}

	public void CreatePopUp(Vector3 location)
	{location.y += 5;
		GameObject obj = (GameObject)Instantiate (Resources.Load ("PopUp"), location, Quaternion.identity);
		if (mySprite != null) {
			obj.GetComponentInChildren<Image> ().enabled = true;
			obj.GetComponentInChildren<Image> ().sprite = mySprite;
		} else {
			obj.GetComponentInChildren<Text> ().text = mytext;
			obj.GetComponentInChildren<Text> ().color = textColor;
		}

	}

}
