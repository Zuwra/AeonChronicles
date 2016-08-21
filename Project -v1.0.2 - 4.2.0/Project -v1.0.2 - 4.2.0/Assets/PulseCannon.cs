using UnityEngine;
using System.Collections;

public class PulseCannon : MonoBehaviour {

	//Like the Iweapon but this fires at every person around it 
	public float fireRate;
	private float nextTime;
	UnitManager myManager;
	public float damage;

	public AudioClip mySoundEffect;
	public AudioSource myAudio;

	public GameObject projectile;
	// Use this for initialization
	void Start () {
		myManager = GetComponent<UnitManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextTime) {
			nextTime = Time.time + fireRate;


			myManager.enemies.RemoveAll (item => item == null);
			int i = 0;
			foreach (GameObject obj in myManager.enemies) {
				StartCoroutine( Fire ((i * .08f ),obj));
				i++;
			}

		}


	
	}

	IEnumerator Fire (float time, GameObject target)
	{
		myManager.animAttack ();
		yield return new WaitForSeconds(time);

		if (target) {



			UnitStats targetStats = target.GetComponent<UnitStats> ();


		


			GameObject proj = null;
			if (projectile != null) {
				Vector3 pos = this.gameObject.transform.position;
				pos.y += this.gameObject.GetComponent<CharacterController> ().radius;
				proj = (GameObject)Instantiate (projectile, pos, Quaternion.identity);

				Projectile script = proj.GetComponent<Projectile> ();
				proj.SendMessage ("setSource", this.gameObject);
				proj.SendMessage ("setTarget", target);
				proj.SendMessage ("setDamage", damage);
				script.damage = damage;

				script.target = target;
				script.Source = this.gameObject;

			} else {

				//OnAttacking();
				damage = target.GetComponent<UnitStats> ().TakeDamage (damage, this.gameObject, DamageTypes.DamageType.Regular);
				myManager.myStats.veteranDamage (damage);

			}
			if (target == null) {
				myManager.cleanEnemy ();
			}
			if (mySoundEffect) {

				myAudio.PlayOneShot (mySoundEffect);
			}

		}

	
	}

}
