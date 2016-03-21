using UnityEngine;
using System.Collections;
using Pathfinding.RVO;

public class simpleROV : MonoBehaviour {

	RVOController controller;

	Vector3 target;
	// Use this for initialization
	void Start () {
		target = this.gameObject.transform.position;
		target.x += 100;
		controller = GetComponent<RVOController> ();
	}
	
	// Update is called once per frame
	void Update () {

		controller.Move ((target - this.transform.position).normalized * 10);
	
	}
}
