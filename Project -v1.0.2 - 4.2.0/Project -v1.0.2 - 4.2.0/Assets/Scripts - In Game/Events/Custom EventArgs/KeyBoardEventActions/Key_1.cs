using UnityEngine;
using System.Collections;

public class Key_1 : KeyBoardEventArgs {

	public Key_1() : base(KeyCode.Keypad1)
	{
		
	}
	
	public Key_1(bool keyDown, bool keyUp) : base(KeyCode.Keypad1, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (1);
		}
		else
		{
			selectedManager.SelectGroup (1);
		}
	}
}
