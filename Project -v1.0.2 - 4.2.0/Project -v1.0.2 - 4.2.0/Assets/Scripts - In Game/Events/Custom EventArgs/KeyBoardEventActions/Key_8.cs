using UnityEngine;
using System.Collections;

public class Key_8 : KeyBoardEventArgs {

	public Key_8() : base(KeyCode.Keypad8)
	{
		
	}
	
	public Key_8(bool keyDown, bool keyUp) : base(KeyCode.Keypad8, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (8);
		}
		else
		{
			selectedManager.SelectGroup (8);
		}
	}
}
