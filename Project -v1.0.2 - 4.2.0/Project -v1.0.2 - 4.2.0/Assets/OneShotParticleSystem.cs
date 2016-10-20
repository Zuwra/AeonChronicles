﻿using UnityEngine;
using System.Collections;

public class OneShotParticleSystem : MonoBehaviour {


		private ParticleSystem ps;


		public void Start() 
		{
			ps = GetComponent<ParticleSystem>();
		AudioSource aud = GetComponent<AudioSource> ();
		if (aud) {
			aud.priority += Random.Range (0, 100);
			aud.volume = ((float)Random.Range (2, 8)) / 10;
			aud.pitch +=((float)Random.Range (-10, 10)) / 10;
		}


		}

		public void Update() 
		{
			if(ps)
		{
				if(!ps.IsAlive())
				{
					Destroy(gameObject);
				}
			}
		}
	}