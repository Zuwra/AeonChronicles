using UnityEngine;
using System.Collections;

public class FullMetalAchieve : Achievement{


	public override void CheckBeginning (){
	}

	public override void CheckEnd (){

		if(!IsAccomplished()){
			int counter = 0;
			foreach (Upgrade upg in	GameObject.FindObjectOfType<GameManager>().activePlayer.upgradeBall.GetComponents<Upgrade>()) {
				if (upg is SpecificUpgrade) {
					continue;
				}
				counter++;

			}
			if(counter == 22)
			{
				Accomplished ();
			}
			
		}

	}


}
