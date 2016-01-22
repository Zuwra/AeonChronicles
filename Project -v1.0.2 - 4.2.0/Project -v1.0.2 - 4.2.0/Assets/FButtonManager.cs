using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FButtonManager : MonoBehaviour {

	public Text Ffive;
	public Text Fsix;
	public Text fSeven;
	public Text fEight;


	SelectedManager selectManager;


	// Use this for initialization
	void Start () {
		selectManager = GameObject.Find ("Manager").GetComponent<SelectedManager>();
		//setButtons ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
