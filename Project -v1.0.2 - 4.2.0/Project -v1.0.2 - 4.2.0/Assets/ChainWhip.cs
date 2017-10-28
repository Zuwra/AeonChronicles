using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.SoundManagerNamespace;
public class ChainWhip : MonoBehaviour {


	public ChainWhip childWhip;

	public UnitManager myManager;

	public float maxDamage;
	public float maxRadius;
	public IWeapon myWeap;
	//int breakCount = 0;
	bool whipOn;

	public float speed;
	Coroutine myCoro = null;

	Quaternion curRotation;
	public AudioSource audioSource;
	public AudioClip hitSound;

	void OnTriggerEnter(Collider col)
	{
		
		//Debug.Log ("Checking " + col.gameObject);
		UnitManager manage = col.GetComponent<UnitManager> ();
		if (manage) {
			if (manage.PlayerOwner != myManager.PlayerOwner) {

				if (childWhip && myWeap.simpleCanAttack(manage)) {
		
					if (myCoro == null) {
						myCoro = StartCoroutine (WhipSpin());
						//mySpinner = StartCoroutine (UpdateRotation ());
					}
				} else if(!childWhip) {
					float distance = Vector3.Distance (transform.position, col.transform.position);
					manage.myStats.TakeDamage (maxDamage * (distance / maxRadius), myManager.gameObject, DamageTypes.DamageType.Regular);
					SoundManager.PlayOneShotSound(audioSource,hitSound);
		
					/*
					if (distance < transform.localScale.x / 2) {
						breakCount++;
						if (breakCount > 3) {
							halfChain ();
						}
					}*/
				}
			}
		}
	}

	IEnumerator WhipSpin()
	{
		whipOn = true;
		yield return null;

		while (myManager.enemies.Count > 0) {
			setScale (2);
			yield return new WaitForSeconds (.2f);
			myManager.enemies.RemoveAll (item => item == null);
		
		}

		while(childWhip.transform.localScale.x > 10){
			yield return new WaitForSeconds(.12f);

			if (myManager.enemies.Count > 0) {

				myCoro = StartCoroutine (WhipSpin());
			
				break;
			} else {
				setScale (-2);


			}
		}
		if (myManager.enemies.Count > 0) {
			myCoro = StartCoroutine (WhipSpin ());
			//StopCoroutine (mySpinner);


		} else {
			whipOn = false;
			myCoro = null;
		}
	

	}

	void setScale(float changeAmount)
	{
		if (childWhip.transform.localScale.x == maxRadius * 2) {
			return;
		}
		Vector3 newScale = childWhip.transform.localScale;
		newScale.x += changeAmount;
		newScale.x = Mathf.Clamp (newScale.x, 10, maxRadius*2);
		speed = 250 - newScale.x;
		myWeap.range = newScale.x/2 -5;
		childWhip.transform.localScale = newScale;
	}

	void halfChain()
	{	
		//breakCount = 0;
		Vector3 newScale = transform.localScale;
		newScale.x *= .5f;
		if (newScale.x < 10) {
			newScale.x = 10;
		}
		myWeap.range = newScale.x/2 -5;
		transform.localScale = newScale;
	}


	void Update () 
	{ 
	if (whipOn) {
			
			childWhip.transform.rotation = curRotation;
			childWhip.transform.Rotate (Vector3.up, speed * Time.deltaTime);
			curRotation = childWhip.transform.rotation;
		
		}

	}
}
