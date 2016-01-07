using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedManager : MonoBehaviour, ISelectedManager {
	
	private List<IOrderable> SelectedActiveObjects = new List<IOrderable>();	
	private List<RTSObject> SelectedObjects = new List<RTSObject>();
	private List<List<RTSObject>> AbilityGroups = new  List<List<RTSObject>> ();

	private List<List<RTSObject>> Group = new List<List<RTSObject>>();
	
	public static SelectedManager main;

	public UiAbilityManager abilityManager;
	private RaceManager raceMan;

	public string sUnitOne;
	public string sUnitTwo;
	public string sUnitThree;
	public string sUnitFour;


	public GameObject movementInd;
	public GameObject attackInd;

	void Start()
	{abilityManager = GameObject.Find ("GameHud").GetComponent<UiAbilityManager> ();
		raceMan = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer;

	}

	public int OverlayWidth
	{
		get;
		private set;
	}


	void Update () {

		if (Input.GetKeyUp (KeyCode.Tab)) {
			attackMoveO ();
		}

		if (Input.GetKeyUp (KeyCode.CapsLock)) {
			stopO ();
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			// set a control group
			if (Input.GetKeyDown (KeyCode.Alpha1)) 
				{AddUnitsToGroup (0);} 
	
			else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				AddUnitsToGroup (1);}

			else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				AddUnitsToGroup (2);} 

			else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				AddUnitsToGroup (3);} 

			else if (Input.GetKeyDown (KeyCode.Alpha5)) {
				AddUnitsToGroup (4);} 

			else if (Input.GetKeyDown (KeyCode.Alpha6)) {
				AddUnitsToGroup (5);} 

			else if (Input.GetKeyDown (KeyCode.Alpha7)) {
				AddUnitsToGroup (6);} 

			else if (Input.GetKeyDown (KeyCode.Alpha8)) {
				AddUnitsToGroup (7);}

			else if ( Input.GetKeyDown (KeyCode.Alpha9)) {
				AddUnitsToGroup (8);} 

			else if (Input.GetKeyDown (KeyCode.Alpha0)) {
				AddUnitsToGroup (9);

			} 

		} else {
			// Select a control group
		if (Input.GetKeyDown (KeyCode.Alpha1))   
			{	SelectGroup (0);}
		else if (Input.GetKeyDown (KeyCode.Alpha2))
			{SelectGroup (1);}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 
			{	SelectGroup (2);} 
		else if (Input.GetKeyDown (KeyCode.Alpha4)) {	
				SelectGroup (3);} 
		else if (Input.GetKeyDown (KeyCode.Alpha5)) {	
				SelectGroup (4);}
		else if (Input.GetKeyDown (KeyCode.Alpha6)) {	
				SelectGroup (5);} 
		else if (Input.GetKeyDown (KeyCode.Alpha7)) {	
				SelectGroup (6);}
		else if (Input.GetKeyDown (KeyCode.Alpha8)) {	
				SelectGroup (7);}
		else if (Input.GetKeyDown (KeyCode.Alpha9)) {	
				SelectGroup (8);} 
		else if (Input.GetKeyDown (KeyCode.Alpha0)) {	
				SelectGroup (9);}
		
		}

	if (Input.GetKeyUp (KeyCode.Q)) {
			callAbility (0);
		}
		else if (Input.GetKeyUp (KeyCode.W)) {
			callAbility (1);
		}
		else 	if (Input.GetKeyUp (KeyCode.E)) {
			callAbility (2);
		}
		else if (Input.GetKeyUp (KeyCode.R)) {
			callAbility (3);
		}
		else if (Input.GetKeyUp (KeyCode.A)) {
			callAbility (4);
		}
		else if (Input.GetKeyUp (KeyCode.S)) {
			callAbility (5);
		}
		else if (Input.GetKeyUp (KeyCode.D)) {
			callAbility (6);
		}
		else if (Input.GetKeyUp (KeyCode.F)) {
			callAbility (7);
		}
		else if (Input.GetKeyUp (KeyCode.Z)) {
			callAbility (8);
		}
		else if (Input.GetKeyUp (KeyCode.X)) {
			callAbility (9);
		}
		else if (Input.GetKeyUp (KeyCode.C)) {
			callAbility (10);
		}
		else if (Input.GetKeyUp (KeyCode.V)) {
			callAbility (11);
		}



	}
		

	public void callAbility(int n)
	{int X = 0;
		foreach (List<RTSObject> lis in AbilityGroups) {
			if (lis [0].abilityList.Count  > n- X ) {

			
				Debug.Log("Activastin");

				foreach (RTSObject unit in lis) {
					Debug.Log ("Iterating");
					if (!unit.UseAbility (n-X)) {
						break;
					}
				}
				break;
			}
			X += lis [0].abilityList.Count;
		}
	}


	public void Awake()
	{

		for (int i=0; i<10; i++)
		{
			Group.Add (new List<RTSObject>());
		}
		
		main = this;
		
		OverlayWidth = 80;
	}
	
	public void AddObject (RTSObject obj)
	{
	
		if(abilityManager != null){
			abilityManager.resetUI();}

		if (!SelectedObjects.Contains (obj))
		{
			if (obj is IOrderable)// && obj.gameObject.layer == 8)
			{
				SelectedActiveObjects.Add ((IOrderable)obj);
			}
		
			SelectedObjects.Add (obj);

			obj.SetSelected ();
			if(abilityManager != null){
				//abilityManager.addUnit(SelectedObjects);
			}
			sortUnit (obj);
		}

		abilityManager.loadUI (AbilityGroups);
	}



	public void sortUnit(RTSObject obj)
	{foreach (List<RTSObject> lis in AbilityGroups) {
		
			if (obj.gameObject.GetComponent<UnitManager>().UnitName == (lis [0]).gameObject.GetComponent<UnitManager>().UnitName) {
				lis.Add (obj);
				return;
			}
		}
		List<RTSObject> unitList = new List<RTSObject> ();
		unitList.Add (obj);
		AbilityGroups.Add (unitList);
	}
	
	public void DeselectAll()
	{
		foreach (RTSObject obj in SelectedObjects)
		{
			obj.SetDeselected ();
		}
		
		SelectedObjects.Clear ();
		SelectedActiveObjects.Clear ();


		if(abilityManager != null){
			abilityManager.resetUI();}
		
		AbilityGroups.Clear ();
	}
	
	public void DeselectObject(RTSObject obj)
	{	
		if (obj is IOrderable)
		{
			SelectedActiveObjects.Remove ((IOrderable)obj);
		}
		if (obj) {
			obj.SetDeselected ();
		}
		SelectedObjects.Remove (obj);	

		if(abilityManager != null){
			abilityManager.resetUI();}
	}
	
	public void GiveOrder(Order order)
	{
		foreach (IOrderable unit in SelectedActiveObjects)
		{
			unit.GiveOrder(order);
		}


		if (order.OrderType == 1) {
			Vector3 location = order.OrderLocation;
			location.y = location.y + 30;
			GameObject ind = (GameObject)Instantiate (movementInd, location, Quaternion.Euler (90, 0, 0));
			//ind.transform.Rotate (Vector3.down);
		} else if (order.OrderType == 4) {
			Vector3 location = order.OrderLocation;
			location.y = location.y + 30;
			GameObject ind = (GameObject)Instantiate (attackInd, location, Quaternion.Euler (90, 0, 0));
		}
	}
	
	public void AddUnitsToGroup(int groupNumber)
	{
		Group[groupNumber].Clear ();
		foreach (RTSObject obj in SelectedObjects)
		{
			Group[groupNumber].Add (obj);
			
		}		
	}
	
	public void SelectGroup(int groupNumber)
	{
		DeselectAll ();


		foreach (RTSObject obj in Group[groupNumber])
		{
			AddObject (obj);
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
		return SelectedObjects.Contains (obj.GetComponent<RTSObject>());
	}

	public void selectAllArmy ()
		{
		foreach (GameObject obj in  raceMan.getUnitList ()) {
		
			if (!obj.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.structure) 
			    &&!obj.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.worker) ) {
				AddObject (obj.GetComponent<UnitManager> ());
			}
		}
		}

	public void selectAllUnbound()
	{
		
		selectAllArmy ();


		foreach (List<RTSObject> obj in Group) {
			foreach (RTSObject rts in obj) {
				DeselectObject (rts);
			}
		}

	}

	public void selectIdleWorker()
	{
		{
			foreach (GameObject obj in  raceMan.getUnitList ()) {

				if (!obj.GetComponent<UnitStats> ().isUnitType (UnitTypes.UnitTypeTag.worker)) {
					continue;
				}
				UnitManager manager = obj.GetComponent<UnitManager> ();
				if (!manager.isIdle ()) {
					continue;
				}

				if (!SelectedObjects.Contains (manager)) {
					DeselectAll ();
							AddObject (manager);
							break;
						}
					}
						




		}
	}

	public void selectUnitOne()
	{DeselectAll ();
	foreach (GameObject obj in  raceMan.getUnitList ()) {
	
			UnitManager manager = obj.GetComponent<UnitManager> ();
			Debug.Log (manager.UnitName + "    " +  sUnitOne);
			if (manager.UnitName == sUnitOne) {
				AddObject (manager);
			}
		}
		}

	public void selectUnitTwo()
	{DeselectAll ();
		foreach (GameObject obj in  raceMan.getUnitList ()) {

			UnitManager manager = obj.GetComponent<UnitManager> ();
			if (manager.UnitName == sUnitTwo) {
				AddObject (manager);
			}
		}
	}

	public void selectUnitThree()
	{DeselectAll ();
		foreach (GameObject obj in  raceMan.getUnitList ()) {

			UnitManager manager = obj.GetComponent<UnitManager> ();
			if (manager.UnitName == sUnitThree) {
				AddObject (manager);
			}
		}
	}

	public void selectUnitFour()
	{DeselectAll ();
		foreach (GameObject obj in  raceMan.getUnitList ()) {

			UnitManager manager = obj.GetComponent<UnitManager> ();
			if (manager.UnitName == sUnitFour) {
				AddObject (manager);
			}
		}
	}


	public void attackMoveO()
	{
		//We're over the main screen, let's raycast
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;		



		if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16)))
		{
			Vector3 attackMovePoint = hit.point;
			GiveOrder( Orders.CreateAttackMove(attackMovePoint));
		}

	}

	public void stopO()
	{	GiveOrder( Orders.CreateStopOrder());
	}



}
