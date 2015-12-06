using UnityEngine;
using System.Collections;

public class Key_4 : KeyBoardEventArgs {

	public Key_4() : base(KeyCode.Keypad4)
	{
		
	}
	
	public Key_4(bool keyDown, bool keyUp) : base(KeyCode.Keypad4, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (4);
		}
		else
		{
			selectedManager.SelectGroup (4);
		}
	}
}
