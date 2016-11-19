using UnityEngine;
using System.Collections;

public class PulseCannon : IWeapon {

	//Like the Iweapon but this fires at every person around it 

	private float nextTime;
	public int maxPulses = 10;




	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextTime) {
			


			myManager.enemies.RemoveAll (item => item == null);
			int i = 0;
			foreach (UnitManager obj in myManager.enemies) {
				StartCoroutine( AttackWave ((i * .08f ),obj.gameObject));
				i++;
				if(i >= maxPulses)
				{break;}
			}
			nextTime = Time.time + attackPeriod/3 + i *.08f;

		}


	
	}

	IEnumerator AttackWave (float time, GameObject target)
	{
		myManager.animAttack ();
		yield return new WaitForSeconds(time);

		if (target) {



			//UnitStats targetStats = target.GetComponent<UnitStats> ();


		


			GameObject proj = null;
			if (projectile != null) {
				Vector3 pos = this.gameObject.transform.position;
				pos.y += this.gameObject.GetComponent<CharacterController> ().radius;
				proj = (GameObject)Instantiate (projectile, pos, Quaternion.identity);

				Projectile script = proj.GetComponent<Projectile> ();
				proj.SendMessage ("setSource", this.gameObject);
				proj.SendMessage ("setTarget", target.GetComponent<UnitManager>());
				proj.SendMessage ("setDamage", baseDamage);
				script.damage = baseDamage;

				script.target = target.GetComponent<UnitManager>();
				script.Source = this.gameObject;

			} else {

				//OnAttacking();
				baseDamage = target.GetComponent<UnitStats> ().TakeDamage (baseDamage, this.gameObject, DamageTypes.DamageType.Regular);
				myManager.myStats.veteranDamage (baseDamage);

			}
			if (target == null) {
				myManager.cleanEnemy ();
			}
			if (attackSoundEffect && audioSrc) {

				audioSrc.PlayOneShot (attackSoundEffect);
			}

		}

	
	}

}
