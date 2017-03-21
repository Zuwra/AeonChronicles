using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour {




	public float dropPeriod;
	public GameObject toDrop;
	// Use this for initialization
	void Start () {

		InvokeRepeating ("DropObject", dropPeriod, dropPeriod);
	}


	public void DropObject()
	{
		RaycastHit objecthit;
		Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

		if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000,  1 << 8)) {

			Instantiate (toDrop, new Vector3(this.transform.position.x, objecthit.point.y, this.transform.position.z), Quaternion.identity);
		}


	}

}
