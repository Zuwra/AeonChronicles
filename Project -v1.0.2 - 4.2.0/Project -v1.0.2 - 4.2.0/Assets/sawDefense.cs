﻿using UnityEngine;
using System.Collections;

public class sawDefense : MonoBehaviour {



	public Animator myController;
	public UnitManager myManager;

	public GameObject mySaw;
	bool inAttack;
	float attackEndTime;
	int attackType;

	public GameObject targetCircle; 
	public GameObject targetSlice;
	Vector3 targetlocation;
	public float turnSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (inAttack) {
			if (Time.time > attackEndTime) {


				inAttack = false;
			} else {
				mySaw.transform.rotation =
					Quaternion.Slerp (mySaw.transform.rotation, Quaternion.LookRotation (targetlocation), Time.deltaTime* turnSpeed);

			}
		}

		if (!inAttack) {
			if (myManager.enemies.Count > 0) {
		
				GameObject enem = myManager.findClosestEnemy ();
				if (!enem) {
				
					return;}
				targetlocation = (enem.transform.position - mySaw.transform.position).normalized;

				Debug.Log ("target is " + targetlocation);
				inAttack = true;
				if (attackType == 2) {
					attackType = -2;
				}else{
					attackType = 2;
					}
				attackEndTime = Time.time + 5;
				myController.SetInteger ("State", attackType);

				StartCoroutine (showInd ());
				StartCoroutine (delayState());

			}
		}



	}

	IEnumerator showInd()
	{	yield return new WaitForSeconds (.1f);
		if (attackType == 2) {
			targetSlice.gameObject.SetActive (true);
		


		} else {
			targetCircle.transform.position = targetlocation* 47 + mySaw.transform.position;
		
			targetCircle.gameObject.SetActive (true);

		}
		yield return new WaitForSeconds (3f);
		targetCircle.gameObject.SetActive (false);

		targetSlice.gameObject.SetActive (false);


	}

	IEnumerator delayState()
	{
		yield return new WaitForSeconds (2f);
		myController.SetInteger ("State", 0);
	}





}
