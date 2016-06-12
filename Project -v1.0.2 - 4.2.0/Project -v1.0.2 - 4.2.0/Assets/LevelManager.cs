using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public List<GameObject> levelIntros = new List<GameObject> ();




	// Use this for initialization
	void Awake () {
	
		foreach (GameObject obj in levelIntros) {
			obj.SetActive (false);
		}

		levelIntros [LevelData.currentLevel].SetActive (true);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
