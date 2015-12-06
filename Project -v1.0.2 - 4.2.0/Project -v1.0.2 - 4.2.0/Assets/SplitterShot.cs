using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitterShot : Projectile {


	public List<GameObject> hitTargets = new List<GameObject>();
	public float chargesRemaning; // each target hit will decrease by 1. at zero stop bouncing.


	public List<GameObject> nearbyTargets= new List<GameObject>();

	public GameObject ShotINstance;

	public SplitterHitList hitlist;


	void Start () {
		//hitTargets.Add (target);

	//	Debug.Log ("Addign original to hitlist " + target.name);
	}


	public void setSource(GameObject so)
	{
		hitlist = so.GetComponent<SplitterHitList> ();
	
		Source = so;
	}

	
	public void setTarget(GameObject so)
	{

	
		hitlist.hitTargets.Add (target);
		target = so;
	}
	
	public void setDamage(float so)
	{
		
		damage = so;
	}



	override
	public void Terminate(GameObject target)
	{
		if (target != null) {
		//	Debug.Log("TAKE DAMAGE!");
			foreach(Notify not in triggers)
			{not.trigger(this.gameObject,this.gameObject, target);}

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
				

					foreach(GameObject obj in clone.GetComponent<SplitterShot> ().nearbyTargets)
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
					if (other.GetComponent<UnitManager> ().PlayerOwner != Source.GetComponent<UnitManager> ().PlayerOwner) {
					
						nearbyTargets.Add (other.gameObject);
					
					
					}
				}
			}
		}
	}


	void OnTriggerExit(Collider other)
	{nearbyTargets.Remove (other.gameObject);

		
	}

	public GameObject findBestEnemy()
	{
		GameObject best = null;
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
