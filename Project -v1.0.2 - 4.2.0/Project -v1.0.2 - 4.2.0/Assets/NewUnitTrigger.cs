using UnityEngine;
using System.Collections;

public class NewUnitTrigger  : SceneEventTrigger {

	public int currentIndex;



	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
		if (!hasTriggered) {
			hasTriggered = true;
		//	Debug.Log ("Triggered");
			//NewUnitPanel.main.setMaxAlled (index, currentIndex);


	
		}

	}

}
