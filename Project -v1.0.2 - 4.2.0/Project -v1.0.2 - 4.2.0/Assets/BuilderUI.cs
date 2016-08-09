using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuilderUI : MonoBehaviour {





	public List<Button> que = new List<Button>();
	public List<GameObject> numbers = new List<GameObject> ();
	public Text perc;
	private BuildManager myMan;
	private float nextActionTime;
	public Sprite defaultImage;
	private bool buildingStuff;
	public Canvas HelpBox;

	public Text moreSupply;
	// Use this for initialization
	void Start () {
		nextActionTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextActionTime) {
			
			nextActionTime += .3f;
			if (myMan) {

			

				
				if (myMan.buildOrder.Count > 0) {
					if (!buildingStuff) {
						buildingStuff = true;
						foreach (Button b in que) {
							b.gameObject.SetActive (true);
						}

					}

					
					perc.text = (int)(myMan.buildOrder [0].getProgress () * 100) + "%";
				} else {
					if (buildingStuff) {
						buildingStuff = false;
						foreach (Button b in que) {
							b.gameObject.SetActive (false);
						}
					}
					perc.text = "";
				}
			}
		}
	
	}

	public void NoSupply ()
	{
		moreSupply.enabled = true;
	}

	public void hasSupply()
	{
		moreSupply.enabled = false;
	}

	public void loadUnit(RTSObject obj)
	{
		myMan = obj.GetComponent<BuildManager> ();

		bool hasBuild = false;
		buildingStuff = false;
		if (myMan) {

			hasBuild = true;
			buildingStuff = myMan.buildOrder.Count > 0;
		}
	
			


		foreach (Button b in que) {
			b.gameObject.SetActive (hasBuild && buildingStuff);
		}
		
		if (myMan) {
			for (int i = 0; i <5; i++) {
				if (myMan.buildOrder.Count > i) {
					que [i].image.sprite = myMan.buildOrder [i].iconPic;
					numbers [i].SetActive (false);
				} else {
					que [i].image.sprite = defaultImage;
					numbers [i].SetActive (true);
				}
			}
		}
		if (!hasBuild) {
		
			return;}
		if (myMan.buildOrder.Count == 0) {
			perc.text = "";
			hasSupply ();
		
		} else if (myMan.waitingOnSupply) {
			NoSupply ();
			perc.text = "";
		} else if (!myMan.waitingOnSupply) {
			hasSupply ();
		}


	}

	public void bUpdate(GameObject obj)
	{if (myMan && obj == myMan.gameObject) {
			loadUnit (myMan.gameObject.GetComponent<UnitManager> ());
		}
	}


	public void cancelUnit(int n )
	{
		
		myMan.cancel (n);
		if (myMan.buildOrder.Count == 0) {
			HelpBox.enabled = false;}
		//loadUnit (myMan.gameObject.GetComponent<UnitManager> ());

	}

}
