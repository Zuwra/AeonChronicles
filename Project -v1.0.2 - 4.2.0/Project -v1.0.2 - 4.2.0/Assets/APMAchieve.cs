using UnityEngine;
using System.Collections;

public class APMAchieve : Achievement{

	public int minAPM;

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){

		APMCounter apm = GameObject.FindObjectOfType<APMCounter> ();
		if (apm) {
			if (apm.getAPM () >= minAPM) {
				Accomplished ();
			}
		}

	}


}
