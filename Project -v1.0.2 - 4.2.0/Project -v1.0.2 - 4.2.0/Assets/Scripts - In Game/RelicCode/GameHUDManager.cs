using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHUDManager : MonoBehaviour {

/*
	private GameObject previousMenu;
	private GameObject currentMenu;
	public GameObject PlayerPortrait;

	public GameObject InstructionTextArea;
    private UILabel InstructionMessage;
    private Animator InstructionAnimator;
    private bool InstructionTextAreaIsShowing;
	private bool CaptionTextAreaIsShowing;
    
    public GameObject NotificationTextArea;
	private UILabel NotificationMessage;
	private Animator NotificationAnimator;
	private bool NotificationTextAreaIsShowing;
	
	public GameObject KillingSpreeTextArea;
	private UILabel KillingSpreeMessage;
	private Animator KillingSpreeAnimator;
	private bool KillingSpreeTextAreaIsShowing;

	public GameObject upgradeMenu;
	public GameObject PauseScreen;
	
	public UIProgressBar HealthBar;
	public UIProgressBar HealthBarShadow;
	private float HealthBarInitialWidth;
	private float HealthBarSizeIncrement;
	public UILabel HealthText;
	public float HealthChangeDuration = 2;
	public AnimationCurve HealthChangeMovement;
	private bool finishedShrinking = true;
	private float shadowStart;
	
	// XP BAR STUFF
	public GameObject firstXPBar;
	public GameObject XPBarExpansion;
	private List<XPBarDataContainer> XPContainers = new List<XPBarDataContainer>();
	private float nextXPContainerX;
	private int displayDepth;

	public UILabel PlayerLevel;
	private int knownCurrentXP = 0;
	private float knownMaxHP;


	public bool paused = false;
	private bool shownMissionObjectiveOnce = false;
	public bool showMissionObjective = true;
	public GameObject MissionObjective;
	public GameObject SoundOptions;
	public UISlider volumeSlider;
	public bool soundEnabled = true;

	public GameObject VictoryScreen;
	private bool showingVictoryScreen = false;
	public GameObject ArtifactView;
	private bool showingArtifact;
	private int CurrentArtIndex =0;

	private Vector3 InstructionOrigin;


	public GameObject HighScores;
	// This prefab will be instantiated, it should not already exist in the object hierarchy.
	public GameObject StatCounterPanelPrefab;

	private List<StatCounterPanel> statCounters;

	public StatTracker stats { get; private set; }

	private float UIWidth;
	private float UIHeight;

	private bool missionObjectiveFadingOut = false;
	private bool victoryScreenFadingIn;
	private GameObject inv;

	public GameObject player;
//	private PlayerController playerController;
	private HealthComponent playerHealth;

	public EffectBase XPGainFX;

	public UILabel missionTexts;
    //private int wait = 2; // ignore this, used for testing

	// Use this for initialization
    void Start()
	{InstructionOrigin = InstructionTextArea.transform.position;
		CurrentArtIndex = 0;
		player = GameObject.FindGameObjectWithTag("Player");

		inv = GameObject.FindGameObjectWithTag ("Inventory");
		stats = inv.GetComponent<StatTracker>();

		if (VictoryScreen == null)
		{
			VictoryScreen = transform.FindChild("VictoryScreen").gameObject;

		}

		volumeSlider.value = 0.5f;
		if (player == null) {
						player = GameObject.FindGameObjectWithTag ("Player");
				}

		playerHealth = player.GetComponent<HealthComponent> ();
	//	playerHealth.MaxHealth = 5 + GameObject.Find ("LevelManager").GetComponent<LevelBehavior> ().levelNumber;
	//	playerHealth.CurrentHealth = playerHealth.MaxHealth;
	
		knownMaxHP = playerHealth.MaxHealth *10;

		HealthBarInitialWidth = HealthBar.gameObject.GetComponent<UITexture> ().width;
		HealthBarSizeIncrement = HealthBarInitialWidth / knownMaxHP;


		UIWidth = GetComponent<UIPanel>().GetViewSize().x;
		UIHeight = GetComponent<UIPanel>().GetViewSize().y;

		if( InstructionMessage == null)
		{   InstructionMessage = InstructionTextArea.GetComponentInChildren<UILabel>();}
        InstructionMessage = InstructionTextArea.GetComponentInChildren<UILabel>();
        InstructionAnimator = InstructionTextArea.GetComponent<Animator>();

		NotificationMessage = NotificationTextArea.transform.FindChild("Text").GetComponent<UILabel>();
		NotificationAnimator = NotificationTextArea.GetComponent<Animator>();

		KillingSpreeMessage = KillingSpreeTextArea.GetComponentInChildren<UILabel> ();
		KillingSpreeAnimator = KillingSpreeTextArea.GetComponent<Animator> ();
		if (showMissionObjective) {
					setCurrentMenu("JobDescription");
			currentMenu = MissionObjective;
			//PauseGame();
				}
		else
		{ResumeGame();
			missionObjectiveFadingOut = false;}
		this.statCounters = new List<StatCounterPanel>();

		GameObject missionStuff = GameObject.Find ("MissionLogTexts");
		if (missionStuff) {
			for(int i = 1; i < stats.getGameLevel(); i ++)
			{
				foreach(MeshRenderer mesh in missionStuff.GetComponent<LogTexts>().relics[i].GetComponentsInChildren<MeshRenderer>() ){
					mesh.enabled = true;}
				if(missionStuff.GetComponent<LogTexts>().relics[i].GetComponent<MeshRenderer>())
				{missionStuff.GetComponent<LogTexts>().relics[i].GetComponent<MeshRenderer>().enabled = true;}

				missionStuff.GetComponent<LogTexts>().relics[i].GetComponent<CapsuleCollider>().enabled = true;
			}

		
		}
	}



	public void LookLeft()
	{Debug.Log ("Looking Left");

		if(CurrentArtIndex > 0)
		{	ArtifactView.GetComponent<ArtifactViewer>().HideRelicExample();
			CurrentArtIndex--; 
			ArtifactView.GetComponent<ArtifactViewer>().collectedArtifact(stats.getCollectedMiniRelics()[CurrentArtIndex], false);
		}
	}
	
	public void LookRight()
	{Debug.Log ("Looking Right");
		if( stats.getCollectedMiniRelics().Count > CurrentArtIndex +1)
		{	ArtifactView.GetComponent<ArtifactViewer>().HideRelicExample();
			CurrentArtIndex++;
			ArtifactView.GetComponent<ArtifactViewer>().collectedArtifact(stats.getCollectedMiniRelics()[CurrentArtIndex], false);
			Debug.Log("Looking Right, Current: "+ CurrentArtIndex + "  total relics: "+ stats.getCollectedMiniRelics().Count);
			
		}
	}

	
	// Update is called once per frame
    void Update()
	{//Debug.Log (Time.frameCount);
		if (stats== null) {
			if(inv == null)
			{inv = GameObject.Find ("Inventory");}
			stats = inv.GetComponent<StatTracker>();

		}
		if (paused)
		{
			player.GetComponent<MovementComponent>().stun(0.3f); // used to disable the input that causes the objective to hide
		
		}

	
		if (Input.GetKeyDown (KeyCode.Tab) && !showingArtifact && !paused && !showingVictoryScreen) {
			PauseGame();
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Sliding"))
				{
				if(obj.name == "StatCounterPanel(Clone)")
				{Destroy(obj);}
			}
				ArtifactView.GetComponent<ArtifactViewer>().HideRelicExample();
			if(stats.getCollectedMiniRelics().Count !=0){
				ArtifactView.GetComponent<ArtifactViewer>().collectedArtifact(stats.getCollectedMiniRelics()[CurrentArtIndex], false);}

				ShowArtifactViewer (false);	

		} else if (Input.GetKeyDown (KeyCode.Tab)  && paused) {


			ResumeGame();
		

		}



		if (showingArtifact && Input.GetKeyDown (KeyCode.A)) {
			LookLeft();


		}

		if (showingArtifact && Input.GetKeyDown (KeyCode.D)) {
			LookRight();
		}

		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7))
		{
			if (!paused){
				PauseScreen.GetComponent<UIWidget> ().alpha = 1;
				PauseGame();
				PauseScreen.SetActive (true);
				SoundOptions.SetActive (true);
			}
			else{
				ResumeGame();
				PauseScreen.GetComponent<UIWidget> ().alpha = 0;
			}
		}
	//	Debug.Log ("Status:" + missionObjectiveFadingOut);
		if (missionObjectiveFadingOut) {
			//Debug.Log("FadingOUt" + 	currentMenu.GetComponent<UIWidget> ().alpha );
						if (currentMenu != null) {
								//Debug.Log("Fading out"+ shownMissionObjectiveOnce + "    " +currentMenu.GetComponent<UIWidget> ().alpha);
								if (shownMissionObjectiveOnce) {

										currentMenu.GetComponent<UIWidget> ().alpha -= .1f;
								} else {

										currentMenu.GetComponent<UIWidget> ().alpha -= .03f;
								}

								if (currentMenu.GetComponent<UIWidget> ().alpha <= 0) {
										currentMenu.GetComponent<UIWidget> ().alpha = 0;
										currentMenu = null;
										shownMissionObjectiveOnce = true;
										missionObjectiveFadingOut = false;

					Debug.Log("no longer fading");
								}
						}
				}

		if (victoryScreenFadingIn) {

			VictoryScreen.GetComponent<UIWidget> ().alpha += .05f;

			if(VictoryScreen.GetComponent<UIWidget> ().alpha >1)
			{
				
				VictoryScreen.GetComponent<UIWidget> ().alpha = 1;
				victoryScreenFadingIn = false;
			}
		}
		
		//UpdateXPBar ();
		if (playerHealth == null) {
		
			playerHealth = GameObject.Find("Tesla").GetComponent<HealthComponent>();
		}
		if (knownMaxHP != playerHealth.MaxHealth *10) {
			StartCoroutine (UpdateHealthBarSize ());
		}
	
		int IntHealth = (int)(playerHealth.CurrentHealth *10);
		int IntMax = (int)(playerHealth.MaxHealth*10 );
		HealthText.text = IntHealth + " / " + IntMax;
		//PlayerLevel.text = stats.CurrentLevel.ToString();

        if (InstructionTextAreaIsShowing)
        {
            float instructionTimeout = InstructionAnimator.GetFloat("InstructionTextAreaTimeout") - Time.deltaTime;
            InstructionAnimator.SetFloat("InstructionTextAreaTimeout", instructionTimeout);
            if (instructionTimeout <= 0)
            {
                InstructionTextAreaIsShowing = false;
            }
		}
		if (NotificationTextAreaIsShowing)
		{
			float notificationTimeout = NotificationAnimator.GetFloat("NotificationTextAreaTimeout") - Time.deltaTime;
			NotificationAnimator.SetFloat("NotificationTextAreaTimeout", notificationTimeout);
			if (notificationTimeout <= 0)
			{
				foreach (MeshRenderer mr in NotificationTextArea.GetComponentsInChildren<MeshRenderer>())
				{
					mr.GetComponentInChildren<MeshRenderer>().enabled = false;
				}
				NotificationTextAreaIsShowing = false;
			}
		}
		if (KillingSpreeTextAreaIsShowing)
		{
			float killingSpreeTimeout = KillingSpreeAnimator.GetFloat("KillingSpreeTextAreaTimeout") - Time.deltaTime;
			KillingSpreeAnimator.SetFloat("KillingSpreeTextAreaTimeout", killingSpreeTimeout);
			if (killingSpreeTimeout <= 0)
			{
				KillingSpreeTextAreaIsShowing = false;
			}
		}



	}

	public void upgradeTech(float amount){
		stats.bank -= amount;
		currentMenu.transform.FindChild ("Buy").FindChild("Label").GetComponent<UILabel> ().text = "Purchased";
		currentMenu.transform.FindChild ("Buy").GetComponent<UIButton> ().enabled = false;
		currentMenu.transform.FindChild("Bank").GetComponent<UILabel> ().text = "Wallet: $" + GameObject.Find ("Inventory").GetComponent<StatTracker> ().bank;
	}

	private void UpdateXPBar(){
	/*
		if (XPContainers.Count != stats.XPNeeded) {
			for (int i = XPContainers.Count; i < stats.XPNeeded; i++){
				GameObject xp;
				if (i == 0){
					xp = Instantiate(firstXPBar) as GameObject;
					xp.transform.parent = this.transform;
					xp.transform.localScale = new Vector3(1,1,1);
					xp.transform.localPosition = new Vector3 (-UIWidth/2 + 200, UIHeight/2 - 70, 0);
					displayDepth = xp.GetComponent<UITexture>().depth;
					nextXPContainerX = 200 + 40;
				}
				else{
					xp = Instantiate(XPBarExpansion) as GameObject;
					xp.transform.parent = this.transform;
					xp.transform.localScale = new Vector3(1,1,1);
					xp.transform.localPosition = new Vector3 (-UIWidth/2 + nextXPContainerX, UIHeight/2 - 70, 0);
					displayDepth -= 2;
					xp.GetComponent<UITexture>().depth = displayDepth;
					nextXPContainerX = nextXPContainerX + 50;
				}
				XPContainers.Add(new XPBarDataContainer(xp, xp.transform.FindChild("Filler").gameObject));
			}
		}

		if (knownCurrentXP != stats.CurrentXP){
			if (knownCurrentXP < stats.CurrentXP){
				for (int i = knownCurrentXP; i < stats.CurrentXP; i++){
					knownCurrentXP++;
					XPContainers[i].fillerTexture.depth = XPContainers[i].containerTexture.depth - 1;
					StartCoroutine("FillXPBar",XPContainers[i].fillerTexture);
				}

			}
			else if (knownCurrentXP >= stats.CurrentXP){
				knownCurrentXP = 0;
				StartCoroutine("ClearAllXPBars");
			}
		}

	}

	private IEnumerator FillXPBar(UITexture fillerTexture){
	/*

		float waitTime = 0.75f;
		yield return new WaitForSeconds (waitTime);
		if(XPGainFX != null){
			EffectBase newInstance = XPGainFX.GetInstance(XPContainers[knownCurrentXP].container.transform.position);
			Vector3 newPos = new Vector3 (newInstance.transform.position.x-0.05f, newInstance.transform.position.y, newInstance.transform.position.z + 1);
			newInstance.transform.position = newPos;
			newInstance.PlayEffect();
		}

		fillerTexture.enabled = true;

		return null;
	}

	private IEnumerator ClearAllXPBars()
	{		
		foreach (XPBarDataContainer xpContainer in XPContainers){
			xpContainer.fillerTexture.enabled = false;
		}
		yield return null;
	}

	private IEnumerator UpdateHealthBarSize()
	{
		if (knownMaxHP != playerHealth.MaxHealth)
		{
			float difference = playerHealth.MaxHealth - knownMaxHP;
			float currentWidth = HealthBar.gameObject.GetComponent<UITexture> ().width;
			float finalWidth = (int) (currentWidth + (HealthBarSizeIncrement * difference));
			float elapsedTime = 0;
			float delta;
			playerHealth.Heal ((int)difference, true);
			UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
			knownMaxHP = playerHealth.MaxHealth;
			while (elapsedTime < HealthChangeDuration)
			{
				elapsedTime += Time.deltaTime;
				delta = HealthChangeMovement.Evaluate(elapsedTime/HealthChangeDuration);
				HealthBar.gameObject.GetComponent<UITexture> ().width = (int)Mathf.Lerp(currentWidth, finalWidth, delta);
				yield return null;
			} 
		}
	}

    public void DisplayInstructionTextArea(string message, float duration)
    {

		if (InstructionAnimator == null) {
			InstructionAnimator = InstructionTextArea.GetComponent<Animator>();		
		}
        InstructionAnimator.SetFloat("InstructionTextAreaTimeout", duration);
        InstructionTextAreaIsShowing = true;

		if (message.Length > 90) {
						InstructionTextArea.GetComponent<UIWidget> ().height = 200;
				}
		else {
			InstructionTextArea.GetComponent<UIWidget> ().height = 125;
		}
		if( InstructionMessage == null)
		{   InstructionMessage = InstructionTextArea.GetComponentInChildren<UILabel>();}

		if (message.Contains ("Voice")) {

						InstructionMessage.fontStyle = FontStyle.Italic;	
		} else {
			InstructionMessage.fontStyle = FontStyle.Normal;

				
		}

	
        InstructionMessage.fontSize = 80;
        InstructionMessage.text = message;
	
    }


	
	public void DisplayNotificationTextArea(string message, float duration, bool showRelic)
	{if (!showingArtifact && !showingVictoryScreen) {
						if (NotificationAnimator != null) {
								if (showRelic) {
										foreach (MeshRenderer mr in NotificationTextArea.GetComponentsInChildren<MeshRenderer>()) {
												mr.GetComponentInChildren<MeshRenderer> ().enabled = true;
										}
								}
								NotificationAnimator.SetFloat ("NotificationTextAreaTimeout", duration);
								NotificationTextAreaIsShowing = true;
								NotificationMessage.fontSize = 60;
								NotificationMessage.text = message;
						}
				}
		
	}

	public void DisplayKillingSpreeTextArea(string message, float duration)
	{if (KillingSpreeAnimator) {
		//	Debug.Log("Displaying killing spree");
						KillingSpreeAnimator.SetFloat ("KillingSpreeTextAreaTimeout", duration);
						KillingSpreeTextAreaIsShowing = true;
		
						KillingSpreeMessage.fontSize = 80;
						KillingSpreeMessage.text = message;
				}
	}


    public void UpdateInstructionDuration(float duration)
    {
        InstructionAnimator.SetFloat("InstructionTextAreaTimeout", duration);
	}
	
	public void UpdateNotificationDuration(float duration)
	{
		NotificationAnimator.SetFloat("NotificationTextAreaTimeout", duration);
	}
	
	public void UpdateKillingSpreeDuration(float duration)
	{
		KillingSpreeAnimator.SetFloat("KillingSpreeTextAreaTimeout", duration);
	}


	private IEnumerator decreaseHealth = null;
	public void UpdateHealthBar(float current, float max) {
		if (current > playerHealth.CurrentHealth) {
			// was healed
			StartCoroutine (Heal(current, max));
		}
		else{
			// was damaged
			if(decreaseHealth != null){
				StopCoroutine(decreaseHealth);
			}
			decreaseHealth = DecreaseHealth(current, max);
			StartCoroutine (decreaseHealth);
		}
	}

	private IEnumerator Heal(float current, float max)
	{
//		float elapsedTime = 0;

		HealthBar.value = current / max;
		yield return null;
	}

	private IEnumerator DecreaseHealth(float current, float max)
	{		

		if (finishedShrinking) 
		{
			shadowStart = HealthBar.value;
//			shadowTexture.rightAnchor.absolute = healthTexture.rightAnchor.absolute - (int)(healthTexture.width * (1 - HealthBar.value));
		}
		else
		{
			shadowStart = HealthBarShadow.value;
		}




		float elapsedTime = 0;
		float delta = 0;

		HealthBar.value = current / max;
		yield return null;
		float shadowFinish = HealthBar.value;
		
		finishedShrinking = false;
		while (elapsedTime < HealthChangeDuration)
		{
			elapsedTime += Time.deltaTime;
			delta = HealthChangeMovement.Evaluate (elapsedTime / HealthChangeDuration);
			HealthBarShadow.value = Mathf.Lerp (shadowStart, shadowFinish, delta);
			yield return null;
		}
		finishedShrinking = true;
	}

	public void HideVictoryScene()
	{showingVictoryScreen = false;
		VictoryScreen.GetComponent<UIWidget> ().alpha = 0;
		if (paused)
		{
			Time.timeScale = 0;
			PauseScreen.GetComponent<UIWidget> ().alpha = 1;
		}
		else
		{	
			Time.timeScale = 1;
		}
	}

	public void PauseGame ()
	{
		paused = true;
		Time.timeScale = 0;
	}

	public void ResumeGame() 
	{	SoundOptions.GetComponent<UIWidget> ().alpha = 0;
		//missionObjectiveFadingOut = true;
		hideCurrentMenu ();
		showingArtifact = false;
		paused = false;
		ArtifactView.GetComponent<ArtifactViewer> ().HideRelicExample ();
		ArtifactView.GetComponent<ArtifactViewer> ().shopViewer = false;
		PauseScreen.SetActive (false);
		SoundOptions.SetActive (false);

		Time.timeScale = 1;
	}

	public void hideCurrentMenu()
		{if (currentMenu) {


			previousMenu = currentMenu;
			if(currentMenu != ArtifactView)
					{showingArtifact = false;}
			if(!missionObjectiveFadingOut)
			{
				currentMenu.GetComponent<UIWidget> ().alpha = 0;}
						if(currentMenu.GetComponent<PowerUpMenu>())
			{currentMenu.GetComponent<PowerUpMenu> ().breakDown ();}
						//currentMenu = null;
				}
		Time.timeScale = 1;
	}

	public void ReturnPreviousMenu()
		{setCurrentMenu (currentMenu.GetComponent<ParentMenu>().parentmenu.name);}

	public void setCurrentMenu(string name)
	{if (name == "JobDescription") {
			UILabel title =  GameObject.Find("JobDescription").transform.FindChild("Title").GetComponent<UILabel>();;
			if (GameObject.Find ("MissionLogTexts")) {
				if(missionTexts == null)
				{
					missionTexts = GameObject.Find("JobDescription").transform.FindChild("Text").GetComponent<UILabel>();}
				LogTexts texts= GameObject.Find ("MissionLogTexts").GetComponent<LogTexts> ();
				if(title !=null && texts !=null)
				{title.text = texts.titles[stats.getGameLevel () - 1];
					missionTexts.text = texts.texts [stats.getGameLevel () - 1];}
			}
		}


		if (currentMenu == ArtifactView) {
			ArtifactView.GetComponent<ArtifactViewer>().HideRelicExample();		
		}
		PauseGame ();
		hideCurrentMenu ();


		currentMenu = GameObject.Find (name);
		currentMenu.GetComponent<UIWidget> ().alpha = 1;

		if (currentMenu.GetComponent<PowerUpMenu> ()) {
						currentMenu.GetComponent<PowerUpMenu> ().initialize ();
				}
		}



	public void ShowVictoryScreen()
	{showingVictoryScreen = true;
		VictoryScreen.SetActive (true);
		Time.timeScale = 0;
		victoryScreenFadingIn = true;

		VictoryScreen.GetComponent<VictoryScreen> ().BuildStatSheet ();
	}

	public void sellCurrentArtifact()
		{stats.sellArtifact (CurrentArtIndex);
		ShowArtifactViewer (false);
		}

	public void disableButton(string buttonName)
		{ArtifactView.transform.FindChild (buttonName).GetComponent<UIButton> ().enabled = false;
		ArtifactView.transform.FindChild (buttonName).GetComponent<UISprite> ().alpha = 0;
	}

	public void enableButton(string buttonName)
		{ArtifactView.transform.FindChild (buttonName).GetComponent<UISprite> ().alpha = 1;
		ArtifactView.transform.FindChild (buttonName).GetComponent<UIButton> ().enabled = true;
	}


	public void ShowArtifactViewer( bool newartifact)
	{currentMenu = ArtifactView;

	paused = true;
	if (newartifact) {
		if(stats == null)
			{stats = GameObject.Find("Inventory").GetComponent<StatTracker>();}
			CurrentArtIndex = stats.getCollectedMiniRelics().Count -1;		
		}
	


						ArtifactView.GetComponent<ArtifactViewer> ().collectedArtifact (null, false);
				
		if (CurrentArtIndex == 0) {
			disableButton("Previous");
			}
		else {
			enableButton("Previous");
		}

		if (stats.getCollectedMiniRelics().Count < 1 || stats.getCollectedMiniRelics().Count == CurrentArtIndex +1 ) {
			disableButton("Next");

		} else {
			enableButton("Next");
		}

		ArtifactView.GetComponent<UIWidget> ().alpha = 1;
		showingArtifact = true;

	
		if (stats.getCollectedMiniRelics ().Count == 0) {

			disableButton("Sell");	
		}
		else if (!ArtifactView.GetComponent<ArtifactViewer> ().shopViewer || stats.getCollectedMiniRelics()[CurrentArtIndex].GetComponent<Collect> ().sold) {
			disableButton("Sell");	
				} 
		else {
			enableButton("Sell");

				}

		if (ArtifactView.GetComponent<ArtifactViewer> ().shopViewer) {
					ArtifactView.transform.FindChild ("Bank").GetComponent<UILabel> ().text = "Wallet: $" + stats.bank;
					enableButton("Back");
				} else {
			ArtifactView.transform.FindChild ("Bank").GetComponent<UILabel> ().text = "";
			disableButton("Back");
		}

		SoundOptions.SetActive (true);
		Time.timeScale = 0;


	}



	public void ShowSoundOptions ()
	{

		PauseScreen.GetComponent<UIWidget> ().alpha = 0;
		SoundOptions.GetComponent<UIWidget> ().alpha = 1;
	}

	public void HideSoundOptions()
	{
//		SoundOptions.SetActive (false);
		SoundOptions.GetComponent<UIWidget> ().alpha = 0;
	}

	public void ShowHighScores()
	{
		Time.timeScale = 0;
		HighScores.GetComponent<UIWidget> ().alpha = 1;
		Debug.Log ("ShowHighScores2");
		HighScores.GetComponent<HSController> ().GetScore ();
	
	}

	public void HideHighScores()
	{
//		HighScores.SetActive (false);
		HighScores.GetComponent<UIWidget> ().alpha =0;
	}

	public void IncrementStatCounter(float currentStatAmount, float incrementAmount, StatTracker.StatTypes statType) {
		GameObject newPanel = Instantiate(StatCounterPanelPrefab) as GameObject;
		newPanel.transform.parent = this.transform;
		newPanel.transform.localPosition = new Vector3 (UIWidth, UIHeight, 0);
		for(int i = statCounters.Count - 1; i >= 0; i--) {
			StatCounterPanel scp = statCounters[i];
			if(scp == null) {
				statCounters.RemoveAt(i);
			} else {
				scp.MoveDown();
			}
		}

		StatCounterPanel panelScript = newPanel.GetComponent<StatCounterPanel>();
		panelScript.Initialize(currentStatAmount, incrementAmount, statType, UIWidth, UIHeight, stats);
		statCounters.Add(panelScript);

	
	}
*/
}
