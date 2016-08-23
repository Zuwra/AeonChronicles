using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {


	public GameObject poisonEffect;
	private UnitStats targetStats;
	private float nextActionTime;

	public float remainingPoison = 16;
	public float damageRate = 4;

	private float stackAmount =12 ;
	private PopUpMaker popper;
	public float drainEnergyAmount=0;

	// Use this for initialization
	void Start () {targetStats = this.gameObject.GetComponent<UnitStats> ();
		nextActionTime = Time.time;
		if(GetComponent<PopUpMaker>() ==null)
		{
			this.gameObject.AddComponent<PopUpMaker> ();
		}
		popper = GetComponent<PopUpMaker> ();
		popper.textColor = Color.magenta;
	}


	public void startPoison(GameObject poison)
	{
		Vector3 pos = this.gameObject.transform.position;
		//pos.y += 8;
		GameObject obj = (GameObject)Instantiate (poison,pos, Quaternion.identity);
		obj.transform.parent = this.gameObject.transform;
		poisonEffect = obj;



	}
	// Update is called once per frame
	void Update () {	if (Time.time > nextActionTime) {

			nextActionTime = Time.time +  1f;

			targetStats.TakeDamage(damageRate,null, DamageTypes.DamageType.Penetrating);
			targetStats.changeEnergy (-drainEnergyAmount);
			popper.CreatePopUp ("" + (int)remainingPoison, Color.magenta);
			remainingPoison -= damageRate;
		
			if(remainingPoison <=0)
			{
				Destroy(poisonEffect);
				Destroy(this);}
		
		}
	
	}

	public void setEnergyDrain(float n)
	{drainEnergyAmount = n;
		
	}
 

	public void AddPoisonStack()
	{remainingPoison += stackAmount;

}
}