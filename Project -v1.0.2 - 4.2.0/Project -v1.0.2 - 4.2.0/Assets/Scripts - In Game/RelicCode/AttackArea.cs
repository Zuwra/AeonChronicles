using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {/*
	public AttackEnum AttackVariant = AttackEnum.Default;
	public float AttackPeriod = 1f;
	public AttackTargets TargetTypes = AttackTargets.All;
	public DamageEffect Damage;
	
	public CameraShakeManager.CameraShakeEventHandler ImpactCameraShakes;
	public RumbleManager.RumbleEventHandler ImpactRumbles;
	
	public bool CanAttack = true;
	public bool AttacksShamblers = false;
	public bool AttacksChargers = true;
	public bool AttacksPlayer = true;
	
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
		_timeRemainingUntilAttack -= Time.deltaTime;
		if(_timeRemainingUntilAttack < 0){
			_timeRemainingUntilAttack = AttackPeriod;
			CanAttack = true;
		}
		else{
			//CanAttack = false;
		}
	}
	
	public void Attack(Transform target)
	{	
		if (CanAttack) 
		{//Debug.Log("Tag " +target.tag);

			_timeRemainingUntilAttack = AttackPeriod;
			if (target.CompareTag("Player") && !AttacksPlayer){
			}
			else if (target.CompareTag("Shambler") && !AttacksShamblers){
			}
			else if (target.CompareTag("Charger") && !AttacksChargers){
			}
			else{
				LaunchAttack(target);
			}

		}
	}
	
	protected void LaunchAttack(Transform target){
		HealthComponent h = target.gameObject.GetComponentInParent<HealthComponent>();	

		CanAttack = false;
		if(h != null){
			bool doAttack = false;
			if((TargetTypes == AttackTargets.All)){
				doAttack = true;
			}
			if((TargetTypes == AttackTargets.Player || TargetTypes == AttackTargets.PlayerMonsters || TargetTypes == AttackTargets.PlayerEnvironments) &&
			   h.gameObject.GetComponent<PlayerBase>() != null){
				doAttack = true;
			}
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
	
	
	
	
	
	
	public static AttackArea GetAttackByVariant(AttackEnum variant, GameObject target){
		AttackArea[] attacks = target.GetComponents<AttackArea>();
		foreach(AttackArea a in attacks){
			if(a.AttackVariant == variant){
				return a;
			}
		}
		return null;
	}*/
}
