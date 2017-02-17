﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSpawner : MonoBehaviour {



	public List<Vector3> spawnPoints;


	GameObject[] usedPoints;

	int diff;
	public GameObject tunnelObject;
	[Tooltip("How many seconds on average between each spawn")]
	public float spawnRate;
	public float firstSpawnTime;
	[Tooltip("How many second knoecked off the spawn rate with each spawn")]
	public float increaseSpawnRate;

	// Use this for initialization
	void Start () {
		usedPoints = new GameObject[spawnPoints.Count];
		diff = LevelData.getDifficulty ();
	
		firstSpawnTime -= ((diff -1) * 15);
		increaseSpawnRate += ((diff -1) * .2f);
		spawnRate -= ((diff -1) * 2);

		Invoke("SpawnWave", firstSpawnTime);
	}
	
	// Update is called once per frame
	void SpawnWave () {

		bool used= false;
		for (int i = 0; i < 6; i++) {
			int index = Random.Range (0, spawnPoints.Count);
			if (usedPoints [index] == null) {
				Instantiate (tunnelObject, spawnPoints [index], Quaternion.identity);
				used = true;
				break;
			}
		}

		if (!used) {
			for (int i = 0; i < spawnPoints.Count; i++) {
				if (usedPoints [i] == null) {
					Instantiate (tunnelObject, spawnPoints [i], Quaternion.identity);
					break;
				}
			}
		}
		spawnRate -= increaseSpawnRate;
		if (spawnRate < 8) {
			spawnRate = 8;
		}

		Invoke("SpawnWave", spawnRate + Random.Range(-20,20));

	}

	void OnDrawGizmos()
	{
		foreach (Vector3 vec in spawnPoints) {
		
			Gizmos.DrawSphere (vec, 3);
		}



	}
}
