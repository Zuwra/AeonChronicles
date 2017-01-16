using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {

	public AchievementManager myAchievements;
	public GameObject Panel;

	int currentPage = 0;
	List<GameObject> myPanels = new List<GameObject>();
	public GameObject prevButton;
	public GameObject nextButton;

	void Start()
	{
		LoadPage (0);

	}

	public void nextPage()
	{
		currentPage++;
		if (currentPage > myAchievements.myAchievements.Count / 7) {
			currentPage = 0;
		}
		LoadPage (currentPage);
	}

	public void prevPage()
	{
		currentPage--;
		if (currentPage < 0) {
			currentPage = myAchievements.myAchievements.Count / 7;
		}

		LoadPage (currentPage);
	}

	void LoadPage(int num)
	{
		foreach (GameObject obj in myPanels) {
			Destroy (obj);
		}
		myPanels.Clear ();
	
		for (int i =num*7; i < num*7+Mathf.Min (myAchievements.myAchievements.Count - num*7, 7); i++) {
			GameObject obj = (GameObject)Instantiate (Panel, this.transform);
			obj.transform.FindChild ("Title").GetComponent<Text> ().text = myAchievements.myAchievements [i].Title;
			obj.transform.FindChild ("Description").GetComponent<Text> ().text = myAchievements.myAchievements [i].Description;
			obj.transform.FindChild ("Icon").GetComponent<Image> ().sprite = myAchievements.myAchievements [i].myIcon;

			if (myAchievements.myAchievements [i].IsAccomplished ()) {
				Debug.Log ("Its done");
				obj.transform.FindChild ("Icon").GetComponent<Image> ().material = null;
				obj.transform.FindChild ("Title").GetComponent<Text> ().text  += " -Done!"; 
			} 

			obj.transform.localScale = new Vector3 (1,1,1);
			myPanels.Add (obj);
		}

		SetButtons ();
	}

	void SetButtons()
	{
		prevButton.SetActive (true);
		nextButton.SetActive (true);
		if (currentPage == 0) {
			prevButton.SetActive (false);
		}
		if (currentPage == myAchievements.myAchievements.Count / 7) {
			nextButton.SetActive (false);
		}


	}


}
