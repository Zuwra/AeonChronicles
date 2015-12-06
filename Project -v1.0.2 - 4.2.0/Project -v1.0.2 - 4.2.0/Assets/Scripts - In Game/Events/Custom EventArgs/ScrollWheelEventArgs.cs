using System;

public class ScrollWheelEventArgs : EventArgs {

	public float ScrollValue;
	
	public ScrollWheelEventArgs(float ScrollValue)
	{
		this.ScrollValue = ScrollValue;
	}
}
