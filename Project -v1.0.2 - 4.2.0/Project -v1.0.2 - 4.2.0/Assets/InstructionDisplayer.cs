using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionDisplayer : MonoBehaviour {


	private Canvas myCanvas;
	private Text myText;
	public static InstructionDisplayer instance;

	private float turnOffTime;


	// Use this for initialization
	void Start () {
		myText = GetComponentInChildren<Text> ();
		instance = this;
		myCanvas = GetComponent<Canvas> ();
		myCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.time > turnOffTime) {
		
			this.enabled = false;
			myCanvas.enabled = false;
		}
	}

	public void displayText(string input, float duration)
	{	this.enabled = true;
		myText.text = input;
		myCanvas.enabled = true;
		turnOffTime = Time.time + duration;

	}


}
