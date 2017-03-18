using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.EventSystems;

public class MiniMapUIController : MonoBehaviour, IPointerDownHandler , IPointerUpHandler {

    public Image img;
	public Image ScreenTrapz;
	private bool[,] virtTrapezoid;
	private Texture2D screenTrapzoidTex;


	public GameObject warningSymbol;
	public Sprite defaultWarning;
	private bool[,] virtUnitTexture;
	private Texture2D UnitTexture;

    public RaceManager raceMan;
    private GameManager gameMan;
    public float minimapUpdateRate;
	public Image fogger;
	public int unitPixelSize = 3;

    float Left = 726f; 
  
	private bool dragging;
    float top = 1400f; 
   
	float Right = 1447f;
	float bottom = 730f;


	private float WorldHeight;
	private float WorldWidth;
 
    private int textureHeight = 190, textureWidth = 190;

 
	public int scalingfactor =1;

	public float heightOffset;
   

    private float nextActionTimea;

	private float nextActionTimec;

	//Used for detecting MinimapClicks
	private float minimapWidth;
	private float minimapHeight;
	private RectTransform myRect;

	public GameObject megaMap;
	private Point[,] PointArray;

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
	RectTransform newParent;

	MainCamera myCam;


	float UIWidth;
	float UIHeight;

	public void AwakeInitialize(){
		if (newParent != null) {
			Awake ();}

	}

	void Awake()
	{   bool wasOn = transform.parent.gameObject.activeSelf;
		transform.parent.gameObject.SetActive (true);
		newParent = (RectTransform)this.transform.parent.FindChild ("ScreenTrap");
		UIWidth = newParent.rect.width;
		UIHeight = newParent.rect.height;
		transform.parent.gameObject.SetActive (wasOn);
	}
    // Use this for initialization
    public void Initialize() {

		Debug.Log ("Starting");
		myCam = GameObject.FindObjectOfType<MainCamera> ();
		Left = myCam.getBoundries ().xMin;
		Right = myCam.getBoundries ().xMax;
		top = myCam.getBoundries ().yMax;
		bottom = myCam.getBoundries ().yMin;
		
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


		usedUnitPoints = new List<Point> ();
		usedTriangleList = new List<Point> ();
		img.sprite  = Sprite.Create(UnitTexture as Texture2D, new Rect(0f, 0f, textureWidth, textureHeight), Vector2.zero);

		setFog ();
		fogger.sprite = Sprite.Create(_texture as Texture2D, new Rect(0f, 0f, fog.texture.width, fog.texture.height), Vector2.zero);

		GameMenu.main.addDisableScript (this);
		ScreenTrapz.sprite = Sprite.Create (screenTrapzoidTex as Texture2D, new Rect (0f, 0f, textureWidth, textureHeight), Vector2.zero);

		PointArray = new Point[textureWidth,textureHeight];
		for (int i = 0; i < textureWidth; i++) {
			for (int j = 0; j < textureHeight; j++) {
				PointArray [i, j] = new Point (i, j);
			}
		}
		WidthScale = textureWidth/ WorldWidth;
		HeightScale = textureHeight / WorldHeight;
   
		InvokeRepeating ("updateScreenRect", .1f, minimapUpdateRate);
		InvokeRepeating ("setFog", .05f, minimapUpdateRate);
	
	
	}


	public void showWarning(Vector3 location)
	{
		StartCoroutine (displayWarning (location, defaultWarning));
	}

	public void showWarning(Vector3 location, Sprite myImage)
	{
		StartCoroutine (displayWarning (location, myImage));
	}

	IEnumerator displayWarning( Vector3 location, Sprite symbol)
	{
		int iCoord = (int)(((location.x - Left) / (WorldWidth)) *UIWidth);
		int jCoord = (int)(((location.z - bottom) / (WorldHeight)) *UIHeight);

		//Debug.Log ("Creating warning");


		GameObject obj = (GameObject)Instantiate (warningSymbol, new Vector2(iCoord, jCoord), Quaternion.identity, newParent);
		obj.GetComponent<Image> ().sprite = symbol;
		obj.transform.localPosition = new Vector2 (iCoord - UIWidth/2 , jCoord - UIHeight/2);
		yield return new WaitForSeconds (10);
		Destroy (obj);
	}

	List<GameObject> currentIcons = new List<GameObject> ();

