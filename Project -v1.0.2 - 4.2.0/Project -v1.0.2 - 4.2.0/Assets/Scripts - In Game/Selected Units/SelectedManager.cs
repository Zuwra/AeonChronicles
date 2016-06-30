
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedManager : MonoBehaviour, ISelectedManager
{


    private List<RTSObject> SelectedObjects = new List<RTSObject>();

    //used for UI grouping
    private List<List<RTSObject>> tempAbilityGroups = new List<List<RTSObject>>();
    private List<Page> UIPages = new List<Page>();


    private List<List<RTSObject>> Group = new List<List<RTSObject>>();

    private int currentPage = 0;

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
 
    void Start()
	{uiManage = (UIManager)FindObjectOfType (typeof(UIManager));
        abilityManager = GameObject.Find("GameHud").GetComponent<UiAbilityManager>();
        raceMan = GameObject.Find("GameRaceManager").GetComponent<GameManager>().activePlayer;
		main = this;
		controlUI = GameObject.FindObjectOfType<ControlGroupUI> ();
		pageUI = GameObject.FindObjectOfType<PageUIManager> ();
		targetManager = GameObject.FindObjectOfType<TargetCircleManager> ();
		AudioSrc = GetComponent<AudioSource> ();

    }


    void Update()
	{
        if (Input.GetKeyUp(KeyCode.Tab))
        {
			if (Input.GetKey (KeyCode.LeftShift)) {
				PatrolMoveO ();
			} else {
				attackMoveO (Vector3.zero);
			}
        }

        if (Input.GetKeyUp(KeyCode.CapsLock))
        {
			
			stopO ();
			if (Input.GetKey (KeyCode.LeftShift)) {

				GiveOrder(Orders.CreateHoldGroundOrder());
			}
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // set a control group
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (0); 
					controlUI.activateTab (0, Group [0].Count, Group [0] [0].GetComponent<UnitStats> ().Icon);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (1);
					controlUI.activateTab (1, Group [1].Count, Group [1] [0].GetComponent<UnitStats> ().Icon);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (2);
					controlUI.activateTab (2, Group [2].Count, Group [2] [0].GetComponent<UnitStats> ().Icon);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (3);
					controlUI.activateTab (3, Group [3].Count, Group [3] [0].GetComponent<UnitStats> ().Icon);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (4);
					controlUI.activateTab (4, Group [4].Count, Group [4] [0].GetComponent<UnitStats> ().Icon);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (5);
					controlUI.activateTab (5, Group [5].Count, Group [5] [0].GetComponent<UnitStats> ().Icon);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (6);
					controlUI.activateTab (6, Group [6].Count, Group [6] [0].GetComponent<UnitStats> ().Icon);			
				}
			}
			else if (Input.GetKeyDown (KeyCode.Alpha8)) {
				if (SelectedObjects.Count > 0) {
					AddUnitsToGroup (7);
					controlUI.activateTab (7, Group [7].Count, Group [7] [0].GetComponent<UnitStats> ().Icon);
				}
			}
			 else if (Input.GetKeyDown(KeyCode.Alpha9))
			{if(SelectedObjects.Count > 0){
                AddUnitsToGroup(8);
				controlUI.activateTab (8, Group[8].Count, Group[8][0].GetComponent<UnitStats>().Icon);
            }
			}
            else if (Input.GetKeyDown(KeyCode.Alpha0))
			{if(SelectedObjects.Count > 0){
                AddUnitsToGroup(9);
				controlUI.activateTab (9, Group[9].Count, Group[9][0].GetComponent<UnitStats>().Icon);
				}
			}

        }
        else {
            // Select a control group
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { SelectGroup(0); }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            { SelectGroup(1); }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            { SelectGroup(2); }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {SelectGroup(3);}
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {SelectGroup(4); }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            { SelectGroup(5);}
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            { SelectGroup(6);}
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {SelectGroup(7); }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {SelectGroup(8); }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {SelectGroup(9); }

        }

        if (Input.GetKeyUp(KeyCode.Q))
        { callAbility(0);}
        else if (Input.GetKeyUp(KeyCode.W))
        {callAbility(1);}
        else if (Input.GetKeyUp(KeyCode.E))
        {callAbility(2);}
        else if (Input.GetKeyUp(KeyCode.R))
        {callAbility(3);}
        else if (Input.GetKeyUp(KeyCode.A))
        {callAbility(4);}
        else if (Input.GetKeyUp(KeyCode.S))
        {callAbility(5); }
        else if (Input.GetKeyUp(KeyCode.D))
        {callAbility(6);}
        else if (Input.GetKeyUp(KeyCode.F))
        {callAbility(7);}
        else if (Input.GetKeyUp(KeyCode.Z))
        {callAbility(8); }
        else if (Input.GetKeyUp(KeyCode.X))
        {callAbility(9); }
        else if (Input.GetKeyUp(KeyCode.C))
        {callAbility(10);}
        else if (Input.GetKeyUp(KeyCode.V))
        {callAbility(11);}

		if (Input.GetKeyUp (KeyCode.Space)) {
			if (SelectedObjects.Count > 0) {
				if (SelectedObjects [0]) {
					Vector3 location = SelectedObjects [0].gameObject.transform.position;
					location.z -= 90;
					MainCamera.main.Move (location);
				}
			}
		}

        if (Input.GetKeyUp(KeyCode.BackQuote))
        {

            if (currentPage < UIPages.Count-1)
			{
				currentPage++;
				pageUI.selectPage (currentPage);
                abilityManager.loadUI(UIPages[currentPage]);
            }
            else {
				
                currentPage = 0;
				pageUI.selectPage (currentPage);
                abilityManager.loadUI(UIPages[currentPage]);
            }

        }
    }


	public void setPage(int n)
	{
		currentPage = n;
		abilityManager.loadUI(UIPages[currentPage]);
	}


	public void fireAbility(GameObject obj , Vector3 loc, int abilNum)
		{
		
		UIPages [currentPage].fireAtTarget (obj, loc, abilNum);
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
					if (obj[0].gameObject.GetComponent<UnitManager> ().myWeapon.Count >0) {

						float maxRange = 0;
						foreach (IWeapon weap in obj[0].gameObject.GetComponent<UnitManager> ().myWeapon) {
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
				
				targetManager.loadUnits (UIPages [currentPage].getUnitsFromAbilities (n),
					((TargetAbility)UIPages [currentPage].getAbility (n)).range);
				uiManage.SwitchMode (Mode.targetAbility);

				uiManage.setAbility (UIPages [currentPage].getAbility (n), n);
			
			} else if (UIPages [currentPage].isBuildingAbility (n)) {
				
				uiManage.UserPlacingBuilding (((UnitProduction)UIPages [currentPage].getAbility (n)).unitToBuild, n);

			}
			else {
				UIPages [currentPage].useAbility (n);
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
    {

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
	public void selectAllUnitType(RTSObject obj)
	{
		List<RTSObject> tempList = new List<RTSObject> ();
		for (int i = tempAbilityGroups.Count - 1; i > -1; i--) {

			if (obj.gameObject.GetComponent<UnitManager> ().UnitName == (tempAbilityGroups [i]) [0].gameObject.GetComponent<UnitManager> ().UnitName) {
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

			if (obj.gameObject.GetComponent<UnitManager> ().UnitName != (tempAbilityGroups [i]) [0].gameObject.GetComponent<UnitManager> ().UnitName) {
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
            if (obj.gameObject.GetComponent<UnitManager>().UnitName == (lis[0]).gameObject.GetComponent<UnitManager>().UnitName)
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
	{Debug.Log ("Updating control group");
		for (int i = 0; i < 10; i++) {
			if (Group [i].Contains (obj)) {
				Group [i].Remove (obj);
				if (Group [i].Count > 0) {
					controlUI.activateTab (i, Group [i].Count, Group [i] [0].GetComponent<UnitStats> ().Icon);
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
            int min = 100;

            foreach (List<RTSObject> obj in tempAbilityGroups)
			{obj.RemoveAll (item => item == null);
				   
				if (obj[0].AbilityPriority <= min && !usedUnits.Contains(obj[0]))
                {

                    bestPick = obj;
                    min = obj[0].AbilityPriority;
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

        abilityManager.loadUI(UIPages[currentPage]);
		pageUI.setPageCount (UIPages.Count);
    }

	public void applyGlobalSelection(List<List<string>> input)
	{globalSelection = input;
	
	
	}


    public void DeselectAll()
	{//Debug.Log ("Deselcting all");
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

        //don't bother deselecting it if it's not selected in the first place
        if (!SelectedObjects.Contains(obj))
            return;

        if (obj)
        {
            obj.SetDeselected();
        }

		for (int i = tempAbilityGroups.Count - 1; i > -1; i--) {

			if (obj.gameObject.GetComponent<UnitManager> ().UnitName == (tempAbilityGroups [i])[0].gameObject.GetComponent<UnitManager> ().UnitName) {
				tempAbilityGroups [i].Remove (obj);

				if (tempAbilityGroups [i].Count == 0) {

					tempAbilityGroups.Remove (tempAbilityGroups [i]);

				}

			}
		}

        SelectedObjects.Remove(obj);

        if (abilityManager != null)
        {
            UIPages.Clear();

            //tempAbilityGroups.Clear();
            CreateUIPages(0);
        }
    }



    public void GiveOrder(Order order)
	{//fix this once we get to multiplayer games
		//Debug.Log("Ordering " + order.Target + "  " + order.OrderType);
		if(SelectedObjects.Count == 0 || SelectedObjects[0].gameObject.GetComponent<UnitManager>().PlayerOwner != 1)
			{return;}
			

		Vector3 location = order.OrderLocation;
		location.y = location.y + 30;
		if (order.OrderType == 1 && SelectedObjects.Count > 0) {
			if (FogOfWar.current.IsInCompleteFog (location)) {
				Instantiate (fogIndicator);
			
			} else {
				Instantiate (movementInd, location, Quaternion.Euler (90, 0, 0));
			}
			AudioSrc.PlayOneShot (moveSound);
			assignMoveCOmmand (order.OrderLocation, false);

		} else if (order.OrderType == 4 && SelectedObjects.Count > 0) {
			if (FogOfWar.current.IsInCompleteFog (location)) {
				Instantiate (fogAttacker);
			
			} else {
				Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
			}
			AudioSrc.PlayOneShot (attackSound);
			assignMoveCOmmand (order.OrderLocation, true);

		} else if (order.OrderType == 6 && SelectedObjects.Count > 0) {
			

			if ((order.Target.GetComponent<UnitManager> () && order.Target.GetComponent<UnitManager> ().PlayerOwner != 1)
			    || (order.Target.GetComponentInParent<UnitManager> () && order.Target.GetComponentInParent<UnitManager> ().PlayerOwner != 1)) {
				AudioSrc.PlayOneShot (attackSound);

				if (FogOfWar.current.IsInCompleteFog (location)) {
					Instantiate (fogAttacker);

				} else {
					Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
				}
				foreach (IOrderable obj in SelectedObjects) {
					obj.GiveOrder (Orders.CreateInteractCommand (order.Target));
				}
			} else {
				foreach (IOrderable obj in SelectedObjects) {
					obj.GiveOrder (Orders.CreateFollowCommand (order.Target));
				}
				AudioSrc.PlayOneShot (moveSound);
				if (FogOfWar.current.IsInCompleteFog (location)) {
					Instantiate (fogIndicator);
				
				} else {
					Instantiate (movementInd, location, Quaternion.Euler (90, 0, 0));
				}
			}
		
		} else if ((order.OrderType == 0 || order.OrderType == 7) && SelectedObjects.Count > 0) {
			foreach (IOrderable obj in SelectedObjects) {
				obj.GiveOrder (order);
			}
		}
		else if ((order.OrderType == 8) && SelectedObjects.Count > 0) {
			if (FogOfWar.current.IsInCompleteFog (location)) {
				Instantiate (fogIndicator);

			} else {
				Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
			}
			AudioSrc.PlayOneShot (attackSound);
			foreach (IOrderable obj in SelectedObjects) {
				obj.GiveOrder (order);
			}
		}
	

    }

	// Used  for Circular Formation movement, Mostly Broken
	public void assignMoveCOmmand(Vector3 targetPoint, bool attack)
	{
		List<Vector3> points = new List<Vector3> ();
		for (int i = 0; i < SelectedObjects.Count; i++) {

			float deg = 2 * Mathf.PI * i / SelectedObjects.Count;
			Vector3 p = targetPoint + new Vector3 (Mathf.Cos (deg), 0, Mathf.Sin (deg))  *( SelectedObjects.Count -1) *4;
			points.Add (p);
		}
		List<IOrderable> usedGuys = new List<IOrderable> ();
		while (usedGuys.Count < SelectedObjects.Count) {
			float maxDistance = 0;
			IOrderable closestUnit = null;
			foreach (IOrderable obj in SelectedObjects) {
				if (!usedGuys.Contains (obj) && Vector3.Distance (obj.getObject ().transform.position, targetPoint) > maxDistance) {
					maxDistance = Vector3.Distance (obj.getObject ().transform.position, targetPoint);
					closestUnit = obj;
				}
			}
			usedGuys.Add (closestUnit);

			float runDistance = 1000000;
			Vector3 closestSpot = points[0];
			for (int i = 0; i < points.Count; i++) {
				
					if (Vector3.Distance (closestUnit.getObject ().transform.position, points [i]) < runDistance) {
						closestSpot = points [i];
						runDistance = Vector3.Distance (closestUnit.getObject ().transform.position, points [i]);

			
				}
			}

			if (attack) {
				Order o = Orders.CreateAttackMove (closestSpot);

				closestUnit.GiveOrder (o);
			} else {
				Order o = Orders.CreateMoveOrder (closestSpot);
				closestUnit.GiveOrder (o);

			}
	
			points.Remove (closestSpot);
	

		}


	}





    public void AddUnitsToGroup(int groupNumber)
    {
        Group[groupNumber].Clear();
        foreach (RTSObject obj in SelectedObjects)
        {
            Group[groupNumber].Add(obj);

        }
        CreateUIPages(0);
    }


    public void SelectGroup(int groupNumber)
    {
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
	{ raceMan.getUnitList().RemoveAll (item => item == null);
        foreach (GameObject obj in raceMan.getUnitList())
        {

            if (!obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Structure)
                && !obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Worker))
            {
                AddObject(obj.GetComponent<UnitManager>());
            }
        }
		CreateUIPages(0);
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
		CreateUIPages(0);
    }

    public void selectIdleWorker()
    {
        {
            foreach (GameObject obj in raceMan.getUnitList())
            {

                if (!obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Worker))
                {
                    continue;
                }
                UnitManager manager = obj.GetComponent<UnitManager>();
                if (!manager.isIdle())
                {
                    continue;
                }

                if (!SelectedObjects.Contains(manager))
                {
                    DeselectAll();
                    AddObject(manager);
                    break;
                }
            }

        }
		CreateUIPages(0);
    }


	public void globalSelect(int n )
	{ DeselectAll();
		foreach (GameObject obj in raceMan.getUnitList())
		{

			UnitManager manager = obj.GetComponent<UnitManager>();
			if (globalSelection[n].Contains(manager.UnitName))
			{
				AddObject(manager);
			}
		}
		CreateUIPages(0);}




	public void attackMoveO(Vector3 input)
    {
        //We're over the main screen, let's raycast
		if (input == Vector3.zero) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {
				Vector3 attackMovePoint = hit.point;
				GiveOrder (Orders.CreateAttackMove (attackMovePoint));
			}
		} else {
			GiveOrder (Orders.CreateAttackMove (input));
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
			GiveOrder(Orders.CreatePatrol(attackMovePoint));
		}

	}

	public void selectAllBuildings ()
	{DeselectAll();
		foreach (GameObject obj in raceMan.getUnitList())
		{

			if (obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.Structure))
			{
				AddObject(obj.GetComponent<UnitManager>());
			}
		}
		CreateUIPages(0);
	}

    public void stopO()
    {
        GiveOrder(Orders.CreateStopOrder());
    }



}
