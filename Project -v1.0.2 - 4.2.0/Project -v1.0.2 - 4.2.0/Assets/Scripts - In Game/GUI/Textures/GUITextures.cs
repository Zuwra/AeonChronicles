using UnityEngine;
using System.Collections;

public static class GUITextures 
{
	private static Texture2D m_TypeButtonNormal;
	private static Texture2D m_TypeButtonHover;
	private static Texture2D m_TypeButtonSelected;
	
	private static Texture2D m_QueueButtonNormal;
	private static Texture2D m_QueueButtonHover;
	private static Texture2D m_QueueButtonSelected;
	
	private static Texture2D m_QueueContentNormal;
	private static Texture2D m_QueueContentHover;
	private static Texture2D m_QueueContentSelected;
	
	public static Texture2D TypeButtonNormal
	{
		get
		{
			return m_TypeButtonNormal ?? (m_TypeButtonNormal = Resources.Load ("GUI/Buttons/TypeButtons/Normal") as Texture2D);
		}
	}
	
	public static Texture2D TypeButtonHover
	{
		get
		{
			return m_TypeButtonHover ?? (m_TypeButtonHover = Resources.Load ("GUI/Buttons/TypeButtons/Hover") as Texture2D);
		}
	}
	
	public static Texture2D TypeButtonSelected
	{
		get
		{
			return m_TypeButtonSelected ?? (m_TypeButtonSelected = Resources.Load ("GUI/Buttons/TypeButtons/Selected") as Texture2D);
		}
	}
}
