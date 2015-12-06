using UnityEngine;
using System.Collections;

public class Key_9 : KeyBoardEventArgs {

	public Key_9() : base(KeyCode.Keypad9)
	{
		
	}
	
	public Key_9(bool keyDown, bool keyUp) : base(KeyCode.Keypad9, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (9);
		}
		else
		{
			selectedManager.SelectGroup (9);
		}
	}
}
