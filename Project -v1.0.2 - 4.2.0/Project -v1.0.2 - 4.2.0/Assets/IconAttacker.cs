using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class IconAttacker : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler  {

	// Use this for initialization
	private SelectedManager selManager;
	private int playerOwner;
	private GameObject gatling;
	private TurretHealthDisplay healthDisplay;

	void Start()
	{playerOwner = GetComponentInParent<UnitManager> ().PlayerOwner;
		selManager = GameObject.Find ("Manager").GetComponent<SelectedManager> ();
		gatling = transform.parent.parent.gameObject;
		healthDisplay = GetComponentInParent<TurretHealthDisplay> ();
	}



	public void OnPointerEnter(PointerEventData eventData)
	{if (playerOwner != 1) {
			healthDisplay.display ();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (playerOwner != 1) {
			healthDisplay.unDisplay ();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right) {
			if (playerOwner != 1) {
				Debug.Log (gatling);
				selManager.GiveOrder (Orders.CreateInteractCommand (gatling));
				healthDisplay.flash ();
			}


		} else if (eventData.button == PointerEventData.InputButton.Left && !Input.GetKey(KeyCode.LeftShift)) {
			selManager.DeselectAll ();
			selManager.AddObject (gatling.GetComponent<UnitManager>());
			selManager.CreateUIPages (0);
		}




	}



}
