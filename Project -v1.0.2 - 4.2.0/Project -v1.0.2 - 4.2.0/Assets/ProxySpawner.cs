using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxySpawner :VisionTrigger {

	[Tooltip("Max number of guys spawned here in one game")]
	public int maxSpawnCount;
	int totalSpawned = 0;

	[Tooltip("How many guys can be in a waveThis is multiplied by the difficulty number")]
	public int waveCount;
	int currentCharges;

	public int cooldown =13;


	List<WaveSpawner.attackWave> CurrentWaves;
	public WaveContainer.EnemyWave FirstPlayWaveType;

	List<GameObject> spawnedUnits = new List<GameObject> (); // keeps tracks of current
	UnitManager manager;

	Coroutine currentSpawner;
	Coroutine currenCharger;

	int difficulty;

	void Start()
	{
		difficulty = LevelData.getDifficulty ();
		manager = GetComponent<UnitManager> ();


		CurrentWaves = ((GameObject)(Resources.Load ("WaveContainer"))).GetComponent<WaveContainer> ()
			.getWave (FirstPlayWaveType).waveRampUp;

		waveCount *= difficulty;
		currentCharges = waveCount;
	}

	public override void  UnitEnterTrigger(UnitManager manager){
		if (this.enabled) {
			if (currentSpawner == null) {
				currentSpawner = StartCoroutine (spawnEnemies ());
			}
		}
	}
	public override void  UnitExitTrigger(UnitManager manager){}


	Vector3 spawnLocation = Vector3.zero;

	IEnumerator spawnEnemies()
	{
		yield return null;

		while(manager.enemies.Count > 0 && spawnedUnits.Count < waveCount )
		{
			spawnLocation = transform.position;
			float radius = Random.Range (15,40);
			float angle = Random.Range (0, 360);

			spawnLocation.x += Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			spawnLocation.z += Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

			currentCharges--;
			spawnedUnits.Add ((GameObject)Instantiate (CurrentWaves[3].waveType[UnityEngine.Random.Range (0,CurrentWaves[3].waveType.Count)], spawnLocation, Quaternion.identity));
			if (currenCharger == null) {
				currenCharger = StartCoroutine (ReCharge ());
			}
			spawnedUnits.RemoveAll (item => item ==null);

			totalSpawned++;
			if (totalSpawned >= maxSpawnCount) {
				Destroy (this);}

			yield return new WaitForSeconds (2f);
			manager.enemies.RemoveAll (item => item == null);			
		}
		currentSpawner = null;

	}





	IEnumerator ReCharge()
	{
		while (currentCharges < waveCount) {
			currentCharges++;
			yield return new WaitForSeconds (cooldown);
		}
	}


}
