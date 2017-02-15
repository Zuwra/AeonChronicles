using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour {


	public float spawnRate;

	public List<GameObject> enemyTypes;

	Vector3 attackPoint;


	// Use this for initialization
	void Start () {
		if (enemyTypes.Count > 0) {
			Invoke ("SpawnEnemy", 15);
		}
		attackPoint = GameObject.FindObjectOfType<sPoint> ().transform.position;
	}

	void SpawnEnemy()
	{


		
		GameObject unit = (GameObject)Instantiate (enemyTypes [Random.Range (0, enemyTypes.Count - 1)], this.transform.position, Quaternion.identity);
		unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateAttackMove (attackPoint));
		Invoke ("SpawnEnemy", spawnRate + Random.Range(-10,10));
	}

}
