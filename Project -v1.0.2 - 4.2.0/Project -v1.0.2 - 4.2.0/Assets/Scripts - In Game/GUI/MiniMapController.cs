using UnityEngine;
using System.Collections;

public class MiniMapController : MonoBehaviour, IMiniMapController {
	
	//Singleton
	public static MiniMapController main;
	
	//Minimap rect in pixel space
	private Rect m_MiniMapRect;
	
	//Menu Width (needed to calculate correct viewport in minimap)
	private float m_MenuWidth;
	
	//Vectors for viewport co-ordinates in minimap
	private Vector3[] m_ViewPortVectors = new Vector3[4];
	
	private float m_zOffSet;
	private float m_xOffset;
	
	private ICamera m_MainCamera;
	
	private Material mat;
	
	void Awake()
	{
		main = this;
		
		mat = GLMatShader.GetGLMaterial ();
	}

	// Use this for initialization
	void Start () 
	{
		ManagerResolver.Resolve<IEventsManager>().MouseClick += MouseClicked;
		m_MainCamera = ManagerResolver.Resolve<ICamera>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void LoadMiniMap(out float guiWidth, out Rect miniMapRect)
	{
		//Properly configure camera viewport so it's a square and it's in the correct place regardless of resolution
		//Always want the map to appear 3/4 up the screen, with a height of 1/4.5
		float aspectRatio = (float)Screen.width/(float)Screen.height;
		
		float viewPortY = 3.0f/4.0f;
		float viewPortHeight = 1.0f/4.5f;
		
		//Figure width values based on height values
		float viewPortWidth = 1.0f/(4.5f*aspectRatio);
		float viewPortX = 1-(0.25f/aspectRatio);
		
		//Assign camera viewport
		GetComponent<Camera>().rect = new Rect(viewPortX, viewPortY, viewPortWidth, viewPortHeight);
		miniMapRect = GetComponent<Camera>().rect;
		
		//Assign pixel rect
		m_MiniMapRect = new Rect(viewPortX*Screen.width, viewPortY*Screen.height, viewPortWidth*Screen.width, viewPortHeight*Screen.height);
		
		//Now we have the minimap size, determine how wide the GUI should be
		float miniMapX = Camera.main.ViewportToScreenPoint(new Vector3(viewPortX, viewPortY, 0)).x;
		float miniMapX2 = Camera.main.ViewportToScreenPoint(new Vector3(viewPortX+viewPortWidth, viewPortY, 0)).x;
		float miniMapWidth = miniMapX2 - miniMapX;
		float miniMapGap = Screen.width-miniMapX2;
		
		guiWidth = miniMapWidth+(2*miniMapGap);
		m_MenuWidth = guiWidth;
		ManagerResolver.Resolve<ICamera>().SetMenuWidth (m_MenuWidth);
		
		UpdateViewPort ();
		
		//Find map bounds
		RaycastHit hit;
		Ray ray = GetComponent<Camera>().ViewportPointToRay (new Vector3(0,0,0));
		Ray ray2 = GetComponent<Camera>().ViewportPointToRay (new Vector3(1,1,0));
		
		Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 16);
		Vector3 bottomLeft = hit.point;
		
		Physics.Raycast (ray2, out hit, Mathf.Infinity, 1 << 16);
		Vector3 topRight = hit.point;
		
		m_MainCamera.SetBoundries (bottomLeft.x, bottomLeft.z, topRight.x, topRight.z);
	}
	
