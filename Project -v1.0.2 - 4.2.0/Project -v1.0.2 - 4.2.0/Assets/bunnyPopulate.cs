using UnityEngine;
using System.Collections;
using Pathfinding.RVO;
using DigitalRuby.LightningBolt;


public class bunnyPopulate : MonoBehaviour, Notify {

	public float repopulateTime;
	public float randomSpawnRange;
	private float nextRepopulate;
	private UnitStats myStats;

	public LightningBoltScript myLightning;
	public GameObject EggObject;

	UnitManager mymanager;
	bunnyManager bunnyMan;
	// Use this for initialization
	void Start () {

		repopulateTime +=  (43 - LevelData.getDifficulty () * 10);

		mymanager = GetComponent<UnitManager> ();
		bunnyMan =	GameObject.FindObjectOfType<bunnyManager> ();
		if(bunnyMan){
			bunnyMan.changeInBunnyCount (1);}
		nextRepopulate = repopulateTime + Random.Range(0,randomSpawnRange);
		myStats = GetComponent<UnitStats> ();

		foreach(IWeapon weap in mymanager.myWeapon)
			{
			weap.addNotifyTrigger (this);
		}

		Invoke ("BunnyUpdate", nextRepopulate);
	}
	
	// Update is called once per frame
	void BunnyUpdate () {

	
		Vector3 hitzone = this.transform.position + Vector3.up *.7f;
			float radius = Random.Range (6, 20);
			float angle = Random.Range (0, 360);

			hitzone.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			hitzone.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;


		GameObject ob = (GameObject)Instantiate (EggObject, hitzone, Quaternion.identity);//do the thing

			ob.GetComponent<RabbitEgg>().startHatch();
			

			nextRepopulate = repopulateTime + Random.Range(0,randomSpawnRange);
			repopulateTime += 8;

			myStats.HealthRegenPerSec += 1;
			myStats.Maxhealth += 25;
			myStats.heal (50);

			foreach(IWeapon weap in mymanager.myWeapon)
			{
				weap.range +=.5f;
				weap.baseDamage += 1;
				if (weap.range > 15) {
					weap.range = 15;
				}
			}
			this.gameObject.transform.localScale = this.gameObject.transform.localScale + Vector3.one *.15f;
			GetComponent<RVOController> ().radius += .15f;

		Invoke ("BunnyUpdate", nextRepopulate);
	}

	IEnumerator delayedStatChange(GameObject ob)
	{
		yield return new WaitForSeconds(.1f);

		UnitStats theirstat = ob.GetComponent<UnitStats>();
		theirstat.Maxhealth = 100;
		theirstat.heal (100);
		theirstat.HealthRegenPerSec = 1;

		foreach(IWeapon weap in  ob.GetComponent<UnitManager>().myWeapon)
		{
			weap.range +=10;
			weap.baseDamage = 1;
		}
	}


	public void Dying (){
		if(bunnyMan){
			bunnyMan.changeInBunnyCount (-1);
	}
	}

	public float trigger(GameObject source, GameObject proj, UnitManager target,float damage)
	{

		myLightning.Trigger ();

		return damage;

	}

}
