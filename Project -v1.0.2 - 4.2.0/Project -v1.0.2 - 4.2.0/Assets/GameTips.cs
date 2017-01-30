using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameTips : MonoBehaviour {

	public Text myText;

	public TextHolder textHold;
	private int currentIndex;


	// Use this for initialization
	void Start () {
		currentIndex = Random.Range (0, textHold.myTexts [0].myTexts.Count);
		myText.text =  textHold.myTexts [0].myTexts [currentIndex];
	
	}
	


	public void nextTip()
	{
		currentIndex++;
		if (currentIndex == textHold.myTexts [0].myTexts.Count) {
			currentIndex = 0;
		}
		myText.text = textHold.myTexts [0].myTexts [currentIndex];
	}

	public void prevTip()
	{
		currentIndex++;
		if (currentIndex ==-1) {
			currentIndex = textHold.myTexts [0].myTexts.Count -1;
		}
		myText.text = textHold.myTexts [0].myTexts [currentIndex];

	}


}
