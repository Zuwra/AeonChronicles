using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Selected : MonoBehaviour {
		
	public bool IsSelected
	{
		get;
		private set;
	}

	public HealthDisplay buffDisplay;
	public TurretHealthDisplay turretDisplay;

	private GameObject unitIcon;
	private Slider healthslider;
	private Image healthFill;
	public GameObject RallyPoint;
	public GameObject RallyUnit;
	private Slider energySlider;
	//private Image energyFill;

	private Slider coolDownSlider;
	//private Image coolFill;

	private float tempSelectTime;
	private bool tempSelectOn;
	private bool interactSelect;
	public List<SelectionNotifier> selectionNotifiers = new List<SelectionNotifier>();
	private LineRenderer myLine;

	public enum displayType
	{
		always,damaged,selected,never
	}


	public displayType mydisplayType = displayType.damaged;

	private GameObject decalCircle;
	private UnitStats myStats;
	//private bool onCooldown = false;
	// Use this for initialization
	void Start () 
	{		Initialize ();

	}


	public void Initialize()
	{myLine = GetComponent<LineRenderer> ();
		IsSelected = false;


		buffDisplay = GetComponentInChildren<HealthDisplay> ();

		turretDisplay = transform.FindChild("HealthDisplay").GetComponent<TurretHealthDisplay> ();
		if (!turretDisplay) {
			healthslider = transform.FindChild ("HealthDisplay").FindChild ("HealthBar").GetComponent<Slider> ();
			healthFill = transform.FindChild ("HealthDisplay").FindChild ("HealthBar").transform.FindChild ("Fill Area").FindChild ("Fill").GetComponent<Image> ();
		} 
			energySlider = transform.FindChild ("HealthDisplay").FindChild ("EnergyBar").GetComponent<Slider> ();
			//energyFill= transform.FindChild("HealthDisplay").FindChild("EnergyBar").transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();

			coolDownSlider = transform.FindChild ("HealthDisplay").FindChild ("Cooldown").GetComponent<Slider> ();
			//coolFill= transform.FindChild("HealthDisplay").FindChild("Cooldown").transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();
	

		myStats = this.gameObject.GetComponent<UnitStats> ();
		decalCircle = this.gameObject.transform.Find("DecalCircle").gameObject;


		//If this unit is land based subscribe to the path changed event
		mydisplayType = GameObject.Find("GamePlayMenu").GetComponent<GamePlayMenu>().getDisplayType();
		if (myStats.MaxEnergy == 0) {
			energySlider.gameObject.SetActive (false);
		} else {

			updateEnergyBar (myStats.currentEnergy / myStats.MaxEnergy);


		}
		updateHealthBar (myStats.health / myStats.Maxhealth);
		coolDownSlider.gameObject.SetActive (false);
	}



	public void setDisplayType(displayType t)
	{mydisplayType = t;
		if (!turretDisplay) {
			switch (t) {
			case displayType.always: 
				buffDisplay.isOn = true;
				healthslider.enabled = true;
				if (myStats.MaxEnergy > 0) {
					energySlider.gameObject.SetActive (true);
				}
				break;

			case displayType.damaged:
				if (!myStats.atFullHealth ()) {
					buffDisplay.isOn = true;
					healthslider.gameObject.SetActive (false);
				} else {
					buffDisplay.isOn = false;
				}

				if (myStats.MaxEnergy > 0) {
					energySlider.gameObject.SetActive (true);
					buffDisplay.isOn = true;
				}
				break;

			case  displayType.selected:
				if (IsSelected) {
					buffDisplay.isOn = true;
					healthslider.gameObject.SetActive (true);
					if (myStats.MaxEnergy > 0) {
						energySlider.gameObject.SetActive (true);
					}
				}
				break;

			case displayType.never:
				buffDisplay.isOn = false;
				healthslider.gameObject.SetActive (false);
				energySlider.gameObject.SetActive (false);
				break;
			}
		}
	}

	public void setIcon(GameObject obj)
	{//buffDisplay.isOn = true;
		unitIcon = obj;
		if (!turretDisplay) {
			if (healthslider.value > .6) {

	
				unitIcon.GetComponent<Image> ().color = Color.green;

			} else if (healthslider.value > .35) {
			

				unitIcon.GetComponent<Image> ().color = Color.yellow;

			} else if (healthslider.value > .15) {


				unitIcon.GetComponent<Image> ().color = new Color (1, .4f, 0);

			}else {
			
				unitIcon.GetComponent<Image> ().color = Color.red;

			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (tempSelectOn) {
			if (tempSelectTime < Time.time) {
				tempSelectOn = false;
				if (!IsSelected) {
					
					if (!interactSelect) {
						decalCircle.GetComponent<MeshRenderer> ().enabled = false;
					}
					if (RallyPoint) {
						
						RallyPoint.SetActive (false);
					}
				}
					foreach (Transform obj in this.transform) {

						obj.SendMessage ("setDeSelect", SendMessageOptions.DontRequireReceiver);
					}
						

			}
		}
		if (IsSelected || tempSelectOn) {
			if (RallyUnit) {
				myLine.SetPositions (new Vector3[]{ this.gameObject.transform.position, RallyUnit.transform.position });
				RallyPoint.transform.position = RallyUnit.transform.position;
			}
		}
	}

	void LateUpdate()
	{
		
	}

	public void interact()
	{
		StartCoroutine (delayTurnOff ());
	
	}


	IEnumerator delayTurnOff()
	{
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;
		interactSelect = true;
		yield return new WaitForSeconds(.13f);
		interactSelect = false;
		decalCircle.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds(.13f);
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;
		yield return new WaitForSeconds(.13f);
		decalCircle.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds(.13f);
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;
		yield return new WaitForSeconds(.13f);

		if(!IsSelected && !tempSelectOn)
		{
			decalCircle.GetComponent<MeshRenderer> ().enabled = false;
		}


	}

	public void tempSelect()
	{
		if (!tempSelectOn) {
			tempSelectOn = true;
			decalCircle.GetComponent<MeshRenderer> ().enabled = true;
			if (RallyPoint) {
				RallyPoint.SetActive (true);
			}

			foreach (Transform obj in this.transform) {

				obj.SendMessage ("setSelect", SendMessageOptions.DontRequireReceiver);
			}
		}

		tempSelectTime = Time.time + .08f;

	}


	public void updateHealthBar(float ratio)
	{if (!turretDisplay) {
			
			healthslider.value = ratio; 

			if (mydisplayType == displayType.damaged) {
	
				if (ratio > .99) {
					//buffDisplay.isOn = false;
					healthslider.gameObject.SetActive (false);
				} else {
					//buffDisplay.isOn = true;
					healthslider.gameObject.SetActive (true);
				}
	

			}

			if (ratio > .6) {
				healthFill.color = Color.green;
				if (unitIcon) {
					unitIcon.GetComponent<Image> ().color = Color.green;
				}
			} else if (ratio > .35) {
				healthFill.color = Color.yellow;
				if (unitIcon) {
					unitIcon.GetComponent<Image> ().color = Color.yellow;
				}
			} 
			else if (healthslider.value > .15) {
				healthFill.color =new Color (1, .4f, 0);
				if (unitIcon) {
					unitIcon.GetComponent<Image> ().color = new Color (1, .4f, 0);
				}
			}
			else {
				healthFill.color = Color.red;
				if (unitIcon) {
					unitIcon.GetComponent<Image> ().color = Color.red;
				}
			}
		} else {
			turretDisplay.updateHealth (ratio);}
		checkOn ();

	}

	public void updateEnergyBar(float ratio)
	{
		energySlider.value = ratio;
		if (ratio > .99 || ratio < .01) {

			energySlider.gameObject.SetActive (false);
		} else {

			energySlider.gameObject.SetActive (true);
		}
		checkOn ();
	}

	public void checkOn()
	{
		if (coolDownSlider.value > 0 || energySlider.value < .99 || (healthslider.value < .99)) {
			buffDisplay.isOn = true;
		} else {
			buffDisplay.isOn = false;
		}

	}

	public void updateCoolDown(float ratio)
	{
		coolDownSlider.value = ratio;
		if (ratio <= 0) {
			//buffDisplay.isOn = false;

			coolDownSlider.gameObject.SetActive (false);
		} else {
			
			//buffDisplay.isOn = true;
			coolDownSlider.gameObject.SetActive (true);
		}
		checkOn ();

	}


	public void SetSelected()
	{
		IsSelected = true;
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;
		if (RallyPoint) {
			RallyPoint.SetActive (true);
			if (myLine) {
				myLine.enabled = true;
			}
		}

		if (displayType.selected == mydisplayType) {
			buffDisplay.isOn = true;
			healthslider.gameObject.SetActive (true);
			if (myStats.MaxEnergy > 0) {
				energySlider.gameObject.SetActive (true);
			}
		}

		//foreach (Transform obj in this.transform) {

			//obj.SendMessage ("setSelect", SendMessageOptions.DontRequireReceiver);
		//}
	}
	
	public void SetDeselected()
	{
		IsSelected = false;
		unitIcon = null;
		decalCircle.GetComponent<MeshRenderer> ().enabled = false;
		if (RallyPoint) {
			RallyPoint.SetActive (false);
			if (myLine) {
				myLine.enabled = false;
			}
		}

		if (displayType.selected == mydisplayType) {
			buffDisplay.isOn = false;
			healthslider.gameObject.SetActive (false);
			if (myStats.MaxEnergy > 0) {
				energySlider.gameObject.SetActive (false);
			}
		}

	}

}
