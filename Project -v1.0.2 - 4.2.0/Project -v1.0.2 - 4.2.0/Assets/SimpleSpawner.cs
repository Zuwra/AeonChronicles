using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour {


	public float spawnRate;
	public bool increasingSpawnRate;
	public List<GameObject> enemyTypes;
	DifficultyManager difficultyM;
	Vector3 attackPoint;


	// Use this for initialization
	void Start () {
		difficultyM = GameObject.FindObjectOfType<DifficultyManager> ();
		if (enemyTypes.Count > 0) {
			Invoke ("SpawnEnemy", 20);
		}

		spawnRate -= ((LevelData.getDifficulty() -1) * 2);
		attackPoint = GameObject.FindObjectOfType<sPoint> ().transform.position;
	}

	public void setSpawnRate(float time)
	{
		spawnRate = time;
	}

	void SpawnEnemy()
	{


		
		GameObject unit = (GameObject)Instantiate (enemyTypes [Random.Range (0, enemyTypes.Count - 1)], this.transform.position, Quaternion.identity);
		difficultyM.SetUnitStats (unit);
		unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateAttackMove (attackPoint));
		if (increasingSpawnRate && spawnRate > .8f) {
			spawnRate -= 1;
			if (spawnRate < .8f) {
				spawnRate = .8f;
			}
		}
		Invoke ("SpawnEnemy", Mathf.Max(1, spawnRate + Random.Range(-10,10)));
	}

}
