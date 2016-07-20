using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour, IGUIManager {
	
	//Singleton
	public static GUIManager main;
	
	//Member Variables



	public bool Dragging
	{
		get;
		set;
	}
	
	public Rect DragArea
	{
		get;
		set;
	}
	

	
	void Awake()
	{
		//Set singleton
		main = this;

	}

	// Use this for initialization
	void Start () 
	{
		//Load the mini map and assign the menu width and mini map rect
		//IMiniMapController miniMap = ManagerResolver.Resolve<IMiniMapController>();		
		GameObject.FindObjectOfType<MiniMapController> ().LoadMiniMap ();
		//miniMap.LoadMiniMap();
	

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Tell all items that are being built to update themselves
		//GUIEvents.TellItemsToUpdate(Time.deltaTime);
		

	}

	
	public bool IsWithin(Vector3 worldPos)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		Vector3 realScreenPos = new Vector3(screenPos.x, Screen.height-screenPos.y, screenPos.z);
		
		if (DragArea.Contains (realScreenPos))
		{
			return true;
		}
		
		return false;
	}
	





}
