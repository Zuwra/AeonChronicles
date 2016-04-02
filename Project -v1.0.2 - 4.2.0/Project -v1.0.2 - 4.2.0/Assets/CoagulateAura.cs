using UnityEngine;
using System.Collections;

public class CoagulateAura : MonoBehaviour {



	private UnitStats myStats;
	private IMover myMover;
	private GameObject myAura;


	private float nextaction;

	private float endTime;
	// Use this for initialization
	void Start () {
		nextaction = Time.time + .5f;
		endTime = Time.time + 8;
		myStats = GetComponent<UnitManager> ().myStats;
		myMover = GetComponent<UnitManager> ().cMover;
	}

	// Update is called once per frame
	void Update () {

		if (Time.time > endTime) {
			myMover.removeSpeedBuff (this);
			Destroy (myAura);
			Destroy (this);
			return;}


		if (Time.time > nextaction) {

			if (myMover == null) {
				myMover.removeSpeedBuff (this);
				Destroy (myAura);
				Destroy (this);
				return;
			}
			nextaction += .5f;
			myMover.removeSpeedBuff (this);
			myMover.changeSpeed (- (1 - (myStats.health / myStats.Maxhealth)), 0, false, this);

		}

	}

	public void Initialize(GameObject source, GameObject eff)
	{nextaction = Time.time + .5f;
		myStats = GetComponent<UnitManager> ().myStats;
		myMover = GetComponent<UnitManager> ().cMover;
	
		endTime = Time.time + 8;

		if (myMover == null) {
			Destroy (this);
		}

		if (myAura == null) {
			myAura = (GameObject)Instantiate (eff, this.gameObject.transform.position, Quaternion.identity);
			myAura.transform.SetParent (this.gameObject.transform);
			myAura.transform.Rotate (new Vector3 (-90, 0, 0));
		}
		myMover.removeSpeedBuff (this);
		myMover.changeSpeed ( -(1 - (myStats.health / myStats.Maxhealth)), 0, false, this);
	
	}
}
