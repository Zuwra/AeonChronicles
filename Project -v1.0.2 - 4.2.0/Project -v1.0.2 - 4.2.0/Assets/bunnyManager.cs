using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bunnyManager : MonoBehaviour {

	public Text bunnyCount;
	public int currAmount;
	public int maxAmount;

	public void changeInBunnyCount(int change)
	{
		currAmount =GameObject.FindObjectsOfType<bunnyPopulate>().Length -1;


		bunnyCount.text = "Vicious Bunny Count \n" + currAmount + " / " + maxAmount;

		if (currAmount >= maxAmount) {
			VictoryTrigger.instance.Lose();
		}
		else if (currAmount <= 0) {
			VictoryTrigger.instance.Win();
		}
	}
}
