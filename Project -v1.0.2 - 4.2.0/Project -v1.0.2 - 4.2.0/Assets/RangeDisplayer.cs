using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RangeDisplayer : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler {


	private SelectedManager selection;
	// Use this for initialization
	void Start () {
		selection = GameObject.FindObjectOfType<SelectedManager> ();
	}
	

	public void OnPointerEnter(PointerEventData eventd)
	{
		
		selection.toggleRangeIndicator (true);
		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	public void OnPointerExit(PointerEventData eventd)
	{
		selection.toggleRangeIndicator (false);
	}





}
