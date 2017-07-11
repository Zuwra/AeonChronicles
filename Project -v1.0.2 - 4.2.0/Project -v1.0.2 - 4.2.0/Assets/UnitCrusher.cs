using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCrusher : MonoBehaviour, Notify {



	public float stunTime;

	public bool onTarget;
	// Use this for initialization
	void Start () {
		if (GetComponent<Projectile> ()) {
			GetComponent<Projectile> ().triggers.Add (this);
		}

	}

	public void startCrush()
	{
		StartCoroutine (CrushGuy ());
	}
	IEnumerator CrushGuy()
	{	GetComponent<UnitManager> ().StunForTime (null, 15);
		float startY = transform.localScale.y;
		for (float i = 0; i < 8; i += Time.deltaTime) {
			yield return null;
		//	transform.position += Vector3.up * Time.deltaTime;
			transform.localScale = new Vector3 (transform.localScale.x, startY * (1 - (i/8f)), transform.localScale.z);
			transform.Rotate (0,0,Time.deltaTime*11.2f );
		}
		transform.localScale = new Vector3 (transform.localScale.x,.01f, transform.localScale.z);
		yield return new WaitForSeconds (1.5f);
		GetComponent<UnitManager> ().myStats.kill (null);
	}



	public float trigger(GameObject source,GameObject proj, UnitManager target, float damage)
	{

		if (target && source != target) {
	

			UnitCrusher crusher = target.gameObject.AddComponent<UnitCrusher> ();

			crusher.onTarget = true;
			crusher.stunTime = stunTime;
			crusher.startCrush();
		}
		return damage;
	}


}