using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegenerativeSap : MonoBehaviour, Modifier {

	private List<int> healthList = new List<int> ();

	private float nextActionTime;
	private UnitStats mystats;

	// Use this for initialization
	void Start () {
		nextActionTime = Time.time + 1;
		mystats = GetComponent<UnitStats> ();
		mystats.addModifier (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (nextActionTime < Time.time) {
			nextActionTime += 1;
		
			heal ();
		}
	}


	public void heal()
	{float n = 0;
		for (int i = 0; i < healthList.Count; i ++) {
			healthList [i]--;
			n++;

			if (healthList [i] == 0) {
				healthList.Remove (0);
				i--;
			}
		}

		mystats.heal(n);
	}


	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
		{
		if (healthList.Count < 15) {
			healthList.Add (10);
		
		} else {
		
			int min = 15;
			foreach (int i in healthList) {
				if (i < min) {
					min = i;
				}
			}
			healthList.Remove (min);
			healthList.Add (10);
		}
		return damage;
		}
}
