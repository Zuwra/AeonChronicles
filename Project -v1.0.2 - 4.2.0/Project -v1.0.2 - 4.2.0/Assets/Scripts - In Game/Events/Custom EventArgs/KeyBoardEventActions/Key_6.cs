using UnityEngine;
using System.Collections;

public class Key_6 : KeyBoardEventArgs {

	public Key_6() : base(KeyCode.Keypad6)
	{
		
	}
	
	public Key_6(bool keyDown, bool keyUp) : base(KeyCode.Keypad6, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (6);
		}
		else
		{
			selectedManager.SelectGroup (6);
		}
	}
}
