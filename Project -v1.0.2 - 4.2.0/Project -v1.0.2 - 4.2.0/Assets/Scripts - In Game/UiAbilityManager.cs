﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;
public class UiAbilityManager : MonoBehaviour {


	private bool shiftDown;
	private bool ctrlDown;
	//public List<GameObject> UIButtons = new List<GameObject>();

	public List<Button> quickButtons = new List<Button>();
	public List<AbilityBox> quickAbility = new List<AbilityBox>();

	[System.Serializable]
	public struct buttonSet{
		public GameObject QButton;
		public Slider QSlide;
		public Text Qtext;
		public Image QAuto;

		public GameObject WButton;
		public Slider WSlide;
		public Text Wtext;
		public Image WAuto;

		public GameObject EButton;
		public Slider ESlide;
		public Text Etext;
		public Image EAuto;

		public GameObject RButton;
		public Slider RSlide;
		public Text Rtext;
		public Image RAuto;

	}
	public GameObject topDividerBar;
	public GameObject bottomBar;
	public List<buttonSet> certainButtons = new List<buttonSet> ();

	public UnitCardCreater cardCreator;
	public Canvas OreCanvas;
	private int currentX;
	private int currentY;

	public GameObject buttonTemplate;

	public List<StatsUI> Stats = new List<StatsUI> ();

	public List<GameObject> IconStartPoints = new List<GameObject> ();
	//Key - Icon, Value - Unit
	private Dictionary<GameObject, GameObject> unitIcons = new Dictionary<GameObject, GameObject> ();
 	private SelectedManager selectMan;

	private Page currentPage;

	private float nextActionTime;

	// Use this for initialization
	void Start () {
		GameMenu.main.addDisableScript (this);
		nextActionTime = Time.time;
		selectMan = GameObject.FindObjectOfType<SelectedManager> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyUp (KeyCode.Q)) {
			
				callAbility (0);

		} else if (Input.GetKeyUp (KeyCode.W)) {
			callAbility (1);
		} else if (Input.GetKeyUp (KeyCode.E)) {
			callAbility (2);
		} else if (Input.GetKeyUp (KeyCode.R)) {
			callAbility (3);
		} else if (Input.GetKeyUp (KeyCode.A)) {
			callAbility (4);
		} else if (Input.GetKeyUp (KeyCode.S)) {
			callAbility (5);
		} else if (Input.GetKeyUp (KeyCode.D)) {
			callAbility (6);
		} else if (Input.GetKeyUp (KeyCode.F)) {
			callAbility (7);
		} else if (Input.GetKeyUp (KeyCode.Z)) {
			callAbility (8);
		} else if (Input.GetKeyUp (KeyCode.X)) {
			callAbility (9);
		} else if (Input.GetKeyUp (KeyCode.C)) {
			callAbility (10);
		} else if (Input.GetKeyUp (KeyCode.V)) {
			callAbility (11);
		} 







