using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bunnyManager : MonoBehaviour {

	public Text bunnyCount;
	public int currAmount;
	public int maxAmount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeInBunnyCount(int change)
	{
		currAmount += change;
		bunnyCount.text = "Vicious Bunny Count \n" + currAmount + " / " + maxAmount;

		if (currAmount >= maxAmount) {
			VictoryTrigger.instance.Lose();
		}
		else if (currAmount <= 0) {
			VictoryTrigger.instance.Win();
		}
	}
}
