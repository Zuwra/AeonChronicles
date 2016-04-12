using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class IconAttacker : MonoBehaviour, IPointerClickHandler  {

	// Use this for initialization
	private SelectedManager selManager;
	private int playerOwner;
	private GameObject gatling;

	void Start()
	{playerOwner = GetComponentInParent<UnitManager> ().PlayerOwner;
		selManager = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
		gatling = transform.parent.parent.gameObject;
	}



	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right) {
			if (playerOwner != 1) {
				Debug.Log (gatling);
				selManager.GiveOrder (Orders.CreateInteractCommand(gatling));
			}


		}




	}



}
