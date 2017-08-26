using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {


	public bool rotateY;

	Coroutine frontFace;
	Coroutine trackingTarget;
	
	//float lastTargetTime;

	GameObject myTarget; 

	public void Target(GameObject target)
	{
		
		myTarget = target;
		//lastTargetTime = Time.time;


		if (frontFace != null) {
			StopCoroutine (frontFace);// = StartCoroutine (turnFront ());
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


			spotter = myTarget.transform.position;
			if (!rotateY) {
				spotter.y = this.transform.position.y;
			}

			this.gameObject.transform.LookAt(spotter);

			yield return null;
		}

		frontFace = StartCoroutine (turnFront());

	}




	IEnumerator turnFront()
	{
		

		for (float i = 0; i < 2f; i += Time.deltaTime) {
			//Debug.Log ("Turning!");
			transform.rotation = Quaternion.Slerp(transform.rotation,transform.parent.rotation, i/2);

			yield return null;
		}
		frontFace = null;

	}

	}

