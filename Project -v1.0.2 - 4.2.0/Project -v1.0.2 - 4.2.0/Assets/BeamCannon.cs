using UnityEngine;
using System.Collections;

public class BeamCannon : MonoBehaviour {

	private float nextActionTIme;
	public GameObject Shot1;
	public GameObject Shot2;
	public GameObject Wave;
	public float Disturbance = 0;

	public int ShotType = 0;

	private GameObject NowShot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.time > nextActionTIme) {
			nextActionTIme = Time.time + .15f;
			GameObject Bullet = Shot1;
			//Fire
			GameObject s1 = (GameObject)Instantiate (Bullet, this.transform.position, this.transform.rotation);
			//s1.GetComponent<BeamParam> ().SetBeamParam (this.GetComponent<BeamParam> ());
			s1.transform.localScale *=4;
			GameObject wav = (GameObject)Instantiate (Wave, this.transform.position, this.transform.rotation);
			wav.transform.localScale *=  4;
			wav.transform.Rotate (Vector3.left, 90.0f);
			//wav.GetComponent<BeamWave> ().col = this.GetComponent<BeamParam> ().BeamColor;

		}


	}
}
