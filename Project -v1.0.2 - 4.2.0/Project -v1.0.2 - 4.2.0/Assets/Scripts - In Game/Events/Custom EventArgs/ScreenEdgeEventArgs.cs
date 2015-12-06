using System;

public class ScreenEdgeEventArgs : EventArgs {

	public int x, y;
	public float duration;
	
	public ScreenEdgeEventArgs(int x, int y)
	{
		this.x = x;
		this.y = y;
		this.duration = 0;
	}
	
	public ScreenEdgeEventArgs(int x, int y, float duration)
	{
		this.x = x;
		this.y = y;
		this.duration = duration;
	}	
}
