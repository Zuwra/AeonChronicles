using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour {


	public List<UnitStats> enemies = new List<UnitStats> ();


	public DamageTypes.DamageType myType = DamageTypes.DamageType.Regular;
	public int Owner;
	public GameObject cutEffect;

	public float damage = 5;
	private AudioSource myAudio;
	public AudioClip chopSound;

	private int iter = 0;


	// Use this for initialization
	void Start () {
		myAudio = GetComponent<AudioSource> ();

		InvokeRepeating ("UpdateDamage", .1f, .2f);
	}

	// Update is called once per frame
	void UpdateDamage () {

		if (enemies.Count > 0) {

			enemies.RemoveAll (item => item == null);
			foreach (UnitStats s in enemies) {

		
					s.TakeDamage (damage, this.gameObject.gameObject.gameObject, myType);
					iter++;
					if (iter == 6) {
						PopUpMaker.CreateGlobalPopUp (-(damage*2) + "", Color.red, s.gameObject.transform.position);
						iter = 0;
					}

				if (cutEffect) {
					Instantiate (cutEffect, s.gameObject.transform.position, Quaternion.identity);
				}
				//obj.transform.SetParent (this.gameObject.transform);
			}
		}


	}


	public void turnOn()
	{
		GetComponent<BoxCollider> ().enabled = true;
		this.enabled = true;
	}


	public void setOwner(int n)
	{Owner = n;
	}

	public Vector3 getImpactLocation()
	{
		Vector3 vec = this.transform.position;
		//vec.y = 1;
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
			if (chopSound) {
				myAudio.PlayOneShot (chopSound);
			}
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

		enemies.Remove (manage.myStats);
		
	}


}
