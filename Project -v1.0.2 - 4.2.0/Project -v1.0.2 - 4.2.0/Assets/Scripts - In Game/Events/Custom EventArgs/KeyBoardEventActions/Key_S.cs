using UnityEngine;
using System.Collections;

public class Key_S : KeyBoardEventArgs {

	public Key_S() : base(KeyCode.S)
	{
		
	}
	
	public Key_S(bool keyDown, bool keyUp) : base(KeyCode.S, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		selectedManager.GiveOrder (Orders.CreateStopOrder ());
	}
}
