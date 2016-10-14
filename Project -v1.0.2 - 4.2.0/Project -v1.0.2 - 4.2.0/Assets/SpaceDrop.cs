using UnityEngine;
using System.Collections;

public class SpaceDrop : MonoBehaviour {

	public float speed;


	//private float distance;
	private float currentDistance;

	//public ProjectileMover mover;

	//private CharacterController control;


	private Vector3 lastLocation;


	// Use this for initialization
	void Start () {	


		//control = GetComponent<CharacterController> ();


	}


	public void setLocation(Vector3 loc)
	{



		lastLocation = loc;
		//distance = Vector3.Distance (this.gameObject.transform.position, lastLocation);
		airmover myAir = GetComponent<airmover> ();
		if (myAir) {
			lastLocation += Vector3.up * myAir.flyerHeight/1.5f;
		}
		//gameObject.transform.LookAt (lastLocation);
	}

	// Update is called once per frame
	void Update () {


		if(Vector3.Distance(lastLocation ,this.transform.transform.position) < 3)
		{
			Terminate();
		}


		gameObject.transform.Translate ((lastLocation -this.transform.transform.position )* speed * Time.deltaTime );

		currentDistance += speed * Time.deltaTime ;



	}


	public virtual void Terminate()
	{

		Destroy (this);

	}





}

