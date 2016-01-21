using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class RaceInfo : MonoBehaviour {


	public enum raceType{Daexa, Urden}

	public raceType race;
	public string subtitle;

	public List<GameObject> unitList = new List<GameObject>();
	public List<GameObject> buildingList = new List<GameObject>();
	public List<GameObject> attachmentsList = new List<GameObject>();

	public Material PowerGraph;


	[TextArea(3,10)]
	public string summary;
	[TextArea(3,10)]
	public string playingAgainst;
	[TextArea(3,10)]
	public string playingAs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
