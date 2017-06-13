using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CampTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public string Title;
	public string helpText;


	public Canvas toolbox;
	public Text titleText;
	public Text Description;

	public void OnPointerEnter(PointerEventData eventd)
	{
		if (toolbox) {
			titleText.text = Title;
			Description.text = helpText;
			toolbox.enabled = true;
			titleText.transform.parent.position = new Vector3 (titleText.transform.parent.position.x,transform.position.y, titleText.transform.parent.position.z);
		} 
		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	public void OnPointerExit(PointerEventData eventd)
	{if (toolbox) {
			toolbox.enabled = false;
	}
}


	public void turnOff()
	{
		toolbox.enabled = false;
	}






}
