using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine.Serialization;
public class NewUnitPanel : MonoBehaviour {


	public bool activeOnStart;
	public List<NewUnit> units = new List<NewUnit> ();
	public GameObject nextButton;
	public GameObject prevButton;
	public Text myTitle;
	public Text mydescript;
	public Image myImage;

	private int index =0;

	[System.Serializable]
	public struct NewUnit{
		public Color myColor;
		[TextArea(2,10)]
		public string Title;
		[TextArea(2,10)]
		public string Description;
		public Sprite myPic;


	}

	public static NewUnitPanel main;

	// Use this for initialization
	void Start () {
		main = this;
		previous ();

		if (activeOnStart) {
			GetComponent<Canvas> ().enabled = true;
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void next()
	{index++;
		if (index == units.Count - 1) {

			nextButton.SetActive (false);
		} else if (index == units.Count) {
			index--;
		}
		if (index > 0) {
			prevButton.SetActive (true);
		}
		loadUnit (index);
	}

	public void previous()
	{
		index--;
		if (index != units.Count-1 && units.Count > 1 ) {
	
			nextButton.SetActive (true);
		}
		if (index == -1) {
			index = 0;
		}
		if (index ==0) {
			prevButton.SetActive (false);
		}
		loadUnit (index);
	}

	public void loadUnit(int i)
	{
		myTitle.color = units [i].myColor;
		myTitle.text = units [i].Title;
		mydescript.text = units [i].Description;
		myImage.sprite = units [i].myPic;

	}

	public void exit()
	{Time.timeScale = 1;
		GetComponent<Canvas> ().enabled = false;

	}




}
