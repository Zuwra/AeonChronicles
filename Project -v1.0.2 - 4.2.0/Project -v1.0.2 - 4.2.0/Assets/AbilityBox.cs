using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

	public class AbilityBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

		private bool pointerInside;
		// Testing comment for first time sync

		private Canvas toolbox;
	public Ability myAbility;
	CostBox daCostBox;


		public void OnPointerEnter(PointerEventData eventd)
		{
			pointerInside = true;
			//toolbox.enabled = true;
		if (myFade != null) {
			StopCoroutine (myFade);
		}
		myFade= StartCoroutine (toggleWindow( true));

		toolbox.GetComponent<CostBox> ().setText(myAbility);
		}

		public void OnPointerExit(PointerEventData eventd)
		{
			pointerInside = false;
		//if (myFade != null) {
		//	StopCoroutine (myFade);
		//}
		//myFade =  StartCoroutine (toggleWindow( false));

			toolbox.enabled = false;
		}


	CanvasGroup render;
	Coroutine myFade;

		// Use this for initialization
		void Start () {
			toolbox = GameObject.Find ("AbilityBox").GetComponent<Canvas>();
		daCostBox = toolbox.GetComponent<CostBox> ();

		render = toolbox.GetComponent<CanvasGroup> ();
		if (!render) {
			render = toolbox.gameObject.AddComponent<CanvasGroup> ();
		}


		}

		// Update is called once per frame
		void Update () {
			if (pointerInside) {

			daCostBox.setText(myAbility);
				//toolbox.transform.position = new Vector3 (Input.mousePosition.x + 105, Input.mousePosition.y + 70, 0);

			}
		}
	IEnumerator toggleWindow(  bool onOrOff)
	{
		if (onOrOff) {
			float startalpha = render.alpha /.3f;	
			toolbox.enabled = (onOrOff);
			for (float i = startalpha; i < .3f; i += Time.deltaTime) {

				render.alpha  = (i/.3f);

				yield return null;
			}
			render.alpha  = 1;
		} 

		else {

			for (float i = .1f ; i > 0; i -= Time.deltaTime) {
	
				render.alpha = (i/.1f);
				yield return null;
			}

			render.alpha  = 0;
			toolbox.enabled = (onOrOff);
		}

	}

	void OnDisable()
	{
		if (toolbox) {
			toolbox.enabled = false;
		}
	}



	}
