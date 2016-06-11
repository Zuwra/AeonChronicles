using UnityEngine;
using System.Collections;

public class UIHighLight : MonoBehaviour {

	public GameObject Square;
	public GameObject Rectangle;
	public GameObject wideRectangle;

	public static UIHighLight main;
	private bool turnedOn;
	private float turnOffTime;
	private bool ThingOne;
	private bool ThingTwo;
	private bool ThingThree;
	// Use this for initialization
	void Awake () {
		main = this;
	
	}

	// Update is called once per frame
	void Update () {
	
		if (turnedOn) {
			if (ThingOne) {
				if (Square.transform.localScale.x >= 1) {
					Vector3 tempScale = Square.transform.localScale;
					tempScale *=  1 -( .993f * Time.deltaTime);
					Square.transform.localScale = tempScale;
				} else if (Time.time > turnOffTime) {
					Square.SetActive (false);
					turnedOn = false;
				}
			}
			if (ThingTwo) {
				if (Rectangle.transform.localScale.x >= 1) {
					Vector3 tempScale = Rectangle.transform.localScale;
					tempScale *= 1 -( .993f * Time.deltaTime);
					Rectangle.transform.localScale = tempScale;
				} else if (Time.time > turnOffTime) {
					Rectangle.SetActive (false);
					turnedOn = false;
				}
			}
			if (ThingThree ) {
				if (wideRectangle.transform.localScale.x >= 1) {
					Vector3 tempScale = wideRectangle.transform.localScale;
					tempScale *=  1 - ( .993f * Time.deltaTime);
					wideRectangle.transform.localScale = tempScale;
				} else if (Time.time > turnOffTime) {
					wideRectangle.SetActive (false);
					turnedOn = false;
				}
					
			
			}

		
		}

	}
	

	public void highLight(GameObject input, int size)
	{//Debug.Log ("Activating Highlighter " + size);
		
		turnedOn = true;
		turnOffTime = Time.time + 5;

		if (size == 0) {
			ThingOne = true;
			Square.transform.localScale = new Vector3 (8, 8, 8);
			//Square.transform.SetParent (input.transform);
			Square.SetActive (true);
		}
		else if (size == 1) {
			ThingTwo = true;
			Rectangle.transform.localScale = new Vector3 (8, 8, 8);
			//Rectangle.transform.SetParent (input.transform);
			Rectangle.SetActive (true);
		}
		else if (size == 2) {
			ThingThree = true;
			wideRectangle.transform.localScale = new Vector3 (8, 8, 8);
			//wideRectangle.transform.SetParent (input.transform);
			wideRectangle.SetActive (true);
		}


	}


}
