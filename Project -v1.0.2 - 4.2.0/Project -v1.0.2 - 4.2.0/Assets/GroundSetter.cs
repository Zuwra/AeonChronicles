using UnityEngine;
using System.Collections;

public class GroundSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RaycastHit objecthit;
		Vector3 down = this.gameObject.transform.TransformDirection (Vector3.down);

		if (Physics.Raycast (this.gameObject.transform.position, down, out objecthit, 1000,  1 << 8)) {

			this.gameObject.transform.position = new Vector3(this.transform.position.x, objecthit.point.y-2, this.transform.position.z);
		}

		Destroy (this);
	}
	

}
