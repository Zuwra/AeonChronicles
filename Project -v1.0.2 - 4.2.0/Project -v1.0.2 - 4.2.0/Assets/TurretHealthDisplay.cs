using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretHealthDisplay : HealthDisplay {

	private GameObject camy;

	public Image Icon;
	public Text percentHealth;
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
		int casted = (int)(input * 100);

		if (casted > 98) {
			percentHealth.text = "";
		} else {
			percentHealth.text = casted + "%";
		}

		if (input > .60) {

			percentHealth.color = Color.green;
			Icon.color = Color.green;

		} else if (input > .35) {
			percentHealth.color = Color.yellow;

			Icon.color = Color.yellow;

		} 
		else {
			percentHealth.color = Color.red;
			Icon.color = Color.red;

		}

	}



}
