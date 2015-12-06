using UnityEngine;
using System.Collections;
using System;

//Delegates
public delegate void MouseActions(object sender, MouseEventArgs e);
public delegate void ScrollActions(object sender, ScrollWheelEventArgs e);
public delegate void ScreenEdgeActions(object sender, ScreenEdgeEventArgs e);
public delegate void KeyBoardActions(object sender, KeyBoardEventArgs e);

public partial class EventsManager : MonoBehaviour, IEventsManager {
	
	//--------------------------EVENTS-------------------------	
	public event MouseActions MouseClick;
	public event ScrollActions MouseScrollWheel;
	public event ScreenEdgeActions ScreenEdgeMousePosition;
	public event KeyBoardActions KeyAction;
	//-----------------------------------------------------------------------
		
	//--------------------------SINGLETON--------------
	public static EventsManager main;
	//---------------------------------------------------	
	
	void Awake()
	{
		main = this;
		
		MouseClick += DoubleClickCheck;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{		
		CheckMouseClicks ();		
		CheckKeyBoardPresses ();
		CheckScreenEdgeEvents ();		
	}
}
