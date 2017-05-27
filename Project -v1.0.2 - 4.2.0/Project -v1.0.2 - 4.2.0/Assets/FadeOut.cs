using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour {

	public float blackTime;
	public float fadeLength;
	public Image myImage;
	public Text myText;
	private float fadeStartTime;



	public static FadeOut main;
	// Use this for initialization
	void Awake () {

		main = this;
		fadeStartTime = blackTime;
		myImage.enabled = true;
		myText.enabled = true;

	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Time.timeSinceLevelLoad +"  " +  fadeStartTime);
		if (Time.timeSinceLevelLoad > fadeStartTime) {
			
			myImage.color = new Color (0, 0, 0, 1 - (Time.timeSinceLevelLoad -  fadeStartTime) / fadeLength);
			myText.color = new Color (1, 1, 1, 1 - (Time.timeSinceLevelLoad -  fadeStartTime) / fadeLength);

			if (Time.timeSinceLevelLoad >fadeStartTime + fadeLength) {
				myImage.color = new Color (0, 0, 0, 0);
				myText.color = new Color (0, 0, 0, 0);

				this.enabled = false;
			}
		} else {
			MainCamera.main.goToStart ();
		}

	
	}




	public void startFade(float length)
	{
		
	}

}
