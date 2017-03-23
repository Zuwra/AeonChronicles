using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCubeVulnerable : SceneEventTrigger {


	public List<UnitManager> DeathCubes ;
	public float vulnerableTime;
	public override void trigger (int index, float input, Vector3 location, GameObject target, bool doIt){
	
		StartCoroutine (VulnerateDeathCubes());
	
	
	}


	IEnumerator VulnerateDeathCubes()
	{

		foreach (UnitManager manage in DeathCubes) {
			if (manage) {
				try{
					manage.GetComponent<MiningSawDamager>().enabled = false;
					//manage.myStats.otherTags.Remove (UnitTypes.UnitTypeTag.Invulnerable);
				}catch(System.Exception){

				}
			}
		}


		yield return new WaitForSeconds (vulnerableTime);
		foreach (UnitManager manage in DeathCubes) {
			if (manage) {
				try{
					manage.GetComponent<MiningSawDamager>().enabled = true;
					//manage.myStats.otherTags.Add(UnitTypes.UnitTypeTag.Invulnerable);
				}catch(System.Exception){

				}
			}
		}


	}

}
