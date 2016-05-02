using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {
	
	[TextArea(3,10)]
	public string text;
	public float duration;
	public bool dialogue;
	public AudioClip sound;

	[Tooltip("Length of Cutsccene, Set this to 0 so it wont steal the camera, cutscene length not implemented yet")]

	public float stealCamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.GetComponent<UnitManager> ())
		if (other.GetComponent<UnitManager> ().PlayerOwner == 1) {
			if (!dialogue) {
				Debug.Log ("Opening");
				InstructionHelperManager.instance.addBUtton (text, duration);
			}
			else{
				InstructionDisplayer.instance.displayText (text, duration, sound, .2f);
				if (stealCamera > 0) {
					GameObject.FindObjectOfType<MainCamera> ().setCutScene (this.gameObject.transform.position, 120);
				}
			}
			Destroy (this.gameObject);
		}
	}




}
