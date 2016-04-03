using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetCircleManager : MonoBehaviour {

	public List<TargetCircle> circles = new List<TargetCircle>();

	public List<TargetCircle> currentCircles = new List<TargetCircle> ();

	public bool working;
	private float nextActionTime;


	// Use this for initialization
	void Start () {

		nextActionTime = Time.time + .1f;
		foreach (TargetCircle c in circles) {
			c.gameObject.SetActive (false);
		}
	}


	public void turnOff()
	{
		foreach (TargetCircle c in currentCircles) {
			c.unit = null;
			c.turnOff();
		}

		currentCircles.Clear ();
	}

	public void loadUnits(RTSObject obj, float r)
	{
		turnOff ();
		currentCircles.Add (circles [0]);
		currentCircles [0].Initialize (r, obj.gameObject);

	}


	public void loadUnits(List<RTSObject> obj, float r)
	{
		
		while (obj.Count > circles.Count) {
		
			GameObject o = (GameObject)Instantiate (circles [0].gameObject);
			circles.Add (o.GetComponent<TargetCircle>());

		}

		for (int i = 0; i < obj.Count; i++) {
			currentCircles.Add (circles [i]);
			currentCircles [i].Initialize (r, obj [i].gameObject);

		
		}

	}





	
	// Update is called once per frame
	void Update () {
		if (!working) {
			return;}
			if (Time.time > nextActionTime) {
				nextActionTime = Time.time + .1f;
		
			foreach (TargetCircle cir in currentCircles) {
					foreach (GameObject obj in cir.points) {
						
					foreach (TargetCircle inner in currentCircles) {
							if (inner == cir) {
							
								continue;
							}
					

							bool temp = inRange (obj.transform.position, inner.transform.position, inner.range);
							obj.SetActive (temp);
							if (!temp) {
							
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
