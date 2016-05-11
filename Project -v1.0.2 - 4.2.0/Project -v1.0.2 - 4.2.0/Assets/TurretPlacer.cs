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

	private float lastTrueTimeG;
	private float lastTrueTimeR;
	private float lastTrueTimeM;
	private float lastTrueTimeP;

	public TurretPlacerManager turretManager;

	private bool isON = false;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
	}

	public void setUnit(GameObject u)
	{
		unit = u;
		unitSelect = unit.GetComponentInParent<Selected> ();
		turretManager = unit.GetComponentInParent<TurretPlacerManager> ();

	}

	// Update is called once per frame
	void Update () {
		if (unitSelect.IsSelected != isON) {
			
			isON = unitSelect.IsSelected;

			center.gameObject.SetActive(unitSelect.IsSelected);
			if (!isON) {

				gatling.gameObject.SetActive (unitSelect.IsSelected);
				railgun.gameObject.SetActive (unitSelect.IsSelected);
				if (mortar) {
					mortar.gameObject.SetActive (unitSelect.IsSelected);
				}
				if (repair) {
					repair.gameObject.SetActive (unitSelect.IsSelected);
				}
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
			if (g) {
				lastTrueTimeG = Time.time;
				gatling.interactable = g;
			} else {
				if (Time.time > lastTrueTimeG + .1) {
					gatling.interactable = false;
				}
			}

			if (r) {
				lastTrueTimeR = Time.time;
				railgun.interactable = r;
			} else {
				if (Time.time > lastTrueTimeR + .1) {
					railgun.interactable = false;
				}
			}
			if(mortar){
			if (m) {
				lastTrueTimeM = Time.time;
				mortar.interactable = m;
			} else {
					if (Time.time > lastTrueTimeM + .1 ) {
						mortar.interactable = false;
					}}
			}
			if (repair) {
				if (p ) {
					lastTrueTimeP = Time.time;
					repair.interactable = p;
				} else {
					if (Time.time > lastTrueTimeP + .1f) {
						repair.interactable = false;
					}
				}
			}

		}
	}

	public void ToggleOn()
	{
		center.gameObject.SetActive (!center.gameObject.activeSelf);
	}

	public void showButtons(){
		buttonsOn = !buttonsOn;
		if (turretManager != null) {
			turretManager.deactivate ();
			center.gameObject.SetActive (!center.gameObject.activeSelf);
		} 

		gatling.gameObject.SetActive (buttonsOn);
		railgun.gameObject.SetActive (buttonsOn);
		if (mortar) {
			mortar.gameObject.SetActive (buttonsOn);
		}if (repair) {
			repair.gameObject.SetActive (buttonsOn);
		}
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
