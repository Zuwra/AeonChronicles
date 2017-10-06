using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public bool Ability;
	public string helpText;

	public Canvas toolbox;
	public GameObject ToolObj;

	CanvasGroup render;
	Coroutine myFade;

	public void OnPointerEnter(PointerEventData eventd)
	{if(Time.timeScale != 0)
		if (toolbox) {
			if (myFade != null) {
				StopCoroutine (myFade);
			}
			myFade= StartCoroutine (toggleWindow( true));
			//toolbox.enabled = true;
		} else {
			ToolObj.SetActive (true);
		}
		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	public void OnPointerExit(PointerEventData eventd)
	{if (toolbox) {
			if (myFade != null) {
				StopCoroutine (myFade);
			}
			myFade =  StartCoroutine (toggleWindow( false));
		//	toolbox.enabled = false;
		}else {
			ToolObj.SetActive (false);
		}
	}


	public void turnOff()
	{
		toolbox.enabled = false;
	}


	void OnDisable()
	{
		if (toolbox) {
			if (render) {
				render.alpha = 0;
			}
			toolbox.enabled = (false);
			//	toolbox.enabled = false;
		}else {
			ToolObj.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
		if (toolbox == null && ToolObj == null) {
			try {
				toolbox = GameObject.Find ("ToolTipBox").GetComponent<Canvas> ();
			} catch (System.Exception) {
			}
		}
		if (toolbox) {
			render = toolbox.GetComponent<CanvasGroup> ();
			if (!render) {
				render = toolbox.gameObject.AddComponent<CanvasGroup> ();
			}
		}
	}


	IEnumerator toggleWindow(  bool onOrOff)
	{
		if (onOrOff) {
			float startalpha = render.alpha /.15f;	
			toolbox.enabled = (onOrOff);
			for (float i = startalpha; i < .15f; i += Time.deltaTime) {

				render.alpha  = (i/.15f);
				yield return null;
			}
			render.alpha  = 1;
		} 

		else {

			for (float i = .3f ; i > 0; i -= Time.deltaTime) {
				render.alpha = (i/.3f);
				yield return null;
			}

			render.alpha  = 0;
			toolbox.enabled = (onOrOff);
		}

	}



}
