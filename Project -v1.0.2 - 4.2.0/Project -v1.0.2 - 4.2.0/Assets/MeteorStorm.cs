using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStorm : MonoBehaviour {

	public GameObject MeteorPrefab;
	public List<GameObject> spawnLocations;

	public bool attackBuildings = true;
	public int PlayerTarget;

	public int NumberMissiles;
	public float duration;

	public void setMissileCount(int number)
	{
		NumberMissiles = number;
	}

	public void setDuration(float dur)
	{
		duration = dur;
	}


	public void launchMissiles()
	{
		spawnLocations.RemoveAll (item => item == null);
		StartCoroutine (spawnMissile ());
	}

	IEnumerator spawnMissile( )
	{
		GameObject source = spawnLocations [UnityEngine.Random.Range (0, spawnLocations.Count)];

		yield return null;

		for (int i = 0; i < NumberMissiles; i++) {
			if (!source) {
				break;}

			GameObject obj = Instantiate (MeteorPrefab, source.transform.position, source.transform.rotation);

			if (attackBuildings) {

				UnitManager toAttack = findBuilding ();
				try {
					obj.SendMessage ("setTarget", toAttack, SendMessageOptions.DontRequireReceiver);
					obj.SendMessage ("setSource", source, SendMessageOptions.DontRequireReceiver);
					//	obj.SendMessage ("GiveOrder", Orders.CreateAttackOrder());
				} catch {
				}
			}
			yield return new WaitForSeconds (duration / NumberMissiles);

		}

	}

	public UnitManager findBuilding()
	{
		List<UnitManager> buildings = new List<UnitManager> ();

		foreach (KeyValuePair<string,List<UnitManager>> pair in GameManager.main.playerList [PlayerTarget - 1].getUnitList ()) {
			if (pair.Value.Count > 0) {
				if (pair.Value [0].myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
					foreach (UnitManager man in pair.Value) {
						if (man != null) {
							buildings.Add (man);
						}
					}
				}
			}
		}
		return buildings [UnityEngine.Random.Range (0, buildings.Count)];
	}



}
