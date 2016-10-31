using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class MiniMapUIController : MonoBehaviour, IPointerDownHandler , IPointerUpHandler {

    public Image img;
	public Image ScreenTrapz;
	private bool[,] virtTrapezoid;
	private Texture2D screenTrapzoidTex;


	private bool[,] virtUnitTexture;
	private Texture2D UnitTexture;

    public RaceManager raceMan;
    private GameManager gameMan;
    public float minimapUpdateRate;
	public Image fogger;
	public int unitPixelSize = 3;

    public float Left = 726f; 
  
	private bool dragging;
    public float top = 1400f; 
   
	public float Right = 1447f;
	public float bottom = 730f;


	private float WorldHeight;
	private float WorldWidth;
 
    private int textureHeight = 190, textureWidth = 190;

 
	public int scalingfactor =1;

	public float heightOffset;
   

    private float nextActionTimea;
	private float nextActionTimeb;
	private float nextActionTimec;

	//Used for detecting MinimapClicks
	private float minimapWidth;
	private float minimapHeight;
	private RectTransform myRect;

	public GameObject megaMap;


	private FogOfWar fog;
	Texture2D _texture;
	GUIStyle _panelStyle;


	Ray ray1 ;

	//Top left
	Ray ray2 ;

	//Top right
	Ray ray3 ;

	//Bottom right
	Ray ray4 ;

	Texture2D panelTex ;


    // Use this for initialization
    void Start () {

		// Use for Fog of War
		Texture2D panelTex = new Texture2D (1, 1);
		panelTex.SetPixels32 (new Color32[] { new Color32 (255, 255, 255, 64) });
		panelTex.Apply ();
		_panelStyle = new GUIStyle ();
		_panelStyle.normal.background = panelTex;


		myRect = GetComponent<RectTransform> ();
		minimapWidth = myRect.rect.width ;
		minimapHeight = myRect.rect.height ;

		nextActionTimea = 0;
		nextActionTimeb = Time.time + minimapUpdateRate/2;
		nextActionTimeb = Time.time + minimapUpdateRate/3;


		WorldHeight = top - bottom;
		WorldWidth = Right - Left;
		fog = GameObject.FindObjectOfType<FogOfWar> ();
        
        gameMan = GameObject.FindObjectOfType<GameManager>();
        
		textureWidth *= scalingfactor;
		textureHeight *= scalingfactor;
		virtUnitTexture = new bool[textureWidth,textureHeight];
		virtTrapezoid = new bool[textureWidth,textureHeight];
		screenTrapzoidTex = InitialTexture (screenTrapzoidTex);


		UnitTexture = InitialTexture (UnitTexture);


		img.sprite  = Sprite.Create(UnitTexture as Texture2D, new Rect(0f, 0f, textureWidth, textureHeight), Vector2.zero);

		setFog ();
		fogger.sprite = Sprite.Create(_texture as Texture2D, new Rect(0f, 0f, fog.texture.width, fog.texture.height), Vector2.zero);

		GameMenu.main.addDisableScript (this);
		ScreenTrapz.sprite = Sprite.Create (screenTrapzoidTex as Texture2D, new Rect (0f, 0f, textureWidth, textureHeight), Vector2.zero);

   
	
	
	
	
	}

    // Update is called once per frame
    void Update()
    {
		if (dragging) {
			mapMover ();
		}

		if (Time.time > nextActionTimea) {
			nextActionTimea += minimapUpdateRate;
			updateTexture (UnitTexture, virtUnitTexture);


		} 
		if (Time.time > nextActionTimec) {
			nextActionTimec += minimapUpdateRate;

			updateScreenRect ();

		} 


		if (Time.time > nextActionTimeb) {
			nextActionTimeb += minimapUpdateRate;
			setFog();

		}

		if (Input.GetKeyUp (KeyCode.Tab)) {
		
			attackMoveMinimap ();
		
		}

		if (Input.GetKeyDown (KeyCode.M)) {
			toggleMegaMap ();
		}
    }

	public void toggleMegaMap()
	{
		if (megaMap) {
			megaMap.SetActive (!megaMap.activeSelf);
		}
	}

	private Texture2D InitialTexture(Texture2D tex )
	{ tex = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);

		for (int i = 0; i < textureWidth; i++)
		{
			for (int j = 0; j < textureHeight; j++)
			{
				tex .SetPixel(i, j, Color.clear);
			}
		}
		tex.Apply ();
		return tex;
	}


 
	private void clearTexture(Texture2D tex ,bool[,] virtMap, bool apply)
    {
        for (int i = 0; i < textureWidth; i++)
        {
            for (int j = 0; j < textureHeight; j++)
            {
				if (virtMap [i,j]) {
					virtMap [i,j] = false;
					tex .SetPixel(i, j, Color.clear);
				}

            }
        }
        if (apply)
			tex.Apply();
    }



	private void updateTexture(Texture2D tex, bool[,] virtMap)
	{
		clearTexture (tex, virtMap, false);

		for(int i = 2; i >-1; i --){
		//foreach (RaceManager race in gameMan.playerList) { // Loops 3 times
			Color raceColor = getColorForRaceManager (gameMan.playerList[i]);
            
			foreach (GameObject unit in gameMan.playerList[i].getUnitList()) { // Loops 0 -100 ish timesif(unit){
				
				if (unit == null) {
					continue;}
			
               
				int iCoord = (int)(((unit.transform.position.x - Left) / (WorldWidth)) * textureWidth);
				int jCoord = (int)(((unit.transform.position.z - bottom) / (WorldHeight)) * textureHeight);
				int chitSize = unitPixelSize;
				if (unit.layer == 10) {
					chitSize *= 2;
				}

				for (int n = -chitSize; n <= chitSize; n++)
                {

					for (int j = -chitSize; j <=chitSize; j++)
					{
						try{
						virtMap [n+ iCoord,j+ jCoord] = true;
							tex.SetPixel(n + iCoord, j + jCoord, raceColor);}
						catch(Exception)
						{
							
						}
                    }
					}}
            }
		tex.Apply();
        }



		private void updateScreenRect (){
		clearTexture (screenTrapzoidTex, virtTrapezoid, false);

		// DRAWING CAMERA TRAPEZOID
		Vector2 topLeftP = Vector2.zero;
		Vector2 topRightP= Vector2.zero;
		Vector2 botLeftP= Vector2.zero;
		Vector2 botRightP= Vector2.zero;



		ray1 = Camera.main.ScreenPointToRay (new Vector3(0,180,0));

		//Top left
		ray2 = Camera.main.ScreenPointToRay (new Vector3(0, Screen.height-1, 0));

		//Top right
		ray3 = Camera.main.ScreenPointToRay (new Vector3(Screen.width, Screen.height-1, 0));

		//Bottom right
		ray4 = Camera.main.ScreenPointToRay (new Vector3(Screen.width, 180, 0));






		// LOTS OF COMMENTED OUT STUFF FOR OPTIMIZATIONS!
	
		//Find world co-ordinates
		RaycastHit hit;
		Physics.Raycast (ray1, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v1 = hit.point;

	
		//int iC = (int)(((v1.x - Left) / (WorldWidth)) * textureWidth);
		//int jC = (int)(((v1.z - bottom) / (WorldHeight)) * textureHeight);
	
		botLeftP = new Vector2 ((int)(((v1.x - Left) / (WorldWidth)) * textureWidth)    ,  (int)(((v1.z - bottom) / (WorldHeight)) * textureHeight));


		Physics.Raycast (ray2, out hit, Mathf.Infinity, 1 << 16);
		//Vector3 
		v1 = hit.point;
		//iC = (int)(((v2.x - Left) / (WorldWidth)) * textureWidth);
		//jC = (int)(((v2.z - bottom) / (WorldHeight)) * textureHeight);
	
		topLeftP = new Vector2 ((int)(((v1.x - Left) / (WorldWidth)) * textureWidth), (int)(((v1.z - bottom) / (WorldHeight)) * textureHeight));

		Physics.Raycast (ray3, out hit, Mathf.Infinity, 1 << 16);
		//Vector3
		v1 = hit.point;
		//iC = (int)(((v3.x - Left) / (WorldWidth)) * textureWidth);
		//jC = (int)(((v3.z - bottom) / (WorldHeight)) * textureHeight);
	
		topRightP = new Vector2 ((int)(((v1.x - Left) / (WorldWidth)) * textureWidth),(int)(((v1.z - bottom) / (WorldHeight)) * textureHeight));


		Physics.Raycast (ray4, out hit, Mathf.Infinity, 1 << 16);
		//Vector3
		v1 = hit.point;
		//Debug.Log ("Hit " + hit.collider.gameObject);
		//iC = (int)(((v4.x - Left) / (WorldWidth)) * textureWidth);
		//jC = (int)(((v4.z - bottom) / (WorldHeight)) * textureHeight);

		botRightP = new Vector2 ((int)(((v1.x - Left) / (WorldWidth)) * textureWidth), (int)(((v1.z - bottom) / (WorldHeight)) * textureHeight));

	//	Debug.Log (topRightP + "   " + botRightP + "  " +hit.collider.gameObject);
		drawLine(screenTrapzoidTex,virtTrapezoid, topLeftP,topRightP);

		drawLine(screenTrapzoidTex, virtTrapezoid,botLeftP,botRightP);
		drawLine(screenTrapzoidTex, virtTrapezoid,botLeftP,topLeftP);
		//Debug.Log ("Drawing Right Line " + botRightP + "  " + topRightP);
		drawLine(screenTrapzoidTex, virtTrapezoid,botRightP,topRightP);

		screenTrapzoidTex.Apply();
        
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


	public void drawLine(Texture2D tex,bool[,] virtMap ,Vector2 p1, Vector2 p2)
	{
		Vector2 t = p1;
		float frac = 1 / (Mathf.Pow (p2.x - p1.x, 2) + Mathf.Pow (p2.y - p1.y,2));
		float ctr = 0;

		while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y) {

			t = Vector2.Lerp (p1, p2, ctr);
			ctr += frac;


			if (t.x > 0 && t.y < tex.height && t.x <tex.width)
			{
				virtMap [(int)t.x,(int)t.y] = true;
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
	

		//Debug.Log ("Size is " + fog.texture.width);
		byte[] original = fog.texture.GetRawTextureData ();
		Color32[] pixels = new Color32[original.Length];
		for (int i = 0; i < pixels.Length; i++) {
			pixels [i] = original [i] < 255 ? new Color32 (0, 0, 0, 0) : new Color32 (0, 0, 0, 255);
		
		}
		_texture.SetPixels32 (pixels);
		_texture.Apply ();
	

	}

	public void mapMover()
	{Vector3 clickPos = transform.InverseTransformPoint (Input.mousePosition);

		float x = (clickPos.x) / this.GetComponent<RectTransform> ().rect.width;// minimapWidth;
		float y = (clickPos.y) /this.GetComponent<RectTransform> ().rect.height;

		Vector2 toMove = new Vector2 ((x + .5f) * WorldWidth + Left, (y + .5f) *MainCamera.main.getBoundries().height -50+ bottom-Mathf.Tan(Mathf.Deg2Rad *MainCamera.main.AngleOffset ) * MainCamera.main.HeightAboveGround);

		MainCamera.main.minimapMove (toMove);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			dragging = false;
		
		}
			
	}

	public void OnPointerDown(PointerEventData eventData)
	{if (!this.enabled) {
			return;}
		if (eventData.button == PointerEventData.InputButton.Left) {
			
			dragging = true;
			mapMover ();
			//GetComponent<RectTransform> ().rect.width;
		
			//Debug.Log ( "Clicked    " + toMove.x + "   " + toMove.y);

		} else if (eventData.button == PointerEventData.InputButton.Right) {
		

			Vector3 clickPos = transform.InverseTransformPoint (eventData.pressPosition);
			float x = .5f +(clickPos.x) / minimapWidth;
			float y = .5f + (clickPos.y) /minimapHeight;
			Vector3 RayPoint = new Vector3 ((x * WorldWidth) +Left, 100, (y * WorldHeight) + bottom);

			RaycastHit hit;		

			if (Physics.Raycast (RayPoint, Vector3.down, out hit, 400, ~(1 << 16))) {
	
				//Debug.Log ("moving to " + hit.point);
				SelectedManager.main.GiveOrder (Orders.CreateMoveOrder (hit.point));
			}
		}


	}


	public void attackMoveMinimap()
	{
		if (!this.enabled) {
			return;}
		Vector3 clickPos =  Input.mousePosition;
		clickPos.x -= this.GetComponent<RectTransform> ().position.x;
		clickPos.y -=this.GetComponent<RectTransform> ().position.y;
		float x = .5f +(clickPos.x) / minimapWidth;
		float y = .5f + (clickPos.y) /minimapHeight;
		Vector3 RayPoint = new Vector3 ((x * WorldWidth) +Left, 100, (y * WorldHeight) + bottom);

		RaycastHit hit;		

		if (Physics.Raycast (RayPoint, Vector3.down, out hit, 400, ~(1 << 16))) {

			//Debug.Log ("moving to " + hit.point);
			SelectedManager.main.attackMoveO(hit.point);
		}
	}




}
