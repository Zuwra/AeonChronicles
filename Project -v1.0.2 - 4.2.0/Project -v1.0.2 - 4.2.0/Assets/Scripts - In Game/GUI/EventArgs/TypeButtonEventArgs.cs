using System;
using System.Collections;
using UnityEngine;

public class TypeButtonEventArgs : EventArgs {

	public ButtonType ButtonClicked;
	
	public TypeButtonEventArgs(ButtonType button)
	{
		ButtonClicked = button;
	}
}
