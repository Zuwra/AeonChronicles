using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {
	
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

	void OnTriggerEnter(Collider other)
	{

		if (other.GetComponent<UnitManager> ())
		if (other.GetComponent<UnitManager> ().PlayerOwner == 1) {
			if (!dialogue) {
				
				InstructionHelperManager.instance.addBUtton (text, duration, myPic);
			}
			else{
				ExpositionDisplayer.instance.displayText (text, duration, sound, .2f, myPic);
				if (stealCamera > 0) {
					GameObject.FindObjectOfType<MainCamera> ().setCutScene (this.gameObject.transform.position, 120);
				}
			}
			Destroy (this.gameObject);
		}
	}




}
