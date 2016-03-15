using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MiniMapUIController : MonoBehaviour {

    public Image img;

    public RaceManager raceMan;

    private float topLeftCornerX = 782f;
    private float topLeftCornerY = 40.3f;
    private float topLeftCornerZ = 1427f;

    private float bottomRightCornerX = 1381f;
    private float bottomRightCornerZ = 769f;

	// Use this for initialization
	void Start () {
        //img.sprite = CreateTexture();
        Sprite newSprite = Sprite.Create(CreateTexture() as Texture2D, new Rect(0f, 0f, textureWidth, textureHeight), Vector2.zero);
        img.sprite = newSprite;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private int textureHeight = 100, textureWidth = 100;
    private int separator = 10;
    private int numUnits = 4;

    private float unitX = 1009f;
    private float unitZ = 906f;

    public Texture2D CreateTexture()
    {
        Texture2D texToReturn = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
        for (int i = 0; i < textureWidth; i++)
        {
            for (int j = 0; j < textureHeight; j++)
            {
                texToReturn.SetPixel(i, j, Color.clear);
            }
        }
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                //TODO: Currently does not map unit to correct spot on minimap.

                //int iCoord = i + (int)( ( (topLeftCornerX - unitX) / unitX) * (float)textureWidth);
                //int jCoord = j + (int)( ( (topLeftCornerZ - unitZ) / unitZ) * (float)textureHeight);

                int iCoord = i+30;
                int jCoord = j+70;
                Console.WriteLine(iCoord);
                Console.WriteLine(jCoord);
                texToReturn.SetPixel(iCoord, jCoord, Color.black);
            }
        }
        texToReturn.Apply();
        return texToReturn;
    }

}