	public void MouseClicked(object sender, MouseEventArgs e)
	{
		if (!e.buttonUp)
		{
			if (m_MiniMapRect.Contains (Input.mousePosition))
			{
				//Clicked on mini map
				Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 16))
				{
					Vector3 targetPos = new Vector3(hit.point.x-m_xOffset, hit.point.y, hit.point.z-m_zOffSet);
					m_MainCamera.Move(targetPos);
					ReCalculateViewRect();
				}
			}
		}
	}
	
	public void ReCalculateViewRect ()
	{
		UpdateViewPort ();
	}
	
	private void UpdateViewPort()
	{
		//Need to find co-ordinates for the viewing area within the camera viewport
		//Bottom left
		Ray ray1 = Camera.main.ScreenPointToRay (new Vector3(0,0,0));
		
		//Top left
		Ray ray2 = Camera.main.ScreenPointToRay (new Vector3(0, Screen.height-1, 0));
		
		//Top right
		Ray ray3 = Camera.main.ScreenPointToRay (new Vector3(Screen.width-m_MenuWidth, Screen.height-1, 0));
		
		//Bottom right
		Ray ray4 = Camera.main.ScreenPointToRay (new Vector3(Screen.width-m_MenuWidth, 0, 0));
		
		//Find world co-ordinates
		RaycastHit hit;
		Physics.Raycast (ray1, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v1 = hit.point;
		
		Physics.Raycast (ray3, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v2 = hit.point;
		
		Physics.Raycast (ray2, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v3 = hit.point;
		
		Physics.Raycast (ray4, out hit, Mathf.Infinity, 1 << 16);
		Vector3 v4 = hit.point;
		
		//Find Screen co-ordinates for mini map view rect
		m_ViewPortVectors[0] = GetComponent<Camera>().WorldToScreenPoint (v1);
		m_ViewPortVectors[1] = GetComponent<Camera>().WorldToScreenPoint (v3);
		m_ViewPortVectors[2] = GetComponent<Camera>().WorldToScreenPoint (v2);
		m_ViewPortVectors[3] = GetComponent<Camera>().WorldToScreenPoint (v4);
		
		//Ray cast center to find offset between Main Camera position.z and projection center
		Ray ray5 = Camera.main.ScreenPointToRay (new Vector3(Screen.width/2, Screen.height/2, 0));
		if (Physics.Raycast (ray5, out hit, Mathf.Infinity, 1 << 16))
		{
			m_zOffSet = hit.point.z- Camera.main.transform.position.z;
			float midPointX = hit.point.x;
			
			Ray ray6 = Camera.main.ScreenPointToRay (new Vector3((Screen.width-m_MenuWidth)/2, Screen.height/2, 0));
			if (Physics.Raycast (ray6, out hit, Mathf.Infinity, 1 << 16))
			{
				m_xOffset = hit.point.x-midPointX;
			}
			else
			{
				m_xOffset = 0;
			}
		}
		else
		{
			m_zOffSet = 0;
			m_xOffset = 0;
		}
	}
	
	void OnPostRender()
	{
		GL.PushMatrix ();
		mat.SetPass (0);
		GL.LoadPixelMatrix();
		GL.Color (Color.white);
		GL.Begin(GL.LINES);
		
		GL.Vertex (new Vector3(m_ViewPortVectors[0].x, m_ViewPortVectors[0].y, 0));
		GL.Vertex (new Vector3(m_ViewPortVectors[1].x, m_ViewPortVectors[1].y, 0));
		
		GL.Vertex (new Vector3(m_ViewPortVectors[1].x, m_ViewPortVectors[1].y, 0));
		GL.Vertex (new Vector3(m_ViewPortVectors[2].x, m_ViewPortVectors[2].y, 0));
		
		GL.Vertex (new Vector3(m_ViewPortVectors[2].x, m_ViewPortVectors[2].y, 0));
		GL.Vertex (new Vector3(m_ViewPortVectors[3].x, m_ViewPortVectors[3].y, 0));
		
		GL.Vertex (new Vector3(m_ViewPortVectors[3].x, m_ViewPortVectors[3].y, 0));
		GL.Vertex (new Vector3(m_ViewPortVectors[0].x, m_ViewPortVectors[0].y, 0));
		
		GL.Vertex(new Vector3(1, 0, 0));
        GL.Vertex(new Vector3(0, 1, 0));
								
		GL.End ();
		GL.PopMatrix ();
	}
}
