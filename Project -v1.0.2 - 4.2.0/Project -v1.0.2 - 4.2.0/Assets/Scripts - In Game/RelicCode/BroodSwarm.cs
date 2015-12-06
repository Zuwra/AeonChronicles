using UnityEngine;
using System.Collections;

public class BroodSwarm : MonoBehaviour  {

	/*
	private VisionArc innerVision;

	private MovementComponent mover;
	public float attackSpeed = 5;
	private GameObject attackTarget;
	private bool leftRight;
	private double turnTimer = 0;
	private double MaxTurn = .4;
	private HealthComponent health;
	[SerializeField]
	public float lifetime = 3.0f;

	private float totalLife;
	private float greatestDist;
	public bool dieOnAttack;
	public SoundInformation DeathSoundEffect;
	private bool onCreep = true;

	private GameObject player;
	private float parentSight; 
	
	private Vector3 destination;
	private float wanderTimer;

	private EffectBase newInstance;

	public EffectBase AttackFX;

	Vector3 origin;



	Quaternion targetRotation;
	public GameObject home = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Tesla");
		Random.seed = System.Environment.TickCount + (int)this.gameObject.transform.position.z;
		origin = this.gameObject.transform.position;
		totalLife = lifetime;
		innerVision = (VisionArc)VisionBase.GetVisionByVariant(VisionEnum.Variant2, gameObject);
	
		MaxTurn = (Random.Range (2,10))/4;
		mover = gameObject.GetComponent<MovementComponent>();
		health = GetComponent<HealthComponent>();

	if (this.gameObject.GetComponent<SoundMachine> ()) {

						this.gameObject.GetComponent<SoundMachine> ().playSound ();
				}
	
	}


	// Update is called once per frame
	void Update () {
		if (home != null) {
			if (attackTarget) {
					lifetime -= Time.deltaTime;}
			} 
		else {
			lifetime -= Time.deltaTime;
			}



		if (lifetime < 0.0f) {
			
			if (health != null) {

				health.Kill (AttackEnum.Charger);
				
				if (DeathSoundEffect.SoundFile != null) {

					SoundInstance s = DeathSoundEffect.CreateSoundInstance(this.gameObject);
					s.Play();
				}
				
				Destroy (this.gameObject);
				return;
			}
		}

		if (Vector3.Distance (player.transform.position, this.gameObject.transform.position) > 17) {
			return;		
		}

		if (attackTarget == null || attackTarget.GetComponent<PlayerController>()) {

						foreach (GameObject obj in innerVision.CharactersInVision()) {
								if (obj.GetComponent<ChargingMummy> () || obj.GetComponent<ChargingBruteMonster> ()) {
										if (attackTarget != null) {
												if (Vector3.Distance (attackTarget.transform.position, this.gameObject.transform.position) > Vector3.Distance (obj.transform.position, this.gameObject.transform.position)) {
														attackTarget = obj;
														
												}
										
											}
									 else {
										attackTarget = obj;
										}
									}
								if (obj.GetComponent<PlayerController> () && attackTarget == null) {
										attackTarget = obj;
								}
						
						}
				}

		if (attackTarget != null) {
			KillTarget ();
			return;
		}



	
		wanderTimer  += Time.deltaTime;
		if (wanderTimer > 3) {//maxtimer?
			

			wanderTimer = 0;
			destination = (new Vector3(origin.x + Random.Range(-1, 1), origin.y, origin.z +Random.Range(-1, 1))) -transform.position;
		}
		patrol ();

	
	
	}
	//==============================================================================================
	void OnDestroy()
		{

		if(home !=  null)
			{home.GetComponent<nestspawner>().kidDied();}

		}



	//==============================================================================================


	void patrol()
	{

		// calculate rotation to be done
		if (destination != Vector3.zero) {
						targetRotation = Quaternion.LookRotation (destination);
				}
		
		//NOTE :: If you don't want rotation along any axis you can set it to zero is as :-
		// Setting Rotation along z axis to zero
		targetRotation.z=0; 
		
		// Setting Rotation along x axis to zero
		targetRotation.x=0; 
		// Apply rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*3/2);
		mover.Move(0,(gameObject.transform.forward) * Time.deltaTime * 3/2);
		
	}
	
	//==============================================================================================


	void KillTarget() {

		if (Vector3.Distance (this.transform.position, origin) > parentSight) {
			onCreep = false;		
		} 
		else {onCreep = true;
		}

		if(!attackTarget) return; // if the attackTarget object is removed, don't attempt to kill it anymore
	
		if (Vector3.Distance (attackTarget.transform.position, this.gameObject.transform.position) < 1) {
						AttackBase.GetAttackByVariant (AttackEnum.Default, gameObject).Attack (attackTarget.transform);
						if(newInstance == null)
					{	newInstance = AttackFX.GetInstance (transform.position);

						newInstance.transform.parent = this.gameObject.transform;
							newInstance.PlayEffect ();}
						if (dieOnAttack) {
								health.Kill (AttackEnum.Default);
								Destroy (this.gameObject);
						}

				} else {
			if(newInstance)
			{newInstance.StopEffect();}
				}


		Vector3 destination = (attackTarget.transform.position - transform.position).normalized;
		{
						turnTimer += Time.deltaTime;}	
		if (turnTimer > MaxTurn) {

						turnTimer = 0;
						leftRight = !leftRight;
				
				}




		float  dist = Vector3.Distance(gameObject.transform.position, attackTarget.transform.position)/10;

		if (dist > 1) {
			dist = 1;}
		if (destination.x < 0 && destination.z > 0 || destination.x > 0 && destination.z < 0) {
	
						if (leftRight) {
								destination.x +=dist;
								destination.z += dist;
						} else {
								destination.x -= dist;
								destination.z -= dist;
						}
				} else {
						if (leftRight) {
							destination.x += dist;
							destination.z -= dist;
						} else {
							destination.x -= dist;
							destination.z += dist;
			}
					
		}

		// calculate rotation to be done
		targetRotation  = Quaternion.LookRotation(destination); 

		targetRotation.z=0; 
		targetRotation.x=0; 

		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*6);
		float curve = 1 - Mathf.Pow(((totalLife - lifetime) / totalLife), 4);

		//(((totalLife-lifetime)/totalLife)^3 + 1)
		if (onCreep) {
				mover.Move (0, (gameObject.transform.forward) * Time.deltaTime * curve * attackSpeed*1.8f);

		} 
		else {mover.Move (0, (gameObject.transform.forward) * Time.deltaTime * curve * attackSpeed *1.2f);
				
		}

	
	}

	public void setAttackTarget(GameObject obj)
	{attackTarget = obj;
		}

	//==============================================================================================

	public void setParentSight(float input)
		{parentSight = input;}

*/
	
}