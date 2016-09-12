using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurretplacerTwo : MonoBehaviour {

	public GameObject unit;
	public Button center;
	public Button LaserNode;
	public Button MissileLauncher;

	public BigTurretScreenDisplayer factory;

	private Selected unitSelect;

	private bool buttonsOn = false;

	private GameObject cam;

	private float lastTrueTimeL;
	private float lastTrueTimeM;


	public TurretPlacerManager turretManager;


	private bool isON;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
		turretManager = unit.GetComponentInParent<TurretPlacerManager> ();
	}

	public void setUnit(GameObject u)
	{
		unit = u;
		unitSelect = unit.GetComponentInParent<Selected> ();

	}

	// Update is called once per frame
	void Update () {
		if (unitSelect.IsSelected != isON) {
			isON = unitSelect.IsSelected;
			center.gameObject.SetActive(unitSelect.IsSelected);
			if (!isON) {

				LaserNode.gameObject.SetActive (unitSelect.IsSelected);
				MissileLauncher.gameObject.SetActive (unitSelect.IsSelected);

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

	public void initialize(bool l, bool m)
	{
		if (buttonsOn) {
			if (l) {
				lastTrueTimeL = Time.time;
				LaserNode.interactable = l;
			} else {
				if (Time.time > lastTrueTimeL + .1) {
					LaserNode.interactable = false;
				}
			}

			if (m) {
				lastTrueTimeM = Time.time;
				MissileLauncher.interactable = m;
			} else {
				if (Time.time > lastTrueTimeM + .1) {
					MissileLauncher.interactable = false;
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
			turretManager.deactivate (buttonsOn);
		}
		center.gameObject.SetActive (!center.gameObject.activeSelf);
		LaserNode.gameObject.SetActive (buttonsOn);
		MissileLauncher.gameObject.SetActive (buttonsOn);

	}

	public void buildLaser()
	{
		factory.buildLaser (this);
	}

	public void buildMissile()
	{
		factory.buildMissile (this);
	}





}
