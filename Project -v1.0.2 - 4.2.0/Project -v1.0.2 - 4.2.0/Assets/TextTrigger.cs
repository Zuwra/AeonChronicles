using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextTrigger : SceneEventTrigger {
	
	[TextArea(3,10)]
	public string text;
	public float duration;
	public bool dialogue = true;
	public int Priority =4;
	public AudioClip sound;
	public Sprite myPic;
	[HideInInspector]
	public List<int> VoiceLines = new List<int>();


	[Tooltip("Length of Cutsccene, Set this to 0 so it wont steal the camera, cutscene length not implemented yet")]

	public float stealCamera;

	public void triggerMe (){
		Debug.Log ("Triggering " + this.gameObject);
		if (!hasTriggered) {
			hasTriggered = true;

			if (!dialogue) {
				

				InstructionHelperManager.instance.addBUtton (text, duration, myPic);
				//UIHighLight.main.highLight (null, 0);
			} else {
				
				//	Debug.Log ("Triggering from " + this.gameObject.name);
				if (VoiceLines.Count > 0) {
					foreach (int i in VoiceLines) {
					//	Debug.Log ("Playing " + i);
						dialogManager.instance.playLine (i);
					}

				} else {
					Debug.Log ("Only one");
				//	dialogManager.instance.playLine (VoiceLineIndex);
				}
				//ExpositionDisplayer.instance.displayText (text, duration, sound, .93f, myPic,Priority);
				if (stealCamera > 0) {
					MainCamera.main.setCutScene (this.gameObject.transform.position, 120);
				}
			}
		}

	}


	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){

		//Debug.Log ("Triggering " + this.gameObject);
		if (!hasTriggered) {
			hasTriggered = true;

			if (!dialogue) {
				
				InstructionHelperManager.instance.addBUtton (text, duration, myPic);

			}else {

					foreach (int i in VoiceLines) {
						dialogManager.instance.playLine (i);
					}

			
				//ExpositionDisplayer.instance.displayText (text, duration, sound, .93f, myPic,Priority);
				if (stealCamera > 0) {
					MainCamera.main.setCutScene (this.gameObject.transform.position, 120);
				}
			}
		}

	}




}
