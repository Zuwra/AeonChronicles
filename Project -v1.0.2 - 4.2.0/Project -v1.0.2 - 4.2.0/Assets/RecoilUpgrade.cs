using UnityEngine;
using System.Collections;

public class RecoilUpgrade : SpecificUpgrade {

	override
	public void applyUpgrade(GameObject obj)
	{
		if (confirmUnit (obj)) {
			StartCoroutine (waitAbit (obj));
		}
	
	}

	IEnumerator waitAbit(GameObject obj)
	{
		yield return new WaitForSeconds (.1f);
		DayexaShield ds = obj.GetComponent<DayexaShield> ();
		if (!ds) {
			ds = obj.transform.parent.GetComponentInParent<DayexaShield> ();
		}
		ds.AbsorbRecoil = true;
	}

	public override void unApplyUpgrade (GameObject obj){
		if (confirmUnit (obj)) {
			obj.GetComponent<DayexaShield> ().AbsorbRecoil = false;
		}
	}

	public override float ChangeString (string name, float number)
	{
		return number;
	}

}
