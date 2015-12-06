using UnityEngine;
using System.Collections;

public class Maintanance_Disable : IMaintenanceButtons {
	
	private Rect m_ButtonRect;
	
	public Maintanance_Disable(Rect area)
	{
		m_ButtonRect = area;
	}
	
	public void Execute ()
	{
		if (GUI.Button (m_ButtonRect, "D"))
		{
			
		}
	}
	
	public void Resize(Rect newArea)
	{
		m_ButtonRect = newArea;
	}
}
