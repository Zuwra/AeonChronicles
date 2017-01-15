using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {


	public GameObject Panel;
	public List<Achievement> myAchievements;



	void Start()
	{
		for (int i = 0; i < Mathf.Min (myAchievements.Count, 6); i++) {
			GameObject obj = (GameObject)Instantiate (Panel, this.transform);
			obj.transform.FindChild ("Title").GetComponent<Text> ().text = myAchievements [i].Title;
			obj.transform.FindChild ("Description").GetComponent<Text> ().text = myAchievements [i].Description;
			obj.transform.FindChild ("Icon").GetComponent<Image> ().sprite = myAchievements [i].myIcon;

			if (myAchievements [i].IsAccomplished ()) {
				obj.transform.FindChild ("Icon").GetComponent<Image> ().material = null;
				obj.transform.FindChild ("Title").GetComponent<Text> ().text  += " -Done!"; 
			} 

			obj.transform.localScale = new Vector3 (1,1,1);
		}
	}
}
