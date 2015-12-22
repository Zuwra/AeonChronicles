using UnityEngine;
using System.Collections;
using System.Linq;

public class CursorManager : MonoBehaviour, ICursorManager {

	private Cursor[] Cursors;
	private Cursor currentCursor;
	private float cursorSize = 20.0f;
	
	private bool m_ShowCursor = false;
	Rect pointer;
	public static CursorManager main;
	
	void Awake()
	{
		main = this;
		UnityEngine.Cursor.visible = false;
	}
	
	void Start()
	{	 pointer = new Rect(Input.mousePosition.x-(0/2), Screen.height-Input.mousePosition.y-(0/2), cursorSize, cursorSize);
		Cursor[] temp = GameObject.FindGameObjectWithTag ("Cursors").GetComponents<Cursor>();
		Cursors = new Cursor[temp.Length];
		
		foreach (Cursor c in temp)
		{
			Cursors[c.ID] = c;
		}
		
		currentCursor = Cursors[0];
		
		
	}
	
	void Update()
	{
		if (currentCursor.IsAnimated)
		{
			currentCursor.Animate (Time.deltaTime);
		}
		//GUI.DrawTexture (new Rect(Input.mousePosition.x-(0/2), Screen.height-Input.mousePosition.y-(0/2), cursorSize, cursorSize), currentCursor.GetCursorPicture());
	}
	
	public void UpdateCursor(InteractionState interactionState)
	{	
		currentCursor = Cursors[(int)interactionState];		
	}
	
	void OnGUI()
	{


		GUI.DrawTexture (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorSize, cursorSize), currentCursor.GetCursorPicture ());		/*pointer.Set (Input.mousePosition.x-(0/2), Screen.height-Input.mousePosition.y-(0/2), cursorSize, cursorSize);
		GUI.DrawTexture ((pointer), currentCursor.GetCursorPicture());
		
		Debug.Log ("Here");
		if (m_ShowCursor)
		{
			GUI.depth = -2;
			//Draw Cursor
			float offset;
			if (currentCursor.CenterTexture)
			{
				offset = cursorSize;
			}
			else
			{
				offset = 0;
			}
			//GUI.DrawTexture (new Rect(Input.mousePosition.x-(offset/2), Screen.height-Input.mousePosition.y-(offset/2), cursorSize, cursorSize), currentCursor.GetCursorPicture());
		}*/
	}
	
	public void HideCursor()
	{
		m_ShowCursor = false;
	}
	
	public void ShowCursor()
	{
		m_ShowCursor = true;
	}
}
