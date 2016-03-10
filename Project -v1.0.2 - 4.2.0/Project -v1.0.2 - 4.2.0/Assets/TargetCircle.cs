﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetCircle : MonoBehaviour {
	public GameObject unit;
	public float range;
	public List<GameObject> points = new List<GameObject>();


	// Use this for initialization
	void Start () {
	
		float degree = 360 / points.Count;
		for (int i = 0; i < points.Count; i++) {
			Vector3 location = this.gameObject.transform.position;
			location.y += 20;
			location.x += Mathf.Sin (Mathf.Deg2Rad * degree *i) * range;
			location.z += Mathf.Cos (Mathf.Deg2Rad* degree *i) * range;

			points [i].transform.position = location;
		
		
		}


	}



	// Update is called once per frame
	void Update () {
		Vector3 location = unit.transform.position;
		location.y += 20;
		this.gameObject.transform.position = location;
	}




}
