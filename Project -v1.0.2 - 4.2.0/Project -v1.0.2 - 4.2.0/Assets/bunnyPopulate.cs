using UnityEngine;
using System.Collections;
using Pathfinding.RVO;
using DigitalRuby.LightningBolt;


public class bunnyPopulate : MonoBehaviour, Modifier, Notify {

	public float repopulateTime;
	public float randomSpawnRange;
	private float nextRepopulate;
	private UnitStats myStats;

	public LightningBoltScript myLightning;

	// Use this for initialization
	void Start () {
		GameObject.FindObjectOfType<bunnyManager> ().changeInBunnyCount (1);
		nextRepopulate = Time.time + repopulateTime + Random.Range(0,randomSpawnRange);
		GetComponent<UnitStats> ().addDeathTrigger (this);
		myStats = GetComponent<UnitStats> ();

		foreach(IWeapon weap in GetComponent<UnitManager>().myWeapon)
			{
			weap.addNotifyTrigger (this);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextRepopulate) {

			Vector3 hitzone = this.transform.position;
			float radius = Random.Range (4, 15);
			float angle = Random.Range (0, 360);

			hitzone.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			hitzone.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;


			Instantiate (this.gameObject, hitzone, Quaternion.identity);//do the thing
			nextRepopulate = Time.time + repopulateTime + Random.Range(0,randomSpawnRange);
			repopulateTime += 3;

			myStats.Maxhealth += 5;
			myStats.heal (5);
			this.gameObject.transform.localScale = this.gameObject.transform.localScale + Vector3.one *.1f;
			GetComponent<RVOController> ().radius += .1f;
		}
	
	}


	public float modify (float a,GameObject deathSource){
		GameObject.FindObjectOfType<bunnyManager> ().changeInBunnyCount (-1);
		return a;
	}

	public void trigger(GameObject source, GameObject proj, GameObject target,float damage)
	{

		myLightning.Trigger ();



	}

}
