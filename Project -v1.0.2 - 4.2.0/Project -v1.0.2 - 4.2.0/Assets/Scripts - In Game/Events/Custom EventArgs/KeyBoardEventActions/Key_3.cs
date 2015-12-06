using UnityEngine;
using System.Collections;

public class Key_3 : KeyBoardEventArgs {

	public Key_3() : base(KeyCode.Keypad3)
	{
		
	}
	
	public Key_3(bool keyDown, bool keyUp) : base(KeyCode.Keypad3, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (3);
		}
		else
		{
			selectedManager.SelectGroup (3);
		}
	}
}
