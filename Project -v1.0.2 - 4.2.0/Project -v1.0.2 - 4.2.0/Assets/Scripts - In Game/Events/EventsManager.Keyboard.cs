using UnityEngine;
using System.Collections;

public partial class EventsManager {
	
	private void CheckKeyBoardPresses()
	{
		if (Input.GetKeyDown (KeyCode.Keypad1))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_1());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad2))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_2());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad3))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_3());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad4))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_4());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad5))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_5());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad6))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_6());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad7))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_7());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad8))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_8());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad9))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_9());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad0))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_0());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.A))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_A());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.D))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_D());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.H))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_H());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.S))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_S());
			}
		}
		
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (KeyAction != null)
			{
				KeyAction(this, new Key_Escape());
			}
		}
		
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
