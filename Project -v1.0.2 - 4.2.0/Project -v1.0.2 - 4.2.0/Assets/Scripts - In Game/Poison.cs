using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {


	public GameObject poisonEffect;
	private UnitStats targetStats;
	private float nextActionTime;

	public float remainingPoison = 16;
	public float damageRate = 3;

	private float stackAmount =12 ;

	// Use this for initialization
	void Start () {targetStats = this.gameObject.GetComponent<UnitStats> ();
		nextActionTime = Time.time;

	
	}


	public void startPoison(GameObject poison)
	{
		Vector3 pos = this.gameObject.transform.position;
		pos.y += 8;
		GameObject obj = (GameObject)Instantiate (poison,pos, Quaternion.identity);
		obj.transform.parent = this.gameObject.transform;
		poisonEffect = obj;



	}
	// Update is called once per frame
	void Update () {	if (Time.time > nextActionTime) {

			nextActionTime += 1f;

			targetStats.TakeDamage(damageRate,null, DamageTypes.DamageType.True);
			remainingPoison -= damageRate;

			if(remainingPoison <=0)
			{
				Destroy(poisonEffect);
				Destroy(this);}
		
		}
	
	}

 

	public void AddPoisonStack()
	{remainingPoison += stackAmount;

}
}