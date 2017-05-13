using UnityEngine;
using System.Collections;

public class InstructionHelper : MonoBehaviour {

	[TextArea(3,10)]
	public string text;
	public float duration;

	public AudioClip sound;
	public Sprite myPic;


	public void exit()
	{
		Destroy (this.gameObject);
	}

	public void OpenInstruction()
	{
		InstructionDisplayer.instance.displayText (text, duration, sound, .2f, myPic);
		Destroy (this.gameObject);
	}
}
