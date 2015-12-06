using UnityEngine;
using System.Collections;

public class Maintenance_Sell : IMaintenanceButtons {
	
	private Rect m_ButtonRect;
	
	public Maintenance_Sell(Rect area)
	{
		m_ButtonRect = area;
	}	
	
	public void Execute ()
	{
		if (GUI.Button (m_ButtonRect, "S"))
		{
			
		}
	}
	
	public void Resize(Rect newArea)
	{
		m_ButtonRect = newArea;
	}
}
