using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

	public float attackRadius;

	//private attackWave nextWave ;

	public Vector3 rallyPoint;
	public bool showPoint;
	public bool triggerOnly = false;
	DifficultyManager difficultyM;
	public List<attackWave> myWaves;

	public List<WaveContainer.EnemyWave> ReplayWaves;

	//RaceManager raceMan;

	[System.Serializable]
	public class attackWarning
	{
		[TextArea(2,10)]
		public string textWarning;
		public AudioClip audioWarning;
		public Sprite myPic;
	}

	[System.Serializable]
	public class attackWave
	{public List<GameObject> waveType = new List<GameObject>();
		public List<GameObject> mediumExtra= new List<GameObject>();
		public List<GameObject> HardExtra= new List<GameObject>();


		public List<attackWarning> warnings = new List< attackWarning>();
		public List<SceneEventTrigger> myTriggers = new List<SceneEventTrigger> ();
	}

	// Use this for initialization
	void Awake () {


		if (LevelData.getHighestLevel () > 3) {

			if (ReplayWaves.Count > 0) {
				
				myWaves = new List<attackWave> ();

				List<attackWave> tempWaves = ((GameObject)(Resources.Load ("WaveContainer"))).GetComponent<WaveContainer> ()
					.getWave (ReplayWaves [UnityEngine.Random.Range (0, ReplayWaves.Count - 1)]).waveRampUp;

				for (int i = 0; i < tempWaves.Count; i++) {
					myWaves.Add (new attackWave ());
					//Debug.Log (i + "  " + firstRelease + "   " + addon);

					foreach (GameObject ob in tempWaves[i].waveType) {
						myWaves [i].waveType.Add (ob);
					}

					foreach (GameObject ob in tempWaves[i].mediumExtra) {
						myWaves [i].mediumExtra.Add (ob);
					}

					foreach (GameObject ob in tempWaves[i].HardExtra) {
						myWaves [i].HardExtra.Add (ob);
					}

				// Still need to add in code for attack warnings and triggers
				}

			
			}

		}


		//raceMan = GameObject.FindObjectOfType<GameManager> ().activePlayer;
		difficultyM = GameObject.FindObjectOfType<DifficultyManager> ();
	
	
	}


	/*


	// Update is called once per frame
	void Update () {
		if (Clock.main.getTotalSecond()> nextActionTime) {

			float delay = .1f;

			if (nextWave.warnings.Count > 0) {
				int n = UnityEngine.Random.Range (0, nextWave.warnings.Count - 1);

				ExpositionDisplayer.instance.displayText (nextWave.warnings [n].textWarning, 7, nextWave.warnings [n].audioWarning, .93f, nextWave.warnings[n].myPic,4);

			}


			foreach (MiniMapUIController mini in GameObject.FindObjectsOfType<MiniMapUIController>()) {
				mini.showWarning (this.transform.position);
			}

			foreach (SceneEventTrigger trig in nextWave.myTriggers) {
				if (trig) {
					trig.hasTriggered = false;
					trig.trigger (0, 0, Vector3.zero, null, false);
				}
			}

			foreach (GameObject obj in nextWave.waveType) {

				StartCoroutine(MyCoroutine(delay, obj));
				delay += .2f;

			}

			if (LevelData.getDifficulty () >= 2) {
				SpawnExtra (nextWave);
				foreach (GameObject obj in nextWave.mediumExtra) {
					StartCoroutine (MyCoroutine (delay, obj));
					delay += .2f;

				}
			}
			if (LevelData.getDifficulty () >= 3) {
		
				foreach (GameObject obj in nextWave.HardExtra) {


					StartCoroutine (MyCoroutine (delay, obj));
					delay += .2f;

				}
			}

			nextActionTime = 10000;
		
			findNextWave ();
		}
	
	}*/

	//autobalancing based on how many units the player has
	/*
	void SpawnExtra(attackWave myWave)
	{float delay = .1f;
		if (raceMan.getArmyCount () * .75 > myWave.waveType.Count + myWave.HardExtra.Count + myWave.mediumExtra.Count) {
			foreach (GameObject obj in nextWave.waveType) {

				StartCoroutine(MyCoroutine(delay, obj));
				delay += .2f;

			}
		}

		if (raceMan.getArmyCount () * .50 > myWave.waveType.Count + myWave.HardExtra.Count + myWave.mediumExtra.Count) {
			foreach (GameObject obj in nextWave.HardExtra) {

				StartCoroutine(MyCoroutine(delay, obj));
				delay += .2f;

			}
		}
	}

*/
	IEnumerator MyCoroutine (float amount, GameObject obj)
	{
		yield return new WaitForSeconds(amount);

		Vector3 hitzone = this.gameObject.transform.position;
		float radius = Random.Range(attackRadius/2, attackRadius);
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

		Vector3 attackzone = rallyPoint;
		float radiusA = Random.Range(0, 50);
		float angleA = Random.Range(0, 360);

		attackzone.x += Mathf.Sin(Mathf.Deg2Rad * angleA) * radiusA;
		attackzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angleA)* radiusA;


		difficultyM.SetUnitStats (unit);
		//Debug.Log ("just made " + unit);
		unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateAttackMove (attackzone));

	}




	/*
	public void spawnWave()
	{
		
		float delay = .1f;

			foreach (GameObject obj in nextWave.waveType) {
				
				StartCoroutine (MyCoroutine (delay, obj));
				delay += .2f;
			}
		if (nextWave.warnings.Count > 0) {
			int n = UnityEngine.Random.Range (0, nextWave.warnings.Count - 1);

			ErrorPrompt.instance.showMessage (nextWave.warnings [n].textWarning, nextWave.warnings [n].audioWarning);
		}

	}
*/
	public void spawnWave(int n )
	{

		float delay = .1f;

		Debug.Log ("This spawner " + this.gameObject);
		foreach (GameObject obj in myWaves[n].waveType) {

			StartCoroutine (MyCoroutine (delay, obj));
			delay += .2f;
		}


	}

	public void OnDrawGizmos()
	{if (showPoint) {


			Gizmos.color = Color.blue;
			Gizmos.DrawLine (transform.position, rallyPoint);
		}
			

	}

}