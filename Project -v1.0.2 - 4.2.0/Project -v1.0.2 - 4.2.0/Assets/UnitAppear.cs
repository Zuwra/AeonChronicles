using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAppear  : SceneEventTrigger {


	public List<toChange> toAppear;
	[Tooltip("Do not put untis into here, only things that dont have unitmanagers")]
	public List<toChange> toDisappear;

	public List<UnitToChange> toKill;




	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){

		foreach (toChange obj in toAppear) {
			StartCoroutine (delayedAppear (obj));
		}

		foreach (toChange obj in toDisappear) {
			StartCoroutine (delayedDisappear (obj));
		}



		foreach (UnitToChange obj in toKill) {
			StartCoroutine (delayedKill (obj));}

		}


	IEnumerator delayedAppear(toChange changer )
	{
		yield return new WaitForSeconds (changer.waitTime);
	
		if (changer.myObj) {
			
			changer.myObj.SetActive (true);}
	}

	IEnumerator delayedDisappear(toChange changer)
	{
		yield return new WaitForSeconds (changer.waitTime);
		if (changer.myObj) {
			changer.myObj.SetActive (false);}
	}


	IEnumerator delayedKill(UnitToChange changer )
	{
		yield return new WaitForSeconds (changer.waitTime);
		if (changer.myObj) {
			UnitManager man = changer.myObj.GetComponent<UnitManager> ();
			if (man) {
				if (man.myStats.isUnitType (UnitTypes.UnitTypeTag.Invulnerable)) {
					man.myStats.otherTags.Remove (UnitTypes.UnitTypeTag.Invulnerable);
				}
			//	Debug.Log ("Killing " + man.gameObject);
				man.myStats.kill (null);
			}
		}
	}


}

[System.Serializable]
public class toChange{

	public float waitTime;
	public GameObject myObj;


}
[System.Serializable]
public class UnitToChange{

	public float waitTime;
	public GameObject myObj;


}