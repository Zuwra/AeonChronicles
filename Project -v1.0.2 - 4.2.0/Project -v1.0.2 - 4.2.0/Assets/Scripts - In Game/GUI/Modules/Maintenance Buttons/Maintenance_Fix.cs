using UnityEngine;
using System.Collections;

public class Maintenance_Fix : IMaintenanceButtons {
	
	private Rect m_ButtonRect;
	
	public Maintenance_Fix(Rect area)
	{
		m_ButtonRect = area;
	}
	
	public void Execute ()
	{
		if (GUI.Button (m_ButtonRect, "F"))
		{
			
		}
	}
	
	public void Resize(Rect newArea)
	{
		m_ButtonRect = newArea;
	}
}
