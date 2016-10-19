using UnityEngine;
using System.Collections;
using System;

public partial class EventsManager {
	
	//-----------------------Double Click Paramters------------------
	public float doubleClickTime = 500.0f;	
	private bool checkForDoubleClick = false;	
	private DateTime timeAtFirstClick;

	private void CheckMouseClicks()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (MouseClick != null) 
			{//Debug.Log ("Checkign mouse clicks " + this.gameObject);
				MouseClick(this, new LeftButton_Handler((int)Input.mousePosition.x, (int)Input.mousePosition.y, 0));
			}
		}	
		
		if (Input.GetMouseButtonUp (0))
		{
			if (MouseClick != null) 
			{
				MouseClick(this, new LeftButton_Handler((int)Input.mousePosition.x, (int)Input.mousePosition.y, 0)
				{
					buttonUp = true,
				});
			}
		}
		
		if (Input.GetMouseButtonUp(1))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 8 | 1 << 9 | 1 << 12 | 1 << 13))
			{
				//Right Clicked on unit or building
				if (MouseClick != null) 
				{
					MouseClick(this, new RightButton_Handler((int)Input.mousePosition.x, (int)Input.mousePosition.y, 1, hit.collider.gameObject.GetComponent<RTSObject>()));
				}
			}
			else if (Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 11 | 1 << 18))
			{
				//Right clicked on terrain
				if (MouseClick != null) 
				{
					MouseClick(this, new RightButton_Handler((int)Input.mousePosition.x, (int)Input.mousePosition.y, 1, hit.point));
				}
			}
		}
	
		if (Input.GetAxis ("Mouse ScrollWheel") != 0)
		{
			if (MouseScrollWheel != null) 
			{
				MouseScrollWheel(this, new ScrollWheelEventArgs(Input.GetAxis ("Mouse ScrollWheel")));
			}
		}
		
		if (checkForDoubleClick)
		{
			if ((DateTime.Now-timeAtFirstClick).Milliseconds >= doubleClickTime)
			{
				checkForDoubleClick = false;
			}
		}
	}
	
	private void DoubleClickCheck(object sender, MouseEventArgs e)
	{
		if (e.doubleClick ) return;//|| e.buttonUp moved this out because of weird double click select all, then deselct bug
		
		if (checkForDoubleClick)
		{
			TimeSpan timeBetweenClicks = DateTime.Now-timeAtFirstClick;
			
			if (timeBetweenClicks.Milliseconds < doubleClickTime)
			{
				e.doubleClick = true;	
			//	Debug.Log ("It is double " + Time.frameCount);
			}
		}
		else
		{
			checkForDoubleClick = true;
			timeAtFirstClick = DateTime.Now;
		}		
	}
}