		if (Time.time < nextActionTime) {
			return;
		} 
		nextActionTime += .08f;

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			shiftDown = true;
		}
		else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			shiftDown =false;
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			ctrlDown = true;
		}
		else if (Input.GetKeyUp (KeyCode.LeftControl)) {
			ctrlDown =false;
		}


		if (currentPage == null) {
			return;
		}
		int AbilityX = 0;
	

		for (int j = 0; j < 3; j++) {
			if (currentPage.rows [j] == null) {
				continue;
			}
			AbilityX = 0;
			UnitManager man = currentPage.rows [j] [0].gameObject.GetComponent<UnitManager> ();

			for (int m = 0; m < man.abilityList.Count / 4 + 1; m++) {		

					if (man.abilityList.Count > AbilityX * 4) {
					
						if (man.abilityList [AbilityX * 4] != null) {
						if (man.abilityList [AbilityX * 4].myCost) {
							if (man.abilityList [AbilityX * 4].myCost.cooldown != 0) {

								float maxA = 0;
								foreach (RTSObject obj in currentPage.rows [j]) {
									float n = obj.gameObject.GetComponent<UnitManager> ().abilityList [AbilityX * 4].myCost.cooldownProgress ();
									if (n > maxA) {
										maxA = n;
										if (n > .99f) {
											break;
										}
									}

								}

								certainButtons [j].QSlide.value = maxA;
							
								certainButtons [j].QSlide.gameObject.SetActive (certainButtons [j].QSlide.value < .98);
							}
						} 

						}
					}
					if (man.abilityList.Count >1+ AbilityX * 4) {
						if (man.abilityList [1 + AbilityX * 4] != null) {
							if (man.abilityList [1 + AbilityX * 4].myCost) {
							if (man.abilityList [1 + AbilityX * 4].myCost.cooldown != 0) {


								float maxA = 0;
								foreach (RTSObject obj in currentPage.rows [j]) {
									float n = obj.gameObject.GetComponent<UnitManager> ().abilityList [1 + AbilityX * 4].myCost.cooldownProgress ();
									if (n > maxA) {
										maxA = n;
										if (n > .99f) {
											break;
										}
									}

								}


								certainButtons [j].WSlide.value = maxA;
							
								certainButtons [j].WSlide.gameObject.SetActive (certainButtons [j].WSlide.value < .98);
							}
							}
					
						}
					}
					if (man.abilityList.Count > 2 + AbilityX * 4) {
						if (man.abilityList [2 + AbilityX * 4] != null) {
						if (man.abilityList [2 + AbilityX * 4].myCost) {

							if (man.abilityList [2 + AbilityX * 4].myCost.cooldown != 0) {


								float maxA = 0;
								foreach (RTSObject obj in currentPage.rows [j]) {
									float n = obj.gameObject.GetComponent<UnitManager> ().abilityList [2 + AbilityX * 4].myCost.cooldownProgress ();
									if (n > maxA) {
										maxA = n;
										if (n > .99f) {
											break;
										}
									}


								}


								certainButtons [j].ESlide.value = maxA;
								certainButtons [j].ESlide.gameObject.SetActive (certainButtons [j].ESlide.value < .98);
							}
						}

						}
					}
					if (man.abilityList.Count > 3 + AbilityX * 4) {
						if (man.abilityList [3 + AbilityX * 4] != null) {
							if (man.abilityList [3 + AbilityX * 4].myCost) {

							if (man.abilityList [3 + AbilityX * 4].myCost.cooldown != 0) {


								
								float maxA = 0;
								foreach (RTSObject obj in currentPage.rows [j]) {
									float n = obj.gameObject.GetComponent<UnitManager> ().abilityList [3 + AbilityX * 4].myCost.cooldownProgress ();


									if (n > maxA) {
										maxA = n;
										if (n > .99f) {
											break;
										}
									}


								}

								certainButtons [j].RSlide.value = maxA;
								certainButtons [j].RSlide.gameObject.SetActive (certainButtons [j].RSlide.value < .98);	
							}}
					
						}

					}

			}
			AbilityX++;
		}

	
	}



	public void IconClick(GameObject obj)
	{
		if (!shiftDown && !ctrlDown) {
			GameObject temp = unitIcons [obj];
			selectMan.DeselectAll ();
			selectMan.AddObject (temp.GetComponent<UnitManager> ());

			selectMan.CreateUIPages (0);
		} else if (shiftDown && !ctrlDown) {

			selectMan.DeselectObject (unitIcons [obj].GetComponent<UnitManager> ());
			selectMan.CreateUIPages (0);
		
		} else if (!shiftDown && ctrlDown) {
			selectMan.selectAllUnitType (unitIcons [obj].GetComponent<UnitManager> ());
		
		} else {selectMan.DeSelectAllUnitType (unitIcons [obj].GetComponent<UnitManager> ());
		}
	}

	public void clearPage()
	{
		currentPage = null;

		foreach (StatsUI obj in Stats) {
			if (obj) {
				obj.clear ();
			}
		}

		if (topDividerBar) {
			topDividerBar.SetActive (false);
		}
		if (bottomBar) {
			bottomBar.SetActive (false);
		}

		try{
		foreach (buttonSet obj in certainButtons) {
			
				obj.QButton.SetActive (false);
				obj.WButton.SetActive (false);
				obj.EButton.SetActive (false);
				obj.RButton.SetActive (false);

			}}catch(Exception){
		}

		foreach (KeyValuePair< GameObject, GameObject > del in unitIcons) {
			Destroy (del.Key);
		}

		unitIcons.Clear ();

	

			currentPage = null;
		cardCreator.turnOff ();

			


	}




	public void loadUI(Page uiPage)
	{currentPage = uiPage;
		// Clear old info in the buttons and stats
		foreach (StatsUI obj in Stats) {
			obj.clear ();
		}
		topDividerBar.SetActive (true);
		bottomBar.SetActive (true);
		int n = 0;

		foreach (buttonSet obj in certainButtons) {
			obj.QButton.SetActive (false);
			obj.WButton.SetActive (false);
			obj.EButton.SetActive (false);
			obj.RButton.SetActive (false);
		}



		foreach (KeyValuePair< GameObject, GameObject > del in unitIcons) {
			Destroy (del.Key);
		}

		unitIcons.Clear ();

		int totalUnit = 0;
		//Set divider bars for how many abilities the units have.
		for(int j = 0; j < 3; j ++){
			if (uiPage.rows [j] != null) {
				if (j != 0) {
					if (uiPage.rows [j] == uiPage.rows [j - 1] ) {
						if (j == 1) {
							
							topDividerBar.SetActive (false);
						} else if (j == 2) {
							
							bottomBar.SetActive (false);}
						continue;
					}
				}
					
						totalUnit += uiPage.rows [j].Count;
	
			
			}
		}

		if (totalUnit == 0) {
			currentPage = null;
		
			topDividerBar.SetActive (false);

			bottomBar.SetActive (false);
			cardCreator.gameObject.GetComponent<Canvas> ().enabled = false;
			return;
		}

		else if (totalUnit > 1 ) {
			OreCanvas.enabled = false;
			cardCreator.gameObject.GetComponent<Canvas> ().enabled = false;
			if (totalUnit == 0) {
				topDividerBar.SetActive (false);

				bottomBar.SetActive (false);
				return;
			}
		} else {

			cardCreator.gameObject.GetComponent<Canvas> ().enabled = true;
		}

		for(int j = 0; j < 3; j ++){

			if (uiPage.rows [j] == null) {
				
				continue;
			}

			n = uiPage.rows[j][0].AbilityStartingRow;


			//Sets the unit's stats and count
			UnitManager man = uiPage.rows[j][0].gameObject.GetComponent<UnitManager> ();
			Stats[n].GetComponent<StatsUI> ().loadUnit (man,uiPage.rows[j].Count, man.UnitName);

		// fill the icon panel
		
			if (totalUnit > 1) {
				
				if (j == 0 || uiPage.rows [j] != uiPage.rows [j - 1]) {
					int picCount = Mathf.Min (uiPage.rows [j].Count, 18);
					int separation = 59;

					if (uiPage.rows [j].Count > 12) {
						separation = Mathf.Max (10, 408 / picCount);
					}
				
					int currentX = 140;
					for (int k = 0; k < picCount; k++) {

						Vector3 pos =IconStartPoints[j].transform.position;
						pos.x += currentX *this.transform.localScale.x ;

						GameObject unit = (GameObject)Instantiate (buttonTemplate);
						unit.transform.FindChild("UnitIconTemplate").GetComponent<UnitIconInfo> ().myUnit = uiPage.rows [j] [k].gameObject;
						unit.transform.localScale = this.transform.localScale;
					
						unit.transform.rotation = this.transform.rotation;
						unit.transform.SetParent (this.gameObject.transform);

						unit.transform.position = pos;

						unit.transform.FindChild("UnitIconTemplate").GetComponent<Image> ().sprite = uiPage.rows [j] [k].gameObject.GetComponent<UnitStats> ().Icon;
					
						currentX += separation;
						unit.transform.FindChild("UnitIconTemplate").GetComponent<Button> ().onClick.AddListener(delegate() {IconClick(unit);});
					
						unitIcons.Add (unit, uiPage.rows [j] [k].gameObject);
						uiPage.rows [j] [k].gameObject.GetComponent<Selected> ().setIcon (unit);
					}
				}
			} else {
	
				if (uiPage.rows [j] != null) {

					cardCreator.CreateCard (uiPage.rows [j][0]);
				}
			}
			//Set up the command card
			int AbilityX = 0;
	
			for (int m = 0; m < man.abilityList.Count / 4 +1; m++) {


					if(man.abilityList.Count > AbilityX * 4){
						if(man.abilityList [AbilityX * 4] !=null){
						Transform trans = certainButtons [n].QButton.transform.FindChild("QButton");


				
						certainButtons [j].QSlide.gameObject.SetActive (false);
						trans.parent.gameObject.SetActive (true);
						trans.GetComponent<Image> ().sprite = man.abilityList [AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [0 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;
							
						bool active = false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

							if (!Uman.Silenced() && !Uman.Stunned()) {
								active = (Uman.abilityList [AbilityX * 4].active);
							}
							if (active) {
								break;}
							}
					
						if (active) {
								cb.disabledColor = Color.white;
							if(trans && trans.GetComponent<Button> ())
								trans.GetComponent<Button> ().interactable = true;
							} else {
								cb.disabledColor =new Color(.5f,0,0,1);
								trans.GetComponent<Button> ().interactable = false;

							}

						if (man.abilityList [AbilityX * 4].getMyType()== Ability.type.passive) {
							certainButtons [j].Qtext.enabled = false;
							cb.disabledColor = Color.white;
							trans.GetComponent<Button> ().interactable = false;
						} else {
							certainButtons [j].Qtext.enabled = true;
						}

							trans.GetComponent<Button> ().colors = cb;

						trans.FindChild ("AutoCast").GetComponent<Image> ().enabled = man.abilityList [0 + AbilityX * 4].canAutoCast;

						certainButtons [j].QAuto.enabled = man.abilityList [0 + AbilityX * 4].autocast;

						Text charger = trans.FindChild ("Charge1").GetComponent<Text> ();
						if (man.abilityList [AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [0 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
							}}
					}
			
					if(man.abilityList.Count >1+( AbilityX * 4)){
						if (man.abilityList [1 + AbilityX * 4] != null) {

					
							certainButtons [j].WSlide.gameObject.SetActive (false);


						Transform trans = certainButtons [n].WButton.transform.FindChild("WButton");;

						trans.parent.gameObject.SetActive (true);
						trans.GetComponent<Image> ().sprite = man.abilityList [1 + AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [1 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;

						bool active = false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

							if (!Uman.Silenced() && !Uman.Stunned()) {
								active = (Uman.abilityList [1+AbilityX * 4].active);
							}
							if (active) {
								break;}
						}



						//if (man.abilityList [AbilityX * 4].active) {
						if (active) {

							
								cb.disabledColor = Color.white;
								trans.GetComponent<Button> ().interactable = true;
							} else {
								cb.disabledColor =new Color(.5f,0,0,1);
								trans.GetComponent<Button> ().interactable = false;

							}

						if (man.abilityList [1 + AbilityX * 4].getMyType() == Ability.type.passive) {
							certainButtons [j].Wtext.enabled = false;
						
							cb.disabledColor = Color.white;
							trans.GetComponent<Button> ().interactable = false;
						} else {
							certainButtons [j].Wtext.enabled = true;
						}
							trans.GetComponent<Button> ().colors = cb;


							trans.FindChild ("AutoCast").GetComponent<Image> ().enabled = man.abilityList [1 + AbilityX * 4].canAutoCast;
							certainButtons [j].WAuto.enabled = man.abilityList [1 + AbilityX * 4].autocast;


							Text charger = trans.FindChild ("Charge2").GetComponent<Text> ();
							if (man.abilityList [1 + AbilityX * 4].chargeCount > -1) {
								charger.text = "" + man.abilityList [1 + AbilityX * 4].chargeCount;
							} else {
								charger.text = "";
							}
						}
					}




					if(man.abilityList.Count > 2+(AbilityX * 4)){
						if (man.abilityList [2 +AbilityX * 4] != null) {

				
							certainButtons [j].ESlide.gameObject.SetActive (false);


						Transform trans = certainButtons [n].EButton.transform.FindChild("EButton");;

						trans.parent.gameObject.SetActive (true);
						trans.GetComponent<Image> ().sprite = man.abilityList [2 + AbilityX * 4].iconPic;
							trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [2 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;
				

						bool active = false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

							if (!Uman.Silenced() && !Uman.Stunned()) {
								active = (Uman.abilityList [2 + AbilityX * 4].active);
							}
							if (active) {
								break;}
						}

						if (active) {


								cb.disabledColor = Color.white;
								trans.GetComponent<Button> ().interactable = true;
							} else {
								cb.disabledColor =new Color(.5f,0,0,1);
								trans.GetComponent<Button> ().interactable = false;

							}

						if (man.abilityList [2 + AbilityX * 4].getMyType() == Ability.type.passive) {
							certainButtons [j].Etext.enabled = false;
							cb.disabledColor = Color.white;
							trans.GetComponent<Button> ().interactable = false;
						} else {
							certainButtons [j].Etext.enabled = true;
						}
							trans.GetComponent<Button> ().colors = cb;

						trans.FindChild ("AutoCast").GetComponent<Image> ().enabled = man.abilityList [2 + AbilityX * 4].canAutoCast;
						certainButtons [j].EAuto.enabled = man.abilityList [2+ AbilityX * 4].autocast;


							Text charger = trans.FindChild ("Charge3").GetComponent<Text> ();
							if (man.abilityList [2 + AbilityX * 4].chargeCount > -1) {
								charger.text = "" + man.abilityList [2 + AbilityX * 4].chargeCount;
							} else {
								charger.text = "";
							}
						}

					}

					if(man.abilityList.Count >3+( AbilityX * 4)){
						if (man.abilityList [3 +AbilityX * 4] != null) {

				
							certainButtons [j].RSlide.gameObject.SetActive (false);


						Transform trans = certainButtons [n].RButton.transform.FindChild("RButton");;
	
						trans.parent.gameObject.SetActive (true);
						trans.GetComponent<Image> ().sprite = man.abilityList [3 + AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [3 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;
							

						bool active = false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

							if (!Uman.Silenced() && !Uman.Stunned()) {
								active = (Uman.abilityList [3 +AbilityX * 4].active);
							}
							if (active) {
								break;}
						}


						if (active) {

								cb.disabledColor = Color.white;
								trans.GetComponent<Button> ().interactable = true;
							} else {
								cb.disabledColor =new Color(.5f,0,0,1);
								trans.GetComponent<Button> ().interactable = false;

							}

						if (man.abilityList [3 + AbilityX * 4].getMyType() == Ability.type.passive) {
							certainButtons [j].Rtext.enabled = false;
							cb.disabledColor = Color.white;
							trans.GetComponent<Button> ().interactable = false;
						} else {
							certainButtons [j].Rtext.enabled = true;
						}
							trans.GetComponent<Button> ().colors = cb;

						trans.FindChild ("AutoCast").GetComponent<Image> ().enabled = man.abilityList [3 + AbilityX * 4].canAutoCast;
						certainButtons [j].RAuto.enabled = man.abilityList [3 + AbilityX * 4].autocast;



							Text charger = trans.FindChild ("Charge4").GetComponent<Text> ();
							if (man.abilityList [3 + AbilityX * 4].chargeCount > -1) {
								charger.text = "" + man.abilityList [3 + AbilityX * 4].chargeCount;
							} else {
								charger.text = "";
							}
						}
					}

				AbilityX++;
				n++;
			}
			}

		}



	public void upDateAutoCast(Page uiPage)
	{


		int n = 0;

		for(int j = 0; j < 3; j ++){
			if (uiPage.rows [j] == null) {
				continue;
			}

			n = uiPage.rows[j][0].AbilityStartingRow;

			//Sets the unit's stats and count
			UnitManager man = uiPage.rows[j][0].gameObject.GetComponent<UnitManager> ();
		

			int AbilityX = 0;

			for (int m = 0; m < man.abilityList.Count / 4 +1; m++) {


					if(man.abilityList.Count > AbilityX * 4){
						if (man.abilityList [AbilityX * 4] != null) {
			
						certainButtons [j].QAuto.enabled = man.abilityList [0 + AbilityX * 4].autocast;

						}
					}

					if(man.abilityList.Count >1+( AbilityX * 4)){
						if (man.abilityList [1 + AbilityX * 4] != null) {
	
						certainButtons [j].WAuto.enabled = man.abilityList [1 + AbilityX * 4].autocast;
					
						}
					}

					if(man.abilityList.Count > 2+(AbilityX * 4)){
						if (man.abilityList [2 + AbilityX * 4] != null) {
		
						certainButtons [j].EAuto.enabled = man.abilityList [2 + AbilityX * 4].autocast;
			
						}
					}

					if(man.abilityList.Count >3+( AbilityX * 4)){
						if (man.abilityList [3 +AbilityX * 4] != null) {

						certainButtons [j].RAuto.enabled = man.abilityList [3 + AbilityX * 4].autocast;
				
						}
					}


				}
				AbilityX++;
				n++;
			
		}



	}



	public void updateUI(Page uiPage)
	{
		int n = 0;



		for(int j = 0; j < 3; j ++){
			if (uiPage.rows [j] == null) {
				continue;
			}

			n = uiPage.rows[j][0].AbilityStartingRow;

			//Sets the unit's stats and count
			UnitManager man = uiPage.rows[j][0].gameObject.GetComponent<UnitManager> ();
			Stats[n].GetComponent<StatsUI> ().loadUnit (man, 
				uiPage.rows[j].Count, man.UnitName);

			int AbilityX = 0;

			for (int m = 0; m < man.abilityList.Count / 4 +1; m++) {
		

					if (man.abilityList.Count > AbilityX * 4) {
						if (man.abilityList [AbilityX * 4] != null) {
						Transform trans = certainButtons [n].QButton.transform.FindChild("QButton");;
							//Transform trans = UIButtons [n].transform.FindChild ("QButton");

						trans.GetComponent<Image> ().sprite = man.abilityList [AbilityX * 4].iconPic;
				
					
							trans.GetComponent<Button> ().interactable = man.abilityList [AbilityX * 4].active;
					

							Text charger = trans.FindChild ("Charge1").GetComponent<Text> ();
							if (man.abilityList [AbilityX * 4].chargeCount > -1) {
							
								charger.text = "" + man.abilityList [AbilityX * 4].chargeCount;
							} else {
								charger.text = "";
							}
						}
					}

					if(man.abilityList.Count >1+( AbilityX * 4)){
						if (man.abilityList [1 + AbilityX * 4] != null) {
						Transform trans = certainButtons [n].WButton.transform.FindChild("WButton");;
							//Transform trans = UIButtons [n].transform.FindChild ("WButton");

						trans.GetComponent<Image> ().sprite = man.abilityList [1 + AbilityX * 4].iconPic;
					

							Text charger = trans.FindChild ("Charge2").GetComponent<Text> ();
							trans.GetComponent<Button> ().interactable = man.abilityList [1 + AbilityX * 4].active;

							if (man.abilityList [1 + AbilityX * 4].chargeCount > -1) {
								charger.text = "" + man.abilityList [1 + AbilityX * 4].chargeCount;

							} else {
								charger.text = "";
							}
						}
					}

					if(man.abilityList.Count > 2+(AbilityX * 4)){
						if (man.abilityList [2 + AbilityX * 4] != null) {
						Transform trans = certainButtons [n].EButton.transform.FindChild("EButton");;
							//Transform trans = UIButtons [n].transform.FindChild ("EButton");
					
						trans.GetComponent<Image> ().sprite = man.abilityList [2 + AbilityX * 4].iconPic;
							trans.GetComponent<Button> ().interactable = man.abilityList [2 + AbilityX * 4].active;

							Text charger = trans.FindChild ("Charge3").GetComponent<Text> ();
							if (man.abilityList [2 + AbilityX * 4].chargeCount > -1) {
								charger.text = "" + man.abilityList [2 + AbilityX * 4].chargeCount;
							} else {
								charger.text = "";
							}
						}
					}

					if(man.abilityList.Count >3+( AbilityX * 4)){
						if (man.abilityList [3 + AbilityX * 4] != null) {
						Transform trans = certainButtons [n].RButton.transform.FindChild("RButton");;
							//Transform trans = UIButtons [n].transform.FindChild ("RButton");

						trans.GetComponent<Image> ().sprite = man.abilityList [3 + AbilityX * 4].iconPic;

							trans.GetComponent<Button> ().interactable = man.abilityList [3 + AbilityX * 4].active;
							Text charger = trans.FindChild ("Charge4").GetComponent<Text> ();
							if (man.abilityList [3 + AbilityX * 4].chargeCount > -1) {
								charger.text = "" + man.abilityList [3 + AbilityX * 4].chargeCount;
							} else {
								charger.text = "";
							}
						}
					}


				}
				AbilityX++;
				n++;
			}
		


	}
		



	public void upDateActive(Page uiPage)
	{


		int n = 0;

		for (int j = 0; j < 3; j++) {
			if (uiPage.rows [j] == null) {
				continue;
			}

			n = uiPage.rows [j] [0].AbilityStartingRow;

			//Sets the unit's stats and count
			UnitManager man = uiPage.rows [j] [0].gameObject.GetComponent<UnitManager> ();


			int AbilityX = 0;


		for (int m = 0; m < man.abilityList.Count / 4 +1; m++) {


				if (man.abilityList.Count > AbilityX * 4) {
					if (man.abilityList [AbilityX * 4] != null) {
						Transform trans = certainButtons [n].QButton.transform.FindChild ("QButton");

						ColorBlock cb = trans.GetComponent<Button> ().colors;

						bool active = false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

							if (!Uman.Silenced () && !Uman.Stunned ()) {
								active = (Uman.abilityList [AbilityX * 4].active);
							}
							if (active) {
								break;
							}
						}
		
						if (active) {
							cb.disabledColor = Color.white;
							if (trans && trans.GetComponent<Button> ())
								trans.GetComponent<Button> ().interactable = true;
						} else {
							cb.disabledColor = new Color (.5f, 0, 0, 1);
							trans.GetComponent<Button> ().interactable = false;

						}


						trans.GetComponent<Button> ().colors = cb;

					}
				}
			if(man.abilityList.Count >1+( AbilityX * 4)){
				if (man.abilityList [1 + AbilityX * 4] != null) {

					Transform trans = certainButtons [n].WButton.transform.FindChild ("WButton");
					ColorBlock cb= trans.GetComponent<Button> ().colors;

					bool active = false;
					foreach (RTSObject obj in currentPage.rows [j]) {
						UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

						if (!Uman.Silenced() && !Uman.Stunned()) {
							active = (Uman.abilityList [1+AbilityX * 4].active);
						}
						if (active) {
							break;}
					}



					//if (man.abilityList [AbilityX * 4].active) {
					if (active) {


						cb.disabledColor = Color.white;
						trans.GetComponent<Button> ().interactable = true;
					} else {
						cb.disabledColor =new Color(.5f,0,0,1);
						trans.GetComponent<Button> ().interactable = false;

					}

					trans.GetComponent<Button> ().colors = cb;

				}
			}




			if(man.abilityList.Count > 2+(AbilityX * 4)){
				if (man.abilityList [2 +AbilityX * 4] != null) {
						Transform trans = certainButtons [n].EButton.transform.FindChild ("EButton");
					ColorBlock cb= trans.GetComponent<Button> ().colors;


					bool active = false;
					foreach (RTSObject obj in currentPage.rows [j]) {
						UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

						if (!Uman.Silenced() && !Uman.Stunned()) {
							active = (Uman.abilityList [2 + AbilityX * 4].active);
						}
						if (active) {
							break;}
					}

					if (active) {


						cb.disabledColor = Color.white;
						trans.GetComponent<Button> ().interactable = true;
					} else {
						cb.disabledColor =new Color(.5f,0,0,1);
						trans.GetComponent<Button> ().interactable = false;

					}
								
					trans.GetComponent<Button> ().colors = cb;

				
				}

			}

			if(man.abilityList.Count >3+( AbilityX * 4)){
				if (man.abilityList [3 +AbilityX * 4] != null) {
						Transform trans = certainButtons [n].RButton.transform.FindChild ("RButton");
					ColorBlock cb= trans.GetComponent<Button> ().colors;


					bool active = false;
					foreach (RTSObject obj in currentPage.rows [j]) {
						UnitManager Uman = obj.gameObject.GetComponent<UnitManager> ();

						if (!Uman.Silenced() && !Uman.Stunned()) {
							active = (Uman.abilityList [3 +AbilityX * 4].active);
						}
						if (active) {
							break;}
					}


					if (active) {

						cb.disabledColor = Color.white;
						trans.GetComponent<Button> ().interactable = true;
					} else {
						cb.disabledColor =new Color(.5f,0,0,1);
						trans.GetComponent<Button> ().interactable = false;

					}
							
					trans.GetComponent<Button> ().colors = cb;

				}
			}

			AbilityX++;
			n++;
		}
	}



	}


	public void callAbility(int n)
	{if (Input.GetKey (KeyCode.LeftAlt)) {
			selectMan.setAutoCast (n);
			selectMan.AutoCastUI ();

	}else if (quickButtons [n].IsInteractable() && quickAbility[n].myAbility && quickAbility[n].myAbility.active) {
		
				selectMan.callAbility (n);
			
		}
		
	}






}
