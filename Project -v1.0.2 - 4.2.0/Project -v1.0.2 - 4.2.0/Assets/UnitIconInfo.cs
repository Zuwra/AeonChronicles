using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitIconInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public GameObject myUnit;
	private bool pointerIn;
	public Canvas toolbox;
	public Text textBox;
	public Text energyText;
	private UnitStats myStats;
	public Image myImage;


	BuildManager buildMan;
	public Text BuildNum;




	public void updateNum()
	{try{if (!buildMan) {
			return;
	} else if (buildMan.buildOrder.Count > 0) {
		BuildNum.text = "" + buildMan.buildOrder.Count;
	} else {
		BuildNum.text = "";
	}}
		catch(MissingReferenceException) {
		}


	}

	public void changeColor(Color c)
	{
		myImage.color = c;

	}

	public void setInfo(GameObject obj)
	{
	
		GetComponent<Image> ().sprite =obj.GetComponent<UnitStats> ().Icon;
		buildMan = obj.GetComponent<BuildManager> ();
		myUnit = obj;
		myStats = myUnit.GetComponent<UnitManager> ().myStats;
		if (buildMan) {
			updateNum ();
		}

	}





	public void OnPointerEnter(PointerEventData eventd)
	{//pointerIn = true;
		//this.enabled = true;
		if (toolbox) {

			pointerIn = true;
			toolbox.enabled = true;
			myStats = myUnit.GetComponent<UnitManager> ().myStats;
			StartCoroutine (PointerIsIn ());

		}
	
	}

	IEnumerator PointerIsIn()
	{yield return null;
		while (pointerIn) {
			textBox.text = (int)myStats.health + "/" + (int)myStats.Maxhealth;

			if (myStats.MaxEnergy > 0) {
				energyText.text = (int)myStats.currentEnergy + "/" + (int)myStats.MaxEnergy;
			}
		
			yield return new WaitForSeconds (.2f);
		}


	}


	public void OnPointerExit(PointerEventData eventd)
	{if (toolbox) {pointerIn = false;
		
			toolbox.enabled = false;}
	}



}
