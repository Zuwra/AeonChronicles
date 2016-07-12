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

	void Start(){
		myButt = GetComponent<Button> ();
	}

	public void OnPointerEnter(PointerEventData eventd)
	{

		if (myButt.interactable) {
			TurretUIPanel.instance.displayText (help, image);

		} else {
			if (badImage) {
				TurretUIPanel.instance.displayText (cantBuild, badImage);
			} else {
				TurretUIPanel.instance.displayText (cantBuild, image);
			}
		}


		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}
	public void OnPointerExit(PointerEventData eventd)
	{
		TurretUIPanel.instance.TurnOff ();
	}
	void OnDestroy()
	{
		TurretUIPanel.instance.TurnOff ();
	}

}