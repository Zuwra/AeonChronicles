using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthQuake : MonoBehaviour {


	public Slider cooldownSlider;
	public float CooldownTime;
	bool Coolingdown;
	RaceManager enemyRace;
	public Text percentage;
	public float CameraShakeIntensity = 8;
	int lastPercent;
	public SurviveVictory survival;

	public MultiShotParticle myParticle;
	public GameObject QuakeBuilding;
	// Use this for initialization
	void Start () {
		enemyRace = GameObject.FindObjectOfType<GameManager> ().playerList [1];
		StartCoroutine (onCooldown ());
	}



	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
			generateEarthQuake ();
		}
	}

	IEnumerator onCooldown()
	{
		float timer = CooldownTime;
		Coolingdown = true;
		cooldownSlider.gameObject.SetActive(true);
		while (timer > 0) {
		

			timer -= Time.deltaTime;
			cooldownSlider.value =  1 -timer / CooldownTime;

			int i = (int)(cooldownSlider.value * 100);
			if (i != lastPercent) {
				percentage.text = i + "%";
			}
			lastPercent = i;
			yield return null;

		}
		Coolingdown = false;
		cooldownSlider.gameObject.SetActive (false);

	}

	public void generateEarthQuake()
	{
		if (!Coolingdown) {
			MainCamera.main.ShakeCamera (1.2f,CameraShakeIntensity);
			if (myParticle) {
				myParticle.playEffect ();
			}
			if (survival) {
				survival.increaseWait ();
			}
			PlayerPrefs.SetInt ("EarthQuake", PlayerPrefs.GetInt ("EarthQuake") + 1);
			if (QuakeBuilding) {
				QuakeBuilding.GetComponentInChildren<Animator> ().SetTrigger("Pulse");}

		

			//Making an array to not change the original list while iterating through

			foreach (KeyValuePair<string, List<UnitManager>> pair in enemyRace.getUnitList ()) {
				UnitManager[] unitListCopy = pair.Value.ToArray ();

				for (int i = 0; i < unitListCopy.Length; i++) {
					if(Vector3.Distance(QuakeBuilding.transform.position, unitListCopy[i].transform.position) < 400){
						unitListCopy[i].myStats.TakeDamage (250, null, DamageTypes.DamageType.Penetrating);
						unitListCopy[i].StunForTime (QuakeBuilding, 10);
					}
				}
			}
				

			StartCoroutine (onCooldown ());

		}

	}


}
