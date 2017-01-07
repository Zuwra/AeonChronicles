﻿using UnityEngine;
using System.Collections;

public class HarpyLandingPad : MonoBehaviour {

	public Vector3 RightPadPoint;
	public Vector3 LeftPadPoint;


	public GameObject leftPadInUse;
	public GameObject rightPadInUse;

	public Animator myAnim;


	public Vector3 requestLanding( GameObject incoming)
	{
		if (!rightPadInUse || rightPadInUse == incoming) {
			//Debug.Log ("Assigning " + incoming + "  to right");
			rightPadInUse = incoming;
	
		
		//	myAnim.Play ("BallisticsLabRight");
			return (transform.rotation) * RightPadPoint + this.gameObject.transform.position;

		}


		if (!leftPadInUse || leftPadInUse == incoming) {

			//Debug.Log ("Assigning " + incoming + "  to left");
			//myAnim.Play ("BallisticLeftOut");

			leftPadInUse = incoming;
			return (transform.rotation) * LeftPadPoint + this.gameObject.transform.position;

		}


		return Vector3.zero;
	}

	public void startLanding( GameObject incoming)
	{
		if (rightPadInUse == incoming) {

			myAnim.Play ("BallisticsLabRight");


		}


		if (leftPadInUse == incoming) {

		
			myAnim.Play ("BallisticLeftOut");

		}

	}


	public void finished(GameObject outgoing)
	{
		StartCoroutine (delayedClosing (outgoing));
	}

	IEnumerator delayedClosing(GameObject outgoing)
	{
		if (rightPadInUse == outgoing) {
			rightPadInUse =null;
			yield return new WaitForSeconds (.2f);
			myAnim.Play ("BallisticRightIn");



		}
		if (leftPadInUse == outgoing) {
			leftPadInUse = null;

			yield return new WaitForSeconds (.2f);
			myAnim.Play ("BallisticLeftIn");

		}

	}


	public bool hasAvailable()
	{
		return (!rightPadInUse || !leftPadInUse);

	}

	public void OnDrawGizmos()
	{
		
		Gizmos.DrawSphere ((transform.rotation) * RightPadPoint + this.gameObject.transform.position, .5f);
		Gizmos.DrawSphere ((transform.rotation) * LeftPadPoint + this.gameObject.transform.position, .5f);
	}
}
