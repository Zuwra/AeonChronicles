using UnityEngine;
using System.Collections;



public class EffectSimpleContinuousParticle :MonoBehaviour {

	/*
	ParticleSystem[] _particleSystems;
	private bool _isStopped;
	private float _elapsedTime;
	public float Duration = 1f;
	public bool StopOnDuration = false;
	// Use this for initialization
	void Start () {
		_isStopped = false;
		if(!_isInitialized){
			InitializeEffect();
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if(StopOnDuration){
			_elapsedTime += Time.deltaTime;
			if(_elapsedTime >= Duration){
				StopEffect();
			}
		}

		if(_isPlaying && _isStopped){
			bool readyToStop = false;
			foreach(ParticleSystem p in _particleSystems){
				readyToStop |= p.isPlaying;
			}

			if(!readyToStop){
				StopEffectImmediately();
			}
		}
	}
	
	#region implemented abstract members of EffectBase
	
	protected override void InitializeEffect(){
		_isInitialized = true;
		_particleSystems = this.gameObject.GetComponentsInChildren<ParticleSystem>();


		foreach(ParticleSystem p in _particleSystems){
			p.playOnAwake = false;
			p.Stop();
		}
	}
	
	protected override void StartEffect ()
	{if (_particleSystems != null) {
						foreach (ParticleSystem p in _particleSystems) {
								p.enableEmission = true;
								p.loop = true;
								p.Play ();
						}
				}
				else {
			Debug.Log("NO PARTICLE SYSTEMS!" + this.gameObject.name);}
	}

	public override void StopEffect ()
	{
		_isStopped = true;
		foreach(ParticleSystem p in _particleSystems){
			p.enableEmission = false;
			p.loop = false;
		}
	}

	public override float GetAnimationRate ()
	{
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
		float retVal = 0;
		foreach(ParticleSystem p in _particleSystems){
			retVal = Mathf.Max (retVal, p.emissionRate);
		}
		return retVal;
	}
	
	public override void SetAnimationRate (float val)
	{
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem p in _particleSystems){
			p.emissionRate = val;
		}
	}
	
	public override void MultiplyAnimationRate (float mult)
	{
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem p in _particleSystems){
			p.emissionRate *= mult;
		}
	}
	
	#endregion*/
}
