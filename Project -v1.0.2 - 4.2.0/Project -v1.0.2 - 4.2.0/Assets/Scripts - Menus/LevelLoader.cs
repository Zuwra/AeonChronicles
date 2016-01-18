using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour, ILevelLoader {
	
	//Static Member variable
	private bool m_Loading = false;
	
	//Member variables	
	private Texture2D m_LoadingScreen;
	private Rect m_ScreenRect;
	private Rect m_LabelRect;
	private GUIStyle m_LoadingStyle;
	
	//Singleton
	public static LevelLoader main;
	
	//Loading Message
	private string m_LoadingMessage = "Loading...";

	void Awake()
	{
		//Don't want to destroy this
		DontDestroyOnLoad (this);
		
		//Set singleton
		main = this;
		
		//Generate Black background Texture
		m_LoadingScreen = TextureGenerator.MakeTexture (Color.black);
		
		//Assign Screen Rect
		m_ScreenRect = new Rect(0, 0, Screen.width, Screen.height);
		
		//Set up GUIStyle
		m_LoadingStyle = new GUIStyle();
		m_LoadingStyle.normal.textColor = Color.white;
		m_LoadingStyle.alignment = TextAnchor.MiddleCenter;
		m_LoadingStyle.fontSize = 20;
		
		//Calculate the label Rect
		Vector2 labelSize = m_LoadingStyle.CalcSize (new GUIContent(m_LoadingMessage));
		float labelWidth = labelSize.x;
		float labelHeight = labelSize.y;
		
		float xPos = (Screen.width/2) - (labelWidth/2);
		float yPos = (Screen.height/2) - (labelHeight/2);
		
		m_LabelRect = new Rect(xPos, yPos, labelWidth, labelHeight);
	}
	
	void OnGUI()
	{
		if (m_Loading)
		{
			GUI.DrawTexture (m_ScreenRect, m_LoadingScreen);
			GUI.Label (m_LabelRect, m_LoadingMessage, m_LoadingStyle);
		}
	}
	
	public void LoadLevel(int levelID)
	{
		m_Loading = true;
		//Application.LoadLevelAsync (levelID);
		SceneManager.LoadScene(levelID);
	//	Application.LoadLevel (levelID);
	}
	
	void OnLevelWasLoaded()
	{
		//m_Loading = false;
	}
	
	public void FinishLoading()
	{
		m_Loading = false;
	}
	
	public void ChangeText(string text)
	{
		m_LoadingMessage = text;
		
		//Calculate the label Rect
		Vector2 labelSize = m_LoadingStyle.CalcSize (new GUIContent(m_LoadingMessage));
		float labelWidth = labelSize.x;
		float labelHeight = labelSize.y;
		
		float xPos = (Screen.width/2) - (labelWidth/2);
		float yPos = (Screen.height/2) - (labelHeight/2);
		
		m_LabelRect = new Rect(xPos, yPos, labelWidth, labelHeight);
	}
}
