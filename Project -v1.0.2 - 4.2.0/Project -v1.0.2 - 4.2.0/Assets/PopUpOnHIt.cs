using UnityEngine;
using System.Collections;

public class PopUpOnHIt : MonoBehaviour, Notify {

	public string toShow;
	public Color myColor;
	// Use this for initialization
	void Start () {
		GetComponent<Projectile> ().triggers.Add (this);
	}
	
	public float trigger(GameObject source,GameObject proj,UnitManager target, float damage)
	{

	
		PopUpMaker.CreateGlobalPopUp (toShow, myColor, target.transform.position);
		return damage;
	}

}
