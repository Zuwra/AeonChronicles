using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Target(GameObject target)
	{
		this.gameObject.transform.LookAt(target.transform.position);}

	}

