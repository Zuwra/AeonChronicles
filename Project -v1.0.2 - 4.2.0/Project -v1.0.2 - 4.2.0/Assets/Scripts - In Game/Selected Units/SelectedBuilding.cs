using UnityEngine;
using System.Collections;

public class SelectedBuilding : MonoBehaviour {
	
	private Vector3[] WorldVertices = new Vector3[8];
	private Vector3[] WorldHealthVertices = new Vector3[8];
	
	private Vector3[] ScreenVertices = new Vector3[8];
	private Vector3[] ScreenHealthVertices = new Vector3[8];
	
	private GLItem m_GLItem;
	private IGLManager m_GLManager;
	
	private Material m_GLMat;

	
	private float m_HealthSize = 2.0f;
	private float m_HealthWidth;

	// Use this for initialization
	void Start () 
	{
		//Calculate world co-ordinates
		//8 vertices for outline (determined from collider)
		Bounds bounds = GetComponent<Collider>().bounds;
		Vector3 center = bounds.center;
		float xExtent = bounds.extents.x;
		float yExtent = bounds.extents.y;
		float zExtent = bounds.extents.z;
		
		WorldVertices[0] = center + new Vector3(-xExtent, -yExtent, -zExtent);
		WorldVertices[1] = center + new Vector3(xExtent, -yExtent, -zExtent);
		WorldVertices[2] = center + new Vector3(xExtent, -yExtent, zExtent);
		WorldVertices[3] = center + new Vector3(-xExtent, -yExtent, zExtent);
		
		WorldVertices[4] = center + new Vector3(-xExtent, yExtent, -zExtent);
		WorldVertices[5] = center + new Vector3(xExtent, yExtent, -zExtent);
		WorldVertices[6] = center + new Vector3(xExtent, yExtent, zExtent);
		WorldVertices[7] = center + new Vector3(-xExtent, yExtent, zExtent);
		
		WorldHealthVertices[0] = new Vector3(WorldVertices[6].x, WorldVertices[6].y, WorldVertices[6].z);
		WorldHealthVertices[0].y -= 0.1f;
		WorldHealthVertices[0].z -= 0.1f;
		
		WorldHealthVertices[1] = new Vector3(WorldHealthVertices[0].x, WorldHealthVertices[0].y-m_HealthSize, WorldHealthVertices[0].z);
		WorldHealthVertices[2] = new Vector3(WorldHealthVertices[0].x, WorldHealthVertices[0].y-m_HealthSize, WorldHealthVertices[0].z-m_HealthSize);
		WorldHealthVertices[3] = new Vector3(WorldHealthVertices[0].x, WorldHealthVertices[0].y, WorldHealthVertices[0].z-m_HealthSize);
		
		WorldHealthVertices[4] = new Vector3(WorldVertices[7].x, WorldVertices[7].y, WorldVertices[7].z);
		WorldHealthVertices[4].y -= 0.1f;
		WorldHealthVertices[4].z -= 0.1f;
		
		WorldHealthVertices[5] = new Vector3(WorldHealthVertices[4].x, WorldHealthVertices[4].y-m_HealthSize, WorldHealthVertices[0].z);
		WorldHealthVertices[6] = new Vector3(WorldHealthVertices[4].x, WorldHealthVertices[4].y-m_HealthSize, WorldHealthVertices[0].z-m_HealthSize);
		WorldHealthVertices[7] = new Vector3(WorldHealthVertices[4].x, WorldHealthVertices[4].y, WorldHealthVertices[0].z-m_HealthSize);
		
		//Assign health width
		m_HealthWidth = Mathf.Abs (WorldHealthVertices[0].x - WorldHealthVertices[4].x);
		
		//Create GLItem
		m_GLItem = new GLItem(ExecuteFunction);
		
		//Resolve GLManager
		m_GLManager = ManagerResolver.Resolve<IGLManager>();
		
		//Assign GL material
		m_GLMat = GLMatShader.GetGLMaterial();
		
		//Assign building

	}
	
	public void SetSelected()
	{
		m_GLManager.AddItemToRender(m_GLItem);
	}
	
	public void SetDeselected()
	{
		m_GLManager.RemoveItemToRender (m_GLItem);
	}
	
