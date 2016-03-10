using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
public class UiAbilityManager : MonoBehaviour {



	//public List<GameObject> UIButtons = new List<GameObject>();

	[System.Serializable]
	public struct buttonSet{
		public GameObject QButton;
		public Slider QSlide;
		public GameObject WButton;
		public Slider WSlide;
		public GameObject EButton;
		public Slider ESlide;
		public GameObject RButton;
		public Slider RSlide;

	}

	public List<buttonSet> certainButtons = new List<buttonSet> ();

	public UnitCardCreater cardCreator;
	private int currentX;
	private int currentY;

	public GameObject buttonTemplate;

	public List<GameObject> Stats = new List<GameObject> ();
	private List<GameObject> unitIcons = new List<GameObject> ();
 	private SelectedManager selectMan;

	private Page currentPage;

	// Use this for initialization
	void Start () {
	
		selectMan = GameObject.Find ("Manager").GetComponent<SelectedManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (currentPage == null) {
			return;
		}
		int AbilityX = 0;
	

		for (int j = 0; j < 3; j++) {
			if (currentPage.rows [j] == null) {
				continue;
			}

			UnitManager man = currentPage.rows [j] [0].gameObject.GetComponent<UnitManager> ();
			for (int m = 0; m < man.abilityList.Count / 4 + 1; m++) {		

					if (man.abilityList.Count > AbilityX * 4) {

						if (man.abilityList [AbilityX * 4] != null) {
						if (man.abilityList [AbilityX * 4].myCost) {

							float maxA = 0;
							foreach (RTSObject obj in currentPage.rows [j]) {
								float n = obj.gameObject.GetComponent<UnitManager> ().abilityList[AbilityX * 4].myCost.cooldownProgress();
								if (n > maxA) {
									maxA = n;
								}

								if (n > .99f) {
									break;}
							}

							certainButtons [j].QSlide.value = maxA;
			
							certainButtons [j].QSlide.gameObject.SetActive (certainButtons [j].QSlide.value < .98);

						} else {
		
							certainButtons [j].QSlide.gameObject.SetActive (false);	
						}

						}
					}
					if (man.abilityList.Count >1+ AbilityX * 4) {
						if (man.abilityList [1 + AbilityX * 4] != null) {
							if (man.abilityList [1 + AbilityX * 4].myCost) {


							float maxA = 0;
							foreach (RTSObject obj in currentPage.rows [j]) {
								float n = obj.gameObject.GetComponent<UnitManager> ().abilityList[1 +AbilityX * 4].myCost.cooldownProgress();
								if (n > maxA) {
									maxA = n;
								}

								if (n > .99f) {
									break;}
							}


							certainButtons [j].WSlide.value = maxA;
						
							certainButtons [j].WSlide.gameObject.SetActive (certainButtons [j].WSlide.value < .98);
							}
						else {

							certainButtons [j].WSlide.gameObject.SetActive (false);	
						}
						}
					}
					if (man.abilityList.Count > 2 + AbilityX * 4) {
						if (man.abilityList [2 + AbilityX * 4] != null) {
							if (man.abilityList [2 + AbilityX * 4].myCost) {

							float maxA = 0;
							foreach (RTSObject obj in currentPage.rows [j]) {
								float n = obj.gameObject.GetComponent<UnitManager> ().abilityList[2 + AbilityX * 4].myCost.cooldownProgress();
								if (n > maxA) {
									maxA = n;
								}

								if (n > .99f) {
									break;}
							}


							certainButtons [j].ESlide.value = maxA;
							certainButtons [j].ESlide.gameObject.SetActive (certainButtons [j].ESlide.value < .98);
							}
						else {
							certainButtons [j].ESlide.gameObject.SetActive (false);	
						}
						}
					}
					if (man.abilityList.Count > 3 + AbilityX * 4) {
						if (man.abilityList [3 + AbilityX * 4] != null) {
							if (man.abilityList [3 + AbilityX * 4].myCost) {

							float maxA = 0;
							foreach (RTSObject obj in currentPage.rows [j]) {
								float n = obj.gameObject.GetComponent<UnitManager> ().abilityList[3 + AbilityX * 4].myCost.cooldownProgress();


								if (n > maxA) {
									maxA = n;
								}

								if (n > .99f) {
							
									break;}
							}

							certainButtons [j].RSlide.value = maxA;
							certainButtons [j].RSlide.gameObject.SetActive (certainButtons [j].RSlide.value < .98);	
							}
						else {
							certainButtons [j].RSlide.gameObject.SetActive (false);	
						}
						}

					}

			}
			AbilityX++;
		}

	
	}




