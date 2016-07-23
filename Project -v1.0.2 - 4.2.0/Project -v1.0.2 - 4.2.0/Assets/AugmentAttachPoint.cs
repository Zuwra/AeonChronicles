using UnityEngine;
using System.Collections;

public class AugmentAttachPoint : MonoBehaviour {

	public GameObject myAugment;
	public Vector3 attachPoint;
	public bool showPoint;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnDrawGizmos()
	{if (showPoint) {



			Gizmos.DrawSphere (attachPoint +this.gameObject.transform.position, 1.5f);


		}
	}





}
