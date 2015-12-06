using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class QueueContent : IQueueContent
{
	private List<Item> m_Items = new List<Item>();
	private Rect[] m_Area;
	private int m_ArrowOffset = 0;
	private int m_MaxVisibleItems;
	private bool m_ArrowsEnabled = false;
	private Rect[] m_ArrowRects = new Rect[2];
	private bool m_Building = false;
	
	private IUIManager m_UIManager;
	
	public QueueContent(Rect area)
	{
		CalculateSize (area);
		m_UIManager = ManagerResolver.Resolve<IUIManager>();
	}
	
	public void Execute()
	{
		//Draw Items
		int counter = 0;
		int itemsDrawn = 0;
		
		foreach (Item item in m_Items)
		{
			if (counter >= m_ArrowOffset)
			{
				if (GUI.Button (m_Area[itemsDrawn], "", item.ButtonStyle))
				{
					//Item Clicked
					if (Event.current.button == 0)
					{
						//Left Button
						if (item.Paused)
						{
							item.UnPauseBuild ();
						}
						else if ((item.TypeIdentifier == Const.TYPE_Building || item.TypeIdentifier == Const.TYPE_Support) && item.IsFinished && m_UIManager.CurrentMode == Mode.Normal)
						{
							//Building has finished, needs to be placed, pass control to the UI manager and wait for a response
							m_UIManager.UserPlacingBuilding(item, () => 
							{
								//Building was placed
								m_Building = false;
							});
						}
						else if (!m_Building)
						{
							item.StartBuild ();
							m_Building = true;
						}
					}
                    else if (Event.current.button == 1)
					{
						//Right Button (pause or cancel)
						if (item.Paused || item.IsFinished)
						{
							item.CancelBuild ();
							m_Building = false;
							m_UIManager.SwitchMode (Mode.Normal);
						}
						else if (item.IsBuilding)
						{
							item.PauseBuild();
						}
					}
				}
				
				if (item.IsBuilding && !item.IsFinished)
				{
					int index = (int)(item.GetProgress ()*360);
					
					if (index <= 359)
					{
						GUI.DrawTexture (m_Area[itemsDrawn], ItemProgressTextures.Overlays[index]);
					}
				}
				else if (item.IsBuilding && item.IsFinished)
				{
					GUI.Label (m_Area[itemsDrawn], Strings.Ready, GUIStyles.ItemFinishedLabel);
				}
				
				itemsDrawn++;
				
				if (itemsDrawn >= m_MaxVisibleItems)
				{
					break;
				}
			}
			
			counter++;
		}
		
		//Draw Arrows
		if (GUI.Button (m_ArrowRects[0], "Down") && m_ArrowsEnabled && m_ArrowOffset < m_Items.Count-m_MaxVisibleItems)
		{
			//Using 2 as we're skipping two items
			m_ArrowOffset += 2;
		}
		
		if (GUI.Button (m_ArrowRects[1], "Up") && m_ArrowsEnabled && m_ArrowOffset > 0)
		{
			m_ArrowOffset -= 2;
		}
	}
	
	public void UpdateContents(List<Item> newAvailableItems)
	{
		//Add a deep clone of each new item to the list		
		foreach (Item item in newAvailableItems)
		{
			if (m_Items.FirstOrDefault (x => x.ID == item.ID) == null)
			{
				m_Items.Add (item.DeepClone ());
			}
		}
		
		//Sort the items by their sort order
		m_Items = m_Items.OrderBy(x => x.SortOrder).ToList();
		
		//Items have been added or removed, do we need the arrows?
		if (m_Items.Count > m_MaxVisibleItems)
		{
			m_ArrowsEnabled = true;
		}
		else
		{
			m_ArrowsEnabled = false;
		}
	}
	
	public void Resize(Rect newArea)
	{
		CalculateSize (newArea);
	}

	private void CalculateSize (Rect area)
	{
		//leave a gap of 40 from bottom for arrows
		Rect arrowRect = new Rect(area);
		arrowRect.yMin = arrowRect.yMax-40;
		m_ArrowRects[0] = new Rect(arrowRect.xMin, arrowRect.yMin, arrowRect.width/2, arrowRect.height);
		m_ArrowRects[1] = new Rect(arrowRect.xMin + (arrowRect.width/2), arrowRect.yMin, arrowRect.width/2, arrowRect.height);
		
		area.yMax = arrowRect.yMin;
		
		//Decide how many items there should be visible per column (there's 2 columns)
		float size = area.width/2;
		
		int numberOfRectsPerColumn = (int)(area.height/size);
		m_MaxVisibleItems = numberOfRectsPerColumn*2;
		
		m_Area = new Rect[m_MaxVisibleItems];
		
		float x1 = area.xMin;
		float x2 = area.xMin + size;
		float y = area.yMin - size;
		
		for (int i=0; i<m_MaxVisibleItems; i++)
		{
			if (i%2 == 0)
			{
				y += size;
				m_Area[i] = new Rect(x1, y, size, size);
			}
			else
			{
				m_Area[i] = new Rect(x2, y, size, size);
			}
		}
	}
}
