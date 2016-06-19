using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinematicCamera : MonoBehaviour {

	// Use this for initialization

	public List<scene> myScenes = new List<scene> ();
	public bool showPoints;

	public static CinematicCamera main;
	private int currentScene = -1;
	public int currentShot = 0;
	private float sceneChangeTime;
	private float sceneStartTime;


	void Start () {
		main = this;

	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentScene > -1) {
			if (Time.time > sceneChangeTime) {
				sceneStartTime = Time.time;
				currentShot++;
				if (currentShot == myScenes [currentScene].myShots.Count) {
					exitScene ();
					return;
				} else {
					sceneChangeTime = Time.time + myScenes [currentScene].myShots [currentShot].duration;
				}
			}

			float perc = (sceneChangeTime - Time.time) / myScenes [currentScene].myShots [currentShot].duration;
			this.transform.position = Vector3.Lerp
				(myScenes [currentScene].myShots [currentShot].startLocation, myScenes [currentScene].myShots [currentShot].endLocation,1 -perc );

			this.transform.LookAt(Vector3.Lerp
				(myScenes [currentScene].myShots [currentShot].startTarget, myScenes [currentScene].myShots [currentShot].endTarget, 1 - perc ));
		
		
		}

	
	}

	public void startScene(int sceneNum)
	{GetComponent<Camera> ().enabled = true;
		currentScene = sceneNum;
		sceneChangeTime = Time.time + myScenes [currentScene].myShots [currentShot].duration;
	}

	public void exitScene(){
		currentScene = -1;
		GetComponent<Camera> ().enabled = false;
	}




	[System.Serializable]
	public struct scene{
		public List<shot> myShots;



	}
	[System.Serializable]
	public struct shot{
		public Vector3 startLocation;
		public Vector3 startTarget;
		public Vector3 endLocation;
		public Vector3 endTarget;
		public float duration;

	}


	public void OnDrawGizmos()
	{if (showPoints) {

			foreach (scene s in myScenes) {
				foreach(shot curr in s.myShots)
				{Gizmos.color = Color.blue;
					Gizmos.DrawLine (curr.startLocation,(curr.startTarget));
					Gizmos.DrawSphere (curr.startLocation, 10);
					Gizmos.color = Color.red;
					Gizmos.DrawSphere (curr.endLocation, 10);
					Gizmos.DrawLine (curr.endLocation, curr.endTarget);
				}
			
			}
		
		}
	}





}
