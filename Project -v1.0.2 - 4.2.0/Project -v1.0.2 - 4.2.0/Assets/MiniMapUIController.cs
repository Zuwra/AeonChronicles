using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MiniMapUIController : MonoBehaviour {

    public Image img;
    
    public RaceManager raceMan;
    private GameManager gameMan;
    public float minimapUpdateRate;

    private float topLeftCornerX = 782f;
    private float topLeftCornerY = 40.3f;
    private float topLeftCornerZ = 1427f;

    private float bottomLeftCornerX = 746f;
    private float bottomLeftCornerZ = 726f;

    private float topRightCornerX = 1400f;
    private float topRightCornerZ = 1400f;

    private float bottomRightCornerX = 1447f;
    private float bottomRightCornerZ = 730f;
    
    private int textureHeight = 200, textureWidth = 200;
    private float unitX = 1009f;
    private float unitZ = 1000f;
    private bool floatAfterInt = false;

    private int unitPixelSize = 3;

    private Texture2D texture;

    private float nextActionTime;

    // Use this for initialization
    void Start () {
        //img.sprite = CreateTexture();
        gameMan = GameObject.FindObjectOfType<GameManager>();
        CreateTexture();
        Sprite newSprite = Sprite.Create(texture as Texture2D, new Rect(0f, 0f, textureWidth, textureHeight), Vector2.zero);
        img.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += minimapUpdateRate;
            //updateUnitsOnMinimap(texture);
            clearTexture(false);
            floatAfterInt = true;
        }
        if (floatAfterInt)
        {
            updateUnitsOnMinimap(texture);
            floatAfterInt = false;
        }
    }

    public void CreateTexture()
    {
        texture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
        clearTexture(true);
        
    }
    private void clearTexture(bool apply)
    {
        for (int i = 0; i < textureWidth; i++)
        {
            for (int j = 0; j < textureHeight; j++)
            {
                texture.SetPixel(i, j, Color.clear);
            }
        }
        if (apply)
            texture.Apply();
    }
    private void updateUnitsOnMinimap(Texture2D tex)
    {
        foreach (RaceManager race in gameMan.playerList)
        {
            Color raceColor = getColorForRaceManager(race);
            
            foreach (GameObject unit in race.getUnitList())
            {
                for (int i = -unitPixelSize; i <= unitPixelSize; i++)
                {
                    for (int j = -unitPixelSize; j <= unitPixelSize; j++)
                    {
                        float unitWorldX = unit.GetComponent<Transform>().position.x;
                        float unitWorldZ = unit.GetComponent<Transform>().position.z;

                        //Scale unit x and z coordinates down to 1, then rescale back up to minimap scale
                        int iCoord = i + (int)(((unitWorldX - bottomLeftCornerX) / (topRightCornerX - bottomLeftCornerX)) * (float)textureWidth);
                        int jCoord = j + (int)(((unitWorldZ - bottomLeftCornerZ) / (topRightCornerZ - bottomRightCornerZ)) * (float)textureHeight);
                        
                        tex.SetPixel(iCoord, jCoord, raceColor);
                    }
                }
            }
        }
        tex.Apply();
    }
    private Color getColorForRaceManager(RaceManager raceMan)
    {
        //#1 - Green
        //#2 - Red
        //#3 - Grey
        if (raceMan.playerNumber == 1)
        {
            return Color.green;
        }
        else if (raceMan.playerNumber == 2)
        {
            return Color.red;
        }
        else
        {
            return Color.gray;
        }
    }
}
