using UnityEngine;
using System.Collections;

public class Key_Escape : KeyBoardEventArgs {

	public Key_Escape() : base(KeyCode.Escape)
	{
		
	}
	
	public Key_Escape(bool keyDown, bool keyUp) : base(KeyCode.Escape, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		
	}
}
