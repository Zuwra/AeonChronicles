using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitIconInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public GameObject myUnit;
	private bool pointerIn;
	public Canvas toolbox;
	public Text textBox;
	public Text energyText;
	private UnitStats myStats;

	private float nextActionTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (pointerIn) {
			if (Time.time > nextActionTime) {
				nextActionTime += .2f;
				textBox.text = (int)myStats.health + "/" + (int)myStats.Maxhealth;

				if (myStats.MaxEnergy > 0) {
					energyText.text = (int)myStats.currentEnergy + "/" + (int)myStats.MaxEnergy;
				}
			}
		}
	}



	public void OnPointerEnter(PointerEventData eventd)
	{//pointerIn = true;
		//this.enabled = true;
		nextActionTime = Time.time;
		pointerIn = true;
		toolbox.enabled = true;
		myStats = myUnit.GetComponent<UnitManager> ().myStats;
		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	public void OnPointerExit(PointerEventData eventd)
	{pointerIn = false;
		//this.enabled= false;
		toolbox.enabled = false;
	}



}
