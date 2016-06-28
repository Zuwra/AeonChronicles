using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameTips : MonoBehaviour {

	public Text myText;
	public List<string> myTips = new List<string> ();
	private int currentIndex;


	// Use this for initialization
	void Start () {
		currentIndex = Random.Range (0, myTips.Count);
		myText.text = myTips [currentIndex];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void nextTip()
	{
		currentIndex++;
		if (currentIndex == myTips.Count) {
			currentIndex = 0;
		}
		myText.text = myTips [currentIndex];
	}

	public void prevTip()
	{
		currentIndex++;
		if (currentIndex ==-1) {
			currentIndex = myTips.Count -1;
		}
		myText.text = myTips [currentIndex];

	}


}
