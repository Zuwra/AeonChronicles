using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	private bool pointerInside;
	public bool Ability;
	public string helpText;

	private Canvas toolbox;

	public void OnPointerEnter(PointerEventData eventd)
	{
		pointerInside = true;
		toolbox.enabled = true;
		toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	public void OnPointerExit(PointerEventData eventd)
	{
		pointerInside = false;
		toolbox.enabled = false;
	}



	// Use this for initialization
	void Start () {
		toolbox = GameObject.Find ("ToolTipBox").GetComponent<Canvas>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (pointerInside) {


			toolbox.transform.position = new Vector3 (Input.mousePosition.x + 100, Input.mousePosition.y + 55, 0);

		}
	}



}
