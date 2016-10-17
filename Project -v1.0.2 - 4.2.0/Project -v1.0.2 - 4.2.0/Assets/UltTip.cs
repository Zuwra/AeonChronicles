using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UltTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public bool Ability;

	public AbstractCost myUltCost;
	public Text cooldown;


	public Canvas toolbox;

	Coroutine updater;

	public void OnPointerEnter(PointerEventData eventd)
	{
		updater = StartCoroutine (updateCooldown());
		toolbox.enabled = true;
		//toolbox.gameObject.GetComponentInChildren<Text> ().text = helpText;
	}

	public void OnPointerExit(PointerEventData eventd)
	{
		toolbox.enabled = false;
		StopCoroutine (updater);
	}

	IEnumerator updateCooldown()
	{
		while (true) {
			yield return new WaitForSeconds(.5f);
			if (myUltCost.cooldownProgress () == 1) {
				cooldown.text = "Cooldown: " +Clock.convertToString(myUltCost.cooldown );}
			else{
			cooldown.text = "Cooldown: " +Clock.convertToString((myUltCost.cooldown *  (1 - myUltCost.cooldownProgress ())));
			}
		}

	}


	public void turnOff()
	{
		toolbox.enabled = false;
	}


	// Use this for initialization
	void Start () {
		if (toolbox == null) {
			toolbox = GameObject.Find ("ToolTipBox").GetComponent<Canvas> ();
		}
	}




}
