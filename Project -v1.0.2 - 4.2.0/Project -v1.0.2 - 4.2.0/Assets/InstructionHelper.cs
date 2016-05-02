using UnityEngine;
using System.Collections;

public class InstructionHelper : MonoBehaviour {

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

	public void exit()
	{
		Destroy (this.gameObject);
	}

	public void OpenInstruction()
	{
		InstructionDisplayer.instance.displayText (text, duration, sound, .2f);
		Destroy (this.gameObject);
	}
}
