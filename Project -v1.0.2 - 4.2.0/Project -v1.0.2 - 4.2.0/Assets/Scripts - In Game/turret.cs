using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {


	public bool rotateY;

	Coroutine frontFace;
	Coroutine trackingTarget;
	
	float lastTargetTime;

	GameObject myTarget; 

	public void Target(GameObject target)
	{

		myTarget = target;
		lastTargetTime = Time.time;


		if (frontFace == null) {
			frontFace = StartCoroutine (turnFront ());
		}


		if (trackingTarget != null) 
			{StopCoroutine (trackingTarget);}

		trackingTarget =  StartCoroutine (trackTarget ());



	}

	IEnumerator trackTarget()
	{
		Vector3 spotter = myTarget.transform.position;
		if (!rotateY) {
			spotter.y = this.transform.position.y;
		}

		for (float i = 0; i < 3f; i += Time.deltaTime) {
			if (!myTarget) {
				break;
			}

			this.gameObject.transform.LookAt(spotter);

			yield return null;
		}

	}




	IEnumerator turnFront()
	{
		
		while (Time.time < lastTargetTime + 3.2) {
			yield return new WaitForSeconds (1);

		}
		for (float i = 0; i < 3f; i += Time.deltaTime) {

			transform.rotation = Quaternion.Slerp(transform.rotation,transform.parent.rotation, Time.deltaTime * 30 *  0.2f);

			yield return null;
		}
		frontFace = null;

	}

	}

