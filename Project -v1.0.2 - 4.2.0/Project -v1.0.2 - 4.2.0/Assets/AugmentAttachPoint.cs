using UnityEngine;
using System.Collections;

public class AugmentAttachPoint : MonoBehaviour {

	public GameObject myAugment;
	public Vector3 attachPoint;
	public bool showPoint;



	public void OnDrawGizmos()
	{if (showPoint) {



			Gizmos.DrawSphere (attachPoint +this.gameObject.transform.position, 1.5f);


		}
	}





}
