using UnityEngine;
using System.Collections;


public class ResourceDropOff : MonoBehaviour {

	public bool ResourceOne;
	public bool ResourceTwo;

	// Use this for initialization
	void Start () {
		GameObject.Find ("GameRaceManager").GetComponent<RaceManager> ().addDropOff (this.gameObject);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}







}
