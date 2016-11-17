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



	public void displayText(string input,  Sprite pic)
	{	

		this.enabled = true;
		myText.text = input;
		myCanvas.enabled = true;

	
		//Debug.Log ("This is " + this.gameObject);
		if (pic) {
			personPic.sprite = pic;
		} else {
			Debug.Log ("The pic is null bro");
		}

	}

	public void TurnOff()
	{
		if (this) {
			this.enabled = false;
			myCanvas.enabled = false;
		}

	}
}
