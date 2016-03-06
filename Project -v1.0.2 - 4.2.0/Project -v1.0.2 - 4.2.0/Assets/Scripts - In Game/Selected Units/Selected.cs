using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selected : MonoBehaviour {
		
	public bool IsSelected
	{
		get;
		private set;
	}

	public HealthDisplay buffDisplay;

	private Slider healthslider;
	private Image healthFill;

	private Slider energySlider;
	//private Image energyFill;

	private Slider coolDownSlider;
	//private Image coolFill;

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
	{		IsSelected = false;
		
		buffDisplay = GetComponentInChildren<HealthDisplay> ();

		healthslider = transform.FindChild("HealthDisplay").FindChild("HealthBar").GetComponent<Slider>();
		healthFill = transform.FindChild("HealthDisplay").FindChild("HealthBar").transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();

		energySlider= transform.FindChild("HealthDisplay").FindChild("EnergyBar").GetComponent<Slider>();
		//energyFill= transform.FindChild("HealthDisplay").FindChild("EnergyBar").transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>();

		coolDownSlider= transform.FindChild("HealthDisplay").FindChild("Cooldown").GetComponent<Slider>();
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
	
	// Update is called once per frame
	void Update () 
	{
		
	}





	public void updateHealthBar(float ratio)
	{
		healthslider.value = ratio; 

	
		if(mydisplayType ==displayType.damaged){
	
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
		} else if (ratio > .25) {
			healthFill.color = Color.yellow;
		} else {
			healthFill.color = Color.red;
		}

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
			//onCooldown = false;
			coolDownSlider.gameObject.SetActive (false);
		} else {
			//onCooldown = true;
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
