using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {

	public string text;
	public float duration;
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
			InstructionDisplayer.instance.displayText (text, duration);
			Destroy (this.gameObject);
		}
	}




}
