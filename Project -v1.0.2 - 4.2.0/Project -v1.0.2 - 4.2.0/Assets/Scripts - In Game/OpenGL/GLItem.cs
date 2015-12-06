using UnityEngine;
using System.Collections;

public class GLItem {
	
	public delegate void ExecuteFunction();
	public ExecuteFunction ExecuteCommand;
	
	public GLItem(ExecuteFunction executeCommand)
	{
		ExecuteCommand = executeCommand;
	}
}
