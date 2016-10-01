using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {


	public bool rotateY;

	


	public void Target(GameObject target)
	{

		Vector3 spotter = target.transform.position;
		if (!rotateY) {
			spotter.y = this.transform.position.y;
		}
		this.gameObject.transform.LookAt(spotter);




	}

	}

