using UnityEngine;
using System.Collections;

public class Key_H : KeyBoardEventArgs {

	public Key_H() : base(KeyCode.H)
	{
		
	}
	
	public Key_H(bool keyDown, bool keyUp) : base(KeyCode.H, keyDown, keyUp)
	{
		
	}

	public override void Command()
	{
		
	}
}
