using UnityEngine;
using System.Collections;

public static class Overlays {
	
	private static int Height = 100, Width = 100;
	private static int HealthHeight = 8;
	

	private static Color HealthColour = Color.green;

	public static Texture2D CreateTexture()
	{
		Texture2D texToReturn = new Texture2D(Width, Height, TextureFormat.ARGB32, false);
	
		
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Height; j++)
			{
				//if (i == 0 || i == 1 || j == 0 || j == 1 || i == Width-1 || i == Width-2 || j == Height-1 || j == Height-2 || j == Height-HealthHeight)
				if(j < Height-HealthHeight)
				{

					texToReturn.SetPixel (i, j, Color.clear);
				}
				else if (j > Height-HealthHeight)
				{
					texToReturn.SetPixel (i, j, HealthColour);
				}
				else
				{
					texToReturn.SetPixel (i, j, Color.clear);
				}
			}
			texToReturn.SetPixel(i,Height-1,Color.clear);
			texToReturn.SetPixel(i,Height-HealthHeight,Color.black);
		}
		texToReturn.Apply ();
		return texToReturn;
	}
	
	public static void UpdateTexture(Texture2D overlay, float healthRatio, int ticks)
	{//Debug.Log ("Updating health amount" + healthRatio);

		Color currentColor = Color.green;
	



		if (healthRatio < .25) {

			currentColor = Color.red;
		} else if (healthRatio < .6){

			currentColor = Color.yellow;
		}

		for (int i=0; i<Width; i++)
		{
			for (int j=Height-HealthHeight; j<Height; j++)
			{	if(( i %  (100 /ticks) )==0 || i %  (100 /ticks) ==1 )
					{
					
					overlay.SetPixel (i, j, Color.black);
				}
				else if ((float)i/(float)Width < healthRatio)
				{
					overlay.SetPixel (i, j, currentColor);
				}
			
				else
				{
					overlay.SetPixel (i, j, Color.clear);
				}
			}
			overlay.SetPixel(i,Height-1,Color.clear);
			overlay.SetPixel(i,Height-HealthHeight,Color.black);
		}
		overlay.Apply ();

	}



	public static void UpdateEnergy(Texture2D overlay,float energyRatio)
	{//Debug.Log ("Updating health amount" + healthRatio);
		

		
		for (int i=0; i<Width; i++)
		{
			for (int j=Height-HealthHeight-4; j<Height-HealthHeight; j++)
			{
				if ((float)i/(float)Width < energyRatio)
				{
					overlay.SetPixel (i, j, Color.blue);
				}

			}
		}
		overlay.Apply ();
		
	}

	public static void cooldown(Texture2D overlay, float ratio)
	{	Debug.Log (ratio);
		for (int i=0; i<Width; i++)
		{
			for (int j=Height-2*HealthHeight-4; j<Height-2*HealthHeight; j++)
			{
				if ((float)i / (float)Width < ratio) {
					overlay.SetPixel (i, j, Color.white);
				} else {
					overlay.SetPixel (i, j, Color.clear);
				}

			}
		}
		overlay.Apply ();


	}



}
