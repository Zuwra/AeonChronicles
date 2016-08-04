using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

	public float attackRadius;
	public List<GameObject> waveTypeOne = new List<GameObject> ();
	public List<float> waveOneTimes = new List<float> ();

	public List<GameObject> waveTypeTwo = new List<GameObject> ();
	public List<float> waveTwoTimes = new List<float> ();

	public List<GameObject> waveTypeThree = new List<GameObject> ();
	public List<float> waveThreeTimes = new List<float> ();

	private float nextActionTime = 10000;
	private List<GameObject> nextWave ;

	public Vector3 rallyPoint;
	public bool showPoint;
	// Use this for initialization
	void Start () {
		findNextWave ();


	
	}



	// Update is called once per frame
	void Update () {
		if (Clock.main.getTotalSecond()> nextActionTime) {

			float delay = .1f;
			foreach (GameObject obj in nextWave) {


				StartCoroutine(MyCoroutine(delay, obj));
				delay += .2f;

			}


			nextActionTime = 10000;
		
			findNextWave ();
		}
	
	}

	IEnumerator MyCoroutine (float amount, GameObject obj)
	{
		yield return new WaitForSeconds(amount);

		Vector3 hitzone = this.gameObject.transform.position;
		float radius = Random.Range(attackRadius/2, attackRadius);
		float angle = Random.Range(0, 360);

		hitzone.x += Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
		hitzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angle)* radius;
		hitzone.y -=10;

		GameObject unit = (GameObject)Instantiate (obj, hitzone, Quaternion.identity);
		yield return new WaitForSeconds(.1f);

		Vector3 attackzone = rallyPoint;
		float radiusA = Random.Range(0, 50);
		float angleA = Random.Range(0, 360);

		attackzone.x += Mathf.Sin(Mathf.Deg2Rad * angleA) * radiusA;
		attackzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angleA)* radiusA;


		unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateAttackMove (attackzone));

	}


	public void findNextWave()
		{
		foreach (float wave in waveOneTimes) {
			if (wave < nextActionTime) {
		
				nextActionTime = wave;
				nextWave = waveTypeOne;
			}
		}

		foreach (float wave in waveTwoTimes) {
			if (wave < nextActionTime) {
				
				nextActionTime = wave;
				nextWave = waveTypeTwo;
			}
		}

		foreach (float wave in waveThreeTimes) {
			if (wave < nextActionTime) {

				nextActionTime = wave;
				nextWave = waveTypeThree;
			}
		}

		waveOneTimes.Remove (nextActionTime);
	
		waveTwoTimes.Remove (nextActionTime);
		waveThreeTimes.Remove (nextActionTime);

	}


	public void spawnWave(int n)
	{List<GameObject> toSpawn;
		Debug.Log ("Spawning wave");
		if (n == 0) {
			toSpawn = waveTypeOne;
		} else if (n == 1) {
			toSpawn = waveTypeTwo;
		} else {
			toSpawn = waveTypeThree;
		}
		float delay = .1f;
		foreach (GameObject obj in toSpawn) {
			Debug.Log ("making unit");
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