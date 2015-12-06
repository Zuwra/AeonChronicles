using UnityEngine;
using System.Collections;

public class AttackBase : MonoBehaviour {/*
	public AttackEnum AttackVariant = AttackEnum.Default;
	public float AttackPeriod = 1f;
	public AttackTargets TargetTypes = AttackTargets.Player;
	public DamageEffect Damage;

	public CameraShakeManager.CameraShakeEventHandler ImpactCameraShakes;
	public RumbleManager.RumbleEventHandler ImpactRumbles;

	public bool CanAttack{get{return _timeRemainingUntilAttack <= 0;}}

	public EffectBase ImpactEffect;
	public SoundInformation ImpactSoundEffect;

	private float _timeRemainingUntilAttack;

	public enum AttackTargets {Player, Monsters, Environment, PlayerMonsters, PlayerEnvironments, MonsterEnvironments, All, None}
	// Use this for initialization
	void Start () {
		if(Damage.Source == null){
			Damage.Source = gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(_timeRemainingUntilAttack > 0){
			_timeRemainingUntilAttack-= Time.deltaTime;
		}
	}




	public void Attack(Transform target)
	{
		if (CanAttack) 
		{
			HealthComponent h = null;
			Transform parent = target;

			//Get the health component of the target.
			do 
			{
				h = parent.gameObject.GetComponent<HealthComponent> ();
				parent = parent.transform.parent;
			} 
			while(h == null && parent != null);

			if (h != null) 
			{
				_timeRemainingUntilAttack = AttackPeriod;
				LaunchAttack (target);
			}
			else
			{
				//Debug.Log("Attack ignored: No health component");
			}
		} 
		else 
		{
			//Debug.Log ("Attack ignored.");
		}
	}

	protected void LaunchAttack(Transform target){
		HealthComponent h = null;
		Transform parent = target;

		do{
			h = parent.gameObject.GetComponent<HealthComponent>();
			parent = parent.transform.parent;
		}
		while(h == null && parent != null);

		if(h != null){
			bool doAttack = false;

			//ATTACK ALL
			if(TargetTypes == AttackTargets.All){
				doAttack = true;
			}

			//ATTACKS PLAYER
			if((TargetTypes == AttackTargets.Player || TargetTypes == AttackTargets.PlayerMonsters || TargetTypes == AttackTargets.PlayerEnvironments) &&
			   h.gameObject.GetComponent<PlayerBase>() != null){
				doAttack = true;
			}
		
			//ATTACKS MONSTERS
			if((TargetTypes == AttackTargets.Monsters || TargetTypes == AttackTargets.PlayerMonsters || TargetTypes == AttackTargets.MonsterEnvironments) &&
			   h.gameObject.GetComponent<MonsterBase>() != null){
				doAttack = true;
			}
			if((TargetTypes == AttackTargets.Environment || TargetTypes == AttackTargets.MonsterEnvironments || TargetTypes == AttackTargets.PlayerEnvironments) &&
			   h.gameObject.GetComponent<EnvironmentBase>() != null){
				doAttack = true;
			}

			if(doAttack){

				if(ImpactEffect != null){
					EffectBase newInstance = ImpactEffect.GetInstance(h.transform.position);
					newInstance.PlayEffect();
					h.DamageEffects.Add (newInstance);
				}
				if(ImpactSoundEffect.SoundFile != null){
					//AudioSource.PlayClipAtPoint(ImpactSoundEffect, Camera.main.transform.position);
					SoundInstance s = ImpactSoundEffect.CreateSoundInstance(h.gameObject);
					s.Play();
				}
				ImpactCameraShakes.PlayCameraShakes();
				ImpactRumbles.PlayRumbles();
				h.Damage(Damage,AttackVariant);
			}
		}
	}






	public static AttackBase GetAttackByVariant(AttackEnum variant, GameObject target){
		AttackBase[] attacks = target.GetComponents<AttackBase>();
		foreach(AttackBase a in attacks){
			if(a.AttackVariant == variant){
				return a;
			}
		}
		return null;
	}*/
}
