using UnityEngine;
using System.Collections;

public class AchievementChecker : MonoBehaviour {

	public AchievementManager myAchievements;


	// Use this for initialization
	public void Start () {
		foreach (Achievement ach in myAchievements.myAchievements) {
			ach.CheckBeginning ();
		}
	}
	
	public void EndLevel()
	{
		foreach (Achievement ach in myAchievements.myAchievements) {
			ach.CheckEnd();
		}
	}
}
