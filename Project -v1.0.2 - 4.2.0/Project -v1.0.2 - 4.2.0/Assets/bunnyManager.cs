using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bunnyManager : MonoBehaviour {

	public Text bunnyCount;
	public int currAmount;
	public int maxAmount;

	public Image myPanel;

	Color green = new Color (0, 100, 0);
	Color red = new Color (120, 0, 0);
	Color yellow = new Color (100, 100, 0);

	bool flash;

	Coroutine flashing;

	public void changeInBunnyCount(int change)
	{
		currAmount =GameObject.FindObjectsOfType<bunnyPopulate>().Length -1;



		if (currAmount > maxAmount * .85) {
			flash = true;
			if (flashing == null) {
				flashing = StartCoroutine (flashColor ());
			}
			myPanel.color = red;
		} 
		else if (currAmount > maxAmount * .70) {
			flash = true;
			myPanel.color = red;
		} else if (currAmount > maxAmount * .5) {
			flash = false;
			myPanel.color = yellow;
		} else if (currAmount > maxAmount * .3) {
			flash = false;
			myPanel.color = Color.white;
		} else {
			flash = false;
			myPanel.color = green;
		}

		bunnyCount.text = "Vicious Bunny Count \n" + currAmount + " / " + maxAmount;

		if (currAmount >= maxAmount) {
			VictoryTrigger.instance.Lose();
		}
		else if (currAmount <= 0) {
			VictoryTrigger.instance.Win();
		}
	}


	IEnumerator flashColor()
	{yield return new WaitForSeconds (.5f);
		while (flash) {
			yield return new WaitForSeconds (.65f);
			myPanel.color = red;

			yield return new WaitForSeconds (.65f);
			myPanel.color = Color.white;
		}
		flashing = null;

	}
}
