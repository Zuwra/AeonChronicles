using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExpositionTexts : MonoBehaviour {


	public List<textPiece> theStrings = new List<textPiece>();

	public Text textArea;
	public Text continueButton;
	public Image backGrounda;
	public Image backGroundb;
	public GameObject StartButton;
	public float fadeSpeed;
	private Color startColor;


	public AudioSource audioPlayer;
	[System.Serializable]
	public struct textPiece{
		
		public float timeLength;
		[TextArea(2,10)]
		public string myText;
		public Sprite pic;
		public AudioClip myclip;

	}


	private float nextActionTime;
	private int currentText = -1;


	public void nextText()
	{if (theStrings.Count-1 > currentText) {
		currentText++;
		
			nextActionTime = Time.time + theStrings [currentText].timeLength;

			if (theStrings [currentText].pic) {
				backGrounda.sprite = backGroundb.sprite;
					
				backGroundb.sprite = theStrings [currentText].pic;
				Color c = Color.clear;
				c.r = 1;
				c.g = 1;
				c.b = 1;
				backGroundb.color = c;

				StartCoroutine (FadeIn ());
			} else {
				
				textArea.text = theStrings [currentText].myText;
			
			}

			if (theStrings.Count - 1 == currentText) {
			//	StartButton.SetActive (true);
				//continueButton.text = "Continue";
				
			}

		} else {
		
			nextActionTime += 1000000;
		}
	}

	// Use this for initialization
	void Start () {
		startColor = textArea.color;
		if (theStrings.Count > 0) {
			nextText ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {
		//	nextText ();
		}

		if (Time.time > nextActionTime) {
			nextText ();
		}
	
	}

	public void continueButtonPressed()
	{Debug.Log (currentText);
		if(theStrings.Count-1 == currentText )
		{
			SceneManager.LoadScene (0);
		}
		else{
			nextText();
		}
	}

	IEnumerator FadeIn()
	{audioPlayer.PlayOneShot (theStrings [currentText].myclip);
		while (backGroundb.color.a < 1) {
// Fade in new background 
			Color c = backGroundb.color;

			c.a += (1/fadeSpeed) * Time.deltaTime;
			backGroundb.color = c;

			// Fade out and in text
			if (currentText != 0) {
				if (backGroundb.color.a < .5f) {
					Color n = startColor;
					n.a = 1 - (2 * backGroundb.color.a);
					textArea.color = n;
				} else {
					textArea.text = theStrings [currentText].myText;

					Color n = startColor;
					n.a = (2 * backGroundb.color.a) - 1;
					textArea.color = n;
				}
			} 
			else {
				
				textArea.text = theStrings [currentText].myText;
				Color n = startColor;
				n.a =  ( backGroundb.color.a);
				textArea.color = n;
			}



			yield return new WaitForSeconds(.01f);

		}
		if (theStrings.Count - 1 == currentText) {
			StartButton.SetActive (true);
			continueButton.text = "Continue";

		}
		//audioPlayer.PlayOneShot (theStrings [currentText].myclip);
		yield return new WaitForSeconds(.1f);
		//return null;
	}




}
