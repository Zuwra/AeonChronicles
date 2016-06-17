using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretHealthDisplay : HealthDisplay {

	private GameObject camy;

	public Image Icon;
	public Text percentHealth;

	private bool flashing;
	private int flashNum;
	private float nextflashTime;

	private Color currentColor;
	private float currentHealth =1;

	private bool hoverOver;
	private bool pointerIn;

	// Use this for initialization
	void Start () {
		updateHealth (currentHealth);
		camy = GameObject.FindObjectOfType<MainCamera> ().gameObject;
		percentHealth.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 location = camy.transform.position;
		location.x = this.gameObject.transform.position.x;
		gameObject.transform.LookAt (location);


		if (flashing) {
			if (Time.time > nextflashTime) {
				flashNum++;

				if (flashNum < 5) {
					nextflashTime += .1f;
					if (Icon.color == Color.red) {
						//percentHealth.color = getCurrentColor();
						Icon.color = getCurrentColor();
					} else {
						//percentHealth.color = Color.red;
						Icon.color = Color.red;
					}
				} else {
					flashing = false;
					updateHealth (currentHealth);
				}

			}
		}

	}

	public Color getCurrentColor()
	{	if (currentHealth > .60) {
			return Color.green;
	
		}else if (currentHealth > .30) {
		return Color.yellow;

		}else {
			return Color.red;

		}
	}


	public void updateHealth(float input)
	{currentHealth = input;
		int casted = (int)(input * 100);

		if (casted > 98) {
			//percentHealth.text = "";
			Icon.enabled = false;
		} else {
			//percentHealth.text = casted + "%";
			Icon.enabled = true;
		}

		if (!flashing) {
			if (input > .60) {

				//percentHealth.color = Color.green;
				if (!pointerIn) {
					Icon.color = Color.green;
				}

			} else if (input > .35) {
				//percentHealth.color = Color.yellow;
				if (!pointerIn) {
					Icon.color = Color.yellow;
				}
			} else if (input > .15) {
				//percentHealth.color = Color.yellow;
				if (!pointerIn) {
					Icon.color = new Color (1, .4f, 0);
				}
			}
			else {
				//percentHealth.color = Color.red;
				if (!pointerIn) {
					Icon.color = Color.red;
				}
			}
		}

	}


	public void flash()
	{flashNum = 0;

		//percentHealth.color = Color.red;
		Icon.color = Color.red;
		nextflashTime = Time.time + .1f;
		flashing = true;

	}

	public void hover(bool input)
	{
		hoverOver = input;
		if (pointerIn || hoverOver) {
			setSelect ();
		} else {
			setDeselect ();
		}}

	public void PointerI(bool input)
	{
		pointerIn = input;
		if (pointerIn || hoverOver) {
			setSelect ();
		} else {
			setDeselect ();
		}}

	public void display()
	{
		pointerIn = true;
		Icon.color = Color.red;
	}

	public void unDisplay()
	{
		pointerIn = false;
		updateHealth (currentHealth);
		
	}



	public void setSelect (){
		Icon.enabled = true;
	}

	public void setDeselect (){

		Icon.enabled = false;
		updateHealth (currentHealth);
	}



}
