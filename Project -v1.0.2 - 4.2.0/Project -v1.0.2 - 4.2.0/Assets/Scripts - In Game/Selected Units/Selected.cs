using UnityEngine;
using System.Collections;

public class Selected : MonoBehaviour {
		
	public bool IsSelected
	{
		get;
		private set;
	}

	private bool m_JustBeenSelected = false;
	private float m_JustBeenSelectedTimer = 0;
	private GLItem m_GLItem;
	private Material m_GLMat;
	private IGLManager m_GLManager;
	
	private Texture2D Overlay;
	private Rect OverlayRect = new Rect();
	
	private Rect m_ScreenRect = new Rect(-100, -100, Screen.width+200, Screen.height+200);
	
	private float m_OverlayWidth = 0;
	private float m_OverlayLength = 0;
	private float m_MainMenuWidth;
	
	private Vector3 m_WorldExtents;

	private GameObject decalCircle;
	private UnitStats myStats;

	// Use this for initialization
	void Start () 
	{	myStats = this.gameObject.GetComponent<UnitStats> ();
		decalCircle = this.gameObject.transform.Find("DecalCircle").gameObject;
		Overlay = Overlays.CreateTexture ();
		IsSelected = false;
		FindMaxWorldSize();
		
		m_MainMenuWidth = ManagerResolver.Resolve<IGUIManager>().MainMenuWidth;
		
		m_GLManager = ManagerResolver.Resolve<IGLManager>();
		//m_GLMat = GLMatShader.GetGLMaterial ();
		m_GLItem = new GLItem(GLExecuteFunction);
		
		//If this unit is land based subscribe to the path changed event


		SetOverlaySize ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Update overlay rect
		SetOverlaySize ();

		if (!myStats.atFullHealth() || !myStats.atFullEnergy()) {


			Vector3 centerPoint = Camera.main.WorldToScreenPoint (transform.position);
			OverlayRect.xMin = centerPoint.x - (m_OverlayWidth / 2.0f);
			OverlayRect.xMax = centerPoint.x + (m_OverlayWidth / 2.0f);
			OverlayRect.yMax = Screen.height - (centerPoint.y - (m_OverlayLength / 2.0f) - 5);
			OverlayRect.yMin = Screen.height - (centerPoint.y + (m_OverlayLength / 2.0f) + 15);
		
		
		}
		if (m_JustBeenSelected)
		{
			m_JustBeenSelectedTimer += Time.deltaTime;
			
			if (m_JustBeenSelectedTimer >= 1.0f)
			{
				m_JustBeenSelectedTimer = 0;
				m_JustBeenSelected = false;
				m_GLManager.RemoveItemToRender (m_GLItem);
			}
		}
	}

	public void updateHealthBar(float ratio)
	{
		Overlays.UpdateTexture (Overlay, ratio); 

	}

	public void updateEnergyBar(float ratio)
	{
		Overlays.UpdateTexture (Overlay,1f, ratio); 

	}
	private void FindMaxWorldSize()
	{
		//Calculate size of overlay based on the objects size
		Vector3 worldSize = GetComponent<Collider>().bounds.extents;
		float maxDimension = Mathf.Max (worldSize.x, worldSize.z);
		m_WorldExtents = new Vector3(maxDimension, 0, 0);
	}
	
	private void SetOverlaySize()
	{
		Vector3 maxWorldSize = transform.position + m_WorldExtents;
		Vector3 minWorldSize = transform.position - m_WorldExtents;
		
		Vector3 maxScreenSize = Camera.main.WorldToScreenPoint (maxWorldSize);
		Vector3 minScreenSize = Camera.main.WorldToScreenPoint (minWorldSize);
		
		maxScreenSize.z = 0;
		minScreenSize.z = 0;
		
		//Make sure the screen points are within our screen rect (needed to stop errors in the world to screen conversion process)
		if (m_ScreenRect.Contains (maxScreenSize) && m_ScreenRect.Contains (minScreenSize))
		{
			//Find max size in either z or x direction
			float overlaySize = Mathf.Max (Mathf.Abs (maxScreenSize.x - minScreenSize.x), Mathf.Abs (maxScreenSize.y - minScreenSize.y));
			
			m_OverlayWidth = overlaySize + 10;
			m_OverlayLength = overlaySize + 20;
		}
	}
	
	void OnGUI()
	{
		if (!myStats.atFullHealth() || !myStats.atFullEnergy())
		{
			if (OverlayRect.xMax < Screen.width-m_MainMenuWidth)
			{
				GUI.DrawTexture (OverlayRect, Overlay);
			}
		}
	}
	
	public void SetSelected()
	{
		IsSelected = true;
		m_JustBeenSelected = true;
		m_JustBeenSelectedTimer = 0;
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;
		//m_GLManager.AddItemToRender (m_GLItem);
	}
	
	public void SetDeselected()
	{
		IsSelected = false;
		m_JustBeenSelected = false;
		decalCircle.GetComponent<MeshRenderer> ().enabled = false;
	//	m_GLManager.RemoveItemToRender (m_GLItem);
	}
	
	public void AssignGroupNumber(int number)
	{
		
	}
	
	public void RemoveGroupNumber()
	{
		
	}
	
	private void GLExecuteFunction()
	{
		//Need to get target locat
	/*
		Vector3 target = GetComponent<MovementComponent>().targetLocations.Peek();
		if (target != null) {
			if (IsSelected && target != Vector3.zero) {
				Vector3 screenTarget = Camera.main.WorldToScreenPoint (target);
				Vector3 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
			
				screenTarget.z = 0;
				screenPosition.z = 0;
			
				GL.PushMatrix ();
		
				m_GLMat.SetPass (0);
				GL.LoadPixelMatrix ();
			
				GL.Begin (GL.LINES);
			
				//If we're travelling set to green, if we're attacking set to red (keep as green for now)
				GL.Color (Color.green);
			
				GL.Vertex (screenPosition);
				GL.Vertex (screenTarget);
			
				GL.End ();
			
				//Draw a little quad at target
				GL.Begin (GL.QUADS);
				int quadSize = 3;
				GL.Vertex3 (screenTarget.x - quadSize, screenTarget.y - quadSize, 0);
				GL.Vertex3 (screenTarget.x + quadSize, screenTarget.y - quadSize, 0);
				GL.Vertex3 (screenTarget.x + quadSize, screenTarget.y + quadSize, 0);
				GL.Vertex3 (screenTarget.x - quadSize, screenTarget.y + quadSize, 0);
			
				GL.End ();
			
				GL.PopMatrix ();
			
			}
		}*/
	}
	
	private void PathChanged()
	{
		if (IsSelected)
		{
			m_JustBeenSelected = true;
			m_JustBeenSelectedTimer = 0;
			//m_GLManager.AddItemToRender (m_GLItem);
		}
	}
}
