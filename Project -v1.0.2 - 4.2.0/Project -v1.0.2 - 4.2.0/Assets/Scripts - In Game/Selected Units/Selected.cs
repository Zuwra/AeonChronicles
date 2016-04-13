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

	private Slider energySlider;
	//private Image energyFill;

	private Slider coolDownSlider;
	//private Image coolFill;

	private float tempSelectTime;
	private bool tempSelectOn;

	public List<SelectionNotifier> selectionNotifiers = new List<SelectionNotifier>();

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
	{
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
			updateHealthBar (myStats.currentEnergy / myStats.MaxEnergy);

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
			if (healthslider.value > .55) {

	
				unitIcon.GetComponent<Image> ().color = Color.green;

			} else if (healthslider.value > .25) {
			

				unitIcon.GetComponent<Image> ().color = Color.yellow;

			} else {
			
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
					decalCircle.GetComponent<MeshRenderer> ().enabled = false;

					foreach (Transform obj in this.transform) {

						obj.SendMessage ("setSelect", SendMessageOptions.DontRequireReceiver);
					}
						
				}
			}
		}
	}

	void LateUpdate()
	{
		
	}

	public void tempSelect()
	{
		if (!tempSelectOn) {
			tempSelectOn = true;
			decalCircle.GetComponent<MeshRenderer> ().enabled = true;

			foreach (Transform obj in this.transform) {

				obj.SendMessage ("setDeselect", SendMessageOptions.DontRequireReceiver);
			}
		}

		tempSelectTime = Time.time + .05f;

	}


	public void updateHealthBar(float ratio)
	{if (!turretDisplay) {
			
			healthslider.value = ratio; 

			if (mydisplayType == displayType.damaged) {
	
				if (ratio > .99) {
					buffDisplay.isOn = false;
					healthslider.gameObject.SetActive (false);
				} else {
					buffDisplay.isOn = true;
					healthslider.gameObject.SetActive (true);
				}
	

			}

			if (ratio > .55) {
				healthFill.color = Color.green;
				if (unitIcon) {
					unitIcon.GetComponent<Image> ().color = Color.green;
				}
			} else if (ratio > .25) {
				healthFill.color = Color.yellow;
				if (unitIcon) {
					unitIcon.GetComponent<Image> ().color = Color.yellow;
				}
			} else {
				healthFill.color = Color.red;
				if (unitIcon) {
					unitIcon.GetComponent<Image> ().color = Color.red;
				}
			}
		} else {
			turretDisplay.updateHealth (ratio);}

	}

	public void updateEnergyBar(float ratio)
	{
		energySlider.value = ratio;

	}

	public void updateCoolDown(float ratio)
	{
		coolDownSlider.value = ratio;
		if (ratio <= 0) {
			buffDisplay.isOn = false;

			coolDownSlider.gameObject.SetActive (false);
		} else {
			
			buffDisplay.isOn = true;
			coolDownSlider.gameObject.SetActive (true);
		}

	}


	public void SetSelected()
	{
		IsSelected = true;
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;

		if (displayType.selected == mydisplayType) {
			buffDisplay.isOn = true;
			healthslider.gameObject.SetActive (true);
			if (myStats.MaxEnergy > 0) {
				energySlider.gameObject.SetActive (true);
			}
		}


	}
	
	public void SetDeselected()
	{
		IsSelected = false;
		unitIcon = null;
		decalCircle.GetComponent<MeshRenderer> ().enabled = false;

		if (displayType.selected == mydisplayType) {
			buffDisplay.isOn = false;
			healthslider.gameObject.SetActive (false);
			if (myStats.MaxEnergy > 0) {
				energySlider.gameObject.SetActive (false);
			}
		}

	}

}
