using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ControlGroupUI : MonoBehaviour {


	private SelectedManager selectM;
	public List<Button> controlList = new List<Button> ();

	public static ControlGroupUI instance;

	// Use this for initialization
	void Start () {
		instance = this;
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

	public void pressButton(int n)
	{
		var pointer = new PointerEventData (EventSystem.current);
		ExecuteEvents.Execute (controlList [n].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute (controlList [n].gameObject, pointer, ExecuteEvents.pointerDownHandler);
		StartCoroutine (delayedClick(n));

	}

	IEnumerator delayedClick(int n)
	{var pointer = new PointerEventData (EventSystem.current);
		controlList [n].GetComponent<ToolTip> ().turnOff ();
		yield return new WaitForSeconds (.1f);
		ExecuteEvents.Execute (controlList [n].gameObject, pointer, ExecuteEvents.pointerClickHandler);
		ExecuteEvents.Execute (controlList [n].gameObject, pointer, ExecuteEvents.pointerExitHandler);


	}

}
