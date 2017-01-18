using UnityEngine;
using System.Collections;

public class APMAchieve : Achievement{

	public int minAPM;

	public override string GetDecription()
	{return Description;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd (){
		if (!IsAccomplished ()) {
			APMCounter apm = GameObject.FindObjectOfType<APMCounter> ();
			if (apm) {
				if (apm.getAPM () >= minAPM) {
					Accomplished ();
				}
			}
		}
	}


}
