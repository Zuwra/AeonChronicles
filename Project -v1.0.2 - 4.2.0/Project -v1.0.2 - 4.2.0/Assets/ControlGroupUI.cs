using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControlGroupUI : MonoBehaviour {


	private SelectedManager selectM;
	public List<Button> controlList = new List<Button> ();



	// Use this for initialization
	void Start () {
	
		selectM = SelectedManager.main;;
	}
	


	public void selectGroup(int n)
	{
		selectM.SelectGroup (n);	
	}

	public void activateTab(int n, int count, Sprite icon)
	{
		
		controlList [n].gameObject.SetActive (true);
		//if (controlList [n].gameObject.activeInHierarchy) {
			controlList [n].gameObject.transform.Find ("Count").GetComponent<Text> ().text = "" + count;
			controlList [n].gameObject.transform.Find ("Image").GetComponent<Image> ().sprite = icon;
		//}
	}


	public void deactivate(int n)
	{
		controlList [n].gameObject.SetActive (false);

	}

}
