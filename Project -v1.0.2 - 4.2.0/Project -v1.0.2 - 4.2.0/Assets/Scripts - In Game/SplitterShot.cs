using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitterShot : Projectile {


	public List<UnitManager> hitTargets = new List<UnitManager>();
	public float chargesRemaning; // each target hit will decrease by 1. at zero stop bouncing.


	public List<UnitManager> nearbyTargets= new List<UnitManager>();

	public GameObject ShotINstance;

	public SplitterHitList hitlist;

	public int NumOfBranches=1;

	void Awake()
	{
		Invoke ("resetTarget", .1f);
	}

	void resetTarget()
	{
		randomOffset = Vector3.zero;
	}

	public new void setSource(GameObject so)
	{
		hitlist = so.GetComponent<SplitterHitList> ();
	
		Source = so;
	}


	public new void setTarget(UnitManager so)
	{

	
		hitlist.hitTargets.Add (this.target);
		this.target = so;
	}
	
	public new void setDamage(float so)
	{
		
		damage = so;
	}



	override
	public void Terminate(UnitManager targ)
	{
		if (this.target != null) {
		//	Debug.Log("TAKE DAMAGE!");
			foreach(Notify not in triggers)
			{not.trigger(this.gameObject,this.gameObject, this.target, damage);}

			this.target.GetComponent<UnitStats>().TakeDamage(damage,Source, DamageTypes.DamageType.Regular);
			if(this.target == null)
			{Source.GetComponent<UnitManager>().enemies.RemoveAll(item => item == null);}
		}




		if (chargesRemaning > 0) {
			if (nearbyTargets.Count > 0) {
				chargesRemaning --;

				//Debug.Log ("Before guy is " + this.target);
				if(!hitlist.hitTargets.Contains(this.target)){
				hitlist.hitTargets.Add (this.target);
				}

				this.target = findBestEnemy ();

				if (this.target) {
					//Debug.Log ("New target is " +this.target);
					lastLocation = this.target.transform.position;
					//Debug.Log ("Positions are " + this.transform.position + "   " + this.target.transform.position);
					distance = Vector3.Distance (this.transform.position, this.target.transform.position);
					currentDistance = 0;
					//Debug.Log ("Distance " + currentDistance + "   " + distance);
					hitlist.hitTargets.Add (this.target);
			

					for (int i = 1; i < NumOfBranches; i++) {
						if (findBestEnemy () != null) {
		

							GameObject clone = (GameObject)Instantiate (ShotINstance, this.gameObject.transform.position, new Quaternion ());

							clone.GetComponent<SplitterShot> ().chargesRemaning = chargesRemaning;
							clone.GetComponent<SplitterShot> ().hitlist = this.hitlist;
							clone.GetComponent<SplitterShot> ().Source = this.Source;
				
							clone.GetComponent<SplitterShot> ().target = findBestEnemy ();
							hitlist.hitTargets.Add (findBestEnemy ());
				

							foreach (UnitManager obj in clone.GetComponent<SplitterShot> ().nearbyTargets) {
								this.nearbyTargets.Add (obj);
							}
						}
					}
				}
			}
			else {
			//	Debug.Log("Destroying here");
				Destroy (this.gameObject);
			}



		} else {
		//	Debug.Log ("Last here");
			Destroy (this.gameObject);
		}



		
	}

	public override void setup()
	{	base.setup ();
		hitlist = Source.GetComponent<SplitterHitList> ();

	}


	new void OnTriggerEnter(Collider other)
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
			{//Debug.Log("Setting Prioirty  " + nearbyTargets[i] + "    " +  hitlist.isValidEnemy(nearbyTargets[i]));
				best = nearbyTargets[i];
				priority = Vector3.Distance(nearbyTargets[i].transform.position, this.gameObject.transform.position);
			}
		}
	
		return best;
	}





}
