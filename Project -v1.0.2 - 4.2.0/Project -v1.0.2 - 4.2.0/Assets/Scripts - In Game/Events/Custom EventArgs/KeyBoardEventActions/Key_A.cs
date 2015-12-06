using UnityEngine;
using System.Collections;

public class Key_A : KeyBoardEventArgs {
	
	public Key_A() : base(KeyCode.A)
	{
		
	}
	
	public Key_A(bool keyDown, bool keyUp) : base(KeyCode.A, keyDown, keyUp)
	{
		
	}

	public override void Command ()
	{
		
	}
}
