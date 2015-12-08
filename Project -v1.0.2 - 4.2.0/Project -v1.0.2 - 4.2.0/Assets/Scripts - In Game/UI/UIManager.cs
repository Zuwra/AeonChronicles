using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour, IUIManager {
	
	//Singleton
	public static UIManager main;
	
	//Width of GUI menu
	private float m_GuiWidth;
	
	//Action Variables
	private HoverOver hoverOver = HoverOver.Terrain;
	private GameObject currentObject;
	
	//Mode Variables
	private Mode m_Mode = Mode.Normal;
	
	//Interface variables the UI needs to deal with
	private ISelectedManager m_SelectedManager;
	private ICamera m_Camera;
	private IGUIManager m_GuiManager;
	private IMiniMapController m_MiniMapController;
	
	//Building Placement variables
	private Action m_CallBackFunction;
	private Item m_ItemBeingPlaced;
	private GameObject m_ObjectBeingPlaced;
	private bool m_PositionValid = true;
	private bool m_Placed = false;

	private RaceManager raceManager;
	private Vector3 originalPosition;

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
	{
		//Resolve interface variables
		m_SelectedManager = ManagerResolver.Resolve<ISelectedManager>();
		m_Camera = ManagerResolver.Resolve<ICamera>();	
		m_GuiManager = ManagerResolver.Resolve<IGUIManager>();
		m_MiniMapController = ManagerResolver.Resolve<IMiniMapController>();
		
		//Attach Event Handlers
		IEventsManager eventsManager = ManagerResolver.Resolve<IEventsManager>();
		eventsManager.MouseClick += ButtonClickedHandler;
		eventsManager.MouseScrollWheel += ScrollWheelHandler;
		eventsManager.KeyAction += KeyBoardPressedHandler;
		eventsManager.ScreenEdgeMousePosition += MouseAtScreenEdgeHandler;

		raceManager = GameObject.FindGameObjectWithTag ("GameRaceManager").GetComponent<RaceManager>();
		//Attach gui width changed event	
		GUIEvents.MenuWidthChanged += MenuWidthChanged;
		
		//Loader.main.FinishedLoading (this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (m_Mode)
		{
		case Mode.Normal:
			ModeNormalBehaviour ();
			break;
			
		case Mode.Menu:
			
			break;
			
		case Mode.PlaceBuilding:
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



			if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(8 << 12)))
		{

				currentObject = hit.collider.gameObject;

				switch (hit.collider.gameObject.layer)
				{
				case 8:
					//Friendly unit
					hoverOver = HoverOver.Terrain;
					break;
					
				case 9:
					//Enemy Unit
					hoverOver = HoverOver.Unit;
					break;
					
				case 10:
					hoverOver = HoverOver.Building;
					break;
				
				}				
				}

		
		if (hoverOver == HoverOver.Menu || m_SelectedManager.ActiveObjectsCount() == 0 || m_GuiManager.GetSupportSelected != 0)
		{
			//Nothing orderable Selected or mouse is over menu or support is selected
			CalculateInteraction (hoverOver, ref interactionState);
		}
		else if (m_SelectedManager.ActiveObjectsCount() >0 && (hoverOver == HoverOver.Unit ||hoverOver == HoverOver.Building) )
		{
			//One object selected
			CalculateInteraction (m_SelectedManager.FirstActiveObject (), hoverOver, ref interactionState);
		}

				
		//Tell the cursor manager to update itself based on the interactionstate
		CursorManager.main.UpdateCursor (interactionState);	
	}
	
	private void CalculateInteraction(HoverOver hoveringOver, ref InteractionState interactionState)
	{
		switch (hoveringOver)
		{
		case HoverOver.Menu:			
		case HoverOver.Terrain:
			//Normal Interaction
			interactionState = InteractionState.Nothing;
			break;
			
		case HoverOver.Unit:
			//Select interaction
			interactionState = InteractionState.Select;

			break;
			
			
				
		case HoverOver.Building:
			interactionState = InteractionState.Select;
			break;
			
		}
	}
	
	private void CalculateInteraction(IOrderable obj, HoverOver hoveringOver, ref InteractionState interactionState)
	{	interactionState = InteractionState.Select;
		if (currentObject != null) {
		
			if (currentObject.GetComponentInParent<UnitManager> ().PlayerOwner 
				!= GameObject.Find ("GameRaceManager").GetComponent<RaceManager> ().playerNumber) {
				interactionState = InteractionState.Attack;
		
			} 
		}





	}

	
	private void ModePlaceBuildingBehaviour()
	{
		//Get current location and place building on that location
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 11))
		{
			m_ObjectBeingPlaced.transform.position = hit.point;
		}
		
		//Check validity of current position
		if (Input.GetKeyDown ("v"))
		{
			m_PositionValid = !m_PositionValid;
			
			if (m_PositionValid)
			{
				m_ObjectBeingPlaced.GetComponent<BuildingBeingPlaced>().SetToValid();
			}
			else
			{
				m_ObjectBeingPlaced.GetComponent<BuildingBeingPlaced>().SetToInvalid();
			}
		}
	}
	
	//----------------------Mouse Button Handler------------------------------------
	private void ButtonClickedHandler(object sender, MouseEventArgs e)
	{
		//If mouse is over GUI then we don't want to process the button clicks
		if (e.X < Screen.width-m_GuiWidth)
		{
			e.Command ();
		}
	}
	//-----------------------------------------------------------------------------
	
	//------------------------Mouse Button Commands--------------------------------------------
	public void LeftButton_SingleClickDown(MouseEventArgs e)
	{
		Debug.Log ("left click");
		switch (m_Mode)
		{
		case Mode.Normal:
			//We've left clicked, what have we left clicked on?
			int currentObjLayer = currentObject.layer;
			originalPosition = Input.mousePosition;
			if (currentObjLayer == 8)
			{
				//Friendly Unit, is the unit selected?
				if (m_SelectedManager.IsObjectSelected(currentObject))
				{
					//Is the unit deployable?
					if (currentObject.GetComponent<Unit>().IsDeployable())
					{
						currentObject.GetComponent<Unit>().GiveOrder (Orders.CreateDeployOrder());
					}
				}
			}
			break;
			
		case Mode.PlaceBuilding:
			//We've left clicked, if we're valid place the building
			if (m_PositionValid)
			{
				GameObject newObject = (GameObject)Instantiate (m_ItemBeingPlaced.Prefab, m_ObjectBeingPlaced.transform.position, m_ItemBeingPlaced.Prefab.transform.rotation);
				UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(newObject, "Assets/Scripts - In Game/UI/UIManager.cs (376,5)", m_ItemBeingPlaced.ObjectType.ToString ());
				newObject.layer = 12;
				newObject.tag = "Player";
				
				BoxCollider tempCollider = newObject.GetComponent<BoxCollider>();
				
				if (tempCollider == null)
				{
					tempCollider = newObject.AddComponent<BoxCollider>();
				}
				
				tempCollider.center = m_ObjectBeingPlaced.GetComponent<BuildingBeingPlaced>().ColliderCenter;
				tempCollider.size = m_ObjectBeingPlaced.GetComponent<BuildingBeingPlaced>().ColliderSize;
				tempCollider.isTrigger = true;
				
				m_ItemBeingPlaced.FinishBuild ();
				m_CallBackFunction.Invoke ();
				m_Placed = true;
				SwitchToModeNormal ();
			}
			break;
		}
		
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



		switch (m_Mode)
		{
		case Mode.Normal:
			//If we've just switched from another mode, don't execute
			if (m_Placed)
			{
				m_Placed = false;
				return;
			}
			
			//We've left clicked, have we left clicked on a unit?
			int currentObjLayer = currentObject.layer;
			if (!m_GuiManager.Dragging && (currentObjLayer == 9 || currentObjLayer == 10))
			{
				if (!IsShiftDown)
				{// this shoudl add guys to selection (I dont think it does)
					m_SelectedManager.DeselectAll ();
				}
				if(currentObject.GetComponent<UnitManager>())
					{m_SelectedManager.AddObject (currentObject.GetComponent<UnitManager>());}
				else{
					m_SelectedManager.AddObject (currentObject.GetComponentInParent<UnitManager>());}
			}
			else if (!m_GuiManager.Dragging)
			{
				m_SelectedManager.DeselectAll ();
			}
			else{

				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;		
				Vector3 groundPoint = new Vector3();
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(8)))
				{
				groundPoint = hit.point;}


				Ray Sray = Camera.main.ScreenPointToRay(originalPosition);
				RaycastHit Sechit;		
				Vector3 SgroundPoint = new Vector3();
				if (Physics.Raycast (Sray, out Sechit, Mathf.Infinity, ~(8)))
				{
					SgroundPoint = Sechit.point;}


				Vector3 upperLeft = new Vector3();
				upperLeft.x = Math.Min(groundPoint.x, SgroundPoint.x);
				upperLeft.z = Math.Max(groundPoint.z, SgroundPoint.z);

				Vector3 bottRight =new Vector3();
				bottRight.x = Math.Max(groundPoint.x, SgroundPoint.x);
				bottRight.z = Math.Min(groundPoint.z, SgroundPoint.z);


				Debug.Log(upperLeft + "         "+ bottRight);
				foreach(GameObject obj in raceManager.getUnitSelection(upperLeft,bottRight))
					{
					m_SelectedManager.AddObject(obj.GetComponent<UnitManager>());
				}
								

							}



			break;
			
		case Mode.PlaceBuilding:
			if (m_Placed)
			{
				m_Placed = false;
			}
			break;
		}
	}
	
	public void RightButton_SingleClick(MouseEventArgs e)
	{
	

		switch (m_Mode)
		{
		case Mode.Normal:
			//We've right clicked, have we right clicked on ground, interactable object or enemy?
			int currentObjLayer = currentObject.layer;

			if (currentObjLayer == 8)
			{
				//Terrain -> Move Command

				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;		
				
				
				
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16)))
				{
					Vector3 attackMovePoint = hit.point;
					//GiveOrder( Orders.CreateAttackMove(attackMovePoint));

				//Debug.Log("commanding to move" + attackMovePoint);

					m_SelectedManager.GiveOrder (Orders.CreateMoveOrder (attackMovePoint));}
			}
			else if (currentObjLayer == 9 || currentObjLayer == 10)
			{Debug.Log(currentObject);
				m_SelectedManager.GiveOrder(Orders.CreateAttackOrder(currentObject.GetComponent<UnitManager>()));
				//Friendly Unit -> Interact (if applicable)
			}
			else if (currentObjLayer == 9 || currentObjLayer == 15)
			{
				//Enenmy Unit -> Attack
			}
			else if (currentObjLayer == 12)
			{
				//Friendly Building -> Interact (if applicable)
			}
			else if (currentObjLayer == 13)
			{
				//Enemy Building -> Attack
			}
			break;
			
		case Mode.PlaceBuilding:
			
			//Cancel building placement
			
			
			SwitchToModeNormal();
			break;
		}
		
	}
	
	public void RightButton_DoubleClick(MouseEventArgs e)
	{
		
	}
	
	public void MiddleButton_SingleClick(MouseEventArgs e)
	{
		
	}
	
	public void MiddleButton_DoubleClick(MouseEventArgs e)
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
	
	public void MenuWidthChanged(float newWidth)
	{
		m_GuiWidth = newWidth;
	}
	
	public void UserPlacingBuilding(Item item, Action callbackFunction)
	{
		SwitchToModePlacingBuilding(item, callbackFunction);
	}
	
	public void SwitchMode(Mode mode)
	{
		switch (mode)
		{
		case Mode.Normal:
			SwitchToModeNormal ();
			break;
			
		case Mode.Menu:
			
			break;
			
		case Mode.Disabled:
			
			break;
		}
	}
	
	private void SwitchToModeNormal()
	{
		if (m_ObjectBeingPlaced)
		{
			Destroy (m_ObjectBeingPlaced);
		}
		m_CallBackFunction = null;
		m_ItemBeingPlaced = null;
		m_Mode = Mode.Normal;
	}
	
	private void SwitchToModePlacingBuilding(Item item, Action callBackFunction)
	{
		m_Mode = Mode.PlaceBuilding;
		m_CallBackFunction = callBackFunction;
		m_ItemBeingPlaced = item;
		m_ObjectBeingPlaced = (GameObject)Instantiate (item.Prefab);
		m_ObjectBeingPlaced.AddComponent<BuildingBeingPlaced>();
	}
}

public enum HoverOver
{
	Terrain,
	Menu,
	Unit,
	Building,
	CamPlane
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
	PlaceBuilding,
	Disabled,
}
