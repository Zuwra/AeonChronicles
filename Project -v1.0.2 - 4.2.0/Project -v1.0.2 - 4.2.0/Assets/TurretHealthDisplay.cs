using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretHealthDisplay : HealthDisplay {



	public Image Icon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void updateHealth(float input)
	{
		Color myColor = new Color (1-input, input, 0);
		Icon.color = myColor;

	}



}
