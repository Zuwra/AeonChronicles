using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UltTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public bool Ability;

	AbstractCost myUltCost;
	public Text cooldown;

	[Tooltip("Should be between 1 and 4")]
	public int UltNumber;

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
			
			if (myUltCost.cooldownProgress () >= 1) {
				
				cooldown.text = "Cooldown: " +Clock.convertToString(myUltCost.cooldown );}
			else{
			cooldown.text = "Cooldown: " +Clock.convertToString((myUltCost.cooldown *  (1 - myUltCost.cooldownProgress ())));
			}
			yield return new WaitForSeconds(1);
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

		switch(UltNumber){
		case 1:
			myUltCost = GameManager.main.playerList [0].UltOne.myCost;
			break;
		case 2:
			myUltCost = GameManager.main.playerList [0].UltTwo.myCost;
			break;
		case 3:
			myUltCost = GameManager.main.playerList [0].UltThree.myCost;
			break;
		case 4:
			myUltCost = GameManager.main.playerList [0].UltFour.myCost;
			break;

		
		}
	}




}
