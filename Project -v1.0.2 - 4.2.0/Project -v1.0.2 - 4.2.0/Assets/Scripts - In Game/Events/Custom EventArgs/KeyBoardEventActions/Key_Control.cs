using UnityEngine;
using System.Collections;

public class Key_Control : KeyBoardEventArgs {

	public Key_Control() : base(KeyCode.LeftControl)
	{
		
	}
	
	public Key_Control(bool keyDown, bool keyUp) : base(KeyCode.LeftControl, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (KeyDown)
		{
			uiManager.IsControlDown = true;
		}
		else
		{
			uiManager.IsControlDown = false;
		}
	}
}
