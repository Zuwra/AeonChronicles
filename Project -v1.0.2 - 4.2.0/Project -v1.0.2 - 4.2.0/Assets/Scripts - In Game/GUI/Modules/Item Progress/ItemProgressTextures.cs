using UnityEngine;
using System.Collections;

public static class ItemProgressTextures {
	
	private static Texture2D[] m_Overlays = new Texture2D[360];
	private static Color m_MaskColor;
	private static int m_TextureSize = 200;
	
	public static Texture2D[] Overlays
	{
		get
		{
			return m_Overlays;
		}
	}

	// Use this for initialization
	public static void Initialise() 
	{
		//Set up mask color
		m_MaskColor = new Color(0, 1, 0, 0.5f);
		
		//Create overlays
		for (int i=0; i<360; i++)
		{
			m_Overlays[i] = createTexture (i);
		}
	}
	
	private static Texture2D createTexture(int i)
	{
		Color blank = new Color(0,0,0,0);
		Texture2D texture = new Texture2D(m_TextureSize, m_TextureSize, TextureFormat.ARGB32, false); 
		//Calcuelate line equation
		float m, c, yCut;
		
		for (int x=0; x<m_TextureSize; x++)
		{
			for (int y=0; y<m_TextureSize; y++)
			{
				if (i<90)
				{
					if (x < m_TextureSize/2 || y < m_TextureSize/2)
					{
						texture.SetPixel (x, y, m_MaskColor);
					}
					else
					{
						if (i == 0)
						{
							texture.SetPixel (x, y, m_MaskColor);
						}
						else
						{
							//Do some math
							m = Mathf.Tan ((90-i)*Mathf.Deg2Rad);
							c = (m_TextureSize/2)-((m_TextureSize/2)*m);
							
							//Find y cut off for the x value
							yCut = (m*x)+c;
							
							if (y < yCut)
							{
								texture.SetPixel (x, y, m_MaskColor);
							}
							else
							{
								texture.SetPixel (x, y, blank);
							}
						}
					}
				}
				else if (i<180)
				{
					if (x < m_TextureSize/2)
					{
						texture.SetPixel (x, y, m_MaskColor);
					}
					else if (x >= m_TextureSize/2 && y > m_TextureSize/2)
					{
						texture.SetPixel (x, y, blank);
					}
					else
					{
						if (i == 90)
						{
							texture.SetPixel (x, y, m_MaskColor);
						}
						else
						{
							//Do some math
							m = Mathf.Tan ((90-i)*Mathf.Deg2Rad);
							c = (m_TextureSize/2)-((m_TextureSize/2)*m);
							
							//Find y cut off for the x value
							yCut = (m*x)+c;
							
							if (y < yCut)
							{
								texture.SetPixel (x, y, m_MaskColor);
							}
							else
							{
								texture.SetPixel (x, y, blank);
							}
						}
					}
				}
				else if (i<270)
				{
					if (x < m_TextureSize/2 && y > m_TextureSize/2)
					{
						texture.SetPixel (x, y, m_MaskColor);
					}
					else if (x >= (m_TextureSize/2))
					{
						texture.SetPixel (x, y, blank);
					}
					else
					{
						if (i == 180)
						{
							texture.SetPixel (x, y, m_MaskColor);
						}
						else
						{
							//Do some math
							m = Mathf.Tan ((90-i)*Mathf.Deg2Rad);
							c = (m_TextureSize/2)-((m_TextureSize/2)*m);
							
							//Find y cut off for the x value
							yCut = (m*x)+c;
							
							if (y < yCut)
							{
								texture.SetPixel (x, y, blank);
							}
							else
							{
								texture.SetPixel (x, y, m_MaskColor);
							}
						}
					}
				}
				else
				{
					if (x >= m_TextureSize/2 || y < m_TextureSize/2)
					{
						texture.SetPixel (x, y, blank);
					}
					else
					{
						if (i == 270)
						{
							texture.SetPixel (x, y, m_MaskColor);
						}
						else
						{
							//Do some math
							m = Mathf.Tan ((90-i)*Mathf.Deg2Rad);
							c = (m_TextureSize/2)-((m_TextureSize/2)*m);
							
							//Find y cut off for the x value
							yCut = (m*x)+c;
							
							if (y < yCut)
							{
								texture.SetPixel (x, y, blank);
							}
							else
							{
								texture.SetPixel (x, y, m_MaskColor);
							}
						}
					}
				}
			}
		}
   		texture.Apply();
		return texture;
	}
}