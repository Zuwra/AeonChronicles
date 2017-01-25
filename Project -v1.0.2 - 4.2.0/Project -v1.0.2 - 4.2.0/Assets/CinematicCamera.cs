using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinematicCamera : SceneEventTrigger {

	// Use this for initialization

	public List<scene> myScenes = new List<scene> ();
	public bool showPoints;

	public static CinematicCamera main;
	private int currentScene = -1;
	private int currentShot = 0;
	private float shotChangeTime;
	//private float shotStartTime;

	private float nextDialogue;
	private bool hasNextDialogue;
	Vector3 previousCamPos;


	void Start () {
		main = this;

	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Return) && currentScene >-1) {
			exitScene ();
		
		}

		if (currentScene > -1) {
			if (Time.time > shotChangeTime) {
				//NEW SHOT

				//shotStartTime = Time.time;
				currentShot++;
				if (currentShot == myScenes [currentScene].myShots.Count) {
					exitScene ();
					return;
				} else {
					
					shotChangeTime = Time.time + myScenes [currentScene].myShots [currentShot].duration* GameSettings.gameSpeed;
					if (myScenes [currentScene].myShots [currentShot].dialogueLength > 0) {
						hasNextDialogue = true;
						nextDialogue = Time.time + myScenes [currentScene].myShots [currentShot].dialogueStartDelay* GameSettings.gameSpeed;
					}
				}
			}

			if (hasNextDialogue && Time.time > nextDialogue) {
				hasNextDialogue = false;
				ExpositionDisplayer.instance.displayText (myScenes [currentScene].myShots [currentShot].DialogueText,myScenes [currentScene].myShots [currentShot].duration
					,myScenes [currentScene].myShots [currentShot].dialogueAudio, .8f,myScenes [currentScene].myShots [currentShot].dialogueImage,5);
			
			}
			float timeThroughShot = (shotChangeTime - Time.time) / myScenes [currentScene].myShots [currentShot].duration;
		//	Debug.Log ("Shot time is " + timeThroughShot);
			float perc = timeThroughShot;// myScenes [currentScene].myShots [currentShot].myCurve.Evaluate( timeThroughShot);
			this.transform.position = Vector3.Lerp
				(myScenes [currentScene].myShots [currentShot].startLocation, myScenes [currentScene].myShots [currentShot].endLocation,1 -perc );

			this.transform.LookAt(Vector3.Lerp
				(myScenes [currentScene].myShots [currentShot].startTarget, myScenes [currentScene].myShots [currentShot].endTarget, 1 - perc ));
		
		
		}

	
	}


	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		previousCamPos = MainCamera.main.gameObject.transform.position;


	GetComponent<Camera> ().enabled = true;
		currentScene = index;
		shotChangeTime = Time.time + myScenes [currentScene].myShots [currentShot].duration* GameSettings.gameSpeed;
		if (myScenes [currentScene].myShots [currentShot].dialogueLength > 0) {
			hasNextDialogue = true;
			nextDialogue = Time.time + myScenes [currentScene].myShots [currentShot].dialogueStartDelay * GameSettings.gameSpeed;
		}
		if (GameMenu.main) {
			GameMenu.main.disableInput ();}

	}

	public void exitScene(){
		
		foreach(SceneEventTrigger trig in myScenes[currentScene].nextTrig){

			trig.trigger (0, 0, Vector3.zero, null, false);
		}
		currentScene = -1;
		GetComponent<Camera> ().enabled = false;
		MainCamera.main.gameObject.transform.position = previousCamPos ;
		if (GameMenu.main) {
			GameMenu.main.EnableInput();}

	}




	[System.Serializable]
	public struct scene{
		public List<shot> myShots;
		public List<SceneEventTrigger> nextTrig;



	}
	[System.Serializable]
	public struct shot{
		public Vector3 startLocation;
		public Vector3 startTarget;
		public Vector3 endLocation;
		public Vector3 endTarget;
		public float duration;
		public AnimationCurve myCurve;

		public Sprite dialogueImage;
		public float dialogueStartDelay;
		public float dialogueLength;
		public AudioClip dialogueAudio;
		[TextArea(1,10)]
		public string DialogueText;

	}


	public void OnDrawGizmos()
	{if (showPoints) {

			foreach (scene s in myScenes) {
				foreach(shot curr in s.myShots)
				{Gizmos.color = Color.blue;
					Gizmos.DrawLine (curr.startLocation,(curr.startTarget));
					Gizmos.DrawSphere (curr.startLocation, 5);
					Gizmos.color = Color.red;
					Gizmos.DrawSphere (curr.endLocation, 5);
					Gizmos.DrawLine (curr.endLocation, curr.endTarget);
				}
			
			}
		
		}
	}





}
