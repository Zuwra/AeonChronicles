using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class bunnyManager : Objective {

	public Text bunnyCount;
	public int currAmount;
	public int maxAmount;

	public Image myPanel;
	public Slider mySlide;

	Color green = new Color (0, 100, 0);
	Color red = new Color (120, 0, 0);
	Color yellow = new Color (100, 100, 0);

	bool flash;

	Coroutine flashing;

	public List<voiceLineRandomizer> lineTriggers;

	[System.Serializable]
	public class voiceLineRandomizer{
		public int triggerNumber;
		public List<int> voiceLineOptions;
		private float timeSinceLastPlayed = 0;
		int lastOneUsed = -1;


		public void playVoiceLine() {
			if (Time.time - timeSinceLastPlayed > 30) {
				timeSinceLastPlayed = Time.time;
				int rand = Random.Range (0, voiceLineOptions.Count - 1);
				if(voiceLineOptions.Count > 1) {
					while (lastOneUsed == rand) {
						rand = Random.Range (0, voiceLineOptions.Count - 1);
					}
				} 
				lastOneUsed = rand;
				dialogManager.instance.playLine (voiceLineOptions[rand]); //play that one here
			}
		}
	}


	public void changeInBunnyCount(int change)
	{
		int oldAmount = currAmount;
		currAmount =GameObject.FindObjectsOfType<bunnyPopulate>().Length -1;


		if (oldAmount < currAmount) {
			foreach (voiceLineRandomizer v in lineTriggers) {
				if (currAmount == v.triggerNumber) {
					v.playVoiceLine ();
					break;
				}
			}
		}

		mySlide.value = (float)currAmount / (float)maxAmount;

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

		bunnyCount.text = "Vicious Bunnies Left \n" + currAmount + " / " + maxAmount;

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
