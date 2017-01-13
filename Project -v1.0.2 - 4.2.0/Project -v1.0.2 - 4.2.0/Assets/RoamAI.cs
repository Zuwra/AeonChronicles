using UnityEngine;
using System.Collections;

public class RoamAI : MonoBehaviour {

	Vector3 origin;
	public float roamRange = 22;

	UnitManager myman;

	// Use this for initialization
	void Start () {
		myman = GetComponent<UnitManager> ();
		origin = this.transform.position;
		InvokeRepeating ("setnewLocation",Random.Range(0,6), 8);
	}
	


	void setnewLocation()
	{
		Vector3 hitzone = origin;
		float radius = Random.Range (0, roamRange);
		float angle = Random.Range (0, 360);

		hitzone.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
		hitzone.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

		myman.GiveOrder (Orders.CreateAttackMove (hitzone, false));
	}
}
