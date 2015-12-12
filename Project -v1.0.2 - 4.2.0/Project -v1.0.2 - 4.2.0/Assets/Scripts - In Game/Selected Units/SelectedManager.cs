using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedManager : MonoBehaviour, ISelectedManager {
	
	private List<IOrderable> SelectedActiveObjects = new List<IOrderable>();	
	private List<RTSObject> SelectedObjects = new List<RTSObject>();

	private List<List<RTSObject>> Group = new List<List<RTSObject>>();
	
	public static SelectedManager main;

	public UiAbilityManager abilityManager;
	private RaceManager raceMan;

	void Start()
	{
		raceMan = GameObject.Find ("GameRaceManager").GetComponent<GameManager> ().activePlayer;

	}

	public int OverlayWidth
	{
		get;
		private set;
	}


	void Update () {

		if (Input.GetKeyUp (KeyCode.Tab)) {

				//We're over the main screen, let's raycast
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;		
				
				
				
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16)))
				{
					Vector3 attackMovePoint = hit.point;
				GiveOrder( Orders.CreateAttackMove(attackMovePoint));
				}


		}

		if (Input.GetKeyUp (KeyCode.CapsLock)) {
			GiveOrder( Orders.CreateStopOrder());
		
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

			foreach (IOrderable unit in SelectedActiveObjects)
			{
				if(!unit.UseQAbility())
				{break;}
			}
		}

		if (Input.GetKeyUp (KeyCode.W)) {
			
			foreach (IOrderable unit in SelectedActiveObjects)
			{
				if(!unit.UseWAbility())
				{break;}
			}
		}
		if (Input.GetKeyUp (KeyCode.E)) {
			
			foreach (IOrderable unit in SelectedActiveObjects)
			{
				if(!unit.UseEAbility())
				{break;}
			}
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			
			foreach (IOrderable unit in SelectedActiveObjects)
			{
				if(!unit.UseRAbility())
				{break;}
			}
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
				abilityManager.addUnit(obj.gameObject);}
		}
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




}
