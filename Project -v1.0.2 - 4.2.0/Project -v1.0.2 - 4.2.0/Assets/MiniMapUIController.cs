using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MiniMapUIController : MonoBehaviour {

    public Image img;
    
    public RaceManager raceMan;
    public float minimapUpdateRate;

    private float topLeftCornerX = 782f;
    private float topLeftCornerY = 40.3f;
    private float topLeftCornerZ = 1427f;

    private float bottomRightCornerX = 1381f;
    private float bottomRightCornerZ = 769f;

    private Texture2D texture;

    private float nextActionTime;

    // Use this for initialization
    void Start () {
        //img.sprite = CreateTexture();
        texture = CreateTexture();
        Sprite newSprite = Sprite.Create(texture as Texture2D, new Rect(0f, 0f, textureWidth, textureHeight), Vector2.zero);
        img.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += 1f;
            updateUnitsOnMinimap(texture);
        }
    }

    private int textureHeight = 100, textureWidth = 100;
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
        
        return texToReturn;
    }
    private void updateUnitsOnMinimap(Texture2D tex)
    {
        foreach (GameObject unit in raceMan.getUnitList())
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    float unitWorldX = unit.GetComponent<Transform>().position.x;
                    float unitWorldZ = unit.GetComponent<Transform>().position.z;

                    //Scale unit x and z coordinates down to 1, then rescale back up to minimap scale
                    int iCoord = i + (int)( ( (topLeftCornerX - unitWorldX) / unitWorldX) * (float)textureWidth);
                    int jCoord = j + (int)( ( (topLeftCornerZ - unitWorldZ) / unitWorldZ) * (float)textureHeight);
                    
                    tex.SetPixel(iCoord, jCoord, Color.black);
                }
              }
        }
        tex.Apply();
    }
}
