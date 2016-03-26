using UnityEngine;
using System.Collections;

public class SanguinAura : MonoBehaviour {



	private UnitStats myStats;

	private UnitStats sourceStats;

	private float nextaction;

	private float endTime;
	// Use this for initialization
	void Start () {
		nextaction = Time.time + .5f;
		myStats = GetComponent<UnitManager> ().myStats;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > nextaction) {
			nextaction += .5f;

			float amount = Mathf.Min (myStats.health - 10, 5);
			if (amount > 0) {
				sourceStats.heal (myStats.TakeDamage (amount, sourceStats.gameObject, DamageTypes.DamageType.True) / 2);
			}
		}
		if (Time.time > endTime) {
			Destroy (this);}
	}

	public void Initialize(GameObject source)
	{nextaction = Time.time + .5f;
		myStats = GetComponent<UnitManager> ().myStats;
		sourceStats = source.GetComponent<UnitManager> ().myStats;
		endTime = Time.time + 13;
	}
}
