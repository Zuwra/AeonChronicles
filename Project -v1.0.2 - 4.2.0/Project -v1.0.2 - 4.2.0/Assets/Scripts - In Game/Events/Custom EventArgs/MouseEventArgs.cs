using System;
using UnityEngine;

public abstract class MouseEventArgs : EventArgs {

	public int X;
	public int Y;	
	public int button;
	public bool doubleClick = false;
	public Vector3 WorldPosClick;
	public RTSObject target;
	public bool buttonUp = false;
	
	public MouseEventArgs(int x, int y, int button)
	{
		this.X = x;
		this.Y = y;
		this.button = button;
	}
	
	public MouseEventArgs(int x, int y, int button, Vector3 worldPos)
	{
		this.X = x;
		this.Y = y;
		this.button = button;
		WorldPosClick = worldPos;
	}
	
	public MouseEventArgs(int x, int y, int button, RTSObject target)
	{
		this.X = x;
		this.Y = y;
		this.button = button;
		this.target = target;
	}
	
	public abstract void Command();
}
