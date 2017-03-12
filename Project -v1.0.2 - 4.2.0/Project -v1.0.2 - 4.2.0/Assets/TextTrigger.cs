using UnityEngine;
using System.Collections;

public class TextTrigger : SceneEventTrigger {
	
	[TextArea(3,10)]
	public string text;
	public float duration;
	public bool dialogue;
	public int Priority =4;
	public AudioClip sound;
	public Sprite myPic;
	[Tooltip("Length of Cutsccene, Set this to 0 so it wont steal the camera, cutscene length not implemented yet")]

	public float stealCamera;


	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){

		Debug.Log("Triggering on " + this.gameObject);
		if (!hasTriggered) {
			hasTriggered = true;

			if (!dialogue) {
				
				InstructionHelperManager.instance.addBUtton (text, duration, myPic);
				//UIHighLight.main.highLight (null, 0);
			} else {
				Debug.Log("Calling from  " + this.gameObject);
				ExpositionDisplayer.instance.displayText (text, duration, sound, .93f, myPic,Priority);
				if (stealCamera > 0) {
					GameObject.FindObjectOfType<MainCamera> ().setCutScene (this.gameObject.transform.position, 120);
				}
			}
		}

	}




}
