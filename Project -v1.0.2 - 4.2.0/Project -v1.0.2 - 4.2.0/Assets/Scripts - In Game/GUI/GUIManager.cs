using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour, IGUIManager {
	
	//Singleton
	public static GUIManager main;
	
	//Member Variables
//	private Rect m_MiniMapRect = new Rect();
	private float m_MainMenuWidth;
	

	//Properties
	public float MainMenuWidth
	{
		get
		{
			return m_MainMenuWidth;
		}
		private set
		{
			if (Equals (m_MainMenuWidth, value))
			{
				return;
			}
			
			m_MainMenuWidth = value;
			
			GUIEvents.MenuWidthHasChanged(m_MainMenuWidth);
		}
	}
	
	public bool Dragging
	{
		get;
		set;
	}
	
	public Rect DragArea
	{
		get;
		set;
	}
	
	public int GetSupportSelected
	{
		get
		{
			return 0;
		}
	}
	
	void Awake()
	{
		//Set singleton
		main = this;
		
		//Set Textures
		//m_MainMenuBGColor = TextureGenerator.MakeTexture (Color.black);
	}

	// Use this for initialization
	void Start () 
	{
		//Load the mini map and assign the menu width and mini map rect
		IMiniMapController miniMap = ManagerResolver.Resolve<IMiniMapController>();		
		float tempWidth;
		miniMap.LoadMiniMap(out tempWidth);
		MainMenuWidth = tempWidth;
		
		//Build Borders around the map
		/*

		//Create viewable area rect
		Rect menuArea = new Rect();
		menuArea.xMin = m_LeftMiniMapBG.xMin;
		menuArea.xMax = m_RightMiniMapBG.xMax;
		menuArea.yMin = m_BelowMiniMapBG.yMin;
		menuArea.yMax = Screen.height;

		//Create type buttons
		m_TypeButtons[0] = new TypeButton(ButtonType.Building, menuArea);
		m_TypeButtons[1] = new TypeButton(ButtonType.Support, menuArea);
		m_TypeButtons[2] = new TypeButton(ButtonType.Infantry, menuArea);
		m_TypeButtons[3] = new TypeButton(ButtonType.Vehicle, menuArea);
		m_TypeButtons[4] = new TypeButton(ButtonType.Air, menuArea);

		//Calcualte Maintenace button rects
		float size = m_RightMiniMapBG.width-4;
		float totalHeight = size*3;
		float offSet = (m_RightMiniMapBG.height-totalHeight)/2;
		float x = m_RightMiniMapBG.xMin;
		float y = m_RightMiniMapBG.yMin+offSet;

		Rect rect1 = new Rect(x, y, size, size);
		Rect rect2 = new Rect(x, y+size, size, size);
		Rect rect3 = new Rect(x, y+(size*2), size, size);
		
		//Assign maintenance buttons
		m_MaintenanceButtons[0] = new Maintenance_Sell(rect1);
		m_MaintenanceButtons[1] = new Maintenance_Fix(rect2);
		m_MaintenanceButtons[2] = new Maintanance_Disable(rect3);
		*/
		//Resolve Manager
		//m_Manager = ManagerResolver.Resolve<IManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Tell all items that are being built to update themselves
		GUIEvents.TellItemsToUpdate(Time.deltaTime);
		

	}

	
	public bool IsWithin(Vector3 worldPos)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		Vector3 realScreenPos = new Vector3(screenPos.x, Screen.height-screenPos.y, screenPos.z);
		
		if (DragArea.Contains (realScreenPos))
		{
			return true;
		}
		
		return false;
	}
	
	public void UpdateQueueContents(List<Item> availableItems)
	{

	}




	public void Resize ()
	{
		//Resolution has changed, re-size all GUI elements
		//Mini map first
		ManagerResolver.Resolve<IMiniMapController>().LoadMiniMap (out m_MainMenuWidth);
		/*
		//Build Borders around the map
		float sideBorderWidth = (m_MainMenuWidth-(m_MiniMapRect.width*Screen.width))/2;
		float topBorderHeight = (1-m_MiniMapRect.yMax)*Screen.height;
	
		m_LeftMiniMapBG = new Rect();
		m_LeftMiniMapBG.xMin = Screen.width-m_MainMenuWidth;
		m_LeftMiniMapBG.xMax = m_LeftMiniMapBG.xMin + sideBorderWidth;
		m_LeftMiniMapBG.yMin = 0;
		m_LeftMiniMapBG.yMax = (1-m_MiniMapRect.yMin)*Screen.height;
		
		m_RightMiniMapBG = new Rect();
		m_RightMiniMapBG.xMin = m_MiniMapRect.xMax*Screen.width;
		m_RightMiniMapBG.xMax = Screen.width;
		m_RightMiniMapBG.yMin = 0;
		m_RightMiniMapBG.yMax = (1-m_MiniMapRect.yMin)*Screen.height;
		
		m_AboveMiniMapBG = new Rect();
		m_AboveMiniMapBG.xMin = Screen.width-m_MainMenuWidth;
		m_AboveMiniMapBG.xMax = Screen.width;
		m_AboveMiniMapBG.yMin = 0;
		m_AboveMiniMapBG.yMax = topBorderHeight;
		
		m_BelowMiniMapBG = new Rect();
		m_BelowMiniMapBG.xMin = Screen.width-m_MainMenuWidth;
		m_BelowMiniMapBG.xMax = Screen.width;
		m_BelowMiniMapBG.yMin = ((1-m_MiniMapRect.yMin)*Screen.height)-1;
		m_BelowMiniMapBG.yMax = Screen.height;
		
		//Create viewable area rect
		Rect menuArea = new Rect();
		menuArea.xMin = m_LeftMiniMapBG.xMin;
		menuArea.xMax = m_RightMiniMapBG.xMax;
		menuArea.yMin = m_BelowMiniMapBG.yMin;
		menuArea.yMax = Screen.height;
		
		//Update type buttons with new viewable area
	//	foreach (ITypeButton button in m_TypeButtons)
		{
		//	button.Resize(menuArea);
		}
		
		//Calcualte Maintenace button rects
		float size = m_RightMiniMapBG.width-4;
		float totalHeight = size*3;
		float offSet = (m_RightMiniMapBG.height-totalHeight)/2;
		float x = m_RightMiniMapBG.xMin;
		float y = m_RightMiniMapBG.yMin+offSet;
		
		Rect rect1 = new Rect(x, y, size, size);
		Rect rect2 = new Rect(x, y+size, size, size);
		Rect rect3 = new Rect(x, y+(size*2), size, size);
		
		//Update Maintenance Buttons
		m_MaintenanceButtons[0].Resize (rect1);
		m_MaintenanceButtons[1].Resize (rect2);
		m_MaintenanceButtons[2].Resize (rect3);
		*/
	}
}
