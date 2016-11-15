using UnityEngine;
using System.Collections;

public class TextTrigger : SceneEventTrigger {
	
	[TextArea(3,10)]
	public string text;
	public float duration;
	public bool dialogue;
	public AudioClip sound;
	public Sprite myPic;
	[Tooltip("Length of Cutsccene, Set this to 0 so it wont steal the camera, cutscene length not implemented yet")]

	public float stealCamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		if (!hasTriggered) {
			hasTriggered = true;

			if (!dialogue) {
				
				InstructionHelperManager.instance.addBUtton (text, duration, myPic);
				//UIHighLight.main.highLight (null, 0);
			} else {
				ExpositionDisplayer.instance.displayText (text, duration, sound, .93f, myPic);
				if (stealCamera > 0) {
					GameObject.FindObjectOfType<MainCamera> ().setCutScene (this.gameObject.transform.position, 120);
				}
			}
		}

	}




}
