using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableButton : MonoBehaviour, IPointerClickHandler {

	public int abilityNumber;
	private SelectedManager selManager;

	void Start()
	{
		selManager = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right) {
			selManager.setAutoCast(abilityNumber);
			selManager.AutoCastUI ();
		
		}




	}




}
