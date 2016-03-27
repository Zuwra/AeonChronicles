using UnityEngine;
using System.Collections;

public class MultiShotParticle : MonoBehaviour {

	private ParticleSystem ps;
	private int emmitNUm;

	public void Start() 
	{
		ps = GetComponent<ParticleSystem>();
		emmitNUm = ps.maxParticles;
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
		ps.Emit(emmitNUm);
		ps.startLifetime = ps.startLifetime;
		//ps.Play ();
	


	}
}
