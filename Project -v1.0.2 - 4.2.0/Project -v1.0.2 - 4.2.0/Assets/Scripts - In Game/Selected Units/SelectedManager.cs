
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedManager : MonoBehaviour, ISelectedManager
{

    private List<IOrderable> SelectedActiveObjects = new List<IOrderable>();
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

	public List<List<string>> globalSelection = new List<List<string>> ();


    public GameObject movementInd;
    public GameObject attackInd;

    void Start()
	{uiManage = (UIManager)FindObjectOfType (typeof(UIManager));
        abilityManager = GameObject.Find("GameHud").GetComponent<UiAbilityManager>();
        raceMan = GameObject.Find("GameRaceManager").GetComponent<GameManager>().activePlayer;
		Debug.Log ("Current page " + UIPages.Count);
    }

    public int OverlayWidth
    {
        get;
        private set;
    }


    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            attackMoveO();
        }

        if (Input.GetKeyUp(KeyCode.CapsLock))
        {
            stopO();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // set a control group
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { AddUnitsToGroup(0); }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddUnitsToGroup(1);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddUnitsToGroup(2);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                AddUnitsToGroup(3);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                AddUnitsToGroup(4);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                AddUnitsToGroup(5);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                AddUnitsToGroup(6);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                AddUnitsToGroup(7);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                AddUnitsToGroup(8);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                AddUnitsToGroup(9);

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
            {
                SelectGroup(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SelectGroup(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SelectGroup(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SelectGroup(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SelectGroup(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SelectGroup(8);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SelectGroup(9);
            }

        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            callAbility(0);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            callAbility(1);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            callAbility(2);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            callAbility(3);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            callAbility(4);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            callAbility(5);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            callAbility(6);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            callAbility(7);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            callAbility(8);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            callAbility(9);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            callAbility(10);
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            callAbility(11);
        }

        if (Input.GetKeyUp(KeyCode.BackQuote))
        {

            if (currentPage < UIPages.Count-1)
			{
				currentPage++;

                abilityManager.loadUI(UIPages[currentPage]);
            }
            else {
				
                currentPage = 0;
                abilityManager.loadUI(UIPages[currentPage]);
            }

        }



    }


	public void fireAbility(GameObject obj , Vector3 loc, int abilNum)
		{
		
		UIPages [currentPage].fireAtTarget (obj, loc, abilNum);

	}

    public void callAbility(int n)
	{if (UIPages.Count > 0) {
			
			if (UIPages [currentPage].isTargetAbility (n)) {
				uiManage.SwitchMode (Mode.targetAbility);

				uiManage.setAbility (	UIPages [currentPage].getAbility(n), n);
			} 
			else {
				UIPages [currentPage].useAbility (n);
			}
		}
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

        OverlayWidth = 80;
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
            if (obj is IOrderable)
            {
                SelectedActiveObjects.Add((IOrderable)obj);
            }

            SelectedObjects.Add(obj);

            obj.SetSelected();
            if (abilityManager != null)
            {
                //abilityManager.addUnit(SelectedObjects);
            }
            sortUnit(obj);
        }

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

	public void updateUI()
	{
		CreateUIPages (currentPage);

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
        List<RTSObject> usedUnits = new List<RTSObject>();

        List<RTSObject> bestPick = null;
        while (usedUnits.Count < tempAbilityGroups.Count)
        {
            int min = 100;

            foreach (List<RTSObject> obj in tempAbilityGroups)
            {
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


                if (n > 5)
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

    }


	public void applyGlobalSelection(List<List<string>> input)
	{globalSelection = input;
	
	
	}


    public void DeselectAll()
    {
        if (SelectedObjects.Count == 0)
            return;
        foreach (RTSObject obj in SelectedObjects)
        {
            obj.SetDeselected();
        }

        SelectedObjects.Clear();
        SelectedActiveObjects.Clear();

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

        if (obj is IOrderable)
        {
            SelectedActiveObjects.Remove((IOrderable)obj);
        }
        if (obj)
        {
            obj.SetDeselected();
        }
        SelectedObjects.Remove(obj);

        if (abilityManager != null)
        {
            UIPages.Clear();
            tempAbilityGroups.Clear();
            CreateUIPages(0);
        }
    }

    public void GiveOrder(Order order)
	{//fix this once we get to multiplayer games
		if(SelectedActiveObjects.Count == 0 || SelectedActiveObjects[0].getObject().GetComponent<UnitManager>().PlayerOwner != 1)
			{return;}

        foreach (IOrderable unit in SelectedActiveObjects)
        { 
            unit.GiveOrder(order);
        }


		if (order.OrderType == 1 && SelectedActiveObjects.Count > 0) {
			Vector3 location = order.OrderLocation;
			location.y = location.y + 30;
			Instantiate (movementInd, location, Quaternion.Euler (90, 0, 0));

		} else if (order.OrderType == 4 && SelectedActiveObjects.Count > 0) {
			Vector3 location = order.OrderLocation;
			location.y = location.y + 30;
			Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
		} 
		else if (order.OrderType == 6 && SelectedActiveObjects.Count > 0) {
			if (order.Target.GetComponent<UnitManager> ().PlayerOwner != 1) {
				Vector3 location = order.OrderLocation;
				location.y = location.y + 30;
				Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
			} else {
				Vector3 location = order.OrderLocation;
				location.y = location.y + 30;
				Instantiate (movementInd, location, Quaternion.Euler (90, 0, 0));
			}
		
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
    }

    public int ActiveObjectsCount()
    {
        return SelectedActiveObjects.Count;
    }

    public IOrderable FirstActiveObject()
    {
        return SelectedActiveObjects[0];
    }

    public List<IOrderable> ActiveObjectList()
    {
        return SelectedActiveObjects;
    }

    public bool IsObjectSelected(GameObject obj)
    {
        return SelectedObjects.Contains(obj.GetComponent<RTSObject>());
    }

    public void selectAllArmy()
    {
        foreach (GameObject obj in raceMan.getUnitList())
        {

            if (!obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.structure)
                && !obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.worker))
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

                if (!obj.GetComponent<UnitStats>().isUnitType(UnitTypes.UnitTypeTag.worker))
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




    public void attackMoveO()
    {
        //We're over the main screen, let's raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;



        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 16)))
        {
            Vector3 attackMovePoint = hit.point;
            GiveOrder(Orders.CreateAttackMove(attackMovePoint));
        }

    }

    public void stopO()
    {
        GiveOrder(Orders.CreateStopOrder());
    }



}
