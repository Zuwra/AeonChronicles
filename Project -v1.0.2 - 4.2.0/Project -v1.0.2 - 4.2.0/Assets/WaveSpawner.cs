using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

	public float attackRadius;

	private float nextActionTime = 10000;
	private attackWave nextWave ;

	public Vector3 rallyPoint;
	public bool showPoint;

	DifficultyManager difficultyM;
	public List<attackWave> myWaves;

	[System.Serializable]
	public class attackWave
	{public List<GameObject> waveType;
		public List<GameObject> mediumExtra;
		public List<GameObject> HardExtra;
		public float releaseTime;
		public int repeat;
		public float repeatAddOn;

		public void setRelease (float n)
		{
			releaseTime = n;
			Debug.Log ("My relase is now " + releaseTime);
		}

		public List<SceneEventTrigger> myTriggers;
	}

	// Use this for initialization
	void Start () {

		difficultyM = GameObject.FindObjectOfType<DifficultyManager> ();
		findNextWave ();


	
	}





	// Update is called once per frame
	void Update () {
		if (Clock.main.getTotalSecond()> nextActionTime) {

			float delay = .1f;
			foreach (SceneEventTrigger trig in nextWave.myTriggers) {
				trig.hasTriggered = false;
				trig.trigger (0, 0, Vector3.zero, null, false);
			}

			foreach (GameObject obj in nextWave.waveType) {


				StartCoroutine(MyCoroutine(delay, obj));
				delay += .2f;

			}

			if (LevelData.getDifficulty () >= 2) {
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
	
	}

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
		Debug.Log ("Making new guys! " + nextActionTime);
		yield return new WaitForSeconds(.1f);

		Vector3 attackzone = rallyPoint;
		float radiusA = Random.Range(0, 50);
		float angleA = Random.Range(0, 360);

		attackzone.x += Mathf.Sin(Mathf.Deg2Rad * angleA) * radiusA;
		attackzone.z +=  Mathf.Cos(Mathf.Deg2Rad * angleA)* radiusA;


		difficultyM.SetUnitStats (unit);
		Debug.Log ("just made " + unit);
		unit.GetComponent<UnitManager> ().GiveOrder (Orders.CreateAttackMove (attackzone));

	}



	public void findNextWave()
		{
		nextActionTime = 10000000;
	
	

		foreach (attackWave aw in myWaves) {
			if (aw.releaseTime < nextActionTime) {
				nextActionTime = aw.releaseTime;
				nextWave = aw;
			
			}
		
		}

		if (nextWave.repeat == 1) {
			myWaves.Remove (nextWave);

		} else {
			nextWave.repeat--;
			nextWave.setRelease (nextWave.releaseTime + nextWave.repeatAddOn);// .releaseTime += nextWave.repeatAddOn;
		
		}


	}


	public void spawnWave()
	{
		
		float delay = .1f;

			foreach (GameObject obj in nextWave.waveType) {
				
				StartCoroutine (MyCoroutine (delay, obj));
				delay += .2f;
			}


	}

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