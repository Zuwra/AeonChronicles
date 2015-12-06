using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueueButton : IQueueButton
{
	private bool m_Selected = false;
	
	private IQueueContent m_QueueContent;
	
	private GUIStyle m_ButtonStyle;
	
	private int m_BuildingIdentifier;
	private int m_ID;
	
	private Rect m_ButtonRect;
	
	private int m_TeamIdentifier;
	private int m_TypeIdentifier;
	
	public bool Selected
	{
		get
		{
			return m_Selected;
		}
		private set
		{
			if (Equals (m_Selected, value))
			{
				return;
			}
			
			m_Selected = value;
			SelectedValueChanged (m_Selected);
		}
	}
	
	public int BuildingID
	{
		get
		{
			return m_BuildingIdentifier;
		}
	}
	
	public int ID
	{
		get
		{
			return m_ID;
		}
	}
	
	public int TeamIdentifier
	{
		get
		{
			return m_TeamIdentifier;
		}
	}
	
	public QueueButton(int Id, int buildingId, int TeamID, int TypeID, Rect menuArea)
	{
		//Calculate rect
		//Need to determine button rect, margins are 1% of width
		float margin = menuArea.width*0.01f;
		
		float buttonSize = (menuArea.width-(margin*2))/5.0f;
		
		float buttonStartY = menuArea.yMin + margin;
		float buttonStartX = (menuArea.xMin + margin) + (buttonSize*Id);
		
		//Assign Rect
		m_ButtonRect = new Rect(buttonStartX, buttonStartY, buttonSize, buttonSize);
		
		//Assign ID's
		m_ID = Id;
		m_BuildingIdentifier = buildingId;
		
		//Create style
		m_ButtonStyle = GUIStyles.CreateQueueButtonStyle();
		
		//Attach to events
		GUIEvents.QueueButtonChanged += ButtonPressedEvent;
		
		//Assign identifiers
		m_TeamIdentifier = TeamID;
		m_TypeIdentifier = TypeID;
		
		//Create Content Object
		menuArea.yMin = m_ButtonRect.yMax+10;
		m_QueueContent = new QueueContent(menuArea);
	}
	
	private void SelectedValueChanged(bool newValue)
	{
		if (newValue)
		{
			//Button has been clicked, set to highlight
			m_ButtonStyle.normal.background = GUITextures.TypeButtonSelected;
			m_ButtonStyle.hover.background = GUITextures.TypeButtonSelected;
			
		}
		else
		{
			//Button has been deselected, remove highlight
			m_ButtonStyle.normal.background = GUITextures.TypeButtonNormal;
			m_ButtonStyle.hover.background = GUITextures.TypeButtonHover;
			
		}
	}
	
	private void ButtonPressedEvent(object sender, QueueButtonEventArgs e)
	{
		if (sender == this)
		{
			Selected = true;
		}
		else
		{
			Selected = false;
		}
	}
	
	public void Execute()
	{
		//Draw Button
		if (GUI.Button (m_ButtonRect, (ID+1).ToString (), m_ButtonStyle))
		{
			GUIEvents.QueueButtonPressed (this, ID);
		}
		
		if (Selected)
		{
			//Draw Queue Content
			m_QueueContent.Execute ();
		}
	}
	
	public void UpdateRect (int Id)
	{
		if (ID > Id)
		{
			float size = m_ButtonRect.width;
			m_ButtonRect.xMin -= size;
			m_ButtonRect.xMax -= size;
			m_ID--;
		}
	}
	
	public void SetSelected()
	{
		Selected = true;
	}
	
	public void UpdateQueueContents(List<Item> availableItems)
	{
		m_QueueContent.UpdateContents (availableItems.FindAll (x => x.TypeIdentifier == m_TypeIdentifier && x.TeamIdentifier == m_TeamIdentifier));
	}
	
	public void Resize(Rect newArea)
	{
		//Calculate rect
		//Need to determine button rect, margins are 1% of width
		float margin = newArea.width*0.01f;
		
		float buttonSize = (newArea.width-(margin*2))/5.0f;
		
		float buttonStartY = newArea.yMin + margin;
		float buttonStartX = (newArea.xMin + margin) + (buttonSize*m_ID);
		
		//Assign Rect
		m_ButtonRect = new Rect(buttonStartX, buttonStartY, buttonSize, buttonSize);
		
		//Update contents
		Rect contentArea = new Rect(newArea);
		contentArea.yMin = m_ButtonRect.yMax+10;
		m_QueueContent.Resize (contentArea);
	}
}
