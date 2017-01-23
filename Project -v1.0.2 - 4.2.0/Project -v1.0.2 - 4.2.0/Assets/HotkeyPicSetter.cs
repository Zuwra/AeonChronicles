using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HotkeyPicSetter : MonoBehaviour {

	public List<Sprite> myPics;
	public Image myImage;
	int currentPage = 0;

	public void nextPic()
	{
		currentPage++;
		if (currentPage == myPics.Count) {
			currentPage = 0;
		}
		loadPage ();
	}

	public void prevPic()
	{
		currentPage--;
		if (currentPage == -1) {
			currentPage = myPics.Count - 1;
		}
		loadPage ();
	}

	void loadPage()
	{
		myImage.sprite = myPics [currentPage];

	}
}
