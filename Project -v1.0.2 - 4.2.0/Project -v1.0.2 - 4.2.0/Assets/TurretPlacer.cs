using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurretPlacer : MonoBehaviour {

	public GameObject unit;
	public Button center;
	public Button gatling;
	public Button railgun;
	public Button mortar;
	public Button repair;

	private List<TurretScreenDisplayer> myFactories = new List<TurretScreenDisplayer>();

	public Sprite armImage;
	public Sprite NonArm;

	public Selected unitSelect;

	private bool buttonsOn = false;

	private GameObject cam;

	private float lastTrueTimeG;
	private float lastTrueTimeR;
	private float lastTrueTimeM;
	private float lastTrueTimeP;
	public TurretMount myMount;

	public TurretPlacerManager turretManager;

	private bool isON = false;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
		myRect = this.gameObject.GetComponent<RectTransform> ();
	}


	public bool addFact(TurretScreenDisplayer fact)
	{

		myFactories.Add (fact);
		center.image.sprite = armImage;
		//center.interactable = true;

		return true;
	}


	public bool removeFact(TurretScreenDisplayer fact)
		{myFactories.Remove (fact);
		myFactories.RemoveAll (item => item == null);

		if (myFactories.Count > 0) {
			return true;
		} else {

			gatling.gameObject.SetActive (false);
			railgun.gameObject.SetActive (false);
			if (mortar) {
				mortar.gameObject.SetActive (false);
			}
			if (repair) {
				repair.gameObject.SetActive (false);
			}

			center.image.sprite = NonArm;
		}
		return false;
	}


	public void setUnit(GameObject u)
	{
		unit = u;
		unitSelect = unit.GetComponentInParent<Selected> ();
		turretManager = unit.GetComponentInParent<TurretPlacerManager> ();

	}

	public bool factCount()
	{return (myFactories.Count > 0);
	}


	RectTransform myRect;
	Vector3 pos;
	Vector3 dif ;
	Vector3 location;
	// Update is called once per frame
	void Update () {
		if (!unit) {
			Destroy (this.gameObject);
			return;
		}

		if (unitSelect.IsSelected != isON) {

			//Debug.Log (unitSelect.IsSelected + "  Turning on center  " + isON);
			isON = unitSelect.IsSelected;



				//center.gameObject.SetActive (true);
		
			if (!isON) {
				center.gameObject.SetActive (true);
				gatling.gameObject.SetActive (false);
				railgun.gameObject.SetActive (false);
				if (mortar) {
					mortar.gameObject.SetActive (false);
				}
				if (repair) {
					repair.gameObject.SetActive (false);
				}
				buttonsOn = false;
			}

		}

		pos = myMount.transform.position + Vector3.up *5;
		myRect.position = pos;

		dif = (cam.transform.position - this.gameObject.transform.position) * .5f;

		myRect.position = dif + pos;

		location = cam.transform.position;
		location.x = this.gameObject.transform.position.x;
		gameObject.transform.LookAt (location);


	}

	public void initialize(bool g, bool r, bool p , bool m)
	{
		//Debug.Log (g + "  " + r+ "  ");

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

	public void ToggleOn(bool offOn)
	{
		center.gameObject.SetActive (offOn);
	}

	public void showButtons(){
		if (UIManager.main.CurrentMode == Mode.globalAbility) {
			return;
		}
		buttonsOn = !buttonsOn;


		if (!Input.GetKey (KeyCode.LeftShift)) {
			SelectedManager.main.DeselectAll ();
		}

		if (!unitSelect.IsSelected) {
			SelectedManager.main.AddObject (GetComponentInParent<UnitManager> ());
			SelectedManager.main.CreateUIPages (0);
		}
		//Debug.Log ("I am selected " + unitSelect.IsSelected);
		if (turretManager != null) {
			turretManager.deactivate (!buttonsOn);
			center.gameObject.SetActive (true);
		} 

		if (myFactories.Count > 0) {
			gatling.gameObject.SetActive (buttonsOn);
			railgun.gameObject.SetActive (buttonsOn);
			if (mortar) {
				mortar.gameObject.SetActive (buttonsOn);
			}
			if (repair) {
				repair.gameObject.SetActive (buttonsOn);
			}
		}
	}

	public void buildGatling()
	{

		foreach (TurretScreenDisplayer s in myFactories) {
			if (s.buildGatling (this)) {
				TurretUIPanel.instance.TurnOff();
				Selected sel = unit.GetComponent<Selected> ();
				if (sel && sel.IsSelected) {
					SelectedManager.main.updateUI ();
				}
				break;
			}
		}
		if (turretManager) {
			turretManager.deactivate (true);
		}
	}

	public void buildRailGun()
	{
		foreach (TurretScreenDisplayer s in myFactories) {
			if (s.buildRailGun (this)) {
				TurretUIPanel.instance.TurnOff();
				Selected sel = unit.GetComponent<Selected> ();
				if (sel && sel.IsSelected) {
					SelectedManager.main.updateUI ();
				}
				break;
			}
		}

		if (turretManager) {
			turretManager.deactivate (true);
		}
	}

	public void buildMortar()
	{
		foreach (TurretScreenDisplayer s in myFactories) {
			if (s.buildMortar (this)) {
				TurretUIPanel.instance.TurnOff();
				Selected sel = unit.GetComponent<Selected> ();
				if (sel && sel.IsSelected) {
					SelectedManager.main.updateUI ();
				}
				break;
			}
		}
		if (turretManager) {
			turretManager.deactivate (true);
		}
	}

	public void buildRepair()
	{
		foreach (TurretScreenDisplayer s in myFactories) {
			if (s.buildRepair (this)) {
				TurretUIPanel.instance.TurnOff();
				Selected sel = unit.GetComponent<Selected> ();
				if (sel && sel.IsSelected) {
					SelectedManager.main.updateUI ();
				}
				break;
			}
		}
		if (turretManager) {
			turretManager.deactivate (true);
		}
	}



}
