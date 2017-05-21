using UnityEngine;
using System.Collections;

public class LootDrop : MonoBehaviour,Modifier {

	public GameObject Loot;
	public Vector3 PositionOffset;

	void Start()
	{
		GetComponent<UnitStats> ().addDeathTrigger (this);

	}

	public float modify (float a, GameObject deathSource, DamageTypes.DamageType theType){

		if (Loot) {
			Instantiate (Loot, this.gameObject.transform.position + PositionOffset, Quaternion.identity);
		}
		return a;

	}

	void OnDrawGizmos()
	{
		Gizmos.DrawCube (transform.position + PositionOffset, Vector3.one);
	}

}
