using UnityEngine;
using System.Collections;

public static class GameObjectExtentions
{

#if UNITY_4_3
	public static T GetComponentInParent<T>(this GameObject component) where T : Component
	{
		GameObject target = component.gameObject;
		
		while (target != null)
		{
			T result = target.GetComponent<T>();
			
			if (result != null)
			{rege
				return result;
			}
			
			target = target.transform.parent != null ? target.transform.parent.gameObject : null;
		}
		
		return null;
	}

	public static T GetComponentInParent<T>(this Component component) where T : Component
	{
		return component.gameObject.GetComponentInParent<T>();
	}
#endif
}

public class PlayerController : MonoBehaviour {/*
	//Public variables
	public float WalkSpeed = 4.0f;
	public float WalkMaxRadiansDelta = 5.0f;
	public float JumpForce = 1.5f;
	public float JumpSpeed = 3.0f;
	public float RotationSpeed = 280.0f;
    public float Gravity = 3.0f;
    public float ActionRadius = 1f;
    public float DashActionRadius = 0.1f;
	public float LargeNotifyRadius = 2f;
	public float SecondsToMaxWalk = 0.3f;
	public float DodgeSpeed = 10.0f;
	public float DodgeDistance = 3.0f;
	public float DodgeDelay = 2.0f;
	public float DodgeAutoAimDegrees = 30;
	public float ThrowAutoAimDegrees = 30;
	public float ThrowDistance = 15;
	// Total "force" exerted on both you and shambler.
	public float MomentumMultiplier = .64f;
	public float MomentumKnockbackMultiplier = .75f;

	public float ActionTime = 0.5f;
	public float DamageTime = 1.0f;
	public float DamageSpeed = 2.0f;
	public float DamageInvincibilityTime = 2.0f;
	public float HoldDistance = 0.5f;

	public EffectBase DustFX;
	public EffectBase SplashFX;
	public EffectBase DodgeFX;
	public EffectBase PushFX;
	public EffectBase HoldFX;
	public EffectBase JumpFX;
	public EffectBase DamageFX;
	public EffectBase PickUpArtifactFX;
	public EffectBase LevelUpFX;
	public EffectBase InitialSpawnEffect;

	public SoundInformation DodgeSoundEffect;
	public SoundInformation JumpSoundEffect;
	public SoundInformation LandSoundEffect;
	public SoundInformation FootstepSoundEffect;

	//Components
	private Animator _animator;
	private CameraManagerScript _cameraManager;
	private MovementComponent _movement;
	private HealthComponent _healthComponent;
	[SerializeField]
	private SkinnedMeshRenderer _meshRenderer;
	private LineRenderer _lineRenderer;
	private Animator _characterAnimator;

	//Misc
	private bool moving;
	private bool jumping;
	private bool jumpReady;
	private bool dodgeReady;
	private bool actionReady;
	private bool isOnWall;
	private float dodgeDelayTimer;
	private float currentHealth;

	public GameObject ThrowIndicator;

	private StateMachine stateMachine;

	private int flashCount = 7;
	private float flashTime = 0.1f;

	private InteractableComponent closestIC;
	private VisionArc FrontalTargetingCone;
	private VisionArc ThrowingRange;
	private VisionArc ProximityArea;
	private VisionArc BreakablesVision;
	private float baseMoveSpeed;

	public bool IsWalkingInWater = false;

	private GameObject[] _proximityVisionObjects;
	private GameObject[] _largeProximityVisionObjects;

	void Start () {
		
		FrontalTargetingCone = this.gameObject.AddComponent<VisionArc> ();
		FrontalTargetingCone.ArcSize = DodgeAutoAimDegrees*2;
		FrontalTargetingCone.Distance = DodgeDistance;
		FrontalTargetingCone.VisibleLayers = 1 << LayerMask.NameToLayer("Interactables");
		FrontalTargetingCone.VisionBlockedBy = ~(1 << LayerMask.NameToLayer ("Interactables"));
		FrontalTargetingCone.CanSeeThroughWalls = false;
		FrontalTargetingCone.CanSeeTriggerColliders = false;

		BreakablesVision = this.gameObject.AddComponent<VisionArc> ();
		BreakablesVision.ArcSize = 360;
		BreakablesVision.Distance = 2.5f;
		BreakablesVision.VisibleLayers = 1 << LayerMask.NameToLayer("Breakables");
		BreakablesVision.VisionBlockedBy = ~(1 << LayerMask.NameToLayer ("Breakables"));
		BreakablesVision.CanSeeThroughWalls = false;
		BreakablesVision.CanSeeTriggerColliders = true;


		ThrowingRange = this.gameObject.AddComponent<VisionArc> ();
		ThrowingRange.ArcSize = ThrowAutoAimDegrees;
		ThrowingRange.Distance = ThrowDistance;
		ThrowingRange.VisibleLayers = 1 << LayerMask.NameToLayer("Monster") | 1 << LayerMask.NameToLayer("Interactables");
		ThrowingRange.VisionBlockedBy = ~(1 << LayerMask.NameToLayer("Monster") | 1 << LayerMask.NameToLayer("Interactables"));
		ThrowingRange.CanSeeThroughWalls = false;
		ThrowingRange.CanSeeTriggerColliders = false;

		ProximityArea = this.gameObject.AddComponent<VisionArc> ();		
		ProximityArea.ArcSize = 240;
		ProximityArea.Distance = ActionRadius;
		ProximityArea.VisibleLayers = 1 << LayerMask.NameToLayer("Interactables");
		ProximityArea.VisionBlockedBy = ~((1 << LayerMask.NameToLayer ("Interactables")) | (1 << LayerMask.NameToLayer("DoesntBlockVision")));
		ProximityArea.CanSeeThroughWalls = false;
		ProximityArea.CanSeeTriggerColliders = false;
		ProximityArea.eyeCenter = new Vector3(0, 0.4f, 0);

		baseMoveSpeed = WalkSpeed;

		_animator = GetComponent<Animator> ();

		_cameraManager = (CameraManagerScript)GameObject.Find("CameraManager").GetComponent("CameraManagerScript");

		_movement = this.GetComponent<MovementComponent> ();
		_healthComponent = this.GetComponent<HealthComponent>();
		_healthComponent.OnDamage += HandleOnDamage;
		_healthComponent.OnDeath += HandleOnDeath;
		_characterAnimator = GetComponent<Animator> ();
		_lineRenderer = GetComponent<LineRenderer> ();

		//GameUI.UpdateHealthBar(_healthComponent.CurrentHealth, _healthComponent.MaxHealth);


		InitializeState ();

		moving = true;
		jumping = false;
		jumpReady = true;
		dodgeReady = true;
		actionReady = true;
		dodgeDelayTimer = 0.0f;
		currentHealth = _healthComponent.CurrentHealth;

		if(InitialSpawnEffect != null){
			InitialSpawnEffect.GetInstance(this.transform.position).PlayEffect();
		}

		ActivateEntity();
	}

	void HandleOnDeath ()
	{
		stateMachine.SetNextState (new DeathState (this), true);
		//LevelBehavior.Instance.ResetPlayer();
		_cameraManager.playerDied = true;

		StopCoroutine("FlashRed");
		if(_meshRenderer != null){
			_meshRenderer.material.color = Color.white;
		}
		foreach(EffectBase e in _healthComponent.DamageEffects){
			e.StopEffectImmediately();
		}

	}

	public void ResetOnDeath(){
		LevelBehavior.Instance.ResetPlayer();
		_healthComponent.ReturnToLife();
		currentHealth = _healthComponent.CurrentHealth;
		GameUI.UpdateHealthBar(currentHealth, _healthComponent.MaxHealth);
	}

	void HandleOnDamage (DamageEffect damage)
	{
		DropPickedUpItem();
		StartCoroutine("FlashRed");

		GameObject damVin = GameObject.Find("DamageVignette");
		if (damVin != null)
		{
			damVin.GetComponent<DamageVignette>().AddIntensity(damage.DamageAmount);
		}
	}

	public void DropPickedUpItem() {
		if (_theThingThatIsPickedUp != null) {
			_theThingThatIsPickedUp.Drop();
			_theThingThatIsPickedUp = null;
			if(ThrowIndicator != null) {
				ThrowIndicator.GetComponentInChildren<MeshRenderer> ().enabled = false;
			}
		}
	}
	
	private IEnumerator FlashRed()
	{
		for (int i = 0; i < flashCount; ++i)
		{
			float time = 0.0f;

			while (time < flashTime)
			{
				float lerp = 2.0f * time / flashTime;

				if (lerp > 1.0f)
				{
					lerp = 2.0f - lerp;
				}
				if(_meshRenderer != null){
					_meshRenderer.material.color = Color.Lerp(Color.white, Color.red, lerp);
				}

				yield return null;
				time += Time.deltaTime;
			}
		}
	}

	void OnBecameVisible(){
//		Debug.Log ("Player is visible!");
		_movement.unstun();
//		foreach(RoomBehavior r in LevelBehavior.Instance.Rooms){
//			if(r.PlayerInRoom){
				//r.ActivateMonsters();
//			}
//		}
	}

	void OnBecameInvisible(){
//		Debug.Log ("Player is invisible!");
		_movement.stun(30f);
//		foreach(RoomBehavior r in LevelBehavior.Instance.Rooms){
//			if(r.PlayerInRoom){
				//r.DeactivateMonsters();
//			}
//		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		dodgeDelayTimer += Time.deltaTime;
	
		isOnWall = false;
		if (transform.position.y < -10) {
			_healthComponent.Kill();
			transform.position = new Vector3(transform.position.x, -9, transform.position.z);
		}

		_proximityVisionObjects = ProximityArea.ObjectsInVision ();
		_largeProximityVisionObjects = FrontalTargetingCone.ObjectsInVision ();

		// see if the interactable is within short range, always hit it
		GameObject closest = getClosestTarget (_largeProximityVisionObjects, out closestIC);
		if (closest != null){
			if (closestIC != null){
				closestIC.NotifyLargeProximity(new InteractableLargeNotifyEventData(this.gameObject,true,0f,0f));
			}
		}

		// Hack to get the pots to light up when you are nearby
		foreach(GameObject g in BreakablesVision.ObjectsInVision())
		{
			InteractableComponent ic = g.GetComponent<InteractableComponent>();
			if (ic != null){
				ic.NotifyLargeProximity(new InteractableLargeNotifyEventData(this.gameObject,true,0f,0f));
			}
		}

		
		closestIC = null;
		closest = getClosestTarget (_proximityVisionObjects, out closestIC);
		if (closestIC != null){
			if (closestIC != null){
				closestIC.NotifyLargeProximity(new InteractableLargeNotifyEventData(this.gameObject,true,0f,0f));
			}
		}

		foreach (GameObject g in _proximityVisionObjects){
			closestIC = g.GetComponentInParent<InteractableComponent>();
			if (closestIC != null){
				closestIC.NotifyProximity(new InteractableNotifyEventData(this.gameObject,true,0f,0f));
			}
		}

		//Check for damage
		if ((currentHealth > _healthComponent.CurrentHealth) && !jumping) {
			if(currentHealth - _healthComponent.CurrentHealth > .5){
				stateMachine.SetNextState("damage");
			} else {
				stateMachine.SetNextState("damageSpecial");
			}
			currentHealth = _healthComponent.CurrentHealth;
			GameUI.UpdateHealthBar(currentHealth, _healthComponent.MaxHealth);		
		} else if (currentHealth < _healthComponent.CurrentHealth) { // check for health pickup
			currentHealth = _healthComponent.CurrentHealth;
			GameUI.UpdateHealthBar(currentHealth, _healthComponent.MaxHealth);		
		}


		if(!_movement.isStunned()){
			//Update state machine
			if(stateMachine == null)
				{InitializeState();}
			stateMachine.Update ();
			//Handle buttons on press down
			if (Input.GetAxisRaw ("Jump") > -1.0f) {
				jumpReady = true;
			}
			if (Input.GetAxisRaw ("Dodge") > -1.0f && dodgeDelayTimer > DodgeDelay) {
				dodgeReady = true;
				dodgeDelayTimer = 0.0f;
			}
			if (Input.GetButtonUp("Action")) {
				actionReady = true;
			}
			
			if (_movement.Velocity.y < -80) {
                Debug.Log("Player should die of Going Too fast!");
                _movement.ResetVelocity();
                _healthComponent.Kill(AttackEnum.Default);
			}
		}


		//Update camera manager
		_cameraManager.CameraUpdate();
		if (_theThingThatIsPickedUp != null && _lineRenderer!=null) {
			_lineRenderer.SetWidth (.3f, .6f);
			_lineRenderer.enabled = true;
			Vector3 throwTarget = (PlayerUtils.getInputDirection () * ThrowDistance) + transform.position;
			if (PlayerUtils.getInputDirection ().Equals (Vector3.zero)) {
				throwTarget = transform.position + (this.transform.forward * ThrowDistance);
			}
			
			// checks for vision for autotargeting (Monsters and pillars.)
			InteractableComponent dummyObject = null;
			GameObject closestObject = getClosestTarget (getThrowingRange ().ObjectsInVision (), out dummyObject);
			if (closestObject != null) {
				
				throwTarget = closestObject.transform.position;
			}
			
			// Calculate direction
			Vector3 throwDirection = throwTarget - transform.position;
			throwDirection.Normalize ();
			
			
			
			
			
			//Vector3 dir = this.transform.forward;
			//dir = new Vector3 (dir.x, 0, dir.z).normalized;
			
			Vector3 dir = throwDirection;
			
			Vector3 initialPosition = this.transform.position;
			
			Vector3 offset = new Vector3 (0.1f, 1.2f, HoldDistance);
			Quaternion playerAngle = Quaternion.AngleAxis (this.transform.eulerAngles.y, new Vector3 (0, 1, 0));
			
			offset = (playerAngle * offset);
			
			//	Debug.Log ("?4");
			initialPosition += offset;
			
			//	initialPosition += dir * .1f;
			//	initialPosition += new Vector3(0, .1f, 0);
			
			Vector3 toKick = (dir + new Vector3 (0, .2f, 0));
			
			Rigidbody rb = _theThingThatIsPickedUp.GetComponent<Rigidbody> ();
			ThrowablePrefab tp = _theThingThatIsPickedUp.GetComponent<ThrowablePrefab> ();
			
			toKick = toKick * tp.kickStrength;
			
			
			
			toKick = (toKick / rb.mass) * Time.fixedDeltaTime;
			
			if (_lineRenderer.enabled) {
				UpdateTrajectory (initialPosition, toKick, new Vector3 (0, -9.81f, 0));
			}
		} else {
			if(_lineRenderer!=null)
				_lineRenderer.SetWidth (0, 0);
		}
	}
	
	void UpdateTrajectory (Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
	{
		int numSteps = 20; // for example
		float timeDelta = 1.0f / initialVelocity.magnitude; // for example
		
		//	LineRenderer lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.SetVertexCount (numSteps);
		
		Vector3 position = initialPosition;
		Vector3 oldPosition = position;
		Vector3 velocity = initialVelocity;
		for (int i = 0; i < numSteps; ++i) {
			//		Debug.Log ("?1");
			_lineRenderer.SetPosition (i, position);
			oldPosition = position;
			position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
			velocity += gravity * timeDelta;
			//Debug.Log("1 " + position);
			if (TravelTrajectorySegment (oldPosition, ref position)) {
				
				//Debug.Log("2 " + position);
				_lineRenderer.SetVertexCount (i + 2);
				_lineRenderer.SetPosition (i + 1, position);
				
				break;
			}
		}
	}
	
	bool TravelTrajectorySegment (Vector3 startPos, ref Vector3 newPos)
	{
		//Debug.Log ("?2");
		RaycastHit hitInfo;
		Vector3 test = newPos;
		int mask = 1 << LayerMask.NameToLayer ("Ignore Raycast");
		mask = ~mask;
		//	Debug.Log (mask);
		//Debug.Log (~mask);
		bool hasHitSomething = Physics.Linecast (startPos, test, out hitInfo, mask);
		if (hasHitSomething) {
			newPos = hitInfo.point;//.position;
		}
		//	positions.Add(newPos);
		
		return hasHitSomething;
	}

	private void InitializeState()
	{
		if(stateMachine == null){
			stateMachine = new StateMachine((string stateName) => {
				switch (stateName)
				{
				case "start":
					moving = true;
					return new IdleState(this);
				case "dodge":
					DodgeState dState = new DodgeState(this);
					dState.DodgeFX = this.DodgeFX;
					dodgeReady = false;
					return dState;
				case "idle":
					return new IdleState(this);
				case "walk":
					WalkState wState = new WalkState(this);
					wState.Dust = DustFX;
					return wState;
				case "action":
					ActionState aState = new ActionState(this);
					aState.ActionFX = PushFX;
					actionReady = false;
					return aState;
				case "jump":
					JumpState jState = new JumpState(this);
					jState.JumpFX = JumpFX;
					jumpReady = false;
					return jState;
				case "damage":
					DamageState daState = new DamageState(this);
					return daState;
				case "damageSpecial":
					DamageSpecialState daStateSpecial = new DamageSpecialState(this);
					return daStateSpecial;
				case "hold":
					HoldState hState = new HoldState(this);
					hState.Dust = DustFX;
					hState.Hold = HoldFX;
					return hState;
				}
				return null;
			});
			moving = true;
		}
		stateMachine.SetState((string stateName) => {
			switch (stateName)
			{
			case "start":
				moving = true;
				return new IdleState(this);
			case "dodge":
				DodgeState dState = new DodgeState(this);
				dState.DodgeFX = this.DodgeFX;
				dodgeReady = false;
				return dState;
			case "idle":
				return new IdleState(this);
			case "walk":
				WalkState wState = new WalkState(this);
				wState.Dust = DustFX;
				return wState;
			case "action":
				ActionState aState = new ActionState(this);
				aState.ActionFX = PushFX;
				actionReady = false;
				return aState;
			case "jump":
				JumpState jState = new JumpState(this);
				jState.JumpFX = JumpFX;
				jumpReady = false;
				return jState;
			case "damage":
				DamageState daState = new DamageState(this);
				return daState;
			case "damageSpecial":
				DamageSpecialState daStateSpecial = new DamageSpecialState(this);
				return daStateSpecial;
			case "hold":
				HoldState hState = new HoldState(this);
				hState.Dust = DustFX;
				hState.Hold = HoldFX;
				return hState;
			}
			return null;
		});
	}


	public Animator GetCharAnimator(){
		return _animator;
	}

	public MovementComponent GetMoveComponent(){
		return _movement;
	}

	public bool IsGrounded
	{
		get
		{
			return _movement.IsGrounded;
		}
	}

	public bool IsOnWall
	{
		get
		{
			return isOnWall;
		}
	}

	public bool IsMoving(){
		return moving;
	}

	public bool JumpReady(){
		return jumpReady;
	}

	public bool DodgeReady(){
		return dodgeReady;
	}

	public bool ActionReady(){
		return actionReady;
	}

	public void ResetDodgeTimer(){
		dodgeReady = false;
		dodgeDelayTimer = 0.0f;
	}
	
	public void OnExtendedControllerColliderHit(ExtendedControllerColliderHit hit)
	{
		if (Mathf.Abs(hit.normal.y) < 0.1f)
		{
			isOnWall = true;
		}
	}

	public void SetJumping(bool jump){
		jumping = jump;
	}

	//PLACEHOLDER STUFF FOR PICKUPABLES
	//public bool _thingPickedUp;
	public ThrowablePrefab _theThingThatIsPickedUp;
	public void PickUpObstacle(ThrowablePrefab pref){
		//Debug.Log ("Pick up called");
		_theThingThatIsPickedUp = pref;
		this.stateMachine.SetNextState ("hold");
		if (ThrowIndicator != null) {
						ThrowIndicator.GetComponentInChildren<MeshRenderer> ().enabled = true;
				}



		/*
		pref.transform.parent = transform;
		_theThingThatIsPickedUp = pref;
		_thingPickedUp = true;
	}
	
	public VisionArc getThrowingRange(){
		return ThrowingRange;
	}

	public GameObject[] GetLargeProximityViewedObjects(){
		return _largeProximityVisionObjects;
	}

	public VisionArc getProximityArea(){
		return ProximityArea;
	}

	public GameObject[] GetProximityViewedObjects(){
		return _proximityVisionObjects;
	}

	public GameObject getClosestTarget(GameObject[] objects, out InteractableComponent ic, bool ignoreDebris = true){
		ic = null;
		GameObject closest = null;
		InteractableComponent currentIC = null;
		foreach (GameObject g in objects) 
		{
			GameObject current = g;
			if (closest == null) 
			{
				closest = current;
			} 
			float currDistance = Vector3.Distance (transform.position, current.transform.position);
			float oldDistance = Vector3.Distance (transform.position, closest.transform.position);
			if (oldDistance > currDistance)
			{
				closest = current;
			}			

			currentIC = closest.GetComponentInParent<InteractableComponent>();
			if (currentIC != null)
			{
				if (currentIC.IsInteractable && (ignoreDebris ? !currentIC.CompareTag("Debris") : true))
				{
					ic = currentIC;
					closest = ic.gameObject;
				}
			}
		}
		return closest;
	}

	public IEnumerator RemoveSlow(float delay)
	{
		yield return new WaitForSeconds(delay);
		while (WalkSpeed < baseMoveSpeed) {
			WalkSpeed += 0.05f;
		}
	}

	public void PickUpArtifact()
	{
		if(PickUpArtifactFX != null)
			PickUpArtifactFX.PlayEffect();
	}

	public void PlayLevelUpFX() {
		if (LevelUpFX != null) {
			EffectBase newInstance = LevelUpFX.GetInstance(transform.position);
			newInstance.PlayEffect();
		}
	}

	/****************
	 * SOUND EFFECTS
	 ****************/
	/*
	public void playDodgeSFX() {
		if(DodgeSoundEffect.SoundFile != null){
			SoundInstance s = DodgeSoundEffect.CreateSoundInstance(gameObject);
			s.Play();
		}
	}

	public void playJumpSFX() {
		if(JumpSoundEffect.SoundFile != null){
			SoundInstance s = JumpSoundEffect.CreateSoundInstance(gameObject);
			s.Play();
		}
	}

	public void playLandSFX() {
		if(LandSoundEffect.SoundFile != null){
			SoundInstance s = LandSoundEffect.CreateSoundInstance(gameObject);
			s.Play();
		}
	}

	public void playFootstepSFX() {
		if(FootstepSoundEffect.SoundFile != null) {
			SoundInstance s = FootstepSoundEffect.CreateSoundInstance(gameObject);
			if (IsWalkingInWater) {
				s.SetParameter("Water", 1.0f);
			} else {
				s.SetParameter("Water", 0.0f);
			}
			s.Play();
		}
	}

	public void teleportToMouse (Vector3 point)
	{
		this.transform.position = point;
	}*/
}
