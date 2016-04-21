using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {
	
	[TextArea(3,10)]
	public string text;
	public float duration;

	public AudioClip sound;
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
			InstructionDisplayer.instance.displayText (text, duration, sound, .2f);


			Destroy (this.gameObject);
		}
	}




}
