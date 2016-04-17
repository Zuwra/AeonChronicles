using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour, IUIManager {
	
	//Singleton
	public static UIManager main;

	public GameObject buildingPlacer;
	public Material goodPlacement;
	public Material badPlacement;
	//Width of GUI menu

	//Action Variables
	private HoverOver hoverOver = HoverOver.Terrain;
	private GameObject currentObject;
	
	//Mode Variables
	private Mode m_Mode = Mode.Normal;
	
	//Interface variables the UI needs to deal with
	private SelectedManager m_SelectedManager;
	private ICamera m_Camera;
	private IGUIManager m_GuiManager;
	private IMiniMapController m_MiniMapController;
	
	//Building Placement variables

	public GameObject m_ObjectBeingPlaced;
	private bool m_Placed = false;
	private FogOfWar fog;
	private RaceManager raceManager;
	private Vector3 originalPosition;
	public GameObject AbilityTargeter;
	private TargetAbility currentAbility;
	public int currentAbilityNUmber;


	public bool IsShiftDown
	{
		get;
		set;
	}
	
	public bool IsControlDown
	{
		get;
		set;
	}
	
	public Mode CurrentMode
	{
		get
		{
			return m_Mode;
		}
	}
	
	void Awake()
	{
		main = this;
	}

	// Use this for initialization
	void Start () 
	{fog = GameObject.FindObjectOfType<FogOfWar> ();
		//Resolve interface variables
		m_SelectedManager = this.gameObject.GetComponent<SelectedManager>();
		m_Camera = ManagerResolver.Resolve<ICamera>();	
		m_GuiManager = ManagerResolver.Resolve<IGUIManager>();
		m_MiniMapController = ManagerResolver.Resolve<IMiniMapController>();
		
		//Attach Event Handlers
		IEventsManager eventsManager = ManagerResolver.Resolve<IEventsManager>();
		eventsManager.MouseClick += ButtonClickedHandler;
		eventsManager.MouseScrollWheel += ScrollWheelHandler;
		eventsManager.KeyAction += KeyBoardPressedHandler;
		eventsManager.ScreenEdgeMousePosition += MouseAtScreenEdgeHandler;

		raceManager = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<GameManager>().activePlayer;
	
		
		//Loader.main.FinishedLoading (this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (m_Mode)
		{
		case Mode.Normal:
			CursorManager.main.normalMode ();
			ModeNormalBehaviour ();
			break;
			
		case Mode.Menu:
			
			break;
		case Mode.targetAbility:
			ModeNormalBehaviour ();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 16)))
			{
				Vector3 targetPoint = hit.point;
				currentObject = hit.collider.gameObject;

				try{
					
				if (m_SelectedManager.checkValidTarget(targetPoint, currentObject, currentAbilityNUmber)) {
						if(currentAbility.myTargetType == TargetAbility.targetType.ground)
							{AbilityTargeter.GetComponent<Light> ().color = Color.green;}
						else{CursorManager.main.targetMode();}
							
					} else {
						if(currentAbility.myTargetType == TargetAbility.targetType.ground){
							AbilityTargeter.GetComponent<Light> ().color = Color.red;}
						else{
							CursorManager.main.invalidMode();}
				}

				targetPoint.y += 60;
				AbilityTargeter.transform.position =  targetPoint;
					
				}
				catch(NullReferenceException) {
					
					SwitchMode (Mode.Normal);

				}
			
			}

			break;

		case Mode.globalAbility:
			ModeNormalBehaviour ();
			 ray = Camera.main.ScreenPointToRay(Input.mousePosition);


			if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 16)))
			{
				Vector3 targetPoint = hit.point;
				currentObject = hit.collider.gameObject;
				if (currentAbility.isValidTarget (currentObject, targetPoint)) {
					AbilityTargeter.GetComponent<Light> ().color = Color.green;
					CursorManager.main.targetMode();
				} else {
					AbilityTargeter.GetComponent<Light> ().color = Color.red;
					CursorManager.main.invalidMode();
				}

				targetPoint.y += 60;
				AbilityTargeter.transform.position =  targetPoint;

			}

			break;

			
		case Mode.PlaceBuilding:
			ModeNormalBehaviour ();
			CursorManager.main.normalMode ();
			ModePlaceBuildingBehaviour();
			break;
		}

	
	}
	
	private void ModeNormalBehaviour()
	{
		//Handle all non event, and non gui UI elements here
		hoverOver = HoverOver.Terrain;
		InteractionState interactionState = InteractionState.Nothing;
		

			//We're over the main screen, let's raycast
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;		

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(5 << 12))) {

			currentObject = hit.collider.gameObject;

			if (!EventSystem.current.IsPointerOverGameObject ()) {
				
			
				switch (hit.collider.gameObject.layer) {

				case 8:
					hoverOver = HoverOver.Terrain;
					break;
					
				case 9:
					if (!fog.IsInCompleteFog (hit.point)) {
						hoverOver = HoverOver.Unit;
					}
					break;
					
				case 10:
					if (!fog.IsInCompleteFog (hit.point)){
						hoverOver = HoverOver.Building;
				}
					break;

				case 13:	
					if(!fog.IsInCompleteFog(hit.point)){
					hoverOver = HoverOver.neutral;}
					break;
				
				}				
			} else {
				hoverOver = HoverOver.Menu;

			}

		}

		if (hoverOver == HoverOver.Menu || m_SelectedManager.ActiveObjectsCount() == 0 )
		{
			//Nothing orderable Selected or mouse is over menu 
			CalculateInteraction (hoverOver, ref interactionState);
		}
		else if (m_SelectedManager.ActiveObjectsCount() >0 && (hoverOver == HoverOver.Unit ||hoverOver == HoverOver.Building) )
		{
			//One object selected
			CalculateInteraction (m_SelectedManager.FirstActiveObject (), hoverOver, ref interactionState);
		}

				
		//Tell the cursor manager to update itself based on the interactionstate
		//CursorManager.main.UpdateCursor (interactionState);	
	}
	
	private void CalculateInteraction(HoverOver hoveringOver, ref InteractionState interactionState)
	{interactionState = InteractionState.Select;
		if (hoveringOver == HoverOver.Terrain) {
			
			interactionState = InteractionState.Nothing;
		} else {
			if (!isPointerOverUIObject()) {
				Selected sel = currentObject.GetComponentInParent<Selected> ();
				if (sel) {
				sel.tempSelect ();
				}
			
				CursorManager.main.selectMode ();
			}
		}

	}
	
	private void CalculateInteraction(RTSObject obj, HoverOver hoveringOver, ref InteractionState interactionState)
	{	interactionState = InteractionState.Select;

		if (currentObject != null) {
			
			currentObject.GetComponentInParent<Selected> ().tempSelect ();
			if (currentObject.GetComponentInParent<UnitManager> ().PlayerOwner != raceManager.playerNumber) {
				interactionState = InteractionState.Attack;
				CursorManager.main.attackMode ();
				return;
			}



		}


	}


	private void ModePlaceBuildingBehaviour()
	{
		//Get current location and place building on that location
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 8)) {

				Vector3 spot = hit.point;
				spot.y += 7;


				m_ObjectBeingPlaced.transform.position = spot;
				buildingPlacer.transform.position = spot;

			}
		
		}
	}
	
	//----------------------Mouse Button Handler------------------------------------
	private void ButtonClickedHandler(object sender, MouseEventArgs e)
	{

			e.Command ();

	}

	//------------------------Mouse Button Commands--------------------------------------------
	public void LeftButton_SingleClickDown(MouseEventArgs e)
	{
		if(hoverOver != HoverOver.Menu)
		switch (m_Mode)
		{

		case Mode.Menu:
	
			break;
		case Mode.Normal:
			//We've left clicked, what have we left clicked on?
			//int currentObjLayer = currentObject.layer;
			originalPosition = Input.mousePosition;
			break;

		}
		
	}

	public bool isPointerOverUIObject()
	{
		PointerEventData eventDatacurrenPosition = new PointerEventData (EventSystem.current);
		eventDatacurrenPosition.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (eventDatacurrenPosition, results);
		return results.Count > 0;


	}



	public void LeftButton_DoubleClickDown(MouseEventArgs e)
	{
		if (currentObject.layer == 8)
		{
			//Select all units of that type on screen
			
		}
	}
	
	public void LeftButton_SingleClickUp(MouseEventArgs e)
	{

		Vector3 targetPoint = Vector3.zero;
			Ray ray;
		RaycastHit hit;
			switch (m_Mode) {
			case Mode.Menu:
			
				break;

		case Mode.Normal:
			//If we've just switched from another mode, don't execute
				
			if (m_Placed) {
				m_Placed = false;
				return;
			}

				//We've left clicked, have we left clicked on a unit?
			int currentObjLayer = currentObject.layer;//layer tells us what we clicked on
            
				//if we're not dragging and clicked on a unit
			if (!m_GuiManager.Dragging && (currentObjLayer == 9 || currentObjLayer == 10)) {


				if (!isPointerOverUIObject()) {

				/*  TARGET RULES
                    shift selects units without affecting others
                    control deselects units without affecting others
                */
					//deselect if none of the modifiers are being used
					if (!IsShiftDown && !IsControlDown) {
						m_SelectedManager.DeselectAll ();
					}

					//if only control is down, remove the unit from selection
					if (IsControlDown && !IsShiftDown) {
					//	m_SelectedManager.DeselectObject (getUnitManagerFromObject (currentObject));
					}
                //if only shift is down, add the unit to selection
                else if (!IsControlDown && IsShiftDown) {

						m_SelectedManager.AddRemoveObject (getUnitManagerFromObject (currentObject));
					} else {

						m_SelectedManager.AddObject (getUnitManagerFromObject (currentObject));
					}
					m_SelectedManager.CreateUIPages (0);
				}
			}
            //or if we aren't dragging and clicked on empty air
			else if (!m_GuiManager.Dragging) {
				if (!isPointerOverUIObject()) {
					if (!IsShiftDown)
						m_SelectedManager.DeselectAll ();
				}} else {
				

						//Get the drag area
						Vector3 upperLeft = new Vector3 ();
						upperLeft.x = Math.Min (Input.mousePosition.x, originalPosition.x);
						upperLeft.y = Math.Max (Input.mousePosition.y, originalPosition.y);
						Vector3 bottRight = new Vector3 ();
						bottRight.x = Math.Max (Input.mousePosition.x, originalPosition.x);
						bottRight.y = Math.Min (Input.mousePosition.y, originalPosition.y);
						//TODO - control and shift are not working. Somehow it's deselecting when it starts, but I can't track where it's being deselected

						//if we're control-dragging, deselect everything in the drag area
						if (IsControlDown) {
							foreach (GameObject obj in raceManager.getUnitSelection(upperLeft, bottRight)) {
								m_SelectedManager.DeselectObject (getUnitManagerFromObject (obj));
							}
						}
                //if we're shift-dragging, add everything in the drag area  
                else if (IsShiftDown) {
							foreach (GameObject obj in raceManager.getUnitSelection(upperLeft, bottRight)) {
								m_SelectedManager.AddObject (getUnitManagerFromObject (obj));
							}
						}
                //if we're dragging, deselect everything, then add everything in the drag area
                else {
							m_SelectedManager.DeselectAll ();
							foreach (GameObject obj in raceManager.getUnitSelection(upperLeft,bottRight)) {
								m_SelectedManager.AddObject (getUnitManagerFromObject (obj));
							}
						}

						//refresh GUI elements
						m_SelectedManager.CreateUIPages (0);
					

				}
			
				break;

		case Mode.targetAbility:
			if (!EventSystem.current.IsPointerOverGameObject ()) {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
				
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {
					targetPoint = hit.point;
			

					AbilityTargeter.transform.position = targetPoint;
					currentObject = hit.collider.gameObject;
					if (currentObject.layer == 9 || currentObject.layer == 10 || currentObject.layer == 13) {
				
					} else {
						currentObject = null;
					}
				}


				if (m_SelectedManager.checkValidTarget (targetPoint, currentObject, currentAbilityNUmber)) {
					m_SelectedManager.fireAbility (currentObject, targetPoint, currentAbilityNUmber);
					SwitchMode (Mode.Normal);
				}
			}
				break;

		case Mode.globalAbility:
			if (!EventSystem.current.IsPointerOverGameObject ()) {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		

				if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {
					targetPoint = hit.point;


					AbilityTargeter.transform.position = targetPoint;
					currentObject = hit.collider.gameObject;
					if (currentObject.layer == 9 || currentObject.layer == 10 || currentObject.layer == 13) {

					} else {
						currentObject = null;
					}
				}
				if (currentAbility.isValidTarget (currentObject, targetPoint)) {
					((TargetAbility)currentAbility).Cast (currentObject, targetPoint);
		
					SwitchMode (Mode.Normal);
				}
			}
				break;
			
		case Mode.PlaceBuilding:
				
			if (buildingPlacer.GetComponent<BuildingPlacer> ().canBuild ()) {
			

				if (!EventSystem.current.IsPointerOverGameObject ()) {
					ray = Camera.main.ScreenPointToRay (Input.mousePosition);

					if (Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 8)) {
						targetPoint = hit.point;

					}

					m_SelectedManager.fireAbility (null, targetPoint, currentAbilityNUmber);
						
					SwitchMode (Mode.Normal);

				}

			}

				break;
			}
		
	}

	public bool allowDrag()
	{
		switch (m_Mode) {
		case Mode.Menu:
			return false;


		case Mode.globalAbility:
			return false;
		

		case Mode.targetAbility:
			return false;
		

		case Mode.Normal:
			return true;
		

		case Mode.PlaceBuilding:
			return false;
		

		}
		return true;
	}

	public void setAbility(Ability abil, int n)
	{currentAbilityNUmber = n;
		currentAbility = (TargetAbility)abil;
		if (currentAbility.myTargetType == TargetAbility.targetType.unit) {
			
		} else {
			CursorManager.main.offMode ();
			AbilityTargeter.SetActive (true);
			AbilityTargeter.GetComponent<Light> ().cookie = currentAbility.targetArea;
			AbilityTargeter.GetComponent<Light> ().spotAngle = currentAbility.areaSize;
		}
	}



    //A safer way to get a UnitManager
    private static UnitManager getUnitManagerFromObject(GameObject obj)
    {
        if (obj.GetComponent<UnitManager>())
            return obj.GetComponent<UnitManager>();
        else
            return obj.GetComponentInParent<UnitManager>();
    }


    public void RightButton_SingleClick(MouseEventArgs e)
	{

		if (hoverOver != HoverOver.Menu) {
			
			switch (m_Mode) {
			case Mode.Menu:
				break;

			case Mode.Normal:
			//We've right clicked, have we right clicked on ground, interactable object or enemy?
				int currentObjLayer = currentObject.layer;

				if (currentObjLayer == 8) {
					//Terrain -> Move Command

					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit hit;		
				
					if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {
						Vector3 attackMovePoint = hit.point;
			

						m_SelectedManager.GiveOrder (Orders.CreateMoveOrder (attackMovePoint));
					}
				} else if (currentObjLayer == 9 || currentObjLayer == 10 || currentObjLayer == 13) {
				
					m_SelectedManager.GiveOrder (Orders.CreateInteractCommand (currentObject));						

				}
		
		
				break;

			case Mode.targetAbility:
				m_SelectedManager.stoptarget ();
				SwitchMode (Mode.Normal);
				
				break;

			case Mode.globalAbility:
				SwitchMode (Mode.Normal);

				break;

			
			case Mode.PlaceBuilding:

			//Cancel building placement
				buildingPlacer.SetActive (false);
				SwitchToModeNormal ();

				break;
			}
		}
	}
	

	public void MiddleButton_SingleClick(MouseEventArgs e)
	{
		
	}
	

	//------------------------------------------------------------------------------------------
	
	private void ScrollWheelHandler(object sender, ScrollWheelEventArgs e)
	{
		//Zoom In/Out
		m_Camera.Zoom (sender, e);
		m_MiniMapController.ReCalculateViewRect ();
	}
	
	private void MouseAtScreenEdgeHandler(object sender, ScreenEdgeEventArgs e)
	{
	
		m_Camera.Pan (sender, e);
		m_MiniMapController.ReCalculateViewRect ();
	}
	
	//-----------------------------------KeyBoard Handler---------------------------------
	private void KeyBoardPressedHandler(object sender, KeyBoardEventArgs e)
	{
		e.Command();
	}
	//-------------------------------------------------------------------------------------
	
	public bool IsCurrentUnit(RTSObject obj)
	{
		return currentObject == obj.gameObject;
	}
	

	public void UserPlacingBuilding(GameObject item, int i)
	{
		currentAbilityNUmber = i;
		SwitchToModePlacingBuilding(item);
	}
	
	public void SwitchMode(Mode mode)
	{
		switch (mode)
		{
		case Mode.Normal:
			SwitchToModeNormal ();
			AbilityTargeter.SetActive (false);
			CursorManager.main.normalMode ();
		
			break;
			
		case Mode.Menu:
			AbilityTargeter.SetActive (false);
			break;

		case Mode.targetAbility:
			CursorManager.main.targetMode ();
			m_Mode = Mode.targetAbility;
			//AbilityTargeter.SetActive (true);
			break;
			
		case Mode.Disabled:
			
			break;

		case Mode.globalAbility:
			m_Mode = Mode.globalAbility;
			AbilityTargeter.SetActive (true);
			CursorManager.main.targetMode ();
			break;

		}

	
	}



	public void SwitchToModeNormal()
	{buildingPlacer.SetActive (false);

		if (m_ObjectBeingPlaced)
		{
			Destroy (m_ObjectBeingPlaced);
		}
	
		m_Mode = Mode.Normal;

	}
	
	public void SwitchToModePlacingBuilding(GameObject item)
	{
		m_Mode = Mode.PlaceBuilding;
		buildingPlacer.SetActive (true);
		m_ObjectBeingPlaced = (GameObject)Instantiate (item);
		buildingPlacer.GetComponent<BuildingPlacer> ().reset (m_ObjectBeingPlaced, goodPlacement, badPlacement);
	
	}

	public void setToMenu()
	{m_Mode = Mode.Menu;
	}
}

public enum HoverOver
{
	Terrain,
	Menu,
	Unit,
	Building,
	CamPlane,
	neutral
}

public enum InteractionState
{
	Nothing = 0,
	Invalid = 1,
	Move = 2,
	Attack = 3,
	Select = 4,
	Interact = 6
}

public enum Mode
{
	Normal,
	Menu,
	targetAbility,
	globalAbility,
	PlaceBuilding,
	Disabled,
}
