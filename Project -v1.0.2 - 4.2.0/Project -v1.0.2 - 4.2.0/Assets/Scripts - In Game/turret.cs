using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {


	public bool rotateY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Target(GameObject target)
	{

		Vector3 spotter = target.transform.position;
		if (!rotateY) {
			spotter.y = this.transform.position.y;
		}
		this.gameObject.transform.LookAt(spotter);




	}

	}

