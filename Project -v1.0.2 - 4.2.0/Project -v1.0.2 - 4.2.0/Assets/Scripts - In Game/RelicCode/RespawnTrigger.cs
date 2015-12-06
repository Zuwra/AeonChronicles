using UnityEngine;
using System.Collections;

public class RespawnTrigger : MonoBehaviour {
	/*
	public bool showEffect = true;
	public bool showText = true;
	public EffectBase RespawnFX;
	public SoundInformation ActivateSoundEffect;

	public bool returnTripOnly;
	private bool activated;
	public bool both = false;

	private void Start() {
		activated = false;
	}

	public void OnTriggerEnter(Collider other) {
				if (!returnTripOnly || both) {
						if (!this.activated && other.CompareTag ("Player")) {
								if (LevelBehavior.Instance != null)
										LevelBehavior.Instance.SetRespawn (this);
								this.activated = true;
								if (showText) {
										GameUI.DisplayInstructionTextArea ("Checkpoint", 2f);
								}
								if (showEffect) {
										this.GetComponentInChildren<ParticleSystem> ().Play ();
								}
								if (ActivateSoundEffect.SoundFile != null && showEffect) {
										SoundInstance s = ActivateSoundEffect.CreateSoundInstance (gameObject);
										s.Play ();
								}
						}
				} else {
						if (GameObject.Find ("DeathBox").GetComponent<Deathbox> ().isActivated ()) {

			
								if (!this.activated && other.CompareTag ("Player")) {
					GameObject.Find ("LevelManager").GetComponent<LevelBehavior>().RelicPickUpLocation = this.gameObject.transform.position;
					Vector3 newPosition = this.gameObject.transform.position;
					newPosition.z +=10;
					GameObject.Find ("DeathBox").GetComponent<Deathbox> ().setPosition(newPosition);

										if (LevelBehavior.Instance != null)
												LevelBehavior.Instance.SetRespawn (this);
										this.activated = true;
										if (showText) {
												GameUI.DisplayInstructionTextArea ("Checkpoint", 2f);
										}
										if (showEffect) {
												this.GetComponentInChildren<ParticleSystem> ().Play ();
										}
										if (ActivateSoundEffect.SoundFile != null && showEffect) {
												SoundInstance s = ActivateSoundEffect.CreateSoundInstance (gameObject);
												s.Play ();
										}
				
								}
						}
				}
		}

	public void PlayRespawnEffect() {
		if(activated && RespawnFX != null){
			EffectBase newInstance = RespawnFX.GetInstance(transform.position);
			newInstance.transform.position = new Vector3(newInstance.transform.position.x, newInstance.transform.position.y - 1, newInstance.transform.position.z);
			newInstance.PlayEffect();
		}
	}

	public Vector3 GetRespawnPosition() {
		return this.gameObject.transform.position;
	}*/
}
