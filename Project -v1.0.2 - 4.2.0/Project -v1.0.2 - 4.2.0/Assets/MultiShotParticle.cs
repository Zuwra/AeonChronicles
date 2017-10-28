using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiShotParticle : MonoBehaviour {

	private ParticleSystem ps;
	private List<ParticleSystem> otherP;
	private int emmitNUm;
	private AudioPlayer myPlayer;

	public void Awake() 
		{otherP = new List<ParticleSystem> ();
		ps = GetComponent<ParticleSystem>();
		emmitNUm = ps.maxParticles;
		myPlayer = GetComponent<AudioPlayer> ();

		recurseAddChildren (transform);

		this.gameObject.SetActive (false);
	}

	void recurseAddChildren(Transform t)
	{
		foreach (ParticleSystem pps in t.GetComponents<ParticleSystem>()) {
			otherP.Add (pps);
		}
		foreach (Transform trans in t) {
			recurseAddChildren (trans);
	}
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

		if (myPlayer) {
			myPlayer.Start ();
		}
		//ps.Clear ();
		if (ps) {
			ps.Emit (emmitNUm);
			ps.startLifetime = ps.startLifetime;
		}
		if (otherP != null) {
			foreach (ParticleSystem pps in otherP) {
				pps.Emit (emmitNUm);
				pps.startLifetime = ps.startLifetime;
			}
		}
	}
}
