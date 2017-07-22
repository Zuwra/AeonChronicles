using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {


	//int GeneralIndex = 0; // Hadrian = 0, Rucks = 1, Katrina =2, Carbot = 3 



	public WaveContainer.EnemyWave FirstPlayWaveType;
	public List<WaveContainer.EnemyWave> ReplayWaves;

	public List<GameObject> spawnLocations;
	public GameObject EmergencySpawnLocation;

	List<WaveSpawner.attackWave> CurrentWaves;
	public List<waveSetting> myWaves;
	public Vector3 firstRallyPoint;


	int currentWaveIndex;
	private float nextActionTime = 10000;
	int SpawnerCount;

	DifficultyManager difficultyM;
	RaceManager raceMan;

	WaveContainer container = null;
	WaveContainer.WaveOption waveOption;

	[System.Serializable]
	public class waveSetting
	{
		public float waveSpawnTime;
		public GameObject SpawnObject;
		[Tooltip("usually 0-4, how late of a wave type should it be.")]
		public int WaveAdvancement;
		[Tooltip("If it must be here and it gets destroyed, it wont spawn")]
		public bool MustBeHere;
		public List<CustomWarning> CustomWarnings = new List< CustomWarning>();
		public List<SceneEventTrigger> CustomTriggers = new List<SceneEventTrigger> ();
	}

	[System.Serializable]
	public class CustomWarning
	{
		[TextArea(2,10)]
		public List<attackWarning> warningVariants;

	}


	[System.Serializable]
	public class attackWarning
	{
		[TextArea(2,10)]
		public string textWarning;
		public AudioClip audioWarning;
		public Sprite myPic;
	}

	public WaveContainer.WaveOption getCurrentWaveType()
	{
		return waveOption;
	}


	void Awake () {
		//GeneralIndex = PlayerPrefs.GetInt ("VoicePack", 0);
		currentWaveIndex = 0;
		SpawnerCount = spawnLocations.Count;
		container = ((GameObject)(Resources.Load ("WaveContainer"))).GetComponent<WaveContainer> ();
		if (LevelData.getHighestLevel () > 3) {

			if (ReplayWaves.Count > 0) {
				//container = ((GameObject)(Resources.Load ("WaveContainer"))).GetComponent<WaveContainer> ();
				waveOption = container.getWave (ReplayWaves [UnityEngine.Random.Range (0, ReplayWaves.Count)]);
				CurrentWaves = waveOption.waveRampUp;


			}


		} else {
			//Debug.Log (container +" -- ");
			waveOption = container.getWave (ReplayWaves [UnityEngine.Random.Range (0, ReplayWaves.Count)]);
			CurrentWaves = ((GameObject)(Resources.Load ("WaveContainer"))).GetComponent<WaveContainer> ()
				.getWave (FirstPlayWaveType).waveRampUp;
		}


		raceMan = GameObject.FindObjectOfType<GameManager> ().activePlayer;
		difficultyM = GameObject.FindObjectOfType<DifficultyManager> ();
	
		nextActionTime = myWaves [currentWaveIndex].waveSpawnTime;
	}


	// Update is called once per frame
	void Update () {
		if (Clock.main.getTotalSecond () > nextActionTime) {

			float delay = .1f;

			if (spawnLocations.Count < SpawnerCount) {
				spawnLocations.RemoveAll (item => item == null);
				SpawnerCount = spawnLocations.Count;

			}
			GameObject spawner = null;
			if (myWaves [currentWaveIndex].MustBeHere && !myWaves [currentWaveIndex].SpawnObject) {
				setNextWave ();
				return;
			} else if ( myWaves [currentWaveIndex].SpawnObject) {
				spawner = myWaves [currentWaveIndex].SpawnObject;

			} else if (!myWaves [currentWaveIndex].MustBeHere && !myWaves [currentWaveIndex].SpawnObject && spawnLocations.Count > 0) {
				spawner = spawnLocations [Random.Range (0, spawnLocations.Count )];
			} 

			if (!spawner) {
				spawner = EmergencySpawnLocation;
			}



			if (myWaves [currentWaveIndex].CustomTriggers.Count > 0 ||
			    myWaves [currentWaveIndex].CustomWarnings.Count > 0) {
				//Display Custom Warning
				//int n = UnityEngine.Random.Range (0, nextWave.warnings.Count - 1);
				//ExpositionDisplayer.instance.displayText (nextWave.warnings [n].textWarning, 7, nextWave.warnings [n].audioWarning, .93f, nextWave.warnings[n].myPic,4);

			} else {
				ErrorPrompt.instance.EnemyWave (waveOption.warningType);

			}


			foreach (MiniMapUIController mini in GameManager.main.MiniMaps) {
				if (mini) {
					mini.showWarning (this.transform.position);
				}
			}


		

			foreach (GameObject obj in CurrentWaves[currentWaveIndex].waveType) {

				StartCoroutine (MyCoroutine (delay, obj, spawner));
				delay += .2f;

			}



			if (LevelData.getDifficulty () >= 2) {
				SpawnExtra (CurrentWaves [currentWaveIndex], spawner);
				foreach (GameObject obj in CurrentWaves[currentWaveIndex].mediumExtra) {
					StartCoroutine (MyCoroutine (delay, obj, spawner));
					delay += .2f;

				}
			}
			if (LevelData.getDifficulty () >= 3) {

				foreach (GameObject obj in CurrentWaves[currentWaveIndex].HardExtra) {


					StartCoroutine (MyCoroutine (delay, obj, spawner));
					delay += .2f;

				}
			}


			setNextWave ();
		
		}

	}


	void setNextWave()
	{
		if (currentWaveIndex < CurrentWaves.Count - 1) {
			currentWaveIndex++;
			nextActionTime = myWaves [currentWaveIndex].waveSpawnTime;
		}
		else{
			nextActionTime += 140;
		} 

	}


	//autobalancing based on how many units the player has
	void SpawnExtra(WaveSpawner.attackWave myWave, GameObject Spawner)
	{float delay = .1f;
		
			
		if (raceMan.getArmyCount () * .50 >  myWave.waveType.Count + myWave.HardExtra.Count + myWave.mediumExtra.Count) {
			foreach (GameObject obj in myWave.HardExtra) {

				StartCoroutine(MyCoroutine(delay, obj,Spawner));
				delay += .2f;

			}
		}
	}


	IEnumerator MyCoroutine (float amount, GameObject obj, GameObject spawnObject)
	{
		yield return new WaitForSeconds(amount);


		Vector3 hitzone = spawnObject.transform.position;
		float radius = Random.Range(12, 25);
		float angle = Random.Range(0, 360);

		hitzone.x += Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
		hitzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angle)* radius;
		hitzone.y -=5;

		if (obj.GetComponent<airmover> ()) {
			hitzone.y += obj.GetComponent<airmover> ().flyerHeight + 5;
		}

		GameObject unit = (GameObject)Instantiate (obj, hitzone, Quaternion.identity);

		unit.AddComponent<EnemySearchAI> ();
		//Debug.Log ("Making new guys! " + nextActionTime);
		yield return new WaitForSeconds(.1f);

		Vector3 attackzone = firstRallyPoint;
		float radiusA = Random.Range(0, 50);
		float angleA = Random.Range(0, 360);

		attackzone.x += Mathf.Sin(Mathf.Deg2Rad * angleA) * radiusA;
		attackzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angleA)* radiusA;


		difficultyM.SetUnitStats (unit);
		//Debug.Log ("just made " + unit);
		unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateAttackMove (attackzone));

	}






	public void OnDrawGizmos()
	{Gizmos.color = Color.blue;
		foreach (GameObject obj in spawnLocations) {
			if (obj) {

				Gizmos.DrawLine (obj.transform.position, firstRallyPoint);
			}
		}
		Gizmos.color = Color.red;
		Gizmos.DrawLine (EmergencySpawnLocation.transform.position, firstRallyPoint);
	}

}
