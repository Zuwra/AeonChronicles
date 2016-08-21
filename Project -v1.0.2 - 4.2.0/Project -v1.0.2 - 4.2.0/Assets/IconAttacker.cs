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
		selManager = GameObject.FindObjectOfType<SelectedManager> ();
		gatling = transform.parent.parent.gameObject;
		healthDisplay = GetComponentInParent<TurretHealthDisplay> ();
	

	}



	public void OnPointerEnter(PointerEventData eventData)
	{
			healthDisplay.PointerI (true);

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		
			healthDisplay.PointerI (false);

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
			
			if (gatling.transform.parent.parent.GetComponent<Selected>().IsSelected) {

				selManager.DeselectAll ();
				selManager.AddObject (gatling.GetComponent<UnitManager> ());
				selManager.CreateUIPages (0);
			} else {
				selManager.DeselectAll ();
				selManager.AddObject (gatling.transform.parent.parent.GetComponent<UnitManager>());
				selManager.CreateUIPages (0);
			}
		}




	}



}
