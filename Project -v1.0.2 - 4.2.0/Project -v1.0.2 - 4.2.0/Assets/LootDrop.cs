using UnityEngine;
using System.Collections;

public class LootDrop : MonoBehaviour,Modifier {

	public GameObject Loot;


	void Start()
	{
		GetComponent<UnitStats> ().addDeathTrigger (this);

	}

	public float modify (float a, GameObject deathSource){

		if (Loot) {
			Instantiate (Loot, this.gameObject.transform.position, Quaternion.identity);
		}
		return a;

	}

}