	public GameObject showUnitIcon( Vector3 location, Sprite symbol)
	{
		//RectTransform newParent = (RectTransform)this.transform.parent.FindChild ("ScreenTrap");
		int iCoord = (int)(((location.x - Left) / (WorldWidth)) * UIWidth);
		int jCoord = (int)(((location.z - bottom) / (WorldHeight)) * UIHeight);

		//Debug.Log ("Creating warning");


		GameObject obj = (GameObject)Instantiate (warningSymbol, new Vector2(iCoord, jCoord), Quaternion.identity, newParent);
		obj.GetComponent<Image> ().sprite = symbol;
		obj.transform.localPosition = new Vector2 (iCoord - UIWidth/2 , jCoord - UIHeight/2);
		currentIcons.Add (obj);
		return obj;
	}

	public void deleteUnitIcon(GameObject iconObj)
	{
		if (currentIcons.Contains (iconObj)) {
			currentIcons.Remove (iconObj);
			Destroy (iconObj);
		}
	}

	public void updateUnitPos(GameObject sprite, Vector3 location)
	{
		if (currentIcons.Contains (sprite)) {
			
			int iCoord = (int)(((location.x - Left) / (WorldWidth)) * UIWidth);
			int jCoord = (int)(((location.z - bottom) / (WorldHeight)) *UIHeight);
			sprite.transform.localPosition = new Vector2 (iCoord - UIWidth / 2, jCoord - UIHeight / 2);
		}
	}






