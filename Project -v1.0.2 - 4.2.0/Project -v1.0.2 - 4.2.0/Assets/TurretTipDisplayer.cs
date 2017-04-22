using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class TurretTipDisplayer: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public string help;
	public string cantBuild;
	public Sprite image;
	public Sprite badImage;
	private Button myButt;

	bool mouseIn;

	public TurretPlacer myPlacer;
	void Start(){
		myButt = GetComponent<Button> ();
	}

	public void OnPointerEnter(PointerEventData eventd)
	{mouseIn = true;

		if (myButt.interactable) {
			if (myPlacer && myPlacer.factCount()) {
				TurretUIPanel.instance.displayText (help, image);
			} else {
				TurretUIPanel.instance.displayText (cantBuild, badImage);
			}

		} else {
			if (badImage) {
				TurretUIPanel.instance.displayText (cantBuild, badImage);
			} else {
				TurretUIPanel.instance.displayText (cantBuild, image);
			}
		}


		StartCoroutine (checkReadiness ());

		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	IEnumerator checkReadiness()
	{
		while (mouseIn) {
			

			if (myButt.interactable) {
				if (myPlacer && myPlacer.factCount()) {
					TurretUIPanel.instance.displayText (help, image);
				} else {
					//Debug.Log (myPlacer + "   ");
					TurretUIPanel.instance.displayText (cantBuild, badImage);
				}

			} else {
				if (badImage) {
					TurretUIPanel.instance.displayText (cantBuild, badImage);
				} else {
					TurretUIPanel.instance.displayText (cantBuild, image);
				}
			}
			yield return new WaitForSeconds (.2f);
		}

	}


	public void OnPointerExit(PointerEventData eventd)
	{mouseIn = false;

		TurretUIPanel.instance.TurnOff ();
	}
	void OnDestroy()
	{
		if (TurretUIPanel.instance) {
			TurretUIPanel.instance.TurnOff ();
		}
	}

}