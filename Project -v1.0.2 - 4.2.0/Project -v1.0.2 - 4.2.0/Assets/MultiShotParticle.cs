using UnityEngine;
using System.Collections;

public class MultiShotParticle : MonoBehaviour {

	private ParticleSystem ps;


	public void Start() 
	{
		ps = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
	
	}


	public void playEffect()
	{
		if (ps.isPlaying) {
			//ps.Stop ();
			Debug.Log ("Stopping");
		}
		//ps.Clear ();
		ps.Emit(1);
		ps.startLifetime = ps.startLifetime;
		//ps.Play ();
	


	}
}
