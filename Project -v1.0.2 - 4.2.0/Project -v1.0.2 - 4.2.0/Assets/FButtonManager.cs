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
		setButtons ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void setButtons()
	{	if (selectManager.sUnitOne.Length > 0) {
			Ffive.text = "(F5) All " + selectManager.sUnitOne ;
	} else {
		Ffive.text = "";
	}

	if (selectManager.sUnitTwo.Length > 0) {
		Fsix.text = "(F6) All " + selectManager.sUnitTwo;
	}
	else {
		Fsix.text = "";
	}

	if (selectManager.sUnitThree.Length > 0) {
		fSeven.text = "(F7) All " + selectManager.sUnitThree;
	}
	else {
		fSeven.text = "";
	}

	if (selectManager.sUnitFour.Length > 0) {
		fEight.text = "(F8) All " + selectManager.sUnitFour;
	}
	else {
		fEight.text = "";
	}
	}


}
