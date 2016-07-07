using UnityEngine;
using System.Collections;

public partial class EventsManager {
	
	private void CheckKeyBoardPresses()
	{
		

		
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_Shift());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.LeftControl) || Input.GetKeyDown (KeyCode.RightControl))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_Control());
			}
		}
		
		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_Shift(false, true));
			}
		}
		
		if (Input.GetKeyUp (KeyCode.LeftControl) || Input.GetKeyUp (KeyCode.RightControl))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_Control(false, true));
			}
		}
	}
}
