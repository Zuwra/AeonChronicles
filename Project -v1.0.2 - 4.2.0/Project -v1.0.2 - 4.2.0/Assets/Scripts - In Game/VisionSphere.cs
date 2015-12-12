using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionSphere : MonoBehaviour {



	
	public List<GameObject> enemies = new List<GameObject>();
	public UnitManager myManager;

	// Use this for initialization
	void Start () {myManager = this.gameObject.GetComponentInParent<UnitManager> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger) {

			//if (other.gameObject.layer.Equals("Unit"))
			UnitManager manage = other.GetComponent<UnitManager> ();
			if (manage == null) {
				manage = other.GetComponentInParent<UnitManager> ();
			}
			
			if (manage != null) {
				if (other.GetComponent<UnitManager> ().PlayerOwner != myManager.PlayerOwner) {

					enemies.Add (other.gameObject);
					
					
				}
			}

		}
	}
		
			
	public GameObject getClosestEnemy()
				{
				GameObject best = null;
		float distance = 100000;
				foreach (GameObject obj in enemies) {
			if (obj == null) {
				enemies.Remove (obj);
			} else {

				float tempDist = Vector3.Distance (obj.transform.position, this.gameObject.transform.position);
				if (tempDist < distance) {
					best = obj;
					distance = tempDist;
				}
			}
		}
		return best;
			}


	void OnTriggerExit(Collider other)
	{enemies.Remove (other.gameObject);
		
	}


}
