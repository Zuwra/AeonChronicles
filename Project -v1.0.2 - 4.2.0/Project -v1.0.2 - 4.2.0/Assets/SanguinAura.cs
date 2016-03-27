using UnityEngine;
using System.Collections;

public class SanguinAura : MonoBehaviour {



	private UnitStats myStats;
	private GameObject myEffect;
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
		
		Vector3 loc = this.gameObject.transform.position;
		loc.y += 4;
		myEffect.transform.position = loc;


		if (Time.time > nextaction) {
			nextaction += .5f;

			float amount = Mathf.Min (myStats.health - 10, 5);
			if (amount > 0) {
				if (sourceStats) {
					sourceStats.heal (myStats.TakeDamage (amount, sourceStats.gameObject, DamageTypes.DamageType.True) / 2);
				} else {
					Destroy (myEffect);
					Destroy (this);
				}
			}
		}
		if (Time.time > endTime) {
			Destroy (myEffect);
			Destroy (this);}
	}

	public void Initialize(GameObject source,GameObject e)
	{
		if (myEffect == null) {
			Vector3 loc = this.gameObject.transform.position;
			loc.y += 4;
			myEffect = (GameObject)Instantiate (e, loc, Quaternion.identity);
		
		}
		nextaction = Time.time + .5f;
		myStats = GetComponent<UnitManager> ().myStats;
		sourceStats = source.GetComponent<UnitManager> ().myStats;
		endTime = Time.time + 13;
	}
}