	public void ExecuteFunction()
	{
		//Calculate screen co-ordinates
		//White border outline-------------------------------
		ScreenVertices[0] = Camera.main.WorldToScreenPoint(WorldVertices[0]);
		ScreenVertices[1] = Camera.main.WorldToScreenPoint(WorldVertices[1]);
		ScreenVertices[2] = Camera.main.WorldToScreenPoint(WorldVertices[2]);
		ScreenVertices[3] = Camera.main.WorldToScreenPoint(WorldVertices[3]);
		ScreenVertices[4] = Camera.main.WorldToScreenPoint(WorldVertices[4]);
		ScreenVertices[5] = Camera.main.WorldToScreenPoint(WorldVertices[5]);
		ScreenVertices[6] = Camera.main.WorldToScreenPoint(WorldVertices[6]);
		ScreenVertices[7] = Camera.main.WorldToScreenPoint(WorldVertices[7]);
		
		ScreenVertices[0].z = 0;
		ScreenVertices[1].z = 0;
		ScreenVertices[2].z = 0;
		ScreenVertices[3].z = 0;
		ScreenVertices[4].z = 0;
		ScreenVertices[5].z = 0;
		ScreenVertices[6].z = 0;
		ScreenVertices[7].z = 0;
		
		//Health boxes-------------------------------------
		//Need to update x position for right hand side of health bar based on building health

		ScreenHealthVertices[0] = Camera.main.WorldToScreenPoint(WorldHealthVertices[0]);
		ScreenHealthVertices[1] = Camera.main.WorldToScreenPoint(WorldHealthVertices[1]);
		ScreenHealthVertices[2] = Camera.main.WorldToScreenPoint(WorldHealthVertices[2]);
		ScreenHealthVertices[3] = Camera.main.WorldToScreenPoint(WorldHealthVertices[3]);
		ScreenHealthVertices[4] = Camera.main.WorldToScreenPoint(WorldHealthVertices[4]);
		ScreenHealthVertices[5] = Camera.main.WorldToScreenPoint(WorldHealthVertices[5]);
		ScreenHealthVertices[6] = Camera.main.WorldToScreenPoint(WorldHealthVertices[6]);
		ScreenHealthVertices[7] = Camera.main.WorldToScreenPoint(WorldHealthVertices[7]);
		
		ScreenHealthVertices[0].z = 0;
		ScreenHealthVertices[1].z = 0;
		ScreenHealthVertices[2].z = 0;
		ScreenHealthVertices[3].z = 0;
		ScreenHealthVertices[4].z = 0;
		ScreenHealthVertices[5].z = 0;
		ScreenHealthVertices[6].z = 0;
		ScreenHealthVertices[7].z = 0;
		
		//Draw building outline
		GL.PushMatrix ();
		
		m_GLMat.SetPass (0);
		GL.LoadPixelMatrix();
		
		GL.Begin(GL.LINES);
		GL.Color(Color.white);
		
		//Bottom half
		GL.Vertex (ScreenVertices[0]);
		GL.Vertex (ScreenVertices[1]);
		
		GL.Vertex (ScreenVertices[1]);
		GL.Vertex (ScreenVertices[2]);
		
		GL.Vertex (ScreenVertices[2]);
		GL.Vertex (ScreenVertices[3]);
		
		GL.Vertex (ScreenVertices[3]);
		GL.Vertex (ScreenVertices[0]);
				
		//Vertical Lines
		GL.Vertex (ScreenVertices[0]);
		GL.Vertex (ScreenVertices[4]);
		
		GL.Vertex (ScreenVertices[1]);
		GL.Vertex (ScreenVertices[5]);
		
		GL.Vertex (ScreenVertices[2]);
		GL.Vertex (ScreenVertices[6]);
		
		GL.Vertex (ScreenVertices[3]);
		GL.Vertex (ScreenVertices[7]);
		
		GL.End ();
		
		
		//Health Bar
		GL.Begin (GL.QUADS);
		


		
		GL.Vertex (ScreenHealthVertices[0]);
		GL.Vertex (ScreenHealthVertices[1]);
		GL.Vertex (ScreenHealthVertices[2]);
		GL.Vertex (ScreenHealthVertices[3]);
		
		GL.Vertex (ScreenHealthVertices[4]);
		GL.Vertex (ScreenHealthVertices[5]);
		GL.Vertex (ScreenHealthVertices[6]);
		GL.Vertex (ScreenHealthVertices[7]);
		
		GL.Vertex (ScreenHealthVertices[0]);
		GL.Vertex (ScreenHealthVertices[3]);
		GL.Vertex (ScreenHealthVertices[7]);
		GL.Vertex (ScreenHealthVertices[4]);
		
		GL.Vertex (ScreenHealthVertices[2]);
		GL.Vertex (ScreenHealthVertices[3]);
		GL.Vertex (ScreenHealthVertices[7]);
		GL.Vertex (ScreenHealthVertices[6]);
		
		GL.End ();	
		
		//Top half	
		GL.Begin (GL.LINES);
		
		GL.Color (Color.white);
			
		GL.Vertex (ScreenVertices[4]);
		GL.Vertex (ScreenVertices[5]);
		
		GL.Vertex (ScreenVertices[5]);
		GL.Vertex (ScreenVertices[6]);
		
		GL.Vertex (ScreenVertices[6]);
		GL.Vertex (ScreenVertices[7]);
		
		GL.Vertex (ScreenVertices[7]);
		GL.Vertex (ScreenVertices[4]);
		
		GL.End ();
		GL.PopMatrix ();
	}
	
	void OnDestroy()
	{
		//If this gets destroyed make sure to remove any selection
		SetDeselected ();
	}
}
