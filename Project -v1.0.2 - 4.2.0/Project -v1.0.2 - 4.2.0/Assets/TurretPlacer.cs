using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurretPlacer : MonoBehaviour {

	public GameObject unit;
	public Button center;
	public Button gatling;
	public Button railgun;
	public Button mortar;
	public Button repair;
	public GameObject factory;

	private GameObject cam;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
	}

	// Update is called once per frame
	void Update () {

		Vector3 pos = unit.transform.position;
		pos.y += 5;
		this.gameObject.GetComponent<RectTransform> ().position = pos;

		Vector3 dif = cam.transform.position - this.gameObject.transform.position;
		dif *= .5f;
		this.gameObject.GetComponent<RectTransform> ().position = dif + pos;

		Vector3 location = cam.transform.position;
		location.x = this.gameObject.transform.position.x;
		gameObject.transform.LookAt (location);

	


	}


	public void showButtons(){
		center.gameObject.SetActive (false);
		gatling.gameObject.SetActive (true);
		railgun.gameObject.SetActive (true);
		mortar.gameObject.SetActive (true);
		repair.gameObject.SetActive (true);
	}

	public void buildGatling()
	{
		
	}

	public void buildRailGun()
	{
	}

	public void buildMortar()
	{
	}

	public void buildRepair()
	{
	}



}
