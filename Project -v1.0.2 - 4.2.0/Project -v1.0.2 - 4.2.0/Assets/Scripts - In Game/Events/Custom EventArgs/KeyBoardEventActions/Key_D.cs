using UnityEngine;
using System.Collections;

public class Key_D : KeyBoardEventArgs {
	
	public Key_D() : base(KeyCode.D)
	{
		
	}
	
	public Key_D(bool keyDown, bool keyUp) : base(KeyCode.D, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
	 	selectedManager.GiveOrder (Orders.CreateDeployOrder ());
	}
}
