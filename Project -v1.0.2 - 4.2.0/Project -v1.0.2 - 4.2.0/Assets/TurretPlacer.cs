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
	public TurretScreenDisplayer factory;

	private Selected unitSelect;

	private bool buttonsOn = false;

	private GameObject cam;

	private bool isON;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
	}

	public void setUnit(GameObject u)
	{
		unit = u;
		unitSelect = unit.GetComponentInParent<Selected> ();
		Debug.Log (unit.transform.parent);
	}

	// Update is called once per frame
	void Update () {
		if (unitSelect.IsSelected != isON) {
			isON = unitSelect.IsSelected;
			center.gameObject.SetActive(unitSelect.IsSelected);
			if (!isON) {

				gatling.gameObject.SetActive (unitSelect.IsSelected);
				railgun.gameObject.SetActive (unitSelect.IsSelected);
				mortar.gameObject.SetActive (unitSelect.IsSelected);
				repair.gameObject.SetActive (unitSelect.IsSelected);
				buttonsOn = false;
			}

		}

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

	public void initialize(bool g, bool r, bool m , bool p)
	{
		if (buttonsOn) {
			gatling.interactable = g;
			railgun.interactable =r;
			mortar.interactable =m;
			repair.interactable =p;
		}
	}


	public void showButtons(){
		buttonsOn = !buttonsOn;
		gatling.gameObject.SetActive (buttonsOn);
		railgun.gameObject.SetActive (buttonsOn);
		mortar.gameObject.SetActive (buttonsOn);
		repair.gameObject.SetActive (buttonsOn);
	}

	public void buildGatling()
	{
		factory.buildGatling (this);
	}

	public void buildRailGun()
	{
		factory.buildRailGun (this);
	}

	public void buildMortar()
	{
		factory.buildMortar (this);
	}

	public void buildRepair()
	{
		factory.buildRepair (this);
	}



}
