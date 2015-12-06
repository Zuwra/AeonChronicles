using UnityEngine;
using System.Collections;
using System;

public abstract class KeyBoardEventArgs : EventArgs {

	public KeyCode Key;
	protected bool KeyDown = true;
	protected bool KeyUp = false;
	protected IUIManager uiManager;
	protected ISelectedManager selectedManager;
	
	public KeyBoardEventArgs(KeyCode key)
	{
		Key = key;
		uiManager = ManagerResolver.Resolve<IUIManager>();
		selectedManager = ManagerResolver.Resolve<ISelectedManager>();
	}
	
	public KeyBoardEventArgs(KeyCode key, bool keyDown, bool keyUp)
	{
		Key = key;
		KeyDown = keyDown;
		KeyUp = keyUp;
		uiManager = ManagerResolver.Resolve<IUIManager>();
		selectedManager = ManagerResolver.Resolve<ISelectedManager>();
	}
	
	public abstract void Command();
}
