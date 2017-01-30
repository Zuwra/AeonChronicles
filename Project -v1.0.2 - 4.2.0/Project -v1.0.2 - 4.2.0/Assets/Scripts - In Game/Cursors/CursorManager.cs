using UnityEngine;
using System.Collections;
using System.Linq;

public class CursorManager : MonoBehaviour {

	public Texture2D[] Cursors;
	private int currentMode =0;

	public static CursorManager main;
	
	void Awake()
	{
		main = this;
		//UnityEngine.Cursor.visible = false;
	}
	
	void Start()
	{	

		UnityEngine.Cursor.SetCursor (Cursors [0], new Vector2 (0, 0), CursorMode.ForceSoftware);
		
	}


	public int getMode()
	{
		return currentMode;
	}


	public void normalMode()
	{if (currentMode != 0) {
			currentMode = 0;
			UnityEngine.Cursor.SetCursor (Cursors [0], new Vector2 (0, 0), CursorMode.ForceSoftware);
		}
	}

	public void attackMode()
	{
		if (currentMode != 1) {
			currentMode = 1;
			UnityEngine.Cursor.SetCursor (Cursors [1], new Vector2 (16, 16), CursorMode.ForceSoftware);
		}
	}

	public void targetMode()
		{if (currentMode != 2) {
				currentMode = 2;
		UnityEngine.Cursor.SetCursor (Cursors [2], new Vector2 (32, 32), CursorMode.ForceSoftware);
			}}
		

	public void invalidMode()
	{
		if (currentMode != 3) {
			currentMode = 3;
			UnityEngine.Cursor.SetCursor (Cursors [3], new Vector2 (16, 16), CursorMode.ForceSoftware);
		}
	}
	public void offMode()
	{
					if (currentMode != 4) {
						currentMode = 4;
			UnityEngine.Cursor.SetCursor (Cursors [4], new Vector2 (0, 0), CursorMode.ForceSoftware);}
				}

	public void selectMode()
	{
		if (currentMode != 5) {
			currentMode = 5;
			UnityEngine.Cursor.SetCursor (Cursors [5], new Vector2 (16, 16), CursorMode.ForceSoftware);}
	}

	public void MouseDragMode()
	{if (currentMode != 6) {
			currentMode = 6;
			UnityEngine.Cursor.SetCursor (Cursors [6], new Vector2 (16, 16), CursorMode.ForceSoftware);}
		
	}
}
