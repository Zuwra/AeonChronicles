using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceObjective : Objective {


	public RaceObjective RootObj;

	public float RaceStartTime;

	public LineRenderer lineRend;
	float startTime;
	string rawObjectText;
	void Start()
	{
		if (!RootObj) {
			Debug.Log ("I am on " + gameObject);
			startTime = Time.time;
			VictoryTrigger.instance.addObjective (this);
			rawObjectText = description;
			if (checkPoints != null) {
				foreach (GameObject obj in checkPoints) {
					RaceObjective racer = obj.AddComponent<RaceObjective> ();
					racer.RootObj = this;
				}
			}
			InvokeRepeating ("UpdateObj", 1, 1);
		}
	}

	public void UpdateObj()
	{
		float timeleft = RaceStartTime - (Time.time - startTime);
		if (timeleft < 0) {
			timeleft = 0;
		}

		description = rawObjectText + " " + Clock.convertToString(timeleft);
		VictoryTrigger.instance.UpdateObjective (this);
		if (timeleft < 0) {
			complete ();
			CancelInvoke ("UpdateObj");
		}
	}


	public List<GameObject> checkPoints;

	int currentCheckPoint = 0;


	public void hitCheckPoint( UnitManager manager)
	{

		if (manager.PlayerOwner == 1) {
			checkPoints [currentCheckPoint].SetActive (false);

			currentCheckPoint++;
			if (checkPoints.Count > currentCheckPoint) {
				if (lineRend) {
					lineRend.SetPositions (new Vector3[] {
						checkPoints [currentCheckPoint-1].transform.position,
						checkPoints [currentCheckPoint ].transform.position
					});
				}
				checkPoints [currentCheckPoint].SetActive (true);
			} else {
				lineRend.enabled = false;
				VictoryTrigger.instance.Win ();
			}

		} 
	}

	void OnTriggerEnter(Collider col)
	{
		
		UnitManager manag = col.GetComponent<UnitManager> ();
		if (manag) {
			if (RootObj) {
				RootObj.hitCheckPoint (manag);
			}	
		}
	}
}
