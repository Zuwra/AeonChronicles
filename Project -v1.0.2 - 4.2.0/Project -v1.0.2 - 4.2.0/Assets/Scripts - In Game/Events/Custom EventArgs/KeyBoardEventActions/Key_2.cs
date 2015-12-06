using UnityEngine;
using System.Collections;

public class Key_2 : KeyBoardEventArgs {

	public Key_2() : base(KeyCode.Keypad2)
	{
		
	}
	
	public Key_2(bool keyDown, bool keyUp) : base(KeyCode.Keypad2, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (2);
		}
		else
		{
			selectedManager.SelectGroup (2);
		}
	}
}
