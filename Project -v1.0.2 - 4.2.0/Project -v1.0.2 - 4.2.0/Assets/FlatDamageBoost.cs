using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatDamageBoost : MonoBehaviour, Modifier {


	public float FlatDamageIncrease;
	[Tooltip("Preferably a float between -1 and 1")]
	public float PercDamageIncrease;


	public Sprite DebuffIcon;
	public float modify(float damage, GameObject source, DamageTypes.DamageType theType)
	{
		float toReturn = damage + damage * PercDamageIncrease;
		toReturn += FlatDamageIncrease;

	//	Debug.Log ("Returning " + toReturn);
		return toReturn;

	}


	public List<UnitStats> enemies = new List<UnitStats> ();


	public int Owner;



	public void setOwner(int n)
	{Owner = n;
	}

	public Vector3 getImpactLocation()
	{
		Vector3 vec = this.transform.position;
		vec.y = 1;
		return vec;

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger) {
			return;}

		UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
		if (manage == null) {
			return;
		}

		if (manage.PlayerOwner != Owner) {
			enemies.Add (manage.myStats);
			//Debug.Log ("Adding modifer");
			manage.myStats.addModifier (this);


			Buff buff = manage.gameObject.AddComponent<Buff> ();
			//buff.name = "Flat Damaged"; 
			buff.source = this.gameObject;
			buff.HelpIcon = DebuffIcon;
			buff.toolDescription = "This unit takes an extra " + FlatDamageIncrease + ""+ (PercDamageIncrease*100)+"%" +" Damage from each enemy attack.";
			buff.applyDebuff();

			return;
		}


	}


	void OnTriggerExit(Collider other)
	{UnitManager manage = other.gameObject.GetComponent<UnitManager> ();


		if (manage == null) {
			return;
		}

		if (manage.PlayerOwner == Owner) {
			return;
		}

		if (enemies.Contains (manage.myStats)) {

			enemies.Remove (manage.myStats);
			manage.myStats.removeModifier (this);

			foreach (Buff b in manage.gameObject.GetComponents<Buff>()) {
				if (b.source == this.gameObject) {
					b.removeDebuff ();
					Destroy (b);
				}
			
			}
		}
	}

	void OnDestroy()
	{
		foreach (UnitStats stat in enemies) {
			if (stat) {
				foreach (Buff b in stat.gameObject.GetComponents<Buff>()) {
					if (b.source == this.gameObject) {
						b.removeDebuff ();
						Destroy (b);
					}

				}
			}
		
		
		}


	}


}
