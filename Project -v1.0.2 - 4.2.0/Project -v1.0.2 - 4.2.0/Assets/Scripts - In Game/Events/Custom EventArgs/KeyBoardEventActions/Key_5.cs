using UnityEngine;
using System.Collections;

public class Key_5 : KeyBoardEventArgs {

	public Key_5() : base(KeyCode.Keypad5)
	{
		
	}
	
	public Key_5(bool keyDown, bool keyUp) : base(KeyCode.Keypad5, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		if (uiManager.IsControlDown)
		{
			selectedManager.AddUnitsToGroup (5);
		}
		else
		{
			selectedManager.SelectGroup (5);
		}
	}
}
