using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetCircleManager : MonoBehaviour {

	public List<TargetCircle> circles = new List<TargetCircle>();
	public bool working;
	private float nextActionTime;


	// Use this for initialization
	void Start () {
	
		nextActionTime = Time.time + .1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (working) {
			if (Time.time > nextActionTime) {
				nextActionTime = Time.time + .1f;
		
				foreach (TargetCircle cir in circles) {
					foreach (GameObject obj in cir.points) {
						bool cont =false;
						foreach (TargetCircle inner in circles) {
							if (inner == cir) {
								continue;
							}
							if (inRange (cir.gameObject.transform.position, inner.transform.position, inner.range + cir.range)) {

								cont = true;
								continue;
							}

							bool temp = inRange (obj.transform.position, inner.transform.position, inner.range);
							obj.SetActive (temp);
							if (!temp) {
								break;
							}
						}
						if (cont) {
							
							break;
						}
					}
			
				}

		
			}
		}
	
	}

	public bool inRange(Vector3 a, Vector3 b, float range)
	{
		float x = a.x - b.x;
		float y = a.z - b.z;

		return (Mathf.Sqrt (Mathf.Pow (x, 2) + Mathf.Pow (y,2)) >= range);


	}


}
