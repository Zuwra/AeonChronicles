using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

	private GameObject cam;
	//public List<Image> buffList = new List<Image>();


	public bool isOn;
	public Image BuildingUnit;
	public Image background;
	Vector3 LookLocation;

	//private List<int> colorList = new List<int>();
	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			LookLocation = cam.transform.position;
			LookLocation.x = this.gameObject.transform.position.x;
			gameObject.transform.LookAt (LookLocation);
		}
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



}
