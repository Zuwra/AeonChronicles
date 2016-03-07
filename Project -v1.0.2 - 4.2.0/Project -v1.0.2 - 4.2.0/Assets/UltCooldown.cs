using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UltCooldown : MonoBehaviour {



	public Slider slide;
	public Ability abil;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		slide.value = abil.myCost.cooldownProgress ();
	}
}
