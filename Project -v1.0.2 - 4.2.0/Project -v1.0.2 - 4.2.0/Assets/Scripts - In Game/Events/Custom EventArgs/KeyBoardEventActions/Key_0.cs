using UnityEngine;
using System.Collections;

public class Key_0 : KeyBoardEventArgs {

	public Key_0() : base(KeyCode.Keypad0)
	{
		
	}
	
	public Key_0(bool keyDown, bool keyUp) : base(KeyCode.Keypad0, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (0);
		}
		else
		{
			selectedManager.SelectGroup (0);
		}
	}
}