    // Update is called once per frame
    void Update()
    {

		if (Time.time > nextActionTimea) {
			nextActionTimea += minimapUpdateRate;
			updateTexture (UnitTexture, virtUnitTexture);

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


 
	private void clearTexture(Texture2D tex ,bool[,] virtMap, bool apply, List<Point> used)
    {
		//Debug.Log ("Points " + used.Count);
		foreach (Point p in used) {
			virtMap [p.x,p.y] = false;
			tex .SetPixel(p.x,p.y, Color.clear);
		}
		used.Clear ();
		/*
        for (int i = 0; i < textureWidth; i++)
        {
            for (int j = 0; j < textureHeight; j++)
            {
				if (virtMap [i,j]) {
					virtMap [i,j] = false;
					tex .SetPixel(i, j, Color.clear);
				}

            }
        }*/
        if (apply)
			tex.Apply();
    }

	class Point
	{

		public Point(int X, int Y)
		{x = X;
			y = Y;}
		public int x, y;
	}

	List<Point> usedUnitPoints;
	List<Point> usedTriangleList;
	float WidthScale;
	float HeightScale;
	private void updateTexture(Texture2D tex, bool[,] virtMap)
	{
		//Debug.Log ("A " + Environment.TickCount);

		clearTexture (tex, virtMap, false, usedUnitPoints);
		//Debug.Log ("B " + Environment.TickCount);
		int ChitX;
		int ChitY;

		int iCoord;
		int jCoord;
		int chitSize; 

		for(int i = 0; i <3; i ++){
		//foreach (RaceManager race in gameMan.playerList) { // Loops 3 times
			Color raceColor = getColorForRaceManager (gameMan.playerList[i]);
            
			foreach (GameObject unit in gameMan.playerList[i].getUnitList()) { // Loops 0 -100 ish timesif(unit){
				
				if (unit == null) {
					continue;}
				
				chitSize = unitPixelSize;
				if (unit.layer == 10) {
					chitSize *= 2;
				}
			
				iCoord = (int)((unit.transform.position.x - Left) *WidthScale);
				jCoord = (int)((unit.transform.position.z - bottom) *HeightScale);


				for (int n = -chitSize; n <= chitSize; n++)
                {

					for (int j = -chitSize; j <=chitSize; j++)
					{
						ChitX = n + iCoord;
						ChitY = j + jCoord;
						try{
							if(!virtMap [ChitX,ChitY]){	
								
								usedUnitPoints.Add(PointArray[ChitX,ChitY]);
								virtMap [ChitX,ChitY] = true;
								tex.SetPixel(ChitX,ChitY, raceColor);}}
						catch(Exception)
						{
							
						}
                    }
					}
				}
			
            }
		//Debug.Log ("C " + Environment.TickCount);
		//Debug.Log ("Drawing " + counter);
		tex.Apply();

        }

	Vector2 topLeftP;
	Vector2 topRightP;
	Vector2 botLeftP;
	Vector2 botRightP;
	RaycastHit hit;

		private void updateScreenRect (){
		clearTexture (screenTrapzoidTex, virtTrapezoid, false,usedTriangleList);


		//Debug.Log ("A" + DateTime.Now.Millisecond );
		// DRAWING CAMERA TRAPEZOID
		topLeftP = Vector2.zero;
		topRightP= Vector2.zero;
		botLeftP= Vector2.zero;
		botRightP= Vector2.zero;



		ray1 = Camera.main.ScreenPointToRay (new Vector3(0,150,0));

		//Top left
		ray2 = Camera.main.ScreenPointToRay (new Vector3(0, Screen.height-1, 0));

		//Top right
		ray3 = Camera.main.ScreenPointToRay (new Vector3(Screen.width, Screen.height-1, 0));

		//Bottom right
		ray4 = Camera.main.ScreenPointToRay (new Vector3(Screen.width, 150, 0));

	//	Debug.Log ("B" + DateTime.Now.Millisecond );

		// LOTS OF COMMENTED OUT STUFF FOR OPTIMIZATIONS!
	
		//Find world co-ordinates

		Physics.Raycast (ray1, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v1 = hit.point;

	
		//int iC = (int)(((v1.x - Left) / (WorldWidth)) * textureWidth);
		//int jC = (int)(((v1.z - bottom) / (WorldHeight)) * textureHeight);
	
		botLeftP = new Vector2 ((int)(((v1.x - Left) / (WorldWidth)) * textureWidth),  (int)(((v1.z - bottom) / (WorldHeight)) * textureHeight));


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
		//Debug.Log ("C" + DateTime.Now.Millisecond );
	//	Debug.Log (topRightP + "   " + botRightP + "  " +hit.collider.gameObject);

		drawLine(screenTrapzoidTex,virtTrapezoid, topLeftP,topRightP);
		drawLine(screenTrapzoidTex, virtTrapezoid,botLeftP,botRightP);
		drawLine(screenTrapzoidTex, virtTrapezoid,botLeftP,topLeftP);
		drawLine(screenTrapzoidTex, virtTrapezoid,botRightP,topRightP);

		screenTrapzoidTex.Apply();
		//Debug.Log ("D" + DateTime.Now.Millisecond );
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
	
		float Iterate = Mathf.Max (Mathf.Abs (p1.x - p2.x), Mathf.Abs (p1.y - p2.y));
		float ctr = 0;
		int counter = 0;
		while (((int)t.x != (int)p2.x || (int)t.y != (int)p2.y) && counter <= Iterate) {
			counter++;
			t = Vector2.Lerp (p1, p2, ctr);
		
			ctr += 1/Iterate;

			if (t.x > 0 && t.y < tex.height && t.x <tex.width)
				{
				try{
				usedTriangleList.Add (PointArray[(int)t.x, (int)t.y]);
				virtMap [(int)t.x,(int)t.y] = true;
					tex.SetPixel ((int)t.x, (int)t.y, Color.magenta);}
				catch(Exception){
				}
			}
		
		}

	
	}


	Color32[] pixelsArray;
	Color32 transParent = new Color32 (0, 0, 0, 0);
	Color32 blackColor = new Color32 (0, 0, 0, 255);
	public void setFog()
	{
		if (_texture == null) {
			_texture = new Texture2D (fog.texture.width, fog.texture.height);
			_texture.wrapMode = TextureWrapMode.Clamp;
		}
	
		if (!fog.HasUnFogged) {
			return;
		}

		//Debug.Log ("Size is " + fog.texture.width);
		byte[] original = fog.getTexture().GetRawTextureData ();
		if (pixelsArray == null) {
			pixelsArray  = new Color32[original.Length];
		}
			
		for (int i = 0; i < pixelsArray.Length; i++) {
			
			pixelsArray[i] = original [i] < 255 ? transParent : blackColor;
		
		}
		_texture.SetPixels32  (pixelsArray);
		_texture.Apply ();
	

	}

	public void mapMover()
	{Vector3 clickPos = transform.InverseTransformPoint (Input.mousePosition);

		float x = ((clickPos.x) / myRect.rect.width )+ .5f;// minimapWidth;
		float y = ((clickPos.y) /myRect.rect.height) + .5f;

		Vector2 toMove = new Vector2 ((x ) * WorldWidth + Left, 
			y  *WorldHeight - 65+ bottom-  Mathf.Tan(Mathf.Deg2Rad *MainCamera.main.AngleOffset ) * (MainCamera.main.transform.position.y - MainCamera.main.m_MinFieldOfView));

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
			StartCoroutine (DragMini ());

		
		
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


	IEnumerator DragMini()
	{
		while (dragging) {
			mapMover ();
			yield return null;
		}

	}

	public void attackMoveMinimap()
	{
		if (!this.enabled) {
			return;}
		Vector3 clickPos =  Input.mousePosition;
		clickPos.x -= myRect.position.x;
		clickPos.y -=myRect.position.y;
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
