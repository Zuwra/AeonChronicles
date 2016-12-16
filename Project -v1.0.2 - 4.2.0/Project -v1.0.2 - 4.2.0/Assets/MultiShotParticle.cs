using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiShotParticle : MonoBehaviour {

	private ParticleSystem ps;
	private List<ParticleSystem> otherP;
	private int emmitNUm;

	public void Start() 
		{otherP = new List<ParticleSystem> ();
		ps = GetComponent<ParticleSystem>();
		emmitNUm = ps.maxParticles;

		foreach (ParticleSystem pps in GetComponentsInChildren<ParticleSystem>()) {
			otherP.Add (pps);
		}
		this.gameObject.SetActive (false);
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

		foreach (ParticleSystem pps in otherP) {
			pps.Emit(emmitNUm);
			pps.startLifetime = ps.startLifetime;
		}

	}
}
