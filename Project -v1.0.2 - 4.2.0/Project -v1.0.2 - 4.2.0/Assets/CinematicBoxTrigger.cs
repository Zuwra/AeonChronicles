using UnityEngine;
using System.Collections;

public class CinematicBoxTrigger : MonoBehaviour {


	public int sceneNumber;
	public bool showUI;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter(Collider other)
	{
		CinematicCamera.main.trigger(sceneNumber, 0 , Vector3.zero,null,false);

		

	}

}
