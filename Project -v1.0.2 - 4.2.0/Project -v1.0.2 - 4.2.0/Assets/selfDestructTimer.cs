using UnityEngine;
using System.Collections;

public class selfDestructTimer : MonoBehaviour {
	public float timer;
	public bool showTimer;
	private float deathTime;

	private Selected hd;
	// Use this for initialization
	void Start () {
		if (showTimer) {
			hd = GetComponent<Selected> ();
		}

		deathTime = Time.time + timer;



	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > deathTime) {
			if (hd) {
				hd.updateCoolDown (1);
			}
				if (GetComponent<UnitStats> ()) {
				GetComponent<UnitStats> ().kill (null);
			} else {
				Destroy (this.gameObject);}
		}
			if(hd){
		hd.updateCoolDown ((deathTime - Time.time) / timer);
			}
		}

	




	

}
