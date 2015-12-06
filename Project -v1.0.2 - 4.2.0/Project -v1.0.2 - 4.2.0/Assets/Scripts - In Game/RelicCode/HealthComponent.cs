using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealthComponent : MonoBehaviour {/*
	public float MaxHealth = 9;
	public float StartingHealth = 9;
	public bool DefaultBehavior = true;
	private Color DamagedColor = Color.red;
	private float BlinkInterval = .125f;
	private Renderer[] gameObjectRenderers;
	private Material[] originalMats;	

	[ReadOnlyAttribute]
	public float CurrentHealth;

	public float HealthPercentage{get{return CurrentHealth/MaxHealth;}}
	public bool IsAlive{get;private set;}
	
	public SoundInformation WasHitSound;
	public SoundInformation DieSound;
	public SoundInformation WasHealedSound;

	public EffectBase DeathFX;

	public DeathScript deathscript;

	public List<EffectBase> DamageEffects;

	public bool CanBeHurt {get;set;}
	private float _invulnerableElapsedTime;
	private float _invulnerableTargetTime;

	// Use this for initialization
	void Start () {
		DamageEffects = new List<EffectBase>();
		CurrentHealth = StartingHealth;
		IsAlive = true;
		CanBeHurt = true;
		deathscript = this.GetComponent<DeathScript>();

		// back these up in case they get overwritten
		gameObjectRenderers = this.GetComponentsInChildren<Renderer>();
		originalMats = new Material[gameObjectRenderers.Length]; // Is this getting deleted ever?
		for (int i=0; i < gameObjectRenderers.Length; i++) {
			originalMats[i] = gameObjectRenderers[i].material;
		}
	}
	
	// Update is called once per frame
	void Update () {
		DamageEffects.RemoveAll(e => e == null);
		DamageEffects.RemoveAll(e => e.IsFinished);

		if(_invulnerableTargetTime != 0f){
			_invulnerableElapsedTime += Time.deltaTime;
			if(_invulnerableElapsedTime >= _invulnerableTargetTime){
				_invulnerableTargetTime = 0f;
				_invulnerableElapsedTime = 0f;
				CanBeHurt = true;
			}
		}
		hitSoundTimer += Time.deltaTime;
	}

	public void SetInvulnerable(float invulnTime){
		CanBeHurt = false;
		_invulnerableTargetTime = invulnTime;
		_invulnerableElapsedTime = 0f;
	}

		
	public void Kill(){
		Kill(AttackEnum.Default);
	}

	public void Damage(DamageEffect damage){
		Damage(damage, AttackEnum.Default);
	}

	public void Damage(DamageEffect damage, AttackEnum AttackVariant){

		if (CanBeHurt){
			if (CurrentHealth > 0) {
				if(OnDamage != null){
					OnDamage(damage);
				}
				CurrentHealth -= damage.DamageAmount;
				CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth; // set to zero if overkill damage was applied

				if (gameObjectRenderers != null) {
					//print (gameObjectRenderers.Length + " renderers");

					for (int i=0; i < gameObjectRenderers.Length; i++){
						Renderer rend = gameObjectRenderers[i];
						Material originalMat = originalMats[i];
						StartCoroutine (FlashRed (rend, originalMat));
					}
				}
			}

			if (CurrentHealth <= 0) {
				if (DeathFX != null) {
					Vector3 attacker = damage.Source.transform.position;
					Vector3 dead = transform.position;
					EffectBase newInstance = DeathFX.GetInstance(attacker);
					newInstance.transform.LookAt (dead);
					newInstance.transform.position = dead;
					newInstance.transform.rotation = Quaternion.Euler(new Vector3(0, newInstance.transform.eulerAngles.y, 0));
					newInstance.PlayEffect();
				}
				Kill(AttackVariant);
			} else {
				playWasHitSound();
			}
		}
	}

	public void Heal(float amount, bool makeSound){
		CurrentHealth += amount;
		if (CurrentHealth > MaxHealth) {
			CurrentHealth = MaxHealth;
		}
		if (makeSound) {
						playWasHealedSound ();
				}
	}

	public void IncreaseMaxHealth(int amount){
		MaxHealth += amount;
//		CurrentHealth = MaxHealth;
	}

	public void Kill(AttackEnum AttackVariant){
	//	print("Kill called");

	
		if(IsAlive){
			if (this.GetComponent<ChargingMummy>() != null || this.GetComponent<ChargingBruteMonster>() != null || this.gameObject.GetComponent<nestspawner>()
			    )
			{
                GameObject[] inventory = GameObject.FindGameObjectsWithTag("Inventory");
                if (inventory.Length > 0)
                {
                    GameObject obj = inventory[0];
					StatTracker tracker = obj.GetComponent<StatTracker>();
                    if (obj != null)
            		{
						if(AttackVariant != AttackEnum.DeathBox)
						{
							tracker.upKill(AttackVariant, this.gameObject);
						}
					}
                }
			}
			playDieSound();
			IsAlive = false;
			if(deathscript != null){
				deathscript.OnDeath();
			}
			if(DefaultBehavior){
				defaultDeath();				
			}
			if(OnDeath != null){
				OnDeath();
			}
		
		}
	}

	public void ReturnToLife(){
		GameObject[] inventory = GameObject.FindGameObjectsWithTag("Inventory");
		GameObject obj = inventory[0];
		obj.GetComponent<StatTracker>().upDeath();
		CurrentHealth = MaxHealth;
		SetInvulnerable (3f);
		IsAlive = true;
	}

	private void defaultDeath(){
		//TODO have this start an animation coroutine that runs through the death animation, THEN deletes the object


		GameObject.Destroy(gameObject);

	//	StartCoroutine(RestartLevel());

		//Application.LoadLevel (Application.loadedLevel);


	}

	//IEnumerator RestartLevel() {
//		yield return new WaitForSeconds(2f);
	//	Application.LoadLevel(Application.loadedLevel);
	//	GameObject.Destroy(gameObject);
	//	print("Character Has Died 2 seconds passed");
//	}

	public delegate void DeathEvent();
	public event DeathEvent OnDeath;

	public delegate void DamageEvent(DamageEffect damage);
	public event DamageEvent OnDamage;

	public static bool IsDead(HealthComponent target)
	{
		return target == null || target.CurrentHealth <= 0.0f;
	}

	IEnumerator FlashRed(Renderer gameObjectRenderer, Material originalMat){
		//Debug.Log (this + "Should be flashing red");

		Material newMaterial = (Material) Instantiate(originalMat);
		
		newMaterial.color = DamagedColor;
		if (gameObjectRenderer != null){
			gameObjectRenderer.material = newMaterial; // character is tinted with new color
			yield return new WaitForSeconds (BlinkInterval);

			gameObjectRenderer.material = originalMat; // change color back
			yield return new WaitForSeconds (BlinkInterval);

			gameObjectRenderer.material = newMaterial;
			yield return new WaitForSeconds (BlinkInterval);
			
			gameObjectRenderer.material = originalMat;
			yield return new WaitForSeconds (BlinkInterval);
		}

		GameObject.Destroy (newMaterial);
		
	}

	/*****************
	  Playing sounds
	 *****************/
	/*
	public void playWasHealedSound() {
		if(WasHealedSound != null && WasHealedSound.SoundFile != null) {
			SoundInstance s = WasHealedSound.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}

	public float hitSoundCooldown = .5f;
	private float hitSoundTimer = 0f;
	public void playWasHitSound() {
		if(WasHitSound != null && WasHitSound.SoundFile != null) {
			if(hitSoundTimer>hitSoundCooldown){
				SoundInstance s = WasHitSound.CreateSoundInstance(this.gameObject);
				s.Play();
				hitSoundTimer = 0f;
			}
		}
	}
	
	public void playDieSound() {
		if(DieSound != null && DieSound.SoundFile != null) {
			SoundInstance s = DieSound.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
	*/
}
