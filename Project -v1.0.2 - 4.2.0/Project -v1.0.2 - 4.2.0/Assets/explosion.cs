using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class explosion : MonoBehaviour {


	public GameObject source;
	public int sourceInt = 1;
	public GameObject particleEff;
	public float friendlyFireRatio;
	public float damageAmount;
	public DamageTypes.DamageType type;
	public float maxSize= 5.0f;
	public float growthRate = 1.0f;
	private float scale = 1.0f;
	public float TurretDamageRatio = .5f;
	public List<Notify> triggers = new List<Notify> ();

	private List<GameObject> hitStuff= new List<GameObject>();


	public IWeapon.bonusDamage[] extraDamage;

	UnitManager mySrcMan;
	// Use this for initialization
	void Start () {
		if (particleEff) {
		
			Instantiate (particleEff, this.gameObject.transform.position, Quaternion.identity);
		}
	
	}


	public void setSource(GameObject sr)
	{
		source = sr;
		if (source) {
			mySrcMan = source.GetComponent<UnitManager> ();
		}
		if (mySrcMan) {
			sourceInt = mySrcMan.PlayerOwner;
		}
	}

	// Update is called once per frame
	void Update () {



		transform.localScale = Vector3.one * scale;
		scale += growthRate * Time.deltaTime;
		if (scale > maxSize) Destroy (gameObject);

	
	}



	void OnTriggerEnter(Collider other)
	{if (other.isTrigger) {
			return;}
		
		if(!hitStuff.Contains(other.gameObject)) {
			hitStuff.Add (other.gameObject);
			UnitManager manager = other.gameObject.GetComponent<UnitManager> ();

			if (manager) {

		
					float amount = damageAmount	;

					if (sourceInt == manager.PlayerOwner) {
						amount *= friendlyFireRatio;
					}

					UnitStats stats = manager.myStats;
					foreach ( IWeapon.bonusDamage tag in extraDamage) {
						if ( manager.myStats.isUnitType (tag.type)) {
							amount += tag.bonus;
						}
					}
				
					float total = 0;

					if (stats.isUnitType (UnitTypes.UnitTypeTag.Turret)) {
						total = stats.TakeDamage (amount*TurretDamageRatio, source, type);
					} else {
						total = stats.TakeDamage (amount, source, type);
					}

					if (mySrcMan) {
						mySrcMan.myStats.veteranDamage (total);
					}

					foreach (Notify not in triggers) {
					
						not.trigger(source,  null, manager, amount);
					}


			}
		}
	}

}