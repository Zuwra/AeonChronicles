using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialPage : MonoBehaviour {

	public List<GameObject> advanced = new List<GameObject>();


	// Use this for initialization
	void Start () {
	

		foreach (GameObject obj in advanced) {
			obj.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
