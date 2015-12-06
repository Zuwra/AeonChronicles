using UnityEngine;
using System.Collections;
using System;

public class MaintenanceButtonEventArgs : EventArgs {
	
	public int Button;

	public MaintenanceButtonEventArgs(int button)
	{
		Button = button;
	}
}
