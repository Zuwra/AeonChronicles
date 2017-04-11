using UnityEngine;
using System.Collections;

public class ParticleAn : animate {

	private ParticleSystem ps;
	private bool change;

	private int emmisionRate;
	public void Start() 
	{
		ps = GetComponent<ParticleSystem>();
		change = active;
		emmisionRate = ps.main.maxParticles;
		ParticleSystem.MainModule myModule = ps.main;
		myModule.maxParticles = 0;
		ps.maxParticles = 0;
	}

	// Update is called once per frame
	void Update () {
		if (change != active) {
			change = active;

			if (!change) {

			
				ps.maxParticles = 0;
			} else {
				
				ps.maxParticles = emmisionRate;
			}
			ps.startLifetime = ps.main.startLifetime.constant;

		}
	
	}
}
