using UnityEngine;
using System.Collections;

public class Key_Shift : KeyBoardEventArgs {

	public Key_Shift() : base(KeyCode.LeftShift)
	{
		
	}
	
	public Key_Shift(bool keyDown, bool keyUp) : base(KeyCode.LeftShift, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (KeyDown)
		{
			uiManager.IsShiftDown = true;
		}
		else
		{
			uiManager.IsShiftDown = false;
		}
	}
}
