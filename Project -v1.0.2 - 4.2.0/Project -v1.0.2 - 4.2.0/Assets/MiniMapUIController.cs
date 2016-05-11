using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class MiniMapUIController : MonoBehaviour, IPointerClickHandler  {

    public Image img;
	public Image ScreenTrapz;
	private Texture2D screenTrap;
    
    public RaceManager raceMan;
    private GameManager gameMan;
    public float minimapUpdateRate;
	public Image fogger;
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

    private float nextActionTimea;
	private float nextActionTimeb;

	//Used for detecting MinimapClicks
	private float minimapWidth;
	private float minimapHeight;
	private RectTransform myRect;



	private FogOfWar fog;
	Texture2D _texture;
	GUIStyle _panelStyle;

    // Use this for initialization
    void Start () {
		myRect = GetComponent<RectTransform> ();
		minimapWidth = myRect.rect.width;
		minimapHeight = myRect.rect.height;

		nextActionTimea = 0;
		nextActionTimeb = Time.time + minimapUpdateRate/2;

		WorldHeight = top - bottom;
		WorldWidth = Right - Left;
		fog = GameObject.FindObjectOfType<FogOfWar> ();
        
        gameMan = GameObject.FindObjectOfType<GameManager>();
        
		CreateTexture();
		img.sprite  = Sprite.Create(texture as Texture2D, new Rect(0f, 0f, textureWidth, textureHeight), Vector2.zero);

		setFog ();
		fogger.sprite = Sprite.Create(_texture as Texture2D, new Rect(0f, 0f, fog.texture.width, fog.texture.height), Vector2.zero);

		screenTrap = CreateTexture (screenTrap);
		ScreenTrapz.sprite = Sprite.Create (screenTrap as Texture2D, new Rect (0f, 0f, textureWidth, textureHeight), Vector2.zero);

    }

    // Update is called once per frame
    void Update()
    {
		if (floatAfterInt)
		{

			updateUnitsOnMinimap(texture);
			updateScreenRect ();
			floatAfterInt = false;


		}

		if (Time.time > nextActionTimea) {
			nextActionTimea += minimapUpdateRate;
			clearTexture (screenTrap, false);
			clearTexture (texture, false);
			floatAfterInt = true;

		} 
			

		if (Time.time > nextActionTimeb) {
			nextActionTimeb += minimapUpdateRate;
			setFog();

		}
      
    }

	public Texture2D CreateTexture(Texture2D input)
	{
		input = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
		for (int i = 0; i < textureWidth; i++)
		{
			for (int j = 0; j < textureHeight; j++)
			{
				input.SetPixel(i, j, Color.clear);
			}
		}

			input.Apply();
		return input;
	}


    public void CreateTexture()
    {
        texture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
        clearTexture(texture ,true);
        
    }
    private void clearTexture(Texture2D tex , bool apply)
    {
        for (int i = 0; i < textureWidth; i++)
        {
            for (int j = 0; j < textureHeight; j++)
            {
				tex .SetPixel(i, j, Color.clear);
            }
        }
        if (apply)
			tex.Apply();
    }


    private void updateUnitsOnMinimap(Texture2D tex)
	{
		foreach (RaceManager race in gameMan.playerList) { // Loops 3 times
			Color raceColor = getColorForRaceManager (race);
            
			foreach (GameObject unit in race.getUnitList()) { // Loops 0 -100 ish timesif(unit){
				if (unit == null) {
					continue;}
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
		tex.Apply();
        }



		private void updateScreenRect (){
		// DRAWING CAMERA TRAPEZOID
		Vector2 topLeftP = Vector2.zero;
		Vector2 topRightP= Vector2.zero;
		Vector2 botLeftP= Vector2.zero;
		Vector2 botRightP= Vector2.zero;



		//Need to find co-ordinates for the viewing area within the camera viewport
		//Bottom left
		Ray ray1 = Camera.main.ScreenPointToRay (new Vector3(0,0,0));

		//Top left
		Ray ray2 = Camera.main.ScreenPointToRay (new Vector3(0, Screen.height-1, 0));

		//Top right
		Ray ray3 = Camera.main.ScreenPointToRay (new Vector3(Screen.width, Screen.height-1, 0));

		//Bottom right
		Ray ray4 = Camera.main.ScreenPointToRay (new Vector3(Screen.width, 0, 0));

		//Find world co-ordinates
		RaycastHit hit;
		Physics.Raycast (ray1, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v1 = hit.point;
		int iC = (int)(((v1.x - Left) / (WorldWidth)) * textureWidth);
		int jC = (int)(((v1.z - bottom) / (WorldHeight)) * textureHeight);
	
		botLeftP = new Vector2 (iC, jC);


		Physics.Raycast (ray2, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v2 = hit.point;
		iC = (int)(((v2.x - Left) / (WorldWidth)) * textureWidth);
		jC = (int)(((v2.z - bottom) / (WorldHeight)) * textureHeight);
	
		topLeftP = new Vector2 (iC, jC);

		Physics.Raycast (ray3, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v3 = hit.point;
		iC = (int)(((v3.x - Left) / (WorldWidth)) * textureWidth);
		jC = (int)(((v3.z - bottom) / (WorldHeight)) * textureHeight);
	
		topRightP = new Vector2 (iC, jC);


		Physics.Raycast (ray4, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v4 = hit.point;
		iC = (int)(((v4.x - Left) / (WorldWidth)) * textureWidth);
		jC = (int)(((v4.z - bottom) / (WorldHeight)) * textureHeight);

		botRightP = new Vector2 (iC, jC);

		if(screenTrap == null)
		{CreateTexture (screenTrap);}

	//	Debug.Log (topRightP + "   " + botRightP + "  " +hit.collider.gameObject);
		drawLine(screenTrap, topLeftP,topRightP);

		drawLine(screenTrap, botLeftP,botRightP);
		drawLine(screenTrap, botLeftP,topLeftP);
		drawLine(screenTrap, botRightP,topRightP);

		screenTrap.Apply();
        
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


	public void drawLine(Texture2D tex, Vector2 p1, Vector2 p2)
	{
		Vector2 t = p1;
		float frac = 1 / (Mathf.Pow (p2.x - p1.x, 2) + Mathf.Pow (p2.y - p1.y,2));
		float ctr = 0;

		while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y) {

			t = Vector2.Lerp (p1, p2, ctr);
			ctr += frac;


			if (t.x > 0 && t.y < tex.height)
			{
				
				tex.SetPixel ((int)t.x, (int)t.y, Color.magenta);
			}
		
		}
	
	}


	public void setFog()
	{

		if (_texture == null) {
			_texture = new Texture2D (fog.texture.width, fog.texture.height);
			_texture.wrapMode = TextureWrapMode.Clamp;
		}

		if (_panelStyle == null) {
			Texture2D panelTex = new Texture2D (1, 1);
			panelTex.SetPixels32 (new Color32[] { new Color32 (255, 255, 255, 64) });
			panelTex.Apply ();
			_panelStyle = new GUIStyle ();
			_panelStyle.normal.background = panelTex;
		}

		byte[] original = fog.texture.GetRawTextureData ();
		Color32[] pixels = new Color32[original.Length];
		for (int i = 0; i < pixels.Length; ++i)
			pixels [i] = original [i] < 255 ? new Color32 (0, 0, 0, 0) : new Color32 (0, 0, 0, 255);
		_texture.SetPixels32 (pixels);
		_texture.Apply ();

	}


	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {

			Vector3 clickPos = transform.InverseTransformPoint (eventData.pressPosition);
			float x = (clickPos.x ) /minimapWidth;
			float y = (clickPos.y ) / minimapHeight;

			Vector2 toMove = new Vector2 (x * WorldWidth,y * WorldHeight);
			MainCamera.main.minimapMove(toMove);
			//GetComponent<RectTransform> ().rect.width;
		
			//Debug.Log ( "Clicked    " + toMove.x + "   " + toMove.y);

		}


	}




}
