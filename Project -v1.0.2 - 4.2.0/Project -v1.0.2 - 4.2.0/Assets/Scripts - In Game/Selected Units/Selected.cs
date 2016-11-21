using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class Selected : MonoBehaviour {
		
	public bool IsSelected
	{
		get;
		private set;
	}

	public HealthDisplay buffDisplay;
	public TurretHealthDisplay turretDisplay;

	private GameObject unitIcon;
	private UnitIconInfo IconInfo;

	private Slider IconSlider;

	public Slider healthslider;
	private Image healthFill;
	public GameObject RallyPoint;
	public GameObject RallyUnit;
	public Slider energySlider;
	//private Image energyFill;

	private Slider coolDownSlider;
	private Image coolFill;

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

		myStats = this.gameObject.GetComponent<UnitStats> ();
		decalCircle = this.gameObject.transform.Find("DecalCircle").gameObject;


		buffDisplay = GetComponentInChildren<HealthDisplay> ();
	
		try{
		turretDisplay = transform.FindChild("HealthDisplay").GetComponent<TurretHealthDisplay> ();
		if (!turretDisplay) {
			healthslider = transform.FindChild ("HealthDisplay").FindChild ("HealthBar").GetComponent<Slider> ();
			healthFill = transform.FindChild ("HealthDisplay").FindChild ("HealthBar").transform.FindChild ("Fill Area").FindChild ("Fill").GetComponent<Image> ();
		} 
			energySlider = transform.FindChild ("HealthDisplay").FindChild ("EnergyBar").GetComponent<Slider> ();
			//energyFill= transform.FindChild("HealthDisplay").FindChild("EnergyBar").transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();

			coolDownSlider = transform.FindChild ("HealthDisplay").FindChild ("Cooldown").GetComponent<Slider> ();
			coolFill=        coolDownSlider.transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();
	




		//If this unit is land based subscribe to the path changed event
		mydisplayType = GameObject.Find("GamePlayMenu").GetComponent<GamePlayMenu>().getDisplayType();
		if (myStats.MaxEnergy == 0) {
			energySlider.gameObject.SetActive (false);
		} else {

			updateEnergyBar (myStats.currentEnergy / myStats.MaxEnergy);


		}
		updateHealthBar (myStats.health / myStats.Maxhealth);
			coolDownSlider.gameObject.SetActive (false);}
		catch(Exception) {
			
		}
	}



	public void setDisplayType(displayType t)
	{mydisplayType = t;
		if (!turretDisplay) {
			switch (t) {
			case displayType.always: 
				buffDisplay.enabled = true;
				healthslider.enabled = true;
				if (myStats.MaxEnergy > 0) {
					energySlider.gameObject.SetActive (true);
				}
				break;

			case displayType.damaged:
				if (!myStats.atFullHealth ()) {
					buffDisplay.enabled = true;
					healthslider.gameObject.SetActive (false);
				} else {
					buffDisplay.enabled = false;
				}

				if (myStats.MaxEnergy > 0) {
					energySlider.gameObject.SetActive (true);
					buffDisplay.enabled = true;
				}
				break;

			case  displayType.selected:
				if (IsSelected) {
					buffDisplay.enabled = true;
					healthslider.gameObject.SetActive (true);
					if (myStats.MaxEnergy > 0) {
						energySlider.gameObject.SetActive (true);
					}
				}
				break;

			case displayType.never:
				buffDisplay.enabled = false;
				healthslider.gameObject.SetActive (false);
				energySlider.gameObject.SetActive (false);
				break;
			}
		}
	}

	public void updateIconNum()
	{
		IconInfo.updateNum ();
	}

	public void setIcon(GameObject obj)
	{//buffDisplay.isOn = true;
		if (!obj) {
			return;}
		

		unitIcon = obj.transform.FindChild("UnitIconTemplate").gameObject;

		IconInfo = unitIcon.GetComponent<UnitIconInfo> ();
		IconSlider = obj.transform.FindChild ("Slider").gameObject.GetComponent<Slider>();
		if (!turretDisplay) {
			//Debug.Log (this.gameObject);
			if (healthslider.value > .6) {

				IconInfo.changeColor (Color.green);

			} else if (healthslider.value > .35) {
			
				IconInfo.changeColor (Color.yellow);


			} else if (healthslider.value > .15) {
				IconInfo.changeColor (new Color (1, .4f, 0));

			}else {
				IconInfo.changeColor (Color.red);

			}
			if (IconSlider) {
				IconSlider.value = coolDownSlider.value;
				if (IconSlider.value <= 0 || IconSlider.value >= .99f) {
					//buffDisplay.isOn = false;

					IconSlider.gameObject.SetActive (false);
				} else {
					IconSlider.gameObject.SetActive (true);
				}
			}
		}



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

		tempSelectTime = Time.time + .08f;
		if (!tempSelectOn) {

			StartCoroutine (CurrentlyTempSelect ());
			tempSelectOn = true;

			decalCircle.GetComponent<MeshRenderer> ().enabled = true;
			if (RallyPoint) {
				RallyPoint.SetActive (true);
			}
			if (myLine) {
				myLine.enabled = true;
			}

			foreach (Transform obj in this.transform) {

				obj.SendMessage ("setSelect", SendMessageOptions.DontRequireReceiver);
			}
		}

	

	}




	IEnumerator CurrentlyTempSelect()
	{


		while (tempSelectTime > Time.time) {

			if (RallyUnit) {
				myLine.SetPositions (new Vector3[]{ this.gameObject.transform.position, RallyUnit.transform.position });
				RallyPoint.transform.position = RallyUnit.transform.position;
			}
			yield return 0;
		}

		if (!IsSelected) {
	
			if (!interactSelect) {

				decalCircle.GetComponent<MeshRenderer> ().enabled = false;
			}
			if (RallyPoint) {
				myLine.enabled = false;
				RallyPoint.SetActive (false);
			}

			foreach (Transform obj in this.transform) {

				obj.SendMessage ("setDeSelect", SendMessageOptions.DontRequireReceiver);
			}


		}

			
		tempSelectOn = false;

		}



	public void updateHealthBar(float ratio)
	{try{
		if (!turretDisplay) {
		
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
						IconInfo.changeColor(Color.green);	
				
				}
			} else if (ratio > .35) {
				healthFill.color = Color.yellow;
				if (unitIcon) {
						IconInfo.changeColor(Color.yellow);
					
				}
			} 
			else if (healthslider.value > .15) {
				healthFill.color =new Color (1, .4f, 0);
				if (unitIcon) {
						IconInfo.changeColor(new Color (1, .4f, 0));
					
				}
			}
			else {
				healthFill.color = Color.red;
				if (unitIcon) {
						IconInfo.changeColor(Color.red);
					
				}
			}
		} else {
			turretDisplay.updateHealth (ratio);}
			checkOn ();}
		catch(MissingReferenceException) {
			
		}

	}

	public void updateEnergyBar(float ratio)
	{//Debug.Log (this.gameObject + "   " + energySlider);
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
			buffDisplay.enabled = true;
		} else {
			buffDisplay.enabled = false;
		}

	}

	public void updateCoolDown(float ratio)
	{
		coolDownSlider.value = ratio;
		if (ratio <= 0 || ratio >=.999f) {


			coolDownSlider.gameObject.SetActive (false);
		} else {
			
			//buffDisplay.isOn = true;
			coolDownSlider.gameObject.SetActive (true);
		}
		if (IconSlider) {
			IconSlider.value = ratio;
			if (IconSlider.value <= 0|| IconSlider.value >= .99f) {
				//buffDisplay.isOn = false;

				IconSlider.gameObject.SetActive (false);
			}
			else{

				//buffDisplay.isOn = true;
				IconSlider.gameObject.SetActive (true);
			}
		}
		checkOn ();

	}

	public void setCooldownColor(Color c)
	{
		coolFill.color = c;
	}


	public void SetSelected()
	{
		IsSelected = true;

		StartCoroutine (currentlySelected());
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;

		if (RallyPoint) {
			RallyPoint.SetActive (true);
			if (myLine) {
				myLine.enabled = true;
			}
		}

		if (displayType.selected == mydisplayType) {
			buffDisplay.enabled = true;
			healthslider.gameObject.SetActive (true);
			if (myStats.MaxEnergy > 0) {
				energySlider.gameObject.SetActive (true);
			}
		}

		//foreach (Transform obj in this.transform) {

			//obj.SendMessage ("setSelect", SendMessageOptions.DontRequireReceiver);
		//}
	}

	IEnumerator currentlySelected()
	{
		while (IsSelected) {

			if (RallyUnit) {
				myLine.SetPositions (new Vector3[]{ this.gameObject.transform.position, RallyUnit.transform.position });
				RallyPoint.transform.position = RallyUnit.transform.position;
			}
			yield return 0;
		}
	
	}
	
	public void SetDeselected()
	{
		IsSelected = false;
		unitIcon = null;
		IconSlider = null;
		decalCircle.GetComponent<MeshRenderer> ().enabled = false;
		if (RallyPoint) {
			RallyPoint.SetActive (false);
			if (myLine) {
				myLine.enabled = false;
			}
		}

		if (displayType.selected == mydisplayType) {
			buffDisplay.enabled = false;
			healthslider.gameObject.SetActive (false);
			if (myStats.MaxEnergy > 0) {
				energySlider.gameObject.SetActive (false);
			}
		}

	}

}
