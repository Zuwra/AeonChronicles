using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControlGroupUI : MonoBehaviour {


	private SelectedManager selectM;
	public List<Button> controlList = new List<Button> ();



	// Use this for initialization
	void Start () {
	
		selectM = GameObject.FindObjectOfType<SelectedManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selectGroup(int n)
	{
		selectM.SelectGroup (n);	
	}

	public void activateTab(int n)
	{
		controlList [n].gameObject.SetActive (true);
	}

}
