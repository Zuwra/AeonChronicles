using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class TurretTipDisplayer: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public string help;
	public Sprite image;

	public void OnPointerEnter(PointerEventData eventd)
	{
		TurretUIPanel.instance.displayText (help, image);


		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}
	public void OnPointerExit(PointerEventData eventd)
	{
		TurretUIPanel.instance.TurnOff ();
	}

}