using UnityEngine;
using System.Collections;

public class HalfLife : MonoBehaviour {


	public GameObject AcidEffect;
	private UnitStats targetStats;
	private float nextActionTime;

	public float remainingHalves = 3;


	private PopUpMaker popper;

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
		AcidEffect = obj;



	}
	// Update is called once per frame
	void Update () {	if (Time.time > nextActionTime) {

			nextActionTime += 3f;

			if (targetStats.health > 20) {
				targetStats.TakeDamage ((targetStats.health / 2) + 8, null, DamageTypes.DamageType.Penetrating);
				popper.CreatePopUp ("" + (int)(targetStats.health/2), Color.magenta);
			} else {
				targetStats.TakeDamage (18, null, DamageTypes.DamageType.Penetrating);
				popper.CreatePopUp ("" + (int)(10), Color.magenta);
			}
		
			remainingHalves--;

			if(remainingHalves <=0)
			{
				Destroy(AcidEffect);
				Destroy(this);}

		}

	}



	public void AddPoisonStack()
	{remainingHalves = 3;

	}
}