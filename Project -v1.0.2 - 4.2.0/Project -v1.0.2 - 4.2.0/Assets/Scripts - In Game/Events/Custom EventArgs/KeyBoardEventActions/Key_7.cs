using UnityEngine;
using System.Collections;

public class Key_7 : KeyBoardEventArgs {

	public Key_7() : base(KeyCode.Keypad7)
	{
		
	}
	
	public Key_7(bool keyDown, bool keyUp) : base(KeyCode.Keypad7, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (7);
		}
		else
		{
			selectedManager.SelectGroup (7);
		}
	}
}
