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

	void OnEnable()
	{
		SceneManager.sceneLoaded += LevelWasLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= LevelWasLoaded;
	}

	bool hasBeenToLevel;
	void Start()
	{
		//Debug.Log ("Calling from " + this.gameObject);
		if (this && !hasBeenToLevel) {
			if (SceneManager.GetActiveScene ().buildIndex == 3) {
				DontDestroyOnLoad (this.gameObject);
		
			} 


			mySource = GetComponent<AudioSource> ();


			foreach (CampaignUpgrade cu in CampUpRef) {
				cu.setInitialStuff ();
			}
		}
	}

	void LevelWasLoaded(Scene myScene, LoadSceneMode mode)
	{
		if (SceneManager.GetActiveScene ().buildIndex != 3 && SceneManager.GetActiveScene ().buildIndex != 0) {
			RaceManager racer = GameObject.FindObjectOfType<GameManager> ().activePlayer;
			hasBeenToLevel = true;

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
		SceneManager.sceneLoaded -= LevelWasLoaded;
	}

	public void playSound ()
	{
		mySource.PlayOneShot (buttonPress[Random.Range(0, buttonPress.Count -1)]);

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
			cu.reInitialize ();
		
		}
		

	}


}
