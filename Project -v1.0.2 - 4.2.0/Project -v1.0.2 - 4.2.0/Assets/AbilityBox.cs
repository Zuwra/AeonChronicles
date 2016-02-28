using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

	public class AbilityBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

		private bool pointerInside;
		// Testing comment for first time sync

		private Canvas toolbox;
	public Ability myAbility;

		public void OnPointerEnter(PointerEventData eventd)
		{
			pointerInside = true;
			toolbox.enabled = true;
		toolbox.GetComponent<CostBox> ().setText(myAbility);
		}

		public void OnPointerExit(PointerEventData eventd)
		{
			pointerInside = false;
			toolbox.enabled = false;
		}



		// Use this for initialization
		void Start () {
			toolbox = GameObject.Find ("AbilityBox").GetComponent<Canvas>();

		}

		// Update is called once per frame
		void Update () {
			if (pointerInside) {


				//toolbox.transform.position = new Vector3 (Input.mousePosition.x + 105, Input.mousePosition.y + 70, 0);

			}
		}



	}
