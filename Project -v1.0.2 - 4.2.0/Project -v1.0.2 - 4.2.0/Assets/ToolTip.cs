using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public bool Ability;
	public string helpText;

	public Canvas toolbox;
	public GameObject ToolObj;

	public void OnPointerEnter(PointerEventData eventd)
	{
		if (toolbox) {
			toolbox.enabled = true;
		} else {
			ToolObj.SetActive (true);
		}
		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	public void OnPointerExit(PointerEventData eventd)
	{if (toolbox) {
			toolbox.enabled = false;
		}else {
			ToolObj.SetActive (false);
		}
	}


	public void turnOff()
	{
		toolbox.enabled = false;
	}


	// Use this for initialization
	void Start () {
		if (toolbox == null) {
			try{
				toolbox = GameObject.Find ("ToolTipBox").GetComponent<Canvas> ();}
			catch(System.Exception){}
		}
	}



}