	public void loadUI(Page uiPage)
	{currentPage = uiPage;
		
		foreach (GameObject obj in Stats) {
			obj.GetComponent<StatsUI> ().clear ();
		}

		int n = 0;

		foreach (buttonSet obj in certainButtons) {
			obj.QButton.SetActive (false);
			obj.WButton.SetActive (false);
			obj.EButton.SetActive (false);
			obj.RButton.SetActive (false);
		}



		foreach (GameObject del in unitIcons) {
			Destroy (del);
		}

		unitIcons.Clear ();

		int totalUnit = 0;

		for(int j = 0; j < 3; j ++){
			if (uiPage.rows [j] != null) {
				if (j != 0) {
					if (uiPage.rows [j] == uiPage.rows [j - 1]) {
						continue;
					}
				}
					
						totalUnit += uiPage.rows [j].Count;
					

			}
		}

		if (totalUnit > 1 || totalUnit == 0) {
			cardCreator.gameObject.GetComponent<Canvas> ().enabled = false;
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

					if (uiPage.rows [j].Count > 9) {
						separation = Mathf.Max (10, 408 / picCount);
					}
				
					int currentX = 140;
					for (int k = 0; k < picCount; k++) {

						Vector3 pos = Stats [j].transform.position;
						pos.x += currentX *this.transform.localScale.x ;

						GameObject unit = (GameObject)Instantiate (buttonTemplate);
						unit.transform.localScale = this.transform.localScale;
						unit.transform.rotation = this.transform.rotation;
						unit.transform.SetParent (this.gameObject.transform);

						unit.transform.position = pos;

						unit.GetComponent<Image> ().material = uiPage.rows [j] [k].gameObject.GetComponent<UnitStats> ().Icon;
					
						currentX += separation;

						unitIcons.Add (unit);

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
						Transform trans = certainButtons [n].QButton.transform;
	



						trans.gameObject.SetActive (true);
						trans.GetComponent<Image> ().material = man.abilityList [AbilityX * 4].iconPic;
						trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [0 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;
							
						bool active = false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							active = (obj.gameObject.GetComponent<UnitManager> ().abilityList[AbilityX * 4].active);
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
						
							if (man.abilityList [AbilityX * 4].myType == Ability.type.passive) {
								cb.disabledColor = Color.white;
								trans.GetComponent<Button> ().interactable = false;
							}
							trans.GetComponent<Button> ().colors = cb;


						if (man.abilityList [0 + AbilityX * 4].autocast) {
							trans.GetComponent<Button> ().image.color = Color.green;
						} else {
							trans.GetComponent<Button> ().image.color = Color.white;
						}

						Text charger = trans.FindChild ("Charge1").GetComponent<Text> ();
						if (man.abilityList [AbilityX * 4].chargeCount > -1) {
							charger.text =  ""+man.abilityList [0 + AbilityX * 4].chargeCount;
						} else {
							charger.text = "";
							}}
					}
			
					if(man.abilityList.Count >1+( AbilityX * 4)){
						if (man.abilityList [1 + AbilityX * 4] != null) {
							Transform trans = certainButtons [n].WButton.transform;

							trans.gameObject.SetActive (true);
							trans.GetComponent<Image> ().material = man.abilityList [1 + AbilityX * 4].iconPic;
							trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [1 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;

						bool active= false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							active = (obj.gameObject.GetComponent<UnitManager> ().abilityList[1+AbilityX * 4].active);
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

							if (man.abilityList [1+AbilityX * 4].myType == Ability.type.passive) {
								cb.disabledColor = Color.white;
								trans.GetComponent<Button> ().interactable = false;
							}
							trans.GetComponent<Button> ().colors = cb;



						
							if (man.abilityList [1 + AbilityX * 4].autocast) {
								trans.GetComponent<Button> ().image.color = Color.green;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}


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
							Transform trans = certainButtons [n].EButton.transform;

							trans.gameObject.SetActive (true);
							trans.GetComponent<Image> ().material = man.abilityList [2 + AbilityX * 4].iconPic;
							trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [2 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;
							
						bool active= false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							active = (obj.gameObject.GetComponent<UnitManager> ().abilityList[2 + AbilityX * 4].active);
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

							if (man.abilityList [2+AbilityX * 4].myType == Ability.type.passive) {
								cb.disabledColor = Color.white;
								trans.GetComponent<Button> ().interactable = false;
							}
							trans.GetComponent<Button> ().colors = cb;


							if (man.abilityList [2 + AbilityX * 4].autocast) {
								trans.GetComponent<Button> ().image.color = Color.green;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}


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
							Transform trans = certainButtons [n].RButton.transform;
	
							trans.gameObject.SetActive (true);
							trans.GetComponent<Image> ().material = man.abilityList [3 + AbilityX * 4].iconPic;
							trans.GetComponent<AbilityBox> ().myAbility = man.abilityList [3 + AbilityX * 4];

							ColorBlock cb= trans.GetComponent<Button> ().colors;
							
						bool active= false;
						foreach (RTSObject obj in currentPage.rows [j]) {
							active = (obj.gameObject.GetComponent<UnitManager> ().abilityList[3 + AbilityX * 4].active);
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

							if (man.abilityList [3 +AbilityX * 4].myType == Ability.type.passive) {
								cb.disabledColor = Color.white;
								trans.GetComponent<Button> ().interactable = false;
							}
							trans.GetComponent<Button> ().colors = cb;


							if (man.abilityList [3 + AbilityX * 4].autocast) {
								trans.GetComponent<Button> ().image.color = Color.green;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}


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
							Transform trans = certainButtons [n].QButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("QButton");

							if (man.abilityList [AbilityX * 4].autocast) {
								trans.GetComponent<Button> ().image.color = Color.green;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
						}
					}

					if(man.abilityList.Count >1+( AbilityX * 4)){
						if (man.abilityList [1 + AbilityX * 4] != null) {
							Transform trans = certainButtons [n].WButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("WButton");

							if (man.abilityList [1 + AbilityX * 4].autocast) {
								trans.GetComponent<Button> ().image.color = Color.green;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
						}
					}

					if(man.abilityList.Count > 2+(AbilityX * 4)){
						if (man.abilityList [2 + AbilityX * 4] != null) {
							Transform trans = certainButtons [n].EButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("EButton");

							if (man.abilityList [2 + AbilityX * 4].autocast) {
								trans.GetComponent<Button> ().image.color = Color.green;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
						}
					}

					if(man.abilityList.Count >3+( AbilityX * 4)){
						if (man.abilityList [3 +AbilityX * 4] != null) {
							Transform trans = certainButtons [n].RButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("RButton");

							if (man.abilityList [3 + AbilityX * 4].autocast) {
								trans.GetComponent<Button> ().image.color = Color.green;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
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
							Transform trans = certainButtons [n].QButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("QButton");

							trans.GetComponent<Image> ().material = man.abilityList [AbilityX * 4].iconPic;
				
					
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
							Transform trans = certainButtons [n].WButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("WButton");

							trans.GetComponent<Image> ().material = man.abilityList [1 + AbilityX * 4].iconPic;
					

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
							Transform trans = certainButtons [n].EButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("EButton");
					
							trans.GetComponent<Image> ().material = man.abilityList [2 + AbilityX * 4].iconPic;
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
							Transform trans = certainButtons [n].RButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("RButton");

							trans.GetComponent<Image> ().material = man.abilityList [3 + AbilityX * 4].iconPic;

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

			for (int m = 0; m < man.abilityList.Count / 4 + 1; m++) {
		

					if (man.abilityList.Count > AbilityX * 4) {
						if (man.abilityList [AbilityX * 4] != null) {
							Transform trans = certainButtons [n].QButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("QButton");
							trans.GetComponent<Button> ().interactable = man.abilityList [AbilityX * 4].active;
							if (!man.abilityList [AbilityX * 4].active) {
								trans.GetComponent<Button> ().image.color = Color.red;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
						}
					}

					if (man.abilityList.Count > 1 + (AbilityX * 4)) {
						if (man.abilityList [1 + AbilityX * 4] != null) {
							Transform trans = certainButtons [n].WButton.transform;
						//	Transform trans = UIButtons [n].transform.FindChild ("WButton");

							trans.GetComponent<Button> ().interactable = man.abilityList [1 + AbilityX * 4].active;
							if (!man.abilityList [1 + AbilityX * 4].active) {
								trans.GetComponent<Button> ().image.color = Color.red;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
						}
					}

					if (man.abilityList.Count > 2 + (AbilityX * 4)) {
						if (man.abilityList [2 + AbilityX * 4] != null) {
							Transform trans = certainButtons [n].EButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("EButton");

							trans.GetComponent<Button> ().interactable = man.abilityList [2 + AbilityX * 4].active;
							if (!man.abilityList [2 + AbilityX * 4].active) {
								trans.GetComponent<Button> ().image.color = Color.red;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
						}
					}

					if (man.abilityList.Count > 3 + (AbilityX * 4)) {
						if (man.abilityList [3 + AbilityX * 4] != null) {
							Transform trans = certainButtons [n].RButton.transform;
							//Transform trans = UIButtons [n].transform.FindChild ("RButton");

							trans.GetComponent<Button> ().interactable = man.abilityList [3 + AbilityX * 4].active;
							if (!man.abilityList [3 + AbilityX * 4].active) {
								trans.GetComponent<Button> ().image.color = Color.red;
							} else {
								trans.GetComponent<Button> ().image.color = Color.white;
							}
						}
					}


				}
				AbilityX++;
				n++;
			}

	}




	public void callAbilityOne()
	{selectMan.callAbility (0);}

	public void callAbilityTwo()
	{selectMan.callAbility (1);}


	public void callAbilityThree()
	{selectMan.callAbility (2);}


	public void callAbilityFour()
	{selectMan.callAbility (3);}

	public void callAbilityFive()
	{selectMan.callAbility (4);}

	public void callAbilitySix()
	{selectMan.callAbility (5);}

	public void callAbilitySeven()
	{selectMan.callAbility (6);}

	public void callAbilityEight()
	{selectMan.callAbility (7);}

	public void callAbilityNine()
	{selectMan.callAbility (8);}

	public void callAbilityTen()
	{selectMan.callAbility (9);}

	public void callAbilityEleven()
	{selectMan.callAbility (10);}

	public void callAbilityTwelve()
	{selectMan.callAbility (11);}




}
