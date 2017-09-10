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

	public Dropdown Earning;
	public Dropdown WhichLevel;

	Achievement.Earnings myEarning = Achievement.Earnings.all;
	Achievement.Level myLevel = Achievement.Level.all;


	List<Achievement> currentAchievments;
	void Start()
	{
		currentAchievments = myAchievements.myAchievements;
		setEarnings ();


		LoadPage (0);

	}

	void setSettings()
	{
		switch (Earning.value) {
		case 0:
			myEarning = Achievement.Earnings.all;
			break;
		case 1:
			myEarning = Achievement.Earnings.unearned;
			break;
		case 2:
			myEarning = Achievement.Earnings.earned;
			break;
		}
	
		switch (WhichLevel.value) {
		case 0:
			myLevel = Achievement.Level.all;
			break;

		case 1:
			myLevel = Achievement.Level.one;
			break;
		case 2:
			myLevel = Achievement.Level.two;
			break;
		case 3:
			myLevel = Achievement.Level.three;
			break;
		case 4:
			myLevel = Achievement.Level.four;
			break;
		case 5:
			myLevel = Achievement.Level.five;
			break;
		case 6:
			myLevel = Achievement.Level.six;
			break;
		case 7:
			myLevel = Achievement.Level.seven;
			break;

		case 8:
			myLevel = Achievement.Level.eight;
			break;
		case 9:
			myLevel = Achievement.Level.anyLevel;
			break;
		case 10:
			myLevel = Achievement.Level.campaign;
			break;

		}
	}



	public void setEarnings()
	{setSettings ();
		currentPage = 0;
		List<Achievement> newAchievements = new List<Achievement> ();
		foreach (Achievement ach in myAchievements.myAchievements) {

			if ((int)ach.myLevel < LevelData.getHighestLevel () +2) {
				if (AddForLevel (ach) && AddForEarning (ach)) {
					newAchievements.Add (ach);
				}
			}
		}
		currentAchievments = newAchievements;
		LoadPage (currentPage);
	}

	public bool AddForEarning(Achievement ach)
	{
		if (myEarning == Achievement.Earnings.all) {
			return true;}
		
		bool isFinished = ach.IsAccomplished();
		if (isFinished && myEarning == Achievement.Earnings.earned) {
			return true;}
		
		if (!isFinished && myEarning == Achievement.Earnings.unearned) {
			return true;}
		
		return false;
	}

	public bool AddForLevel(Achievement ach)
	{
		if (myLevel == Achievement.Level.all) {
			return true;}

		if (ach.myLevel == myLevel) {
			return true;}

		if (myLevel == Achievement.Level.anyLevel && ach.myLevel != Achievement.Level.campaign) {
			return true;}

		return false;
	}




	public void nextPage()
	{

		if (currentPage < currentAchievments.Count / 6) {
			currentPage++;
		}
		LoadPage (currentPage);
	}

	public void prevPage()
	{
		if (currentPage > -1) {
			currentPage--;

		}
		LoadPage (currentPage);
	}

	void LoadPage(int num)
	{
		foreach (GameObject obj in myPanels) {
			Destroy (obj);
		}
		myPanels.Clear ();
	
		for (int i =num*6; i < num*6+Mathf.Min (currentAchievments.Count - num*6, 6); i++) {
			GameObject obj = (GameObject)Instantiate (Panel, this.transform);
			obj.transform.Find ("Title").GetComponent<Text> ().text = currentAchievments [i].Title;
			obj.transform.Find ("Description").GetComponent<Text> ().text = currentAchievments [i].GetDecription();
			obj.transform.Find ("Icon").GetComponent<Image> ().sprite = currentAchievments [i].myIcon;
			obj.transform.Find ("Reward").GetComponent<Text> ().text = currentAchievments [i].getRewardText ();

			if (currentAchievments[i].IsAccomplished ()) {
				
				obj.transform.Find ("Icon").GetComponent<Image> ().material = null;
				obj.transform.Find ("Title").GetComponent<Text> ().text  += "   -Done!"; 
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
		if (currentPage == (currentAchievments.Count -1) / 6) {
			nextButton.SetActive (false);
		}
	}


}
