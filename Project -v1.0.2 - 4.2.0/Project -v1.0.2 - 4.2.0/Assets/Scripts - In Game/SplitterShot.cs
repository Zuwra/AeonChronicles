using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitterShot : Projectile {


	public List<UnitManager> hitTargets = new List<UnitManager>();
	public float chargesRemaning; // each target hit will decrease by 1. at zero stop bouncing.


	public List<UnitManager> nearbyTargets= new List<UnitManager>();

	public GameObject ShotINstance;

	public SplitterHitList hitlist;




	public new void setSource(GameObject so)
	{
		hitlist = so.GetComponent<SplitterHitList> ();
	
		Source = so;
	}


	public new void setTarget(UnitManager so)
	{

	
		hitlist.hitTargets.Add (target);
		target = so;
	}
	
	public new void setDamage(float so)
	{
		
		damage = so;
	}



	override
	public void Terminate(UnitManager target)
	{
		if (target != null) {
		//	Debug.Log("TAKE DAMAGE!");
			foreach(Notify not in triggers)
			{not.trigger(this.gameObject,this.gameObject, target, damage);}

			target.GetComponent<UnitStats>().TakeDamage(damage,Source, DamageTypes.DamageType.Regular);
			if(target == null)
			{Source.GetComponent<UnitManager>().enemies.RemoveAll(item => item == null);}
		}




		if (chargesRemaning > 0) {
			if (nearbyTargets.Count > 0) {
				chargesRemaning --;

				hitlist.hitTargets.Add (this.target);


				this.target = findBestEnemy ();

				hitlist.hitTargets.Add (this.target);
			
			
			
				if (findBestEnemy () != null) {
		

					GameObject clone = (GameObject)Instantiate (ShotINstance,this.gameObject.transform.position, new Quaternion());

					clone.GetComponent<SplitterShot> ().chargesRemaning = chargesRemaning;
					clone.GetComponent<SplitterShot> ().hitlist =  this.hitlist;
					clone.GetComponent<SplitterShot> ().Source = this.Source;
				
					clone.GetComponent<SplitterShot> ().target = findBestEnemy ();
					hitlist.hitTargets.Add (findBestEnemy());
				

					foreach(UnitManager obj in clone.GetComponent<SplitterShot> ().nearbyTargets)
					{this.nearbyTargets.Add (obj);}
				}
			
			}
			else {
				//Debug.Log("Destroying here");
				Destroy (this.gameObject);
			}



		} else {

			Destroy (this.gameObject);
		}



		
	}


	void OnTriggerEnter(Collider other)
	{
		if (Source) {

			if (!other.isTrigger) {
			
				//if (other.gameObject.layer.Equals("Unit"))
				UnitManager manage = other.GetComponent<UnitManager> ();
				if (manage == null) {
					manage = other.GetComponentInParent<UnitManager> ();
				}
			
				if (manage != null) {
					if (manage.PlayerOwner != Source.GetComponent<UnitManager> ().PlayerOwner) {
					
						nearbyTargets.Add (manage);
					
					
					}
				}
			}
		}
	}


	void OnTriggerExit(Collider other)
	{
		UnitManager manage = other.GetComponent<UnitManager> ();
		if (manage) {
			nearbyTargets.Remove (manage);
		}

		
	}

	public UnitManager findBestEnemy()
	{
		UnitManager best = null;
		float priority = 1000;

		nearbyTargets.RemoveAll(item => item == null);
		for (int i = 0; i < nearbyTargets.Count; i ++) {

		//	Debug.Log(obj.name);
			if(nearbyTargets[i] == null)
			{nearbyTargets.Remove(nearbyTargets[i]);

			}
			else if(hitlist.isValidEnemy(nearbyTargets[i]) && Vector3.Distance(nearbyTargets[i].transform.position, this.gameObject.transform.position) < priority)
			{//Debug.Log("Setting Prioirty");
				best = nearbyTargets[i];
				priority = Vector3.Distance(nearbyTargets[i].transform.position, this.gameObject.transform.position);
			}
		}

		return best;
	}





}
