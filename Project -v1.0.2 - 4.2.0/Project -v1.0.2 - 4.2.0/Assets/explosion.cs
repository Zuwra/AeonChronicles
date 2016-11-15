using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class explosion : MonoBehaviour {


	public GameObject source;
	public int sourceInt;
	public GameObject particleEff;
	public bool friendlyFire;
	public float damageAmount;
	public DamageTypes.DamageType type;
	public float maxSize= 5.0f;
	public float growthRate = 1.0f;
	private float scale = 1.0f;

	public List<Notify> triggers = new List<Notify> ();

	private List<GameObject> hitStuff= new List<GameObject>();

	public IWeapon.bonusDamage[] extraDamage;

	UnitManager mySrcMan;
	// Use this for initialization
	void Start () {
		if (particleEff) {
		
			Instantiate (particleEff, this.gameObject.transform.position, Quaternion.identity);
		}
		if (source) {
			mySrcMan = source.GetComponent<UnitManager> ();
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

				if (!friendlyFire && source.GetComponent<GameManager> ()) {
					return;
				}

				if (friendlyFire || sourceInt != manager.PlayerOwner) {

					float amount = damageAmount	;
					UnitStats stats = other.gameObject.GetComponent<UnitStats> ();
					foreach ( IWeapon.bonusDamage tag in extraDamage) {
						if ( stats.isUnitType (tag.type)) {
							amount += tag.bonus;
						}
					}
					Debug.Log ("Dealing damage " + this.gameObject);
					float total = stats.TakeDamage (amount, source, type);
				

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
}
