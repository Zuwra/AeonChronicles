using UnityEngine;
using System.Collections;

public class RunicBlessing : MonoBehaviour, Modifier {

	//Behavior that prevents the damage that would be done by an attack every X seconds
	public float coolDown;

	private float timer = 0;



	// Use this for initialization
	void Start () {

		this.gameObject.GetComponent<UnitStats> ().addModifier (this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
		timer -= Time.deltaTime;

	}


	public float modify(float damage , GameObject source, DamageTypes.DamageType theType)
	{

		if (timer <= 0) {
			timer = coolDown;
			Debug.Log ("Blessing");
			return 0;
		}
		timer = coolDown;
		return damage;

	}

}
