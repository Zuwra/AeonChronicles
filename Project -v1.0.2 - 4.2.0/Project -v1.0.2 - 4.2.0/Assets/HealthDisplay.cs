﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

	private Transform cam;
	//public List<Image> buffList = new List<Image>();



	public Image BuildingUnit;
	public Image background;
	Vector3 LookLocation;

	// Use this for initialization
	void Start () {
		cam = MainCamera.main.transform;// GameObject.FindObjectOfType<MainCamera> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {

			LookLocation = cam.position;
			LookLocation.x = gameObject.transform.position.x;
			gameObject.transform.LookAt (LookLocation);

	}

	public void  loadIMage(Sprite m)
	{
		BuildingUnit.sprite = m;
		BuildingUnit.gameObject.SetActive(true);
		background.gameObject.SetActive (true);
	}

	public void stopBuilding()
	{BuildingUnit.sprite = null;
		BuildingUnit.gameObject.SetActive(false);
		background.gameObject.SetActive (false);
	}

	public void activate(bool offOn)
	{
		this.gameObject.SetActive (offOn);
	}

}
