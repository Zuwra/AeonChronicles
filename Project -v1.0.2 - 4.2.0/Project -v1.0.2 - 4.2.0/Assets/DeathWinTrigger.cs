using UnityEngine;
using System.Collections;

public class DeathWinTrigger : MonoBehaviour,Modifier {

	// Use this for initialization

	// This script gets added to units that need to die. Added through the TargetDeathTrigger Script
	void Start () {
		GetComponent<UnitStats> ().addDeathTrigger (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float modify(float num, GameObject obj)
	{
		GameObject.FindObjectOfType<TargetDeathVictory> ().IDied (this.gameObject);
		return num;
	}
}
