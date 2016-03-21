using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MiniMapUIController : MonoBehaviour {

    public Image img;
    
    public RaceManager raceMan;
    private GameManager gameMan;
    public float minimapUpdateRate;

	public int unitPixelSize = 3;

 
    public float Left = 726f; 
  

    public float top = 1400f; 
   
	public float Right = 1447f;
	public float bottom = 730f;


	private float WorldHeight;
	private float WorldWidth;

 
    private int textureHeight = 200, textureWidth = 200;

    private bool floatAfterInt = false;

    private Texture2D texture;

    private float nextActionTime;

    // Use this for initialization
    void Start () {
        //img.sprite = CreateTexture();
        gameMan = GameObject.FindObjectOfType<GameManager>();
        CreateTexture();
        Sprite newSprite = Sprite.Create(texture as Texture2D, new Rect(0f, 0f, textureWidth, textureHeight), Vector2.zero);
        img.sprite = newSprite;

		WorldHeight = top - bottom;
		WorldWidth = Right - Left;
    }

    // Update is called once per frame
    void Update()
    {
		if (floatAfterInt)
		{
			updateUnitsOnMinimap(texture);
			floatAfterInt = false;
		}

        if (Time.time > nextActionTime)
        {
            nextActionTime += minimapUpdateRate;
            //updateUnitsOnMinimap(texture);
            clearTexture(false);
            floatAfterInt = true;
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
        foreach (RaceManager race in gameMan.playerList) // Loops 3 times
        {
            Color raceColor = getColorForRaceManager(race);
            
            foreach (GameObject unit in race.getUnitList()) // Loops 0 -100 ish times
			{ if(unit){
				float unitWorldX = unit.transform.position.x;
				float unitWorldZ = unit.transform.position.z;
               
				int iCoord = (int)(((unitWorldX - Left) / (WorldWidth)) * textureWidth);
				int jCoord = (int)(((unitWorldZ - bottom) / (WorldHeight)) * textureHeight);

				for (int i = -unitPixelSize; i <= unitPixelSize; i++)
                {

                    for (int j = -unitPixelSize; j <= unitPixelSize; j++)
                    {
                        tex.SetPixel(i + iCoord, j + jCoord, raceColor);
                    }
					}}
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
