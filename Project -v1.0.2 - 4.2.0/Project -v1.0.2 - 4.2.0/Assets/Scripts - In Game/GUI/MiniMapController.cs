using UnityEngine;
using System.Collections;


public class MiniMapController : MonoBehaviour, IMiniMapController {
	
	//Singleton
	public static MiniMapController main;
	
	//Minimap rect in pixel space
	private Rect m_MiniMapRect;
	
	//Menu Width (needed to calculate correct viewport in minimap)

	
	private float m_zOffSet;
	private float m_xOffset;
	
	private ICamera m_MainCamera;
	



	void Awake()
	{
		main = this;
		
	
	}
		
	void Start () 
	{
		
		m_MainCamera = ManagerResolver.Resolve<ICamera>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void LoadMiniMap()
	{Debug.Log ("Getting called");
		//Properly configure camera viewport so it's a square and it's in the correct place regardless of resolution
		//Always want the map to appear 3/4 up the screen, with a height of 1/4.5
		float aspectRatio = (float)Screen.width/(float)Screen.height;
		
		float viewPortY = .10f/4.0f;
		float viewPortHeight = 1.0f/4.5f;
		
		//Figure width values based on height values
		float viewPortWidth = 1.0f/(4.5f*aspectRatio);
		float viewPortX = 1-(0.25f/aspectRatio);

		//Assign camera viewport
		GetComponent<Camera>().rect = new Rect(viewPortX, viewPortY, viewPortWidth, viewPortHeight);
	
		
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
	


	public void ReCalculateViewRect ()
	{
		//UpdateViewPort ();
	}

}
