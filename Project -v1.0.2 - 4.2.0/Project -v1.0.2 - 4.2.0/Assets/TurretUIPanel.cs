using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TurretUIPanel : MonoBehaviour {


	private Canvas myCanvas;
	private Text myText;
	public static TurretUIPanel instance;


	public Image personPic;

	// Use this for initialization
	void Start () {
		myText = GetComponentInChildren<Text> ();
		instance = this;
		myCanvas = GetComponent<Canvas> ();
		myCanvas.enabled = false;

	}

	// Update is called once per frame
	void Update () {

		//if (Time.time > turnOffTime) {

			//this.enabled = false;
			//myCanvas.enabled = false;
		//}
	}

	public void displayText(string input,  Sprite pic)
	{	

		this.enabled = true;
		myText.text = input;
		myCanvas.enabled = true;

	

		if (pic) {
			personPic.sprite = pic;}

	}

	public void TurnOff()
	{
		this.enabled = false;
		myCanvas.enabled = false;

	}
}
