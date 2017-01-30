using UnityEngine;
using System.Collections;

public class InstructionHelperManager : MonoBehaviour {


	public GameObject button;
	public static InstructionHelperManager instance;
	// Use this for initialization
	void Start () {
		instance = this;
	
	}


	public  void addBUtton( string text, float duration, Sprite pic)
	{
		GameObject obj = (GameObject)Instantiate (instance.button);
		obj.transform.SetParent (instance.transform);
		obj.GetComponent<InstructionHelper> ().text = text;
		obj.GetComponent<InstructionHelper> ().duration =duration;

		obj.GetComponent<InstructionHelper> ().myPic = pic;


	}


}
