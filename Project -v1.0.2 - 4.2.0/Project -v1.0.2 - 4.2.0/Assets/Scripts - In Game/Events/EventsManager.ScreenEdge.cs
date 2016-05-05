using UnityEngine;
using System.Collections;

public partial class EventsManager {
	
	//Screen Edge variables
	private bool atScreenEdge = false;
	private float atScreenEdgeCounter = 0;

	private void CheckScreenEdgeEvents()
	{
		ScreenEdgeEventArgs tempEventArgs = null;
		atScreenEdge = false;
		
		if (Input.mousePosition.x <= 0)
		{			
			if (tempEventArgs == null)
			{
				tempEventArgs = new ScreenEdgeEventArgs(-1, 0);
			}
			else
			{
				tempEventArgs.x = -1;
			}

			atScreenEdge = true;
		}
		
		if (Input.mousePosition.x > Screen.width-2)
		{
			if (tempEventArgs == null)
			{
				tempEventArgs = new ScreenEdgeEventArgs(1, 0);
			}
			else
			{
				tempEventArgs.x = 1;
			}
		
			atScreenEdge = true;
		}
		
		if (Input.mousePosition.y <= 0)
		{
			if (tempEventArgs == null)
			{
				tempEventArgs = new ScreenEdgeEventArgs(0, -1);
			}
			else
			{
				tempEventArgs.y = -1;
			}

			atScreenEdge = true;
		}
		
		if (Input.mousePosition.y > Screen.height-2)
		{
			if (tempEventArgs == null)
			{
				tempEventArgs = new ScreenEdgeEventArgs(0, 1);
			}
			else
			{
				tempEventArgs.y = 1;
			}

			atScreenEdge = true;
		}
		
		if (atScreenEdge)
		{
			atScreenEdgeCounter += Time.deltaTime;
			tempEventArgs.duration = atScreenEdgeCounter;						
		}
		else
		{
			atScreenEdgeCounter = 0;
		}
		
		if (tempEventArgs != null && ScreenEdgeMousePosition != null)
		{
			ScreenEdgeMousePosition(this, tempEventArgs);
		}
	}
}
