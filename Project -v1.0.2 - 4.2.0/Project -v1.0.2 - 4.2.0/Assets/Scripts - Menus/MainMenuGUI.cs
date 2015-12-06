using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour {
	
	private Rect m_MenuArea = new Rect();
	private float m_SpaceBetweenButtons = 50.0f;
	public int m_NumberOfButtons = 4;
	
	private ILevelLoader m_LevelLoader;
	
	//List to contain button rects
	private List<Rect> m_ButtonRects = new List<Rect>();
	
	private ILevelLoader levelLoader
	{
		get
		{
			return m_LevelLoader ?? (m_LevelLoader = ManagerResolver.Resolve<ILevelLoader>());
		}
	}
	
	void Awake()
	{
		//Set MenuArea Rect up
		float width = Screen.width/3.0f;
		float height = Screen.height/1.5f;
		
		m_MenuArea.xMin = Screen.width/3.0f;
		m_MenuArea.xMax = m_MenuArea.xMin+width;
		m_MenuArea.yMin = (Screen.height-height)/2;
		m_MenuArea.yMax = m_MenuArea.yMin+height;
		
		//Find up button width and height
		float buttonWidth = width/2.0f;
		float buttonHeight = (height/m_NumberOfButtons)-(m_SpaceBetweenButtons);
		float startX = width/4.0f;
		float startY = 0;
		
		//Set up button Rects
		for (int i=0; i<m_NumberOfButtons; i++)
		{
			m_ButtonRects.Add (new Rect (startX, startY, buttonWidth, buttonHeight));
			startY += buttonHeight + m_SpaceBetweenButtons;
		}
	}

	// Use this for initialization
	void Start () 
	{
		//m_LevelLoader = ManagerResolver.Resolve<ILevelLoader>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		GUI.BeginGroup (m_MenuArea);
		
		if (GUI.Button (m_ButtonRects[0], "New Game"))
		{
			LevelLoader.main.LoadLevel (1);
		}
		
		if (GUI.Button (m_ButtonRects[1], "Load"))
		{
			
		}
		
		if (GUI.Button (m_ButtonRects[2], "Settings"))
		{
			
		}
		
		if (GUI.Button (m_ButtonRects[3], "Quit"))
		{
			Application.Quit ();
		}
		
		GUI.EndGroup ();
	}
}
