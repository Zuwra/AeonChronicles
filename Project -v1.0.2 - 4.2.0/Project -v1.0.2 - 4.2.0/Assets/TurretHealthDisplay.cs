using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretHealthDisplay : HealthDisplay {

	private GameObject camy;

	public Image Icon;

	// Use this for initialization
	void Start () {
		camy = GameObject.FindObjectOfType<MainCamera> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 location = camy.transform.position;
		location.x = this.gameObject.transform.position.x;
		gameObject.transform.LookAt (location);
	}


	public void updateHealth(float input)
	{
		if (input > .60) {


			Icon.color = Color.green;

		} else if (input > .35) {


			Icon.color = Color.yellow;

		} 
		else {

			Icon.color = Color.red;

		}

	}



}
