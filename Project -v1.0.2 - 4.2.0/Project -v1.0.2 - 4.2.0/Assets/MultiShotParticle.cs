using UnityEngine;
using System.Collections;

public class MultiShotParticle : MonoBehaviour {

	private ParticleSystem ps;
	private int emmitNUm;

	public void Start() 
	{
		ps = GetComponent<ParticleSystem>();
		emmitNUm = ps.maxParticles;

		this.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	
	}


	public void stopEffect()
	{
		this.gameObject.SetActive (false);


	}

	public void continueEffect()
	{this.gameObject.SetActive (true);


	}

	public void playEffect()
	{this.gameObject.SetActive (true);


		//ps.Clear ();
		ps.Emit(emmitNUm);
		ps.startLifetime = ps.startLifetime;

	}
}
