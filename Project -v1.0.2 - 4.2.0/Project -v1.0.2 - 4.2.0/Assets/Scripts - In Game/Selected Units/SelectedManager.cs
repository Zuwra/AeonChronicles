
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SelectedManager : MonoBehaviour, ISelectedManager
{


    private List<RTSObject> SelectedObjects = new List<RTSObject>();

    //used for UI grouping
    private List<List<RTSObject>> tempAbilityGroups = new List<List<RTSObject>>();
    private List<Page> UIPages = new List<Page>();


    private List<List<RTSObject>> Group = new List<List<RTSObject>>();

    private int currentPage = 0;
	private int currentCenterOn = 0;

	int lsatVoiceIndex;
	float lastVoiceTime;
    public static SelectedManager main;
	public UIManager uiManage;
    public UiAbilityManager abilityManager;
    private RaceManager raceMan;

	//Used for the F5-F8 Selection buttons
	public List<List<string>> globalSelection = new List<List<string>> ();
	private TargetCircleManager targetManager;

    public GameObject movementInd;
    public GameObject attackInd;
	public GameObject fogIndicator;
	public GameObject fogAttacker;

	private ControlGroupUI controlUI;
	public PageUIManager pageUI;
	public AudioClip moveSound;
	public AudioClip attackSound;
	private AudioSource AudioSrc;

	private int soundIndex;
 
    void Start()
	{
		GameMenu.main.addDisableScript (this);
		uiManage = (UIManager)FindObjectOfType (typeof(UIManager));
		abilityManager = UiAbilityManager.main;
		raceMan = GameManager.main.activePlayer;

		controlUI = GameObject.FindObjectOfType<ControlGroupUI> ();
		pageUI = GameObject.FindObjectOfType<PageUIManager> ();
		targetManager = GameObject.FindObjectOfType<TargetCircleManager> ();
		AudioSrc =GameObject.FindObjectOfType<ExpositionDisplayer>().GetComponent<AudioSource> ();

    }


    void Update()
	{
        if (Input.GetKeyUp(KeyCode.T))
        {
			if (Input.GetKey (KeyCode.LeftControl)) {
				PatrolMoveO ();
			} else {
				attackMoveO (Vector3.zero);
			}
        }

		if (Input.GetKeyUp (KeyCode.G)) {
			
			stopO ();
			if (Input.GetKey (KeyCode.LeftControl)) {

				GiveOrder (Orders.CreateHoldGroundOrder (Input.GetKey(KeyCode.LeftShift)));
			} 
		} 
		else if (Input.GetKeyUp (KeyCode.Escape)) {
			stopO ();
			cancel ();
		}


		if (Input.GetKey (KeyCode.LeftShift)) {
			// set a control group

			if (Input.GetKeyDown (KeyCode.BackQuote)) {
				DeselectAll();
				foreach (UnitManager obj in raceMan.getAllUnitsOnScreen())
				{
					AddObject(obj);
				}
				CreateUIPages (0);
			}

			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (0, false); 

				}
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (1, false);

				}
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (2, false);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (3, false);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (4, false);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (5, false);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (6, false);		
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (7, false);

				}
			} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (8, false);

				}
			} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (9, false);

				}
			}

		} else if (Input.GetKey (KeyCode.LeftControl)) {

			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				AddUnitsToGroup (0, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				AddUnitsToGroup (1, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				AddUnitsToGroup (2, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				AddUnitsToGroup (3, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				AddUnitsToGroup (4, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				AddUnitsToGroup (5, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				AddUnitsToGroup (6, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
				AddUnitsToGroup (7, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
				AddUnitsToGroup (8, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
				AddUnitsToGroup (9, true);
			}
		} else if (Input.GetKey (KeyCode.LeftAlt)) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				removeFromControlGroups ();
				AddUnitsToGroup (0, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				removeFromControlGroups ();
				AddUnitsToGroup (1, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				removeFromControlGroups ();
				AddUnitsToGroup (2, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				removeFromControlGroups ();
				AddUnitsToGroup (3, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				removeFromControlGroups ();
				AddUnitsToGroup (4, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				removeFromControlGroups ();
				AddUnitsToGroup (5, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				removeFromControlGroups ();
				AddUnitsToGroup (6, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
				removeFromControlGroups ();
				AddUnitsToGroup (7, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
				removeFromControlGroups ();
				AddUnitsToGroup (8, true);
			} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
				removeFromControlGroups ();
				AddUnitsToGroup (9, true);
			}
		
		
		}
        else {
            // Select a control group
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { 
				//SelectGroup(0);
				ControlGroupUI.instance.pressButton (0);}
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            { //SelectGroup(1); 
				ControlGroupUI.instance.pressButton (1);}
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {// SelectGroup(2); 
				ControlGroupUI.instance.pressButton (2);}
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {//SelectGroup(3);
				ControlGroupUI.instance.pressButton (3);}
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {//SelectGroup(4);
				ControlGroupUI.instance.pressButton (4);}
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            { //SelectGroup(5);
				ControlGroupUI.instance.pressButton (5);}
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {// SelectGroup(6);
				ControlGroupUI.instance.pressButton (6);}
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {//SelectGroup(7);
				ControlGroupUI.instance.pressButton (7);}
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {//SelectGroup(8); 
				ControlGroupUI.instance.pressButton (8);}
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {//SelectGroup(9); 
				ControlGroupUI.instance.pressButton (9);}

        }

		if (Input.GetKeyUp (KeyCode.Delete)) {
			if (SelectedObjects.Count > 0 && SelectedObjects [0] != null) {
				SelectedObjects [0].getUnitStats().kill (null);
			}
		}

		else if (Input.GetKeyUp (KeyCode.Space)) {
			if (SelectedObjects.Count == 1) {
				Vector3 location = SelectedObjects [0].gameObject.transform.position;
				location.z -= 70;

				MainCamera.main.Move (location);
			}
			else if (SelectedObjects.Count > 1) {
				int startingPoint = currentCenterOn;
				do {
					currentCenterOn++;
					if (currentCenterOn >= SelectedObjects.Count) {
						currentCenterOn = 0;
					}
					if(startingPoint == currentCenterOn)
						{return;}
				} while(SelectedObjects [currentCenterOn] == null);
				

				if (SelectedObjects [currentCenterOn]) {
					Vector3 location = SelectedObjects [currentCenterOn].gameObject.transform.position;
					location.z -= 70;

					MainCamera.main.Move (location);
				}
			}
		}

		if (Input.GetKeyUp(KeyCode.Tab) && UIPages.Count > 0)
        {

            if (currentPage < UIPages.Count-1)
			{
				currentPage++;
            }
            else {
                currentPage = 0;
            }
			pageUI.selectPage (currentPage);
			abilityManager.loadUI(UIPages[currentPage]);

        }
			
    }


	public void setPage(int n)
	{
		currentPage = n;
		if (UIPages.Count > 0) {
			abilityManager.loadUI (UIPages [currentPage]);
		}
	}


	public void fireAbility(GameObject obj , Vector3 loc, int abilNum)
		{
		
		UIPages [currentPage].fireAtTarget (obj, loc, abilNum , Input.GetKey(KeyCode.LeftShift));
		targetManager.turnOff ();

	}

	public void stoptarget (){
		targetManager.turnOff ();
	}

	public void toggleRangeIndicator(bool onOff)
	{
		if (onOff) {
			foreach (List<RTSObject> obj in UIPages[currentPage].rows) {
				if (obj != null && obj.Count > 0) {
					if (obj[0].getUnitManager().myWeapon.Count >0) {

						float maxRange = 0;
						foreach (IWeapon weap in obj[0].getUnitManager().myWeapon) {
							if (weap.range > maxRange) {
								maxRange = weap.range;
							}
	
						}

						targetManager.loadUnits (obj, maxRange);
						break;
					}
				}
			}
		} else {
			targetManager.turnOff ();
		}

	}




    public void callAbility(int n)
	{if (UIPages.Count > 0) {
			
			
			if (UIPages [currentPage].isTargetAbility (n)) {
				//Debug.Log ("In here 1 ");
				targetManager.loadUnits (UIPages [currentPage].getUnitsFromAbilities (n),
					((TargetAbility)UIPages [currentPage].getAbility (n)).range);
				uiManage.SwitchMode (Mode.targetAbility);

				uiManage.setAbility (UIPages [currentPage].getAbility (n), n,UIPages [currentPage].getUnitsFromAbilities(n)[0].getUnitManager().UnitName);
			
			} else if (UIPages [currentPage].isBuildingAbility (n)) {
				//Debug.Log ("In here 2 ");
				if (UIPages [currentPage].canCast(n)) {
					uiManage.UserPlacingBuilding (((UnitProduction)UIPages [currentPage].getAbility (n)).unitToBuild, n);
				}

			}
			else {
				//Debug.Log ("In here 3 ");
				UIPages [currentPage].useAbility (n, Input.GetKey(KeyCode.LeftShift));
			}
		}
    }

	public bool checkValidTarget(Vector3 location, GameObject obj, int n) 
	{
		if (UIPages.Count > 0) {
			return UIPages [currentPage].validTarget ( obj,location, n);
			}

		return false;
	}


    public void setAutoCast(int n)
    {
        UIPages[currentPage].setAutoCast(n);
    }


    public void Awake()
	{		main = this;

        for (int i = 0; i < 10; i++)
        {
            Group.Add(new List<RTSObject>());
        }

        main = this;

    }

    /**
    * Utility function called by a bunch of other methods. 
    * Adds a unit to the SelectedObjects list if it's not already in the list.
    *
    * If it's orderable (can receive orders) it's added to the SelectedActiveObject list as well.
    * set's the selected property of the object
    * calls the sortUnit method (doesn't really sort. Not sure what that method does precisely, but it's involved in handling the displaying of abilities
    **/

    public void AddObject(RTSObject obj)
    {
        if (!SelectedObjects.Contains(obj))
        {       
            SelectedObjects.Add(obj);

            obj.SetSelected();
            
            sortUnit(obj);
        }

    }

	//removes the unit from the selection if is already in or adds it if not in.
	public void AddRemoveObject(RTSObject obj)
	{
		if (SelectedObjects.Contains (obj)) {
			DeselectObject (obj);
		} else {
			AddObject (obj);
		}

	}

	//Select all of a given unit type that is currently selected
	public void selectAllUnitType(RTSObject obj, string name = null)
	{
		List<RTSObject> tempList = new List<RTSObject> ();
		for (int i = tempAbilityGroups.Count - 1; i > -1; i--) {

			if ((obj && obj.getUnitManager().UnitName == (tempAbilityGroups [i]) [0].getUnitManager().UnitName) || (tempAbilityGroups [i]) [0].getUnitManager().UnitName == name) {
				foreach (RTSObject rts in (tempAbilityGroups [i])) {
					tempList.Add (rts);
				}
			}
		}


		foreach (RTSObject o in SelectedObjects)
		{
			o.SetDeselected();
		}

		SelectedObjects.Clear();

		UIPages.Clear();
		tempAbilityGroups.Clear();

		foreach (RTSObject r in tempList) {
			AddObject (r);	
		}
		CreateUIPages (0);
	}

	public void DeSelectAllUnitType(RTSObject obj)
	{
		List<RTSObject> tempList = new List<RTSObject> ();
		for (int i = tempAbilityGroups.Count - 1; i > -1; i--) {
			//if (obj.gameObject.GetComponent<UnitManager> ().UnitName != (tempAbilityGroups [i]) [0].gameObject.GetComponent<UnitManager> ().UnitName) {
			if (obj.getUnitManager().UnitName != (tempAbilityGroups [i]) [0].getUnitManager().UnitName) {
				foreach (RTSObject rts in (tempAbilityGroups [i])) {
					tempList.Add (rts);
				}
			}
		}


		foreach (RTSObject o in SelectedObjects)
		{
			o.SetDeselected();
		}

		SelectedObjects.Clear();

		UIPages.Clear();
		tempAbilityGroups.Clear();

		foreach (RTSObject r in tempList) {
			AddObject (r);	
		}
		CreateUIPages (0);
	}


    public void sortUnit(RTSObject obj)
    {
        foreach (List<RTSObject> lis in tempAbilityGroups)
        {
			if (obj.getUnitManager().UnitName == (lis[0]).getUnitManager().UnitName)
            {
                lis.Add(obj);
                return;
            }
        }
        List<RTSObject> unitList = new List<RTSObject>();
        unitList.Add(obj);
        tempAbilityGroups.Add(unitList);

    }


	public void updateControlGroups (RTSObject obj)
	{//Debug.Log ("Updating control group");
		for (int i = 0; i < 10; i++) {
			if (Group [i].Contains (obj)) {
				Group [i].RemoveAll (item => item == null);
				Group [i].Remove (obj);
				if (Group [i].Count > 0) {
					controlUI.activateTab (i, Group [i].Count, Group [i] [0].getUnitStats().Icon);
				} else {
					controlUI.deactivate (i);
				}
			}
		
		
		}
	}

	public void updateUI()
	{
		CreateUIPages (currentPage);

	}

	public void updateUIActivity()
	{
		if (UIPages.Count > 0) {
			abilityManager.upDateActive(UIPages [currentPage]);
		}
	}

	public void reImageUI()
	{ if (UIPages.Count > 0) {
			abilityManager.updateUI (UIPages [currentPage]);
		}
		
	}

	public void RedoSingle()
	{ if (UIPages.Count > 0) {
			abilityManager.updateSingleCard ();
		}

	}

	public void AutoCastUI()
		{  abilityManager.upDateAutoCast(UIPages[currentPage]);

		}
	

	public void CreateUIPages(int j)
	{
        currentPage = j;
        UIPages.Clear();
        UIPages.Add(new Page());
	
	

		for (int i = tempAbilityGroups.Count - 1; i > -1; i--) {
			tempAbilityGroups[i].RemoveAll (item => item == null);

			if (tempAbilityGroups[i].Count == 0) {
				
				tempAbilityGroups.Remove (tempAbilityGroups[i]);
			}
		}

        List<RTSObject> usedUnits = new List<RTSObject>();

        List<RTSObject> bestPick = null;

        while (usedUnits.Count < tempAbilityGroups.Count)
        {
            int minPriority = 100;

            foreach (List<RTSObject> objList in tempAbilityGroups)
			{//objList.RemoveAll (item => item == null);
				   
				if (objList[0].AbilityPriority <= minPriority && !usedUnits.Contains(objList[0]))
                {

					bestPick = objList;
					minPriority = objList[0].AbilityPriority;
                }
            }
            usedUnits.Add(bestPick[0]);

            int n = 0;
            while (!UIPages[n].canBeAdded(bestPick))
            {

                n++;
                if (n > 6)
                {
                    break;
                }
                if (UIPages.Count <= n)
                {
                    UIPages.Add(new Page());
                }
            }
		
            UIPages[n].addUnit(bestPick);
		

        }
		if (abilityManager) {
			abilityManager.loadUI (UIPages [currentPage]);
			pageUI.setPageCount (UIPages);
		}
    }

	public void applyGlobalSelection(List<List<string>> input)
	{globalSelection = input;
	
	
	}


    public void DeselectAll()
	{
		uiManage.SwitchToModeNormal ();

        if (SelectedObjects.Count == 0)
            return;
        foreach (RTSObject obj in SelectedObjects)
        {
            obj.SetDeselected();
        }

        SelectedObjects.Clear();

        UIPages.Clear();
        tempAbilityGroups.Clear();
        CreateUIPages(0);
    }

    /**
    * Utility function called from many places.
    * IF the obj is selected:
    *   Removes the object from all applicable lists and calls the deselect method on the object
    *   Calls some methods to refresh the GUI
    * IF the obj is not selected:
    *   Does nothing
    **/
    public void DeselectObject(RTSObject obj)
    {
		if (!obj)
			return;

        //don't bother deselecting it if it's not selected in the first place
        if (!SelectedObjects.Contains(obj))
            return;

        obj.SetDeselected();
		UnitManager manage = obj.getUnitManager();
		for (int i = tempAbilityGroups.Count - 1; i > -1; i--) {


			if (manage.UnitName == (tempAbilityGroups [i])[0].getUnitManager().UnitName) {
				tempAbilityGroups [i].Remove (obj);

				if (tempAbilityGroups [i].Count == 0) {

					tempAbilityGroups.Remove (tempAbilityGroups [i]);
					uiManage.checkForDeadUnit (manage.UnitName);

				}

			}
		}

        SelectedObjects.Remove(obj);
		if (SelectedObjects.Count == 0) {

			UIPages.Clear ();
			abilityManager.clearPage ();
			return;
		}

        else if (abilityManager != null)
        {
            UIPages.Clear();

            //tempAbilityGroups.Clear();
            CreateUIPages(0);

        }
    }

	public void voiceResponse (bool attacker)
	{

		if (!attacker) {
			AudioSrc.PlayOneShot (moveSound,.1f);
		} else {
			AudioSrc.PlayOneShot (attackSound,.1f);
		}

	if (Time.time > lastVoiceTime + 5) {
			
		soundIndex++;
		if (soundIndex > 12) {
			soundIndex = 0;
		}
		if(soundIndex != 1)
			{return;}

			UnitManager listTop = SelectedObjects [0].getUnitManager();
			lastVoiceTime = Time.time;
	
			int n =-1;


			if (attacker) {
				if (listTop.myVoices.attacking.Count > 0) {
					do {
						n = Random.Range (0, listTop.myVoices.attacking.Count);
					} while(n == lsatVoiceIndex);

					ExpositionDisplayer.instance.displayText ("", 2, listTop.myVoices.attacking [n], .5f, null, 0);
				

				}
			} else {
				if (listTop.myVoices.moving.Count > 0) {
					do {
						n = Random.Range (0, listTop.myVoices.moving.Count -1 );
					} while(n == lsatVoiceIndex);

					ExpositionDisplayer.instance.displayText ("", 2, listTop.myVoices.moving [n], .5f, null, 0);
				
				}
			}
			lsatVoiceIndex = n;
		}
	}

	public void GiveOrder(Order order)
	{//fix this once we get to multiplayer games

		if(SelectedObjects.Count <= 0 || SelectedObjects[0].getUnitManager().PlayerOwner != 1)
			{return;}
			

		Vector3 location = order.OrderLocation;
		location.y = location.y + 30;

	
		if (order.Target && order.Target.transform.parent && order.Target.transform.parent.GetComponentInParent<UnitManager> ()) {
			order.Target = order.Target.transform.parent.gameObject;
		}

		if (order.OrderType == 1 ) {
			// MOVE COMMAND
			if (FogOfWar.current.IsInCompleteFog (location)) {
				Instantiate (fogIndicator);
			
			} else {
				Instantiate (movementInd, location, Quaternion.Euler (90, 0, 0));
			}


			voiceResponse (false);

			assignMoveCOmmand (order.OrderLocation, Vector3.zero,false ,1);

		} else if (order.OrderType == 4 ) {
			// MOVE COMMAND
			if (FogOfWar.current.IsInCompleteFog (location)) {
				Instantiate (fogAttacker);
			
			} else {
				Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
			}
			voiceResponse (true);
			assignMoveCOmmand (order.OrderLocation, Vector3.zero, true, 1);

		} else if (order.OrderType == 6 ) {
			// INTERACT
			Selected sel = order.Target.GetComponent<Selected>();
			UnitManager manage = order.Target.GetComponent<UnitManager>();
			UnitManager managePar = order.Target.GetComponentInParent<UnitManager> ();

			if (sel) {
				sel.interact ();
			}
			if ((manage && manage.PlayerOwner != 1)
				|| (managePar && managePar.PlayerOwner != 1)) {
				AudioSrc.PlayOneShot (attackSound, .1f);

				if (FogOfWar.current.IsInCompleteFog (location)) {
					Instantiate (fogAttacker);

				} else {
					Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
				}
				foreach (IOrderable obj in SelectedObjects) {
					obj.GiveOrder (Orders.CreateInteractCommand (order.Target,Input.GetKey(KeyCode.LeftShift)));
				}
			} else {
				foreach (IOrderable obj in SelectedObjects) {
					obj.GiveOrder (Orders.CreateFollowCommand (order.Target,Input.GetKey(KeyCode.LeftShift)));
				}
				voiceResponse (false);
				if (FogOfWar.current.IsInCompleteFog (location)) {
					Instantiate (fogIndicator);
				
				} else {
					Instantiate (movementInd, location, Quaternion.Euler (90, 0, 0));
				}
			}
		
		} else if (order.OrderType == 0 || order.OrderType == 7) {
			foreach (IOrderable obj in SelectedObjects) {
				obj.GiveOrder (order);
			}
		}
		else if (order.OrderType == 8) {
			if (FogOfWar.current.IsInCompleteFog (location)) {
				Instantiate (fogIndicator);

			} else {
				Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
			}
			voiceResponse (true);
			foreach (IOrderable obj in SelectedObjects) {
				obj.GiveOrder (order);
			}
		}
	

    }

	public void GiveMoveSpread(Vector3 a,  Vector3 b)
	{//fix this once we get to multiplayer games

		if(SelectedObjects.Count <= 0 || SelectedObjects[0].getUnitManager().PlayerOwner != 1)
		{return;}
			
		if (FogOfWar.current.IsInCompleteFog ( Vector3.Lerp(a,b,.5f))) {
				Instantiate (fogIndicator);

			} else {
			Instantiate (movementInd, Vector3.Lerp(a,b,.5f), Quaternion.Euler (90, 0, 0));
			}

			voiceResponse (false);

		assignMoveCOmmand (a,b, false, 1 + Vector3.Distance(a,b)/50);

		} 




	// Used  for Circular Formation movement, Mostly Broken
	public void assignMoveCOmmand(Vector3 targetPoint,Vector3 secondPoint, bool attack, float sepDistance)
	{


		//List<RTSObject> realMovers = new List<RTSObject> ();
		Dictionary<int, List<RTSObject>> trueMovers = new Dictionary<int,List< RTSObject>> ();
		List<RTSObject> others = new List<RTSObject> ();

		int MoverCount = 0;
		Vector3 middlePoint = Vector3.zero;
		foreach (RTSObject obj in SelectedObjects) {
			if (!obj.getUnitManager ().cMover) {//.cMover.getUnitStats ().isUnitType (UnitTypes.UnitTypeTag.Turret) || obj.getUnitStats ().isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				others.Add (obj);
			} else {
				MoverCount++;
			//	realMovers.Add (obj);
				if (trueMovers.ContainsKey (obj.getUnitManager ().formationOrder)) {
					trueMovers [obj.getUnitManager ().formationOrder].Add (obj);
				} else {
					trueMovers.Add (obj.getUnitManager ().formationOrder, new List<RTSObject> (){ obj });
				}
				middlePoint += obj.transform.position;
			}
		}
			

		//give move command to all nonmovers, setting rally points and such
		StartCoroutine (staticMove (others, targetPoint));

		middlePoint /= MoverCount;

		float angle;
		if (sepDistance == 1) {
			angle = Vector2.Angle (Vector2.up, new Vector2 (middlePoint.x - targetPoint.x, middlePoint.z - targetPoint.z));
		} else {
			//Used when there is a right click drag spread formation
			angle = Vector2.Angle (Vector2.up, new Vector2 (secondPoint.x - targetPoint.x, secondPoint.z - targetPoint.z)) + 90;
			targetPoint = Vector3.Lerp (targetPoint, secondPoint, .5f);
		}
		if (middlePoint.x < targetPoint.x) {
			angle *= -1;
		} 

		List<Vector3> points = Formations.getFormation (MoverCount, Mathf.Min (2, sepDistance));
		for (int t = 0; t < points.Count; t++) {
	
			points [t] = Quaternion.Euler (0, angle, 0) * points [t] + targetPoint;
		}


		int minPointIndex = 0;
		int maxPointIndex = 0;
		foreach (KeyValuePair<int, List<RTSObject>> kvp in trueMovers.OrderBy(i => i.Key)) {

			maxPointIndex = minPointIndex + kvp.Value.Count;
			List<IOrderable> usedGuys = new List<IOrderable> ();
			while (usedGuys.Count < kvp.Value.Count) {
				float maxDistance = 0;
				IOrderable closestUnit = null;
				foreach (IOrderable obj in kvp.Value) {
					float tempDist = Vector3.Distance (obj.getObject ().transform.position, targetPoint);
					if (!usedGuys.Contains (obj) && tempDist > maxDistance) {
						maxDistance = tempDist;
						closestUnit = obj;
					}
				}
				usedGuys.Add (closestUnit);

				float runDistance = 1000000;
				Vector3 closestSpot = points [0];

				for (int i = minPointIndex; i < maxPointIndex; i++) {
					
					float tempDist = Vector3.Distance (closestUnit.getObject ().transform.position, points [i]);
					if (tempDist < runDistance) {
						closestSpot = points [i];
						runDistance = tempDist;


					}
				}

				StartCoroutine (giveCommand (attack, closestSpot, closestUnit));
				maxPointIndex--;
				points.Remove (closestSpot);


			}
				
			minPointIndex = maxPointIndex;

		}
	}
		// Old formation code based off of who is closest to what
		/*
		List<IOrderable> usedGuys = new List<IOrderable> ();
		while (usedGuys.Count < realMovers.Count) {
			float maxDistance = 0;
			IOrderable closestUnit = null;
			foreach (IOrderable obj in realMovers) {
				float tempDist = Vector3.Distance (obj.getObject ().transform.position, targetPoint);
				if (!usedGuys.Contains (obj) && tempDist > maxDistance) {
					maxDistance = tempDist;
					closestUnit = obj;
				}
			}
			usedGuys.Add (closestUnit);

			float runDistance = 1000000;
			Vector3 closestSpot = points[0];
			for (int i = 0; i < points.Count; i++) {
				float tempDist = Vector3.Distance (closestUnit.getObject ().transform.position, points [i]);
				if (tempDist < runDistance) {
						closestSpot = points [i];
					runDistance = tempDist;

			
				}
			}
		
			StartCoroutine (giveCommand (attack, closestSpot, closestUnit));

			points.Remove (closestSpot);
	

		}
		*/	

	IEnumerator giveCommand(bool attack, Vector3 closestSpot, IOrderable unittoMove)
	{yield return new WaitForSeconds (0.001f);
		if (attack) {
			Order o = Orders.CreateAttackMove (closestSpot, Input.GetKey(KeyCode.LeftShift));

			unittoMove.GiveOrder (o);
		} else {
			Order o = Orders.CreateMoveOrder (closestSpot, Input.GetKey(KeyCode.LeftShift));
			unittoMove.GiveOrder (o);

		}
	
	}

	//Used to multithread and increase response time
	IEnumerator staticMove(List<RTSObject> others , Vector3 targetPoint)
	{yield return new WaitForSeconds (0.005f);
		foreach ( IOrderable io  in others) {
			io.GiveOrder (Orders.CreateMoveOrder (targetPoint,Input.GetKey(KeyCode.LeftShift)));
		}

	}




	public void AddUnitsToGroup(int groupNumber, bool clear)
	{if (clear) {
			
			Group [groupNumber].Clear ();
		}
        foreach (RTSObject obj in SelectedObjects)
        {
			if (!Group [groupNumber].Contains (obj)) {
				Group [groupNumber].Add (obj);
				//ErrorPrompt.instance.showError ("Adding " + obj);
			}

        }
        CreateUIPages(0);
		controlUI.activateTab (groupNumber, Group [groupNumber].Count, Group [groupNumber] [0].getUnitStats().Icon);

    }


    public void SelectGroup(int groupNumber)
	{uiManage.SwitchToModeNormal ();
        DeselectAll();
        foreach (RTSObject obj in Group[groupNumber])
        {
            AddObject(obj);
        }
		CreateUIPages (0);

    }

    public int ActiveObjectsCount()
    {
        return SelectedObjects.Count;
    }

	public RTSObject FirstActiveObject()
    {
        return SelectedObjects[0];
    }

	public List<RTSObject> ActiveObjectList()
    {
        return SelectedObjects;
    }

    public bool IsObjectSelected(GameObject obj)
    {
        return SelectedObjects.Contains(obj.GetComponent<RTSObject>());
    }

    public void selectAllArmy()
	{ uiManage.SwitchToModeNormal ();
		DeselectAll ();

		foreach (KeyValuePair<string, List<UnitManager>> pair in raceMan.getUnitList()) {
			foreach (UnitManager obj in pair.Value) {
        //foreach (GameObject obj in raceMan.getUnitList())
       // {

				if (!obj.myStats.isUnitType(UnitTypes.UnitTypeTag.Structure)
					&& !obj.myStats.isUnitType(UnitTypes.UnitTypeTag.Worker)
					&& !obj.myStats.isUnitType(UnitTypes.UnitTypeTag.Turret))
           			 {
               		 AddObject(obj);
            		}
				}
			}
		CreateUIPages(0);
    }

	public void removeFromControlGroups()
	{
		for (int i = 0; i < 10; i++) {
			Group [i].RemoveAll (item => item == null);
			foreach (RTSObject o in SelectedObjects) {
				if (Group [i].Contains (o)) {
					Group [i].Remove (o);
				}
			}
			if (Group [i].Count > 0) {
				controlUI.activateTab (i, Group [i].Count, Group [i] [0].getUnitStats().Icon);
				} else {
					controlUI.deactivate (i);
				}

		}
	}

    public void selectAllUnbound()
    {
        selectAllArmy();

        foreach (List<RTSObject> obj in Group)
        {
            foreach (RTSObject rts in obj)
            {
                DeselectObject(rts);
            }
        }
		foreach (RTSObject u in SelectedObjects) {
			if (u.getUnitStats().isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				DeselectObject (u);
			}
		}

		CreateUIPages(0);
    }

	public void selectAllUnArmedTanks()
	{uiManage.SwitchToModeNormal ();
		DeselectAll();

		foreach (KeyValuePair<string, List<UnitManager>> pair in raceMan.getUnitList()) {
			foreach (UnitManager obj in pair.Value) {

				TurretMount tm = obj.GetComponentInChildren<TurretMount> ();
				if (tm && !tm.turret) {
					AddObject (obj);

				}
			}
		}
		
		CreateUIPages(0);
	}

	public float getUnarmedTankCount()
	{float i = 0;
		foreach (KeyValuePair<string, List<UnitManager>> pair in raceMan.getUnitList()) {
			foreach (UnitManager obj in pair.Value) {
				TurretMount tm = obj.GetComponentInChildren<TurretMount> ();
				if (tm && !tm.turret) {
					i++;
				}
			}
		}
		return i;
	}


    public void selectIdleWorker()
	{uiManage.SwitchToModeNormal ();
		List<UnitManager> idleWorkers = new List<UnitManager> ();
        

		foreach (KeyValuePair<string, List<UnitManager>> pair in raceMan.getUnitList()) {
			foreach (UnitManager obj in pair.Value) {

				if (!obj.myStats.isUnitType (UnitTypes.UnitTypeTag.Worker)) {
				continue;
			}

			if (!obj.isIdle ()) {
				continue;
			} else {
				idleWorkers.Add (obj);
			}
		}
			}
		if (idleWorkers.Count == 0) {
			return;}

		//iterate through each idle worker
			currentCenterOn++;
			if (currentCenterOn >= idleWorkers.Count) {
				currentCenterOn = 0;
			}


			Vector3 location = idleWorkers [currentCenterOn].gameObject.transform.position;
			location.z -= 70;

			MainCamera.main.Move (location);
			DeselectAll();
			AddObject(idleWorkers [currentCenterOn]);



		CreateUIPages(0);
    }


	public void globalSelect(int n )
	{ DeselectAll();
		uiManage.SwitchToModeNormal ();
		foreach (KeyValuePair<string, List<UnitManager>> pair in raceMan.getUnitList()) {

			if (globalSelection [n].Contains (pair.Key)) {
			
				foreach (UnitManager manager in pair.Value) {

					if (manager.myStats.isUnitType (UnitTypes.UnitTypeTag.Turret)) {

						AddObject (manager.transform.root.GetComponent<UnitManager> ());
						AddObject (manager);
					} else {
						AddObject (manager);
					}
				}

			
			}

		
		}
		CreateUIPages(0);
	}




	public void attackMoveO(Vector3 input)
    {
        //We're over the main screen, let's raycast
		if (input == Vector3.zero) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {
				Vector3 attackMovePoint = hit.point;
				GiveOrder (Orders.CreateAttackMove (attackMovePoint,Input.GetKey(KeyCode.LeftShift)));
			}
		} else {
			GiveOrder (Orders.CreateAttackMove (input,Input.GetKey(KeyCode.LeftShift)));
		}

    }

	public void PatrolMoveO()
	{
		//We're over the main screen, let's raycast
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 16)))
		{
			Vector3 attackMovePoint = hit.point;
			GiveOrder(Orders.CreatePatrol(attackMovePoint,Input.GetKey(KeyCode.LeftShift)));
		}

	}

	public void selectAllBuildings ()
	{DeselectAll();
		uiManage.SwitchToModeNormal ();
		foreach (KeyValuePair<string, List<UnitManager>> pair in raceMan.getUnitList()) {
			foreach (UnitManager manager in pair.Value) {
				
				if (manager.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				AddObject (manager);
				}
			}
		}
		CreateUIPages(0);
	}

    public void stopO()
    {
		GiveOrder(Orders.CreateStopOrder(Input.GetKey(KeyCode.LeftShift)));
    }

	public void cancel()
	{
		foreach (RTSObject obj in SelectedObjects) {
			
				obj.SendMessage ("cancel",SendMessageOptions.DontRequireReceiver);


		}

	}



}
