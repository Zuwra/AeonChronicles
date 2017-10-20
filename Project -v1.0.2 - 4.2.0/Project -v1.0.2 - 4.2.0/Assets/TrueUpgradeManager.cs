using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TrueUpgradeManager : MonoBehaviour {
	public List<AudioClip> buttonPress;
	public AudioSource mySource;

	public List<CampaignUpgrade> CampUpRef;
	public List<CampaignUpgrade.UpgradesPiece> myUpgrades= new List<CampaignUpgrade.UpgradesPiece>();
	// Use this for initialization
	public List<GameObject> UnAppliedUpgrade;

	public static TrueUpgradeManager instance;

	void Awake()
	{instance = this;
		
	}

	public void Unused()
	{

		bool unUsed = false;

		foreach (CampaignUpgrade upgrade in CampUpRef) {
			if (upgrade.myUpgrades.Count > 1 && upgrade.currentIndex == 0 && upgrade.unlocked) {
		
				unUsed = true;
				break;
			}

		}
		foreach (GameObject obj in UnAppliedUpgrade) {
			if (obj) {
				obj.SetActive (unUsed);
			}
		}
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += LevelWasLoaded;
	}

	void OnDisable()
	{
		Debug.Log ("Disabling");
		SceneManager.sceneLoaded -= LevelWasLoaded;
	}

	bool hasBeenToLevel;
	void Start()
	{
		
		if (this && !hasBeenToLevel) {
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				DontDestroyOnLoad (this.gameObject);
			} 

			mySource = GetComponent<AudioSource> ();


			foreach (CampaignUpgrade cu in CampUpRef) {
			//	cu.setInitialStuff ();
			}
		}
	}

	void LevelWasLoaded(Scene myScene, LoadSceneMode mode)
	{
		//Debug.Log ("level was loaded " + SceneManager.GetActiveScene().buildIndex);
		if (SceneManager.GetActiveScene ().buildIndex != 1 && SceneManager.GetActiveScene ().buildIndex != 0) {
			RaceManager racer = GameObject.FindObjectOfType<GameManager> ().activePlayer;
			hasBeenToLevel = true;
			//Debug.Log ("Applying to Upgrade Ball");

			foreach (CampaignUpgrade.UpgradesPiece cu in myUpgrades) {
				if (cu.pointer) {


					racer.addUpgrade (cu.pointer, "");
				}
			}
		} else {
			if (hasBeenToLevel) {
				Destroy (this.gameObject);
			}
		}
		//SceneManager.sceneLoaded -= LevelWasLoaded;
	}

	float lastSound ;
	public void playSound ()
	{
		if (Time.timeSinceLevelLoad > 1 &&  Time.time > lastSound +1) {
			mySource.PlayOneShot (buttonPress [Random.Range (0, buttonPress.Count - 1)]);
		}
		lastSound = Time.time;
	}

	public void upgradeBought(SpecificUpgrade upg, CampaignUpgrade.upgradeType t)
	{
		//Debug.Log ("Upgrade was bought" +upg.name);
		CampaignUpgrade.UpgradesPiece cpu = new CampaignUpgrade.UpgradesPiece ();
		cpu.pointer = upg;
		cpu.name = upg.Name;
		cpu.description = upg.Descripton;
		cpu.unlocked = true;
		cpu.pic = upg.iconPic;
		cpu.myType = t;




		myUpgrades.Add (cpu);

		foreach (CampaignUpgrade cu in CampUpRef) {
			cu.upgradeBought();
		
		}
		

	}


}
