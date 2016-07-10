using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile 
{
	//Member Variables-------------------------------------------------
	private int m_I;
	private int m_J;	
	private int m_Status;
	private int m_LayeredStatus = Const.LAYEREDTILE_NotLayered;
	
	private float m_Size;	
	
	private float m_Costinc = 10.0f;
	private float m_CostDinc = 14.0f;
	
	private Vector3 m_Center;	
	
	private bool m_Buildable = true;
	
	
	
	private int m_NavLayer = 0;
	
	private List<Tile> m_AccessibleTiles = new List<Tile>();
	private List<Tile> m_LayeredTiles;
	
	private RTSObject m_OccupiedBy;
	private bool m_Occupied = false;
	private bool m_OccupiedStatic = false;
	private bool m_ExpectingArrival = false;

	//Properties-----------------------------------------------------------
	public int I
	{
		get
		{
			return m_I;
		}
	}
	
	public int J
	{
		get
		{
			return m_J;
		}
	}
	
	public Vector3 Center
	{
		get
		{
			return m_Center;
		}
	}
	
	public float Costinc
	{
		get
		{
			return m_Costinc;
		}
	}
	
	public float CostDinc
	{
		get
		{
			return m_CostDinc;
		}
	}
	
	public int Status
	{
		get;
		set;
	}
	
	public bool Buildable
	{
		get
		{
			return m_Buildable;
		}
	}
	


	public bool IsOccupied
	{
		get
		{
			return m_Occupied;
		}
	}
	
	public RTSObject OccupiedBy
	{
		get
		{
			return m_OccupiedBy;
		}
		private set
		{
			m_OccupiedBy = value;
			
			m_Occupied = m_OccupiedBy != null;
		}
	}
	
	public bool IsOccupiedStatic
	{
		get
		{
			return m_OccupiedStatic && IsOccupied;
		}
	}
	
	public List<Tile> LayeredTiles
	{
		get
		{
			return m_LayeredTiles;
		}
	}
	
	public float Cost
	{
		get;
		set;
	}
	
	public bool Start
	{
		get;
		set;
	}
	
	public bool End
	{
		get;
		set;
	}
	
	public float hCost
	{
		get;
		set;
	}
	
	public float fCost
	{
		get;
		set;
	}
	
	public Tile ParentTile
	{
		get;
		set;
	}
	
	public List<Tile> AccessibleTiles
	{
		get
		{
			return m_AccessibleTiles;
		}
		set
		{
			m_AccessibleTiles = value;
		}
	}
	
	public int NavLayer
	{
		get
		{
			return m_NavLayer;
		}
		set
		{
			m_NavLayer = value;
		}
	}
	

	
	public bool ExpectingArrival
	{
		get
		{
			return m_ExpectingArrival;
		}
		set
		{
			m_ExpectingArrival = value;
		}
	}
	

	
	public int LayeredStatus
	{
		get
		{
			return m_LayeredStatus;
		}
	}
	
	//Constructor----------------------------------------------------------
	public Tile(int i, int j, Vector3 center, Collider collider = null)
	{
		m_I = i;
		m_J = j;
		m_Size = Grid.main.TileSize;
		m_Center = center;
		Status = Const.TILE_Unvisited;
		m_LayeredTiles = new List<Tile>();
		Evaluate();
		//Debug.Log ("Evaluating");

	}
	
	//Methods------------------------------------------------------------------------
	
	public void Evaluate()
	{
		m_LayeredTiles.Clear ();
		Status = Const.TILE_Unvisited;
		m_Center.y = Terrain.activeTerrain.SampleHeight(m_Center);
		EvaluateTile ();

	}
	
	private void EvaluateTile()
	{
		float halfSize = m_Size/2.0f;
		
		//Check bridges and tunnels
		Vector3 bottomLeft = m_Center + new Vector3(-halfSize, 0, -halfSize);
		Vector3 bottomRight = m_Center + new Vector3(halfSize, 0, -halfSize);
		Vector3 topRight = m_Center + new Vector3(halfSize, 0, halfSize);
		Vector3 topLeft = m_Center + new Vector3(-halfSize, 0, halfSize);
		
		float startOffset = 5.0f;
		
		Ray rayUp = new Ray(m_Center + (Vector3.down*startOffset), Vector3.up);
		Ray rayUp2 = new Ray(bottomLeft + (Vector3.down*startOffset), Vector3.up);
		Ray rayUp3 = new Ray(bottomRight + (Vector3.down*startOffset), Vector3.up);
		Ray rayUp4 = new Ray(topRight + (Vector3.down*startOffset), Vector3.up);
		Ray rayUp5 = new Ray(topLeft + (Vector3.down*startOffset), Vector3.up);
		
		RaycastHit hitCenter;
		
		//Are we within the block indent?
		if (I < Grid.main.BlockIndent || I >= Grid.main.Width-Grid.main.BlockIndent || J < Grid.main.BlockIndent || J >= Grid.main.Length-Grid.main.BlockIndent)
		{
			//Within the indent
			Status = Const.TILE_Blocked;
			return;
		}
		
		//Check steepness of terrain
		//Calculate corner points		
		Terrain terrain = Terrain.activeTerrain;
		float minHeight = Mathf.Min (terrain.SampleHeight (m_Center), terrain.SampleHeight (bottomLeft), terrain.SampleHeight (bottomRight), terrain.SampleHeight (topRight), terrain.SampleHeight (topLeft));
		float maxHeight = Mathf.Max (terrain.SampleHeight (m_Center), terrain.SampleHeight (bottomLeft), terrain.SampleHeight (bottomRight), terrain.SampleHeight (topRight), terrain.SampleHeight (topLeft));
				
		//Debug.Log ("Checking Steepness");
		if (maxHeight-minHeight > Grid.main.MaxSteepness)
		{
			//Debug.Log ("Too steep " + maxHeight + "   " + minHeight);
			//Too steep
			Status = Const.TILE_Blocked;
			m_Buildable = false;
			return;
		}
		
		//Check for obstacles		
		//We want to ignore units, terrain,  so cast against everything apart from layers 8, 9, 11, 18, 19 and 20
		LayerMask layerMask = ~(1 << 8 | 1 << 9 | 1 << 11 | 1 << 18 | 1 << 19 | 1 << 20);
		
		//Need to raycast against center and all 4 corner points
		bool result1 = Physics.Raycast (rayUp, Mathf.Infinity, layerMask);
		bool result2 = Physics.Raycast (rayUp2, Mathf.Infinity, layerMask);
		bool result3 = Physics.Raycast (rayUp3, Mathf.Infinity, layerMask);
		bool result4 = Physics.Raycast (rayUp4, Mathf.Infinity, layerMask);
		bool result5 = Physics.Raycast (rayUp5, Mathf.Infinity, layerMask);
		
		if (result1 || result2 || result3 || result4 || result5)
		{
			//We've hit something above us, so we can't build on this tile, but is it passable?
			m_Buildable = false;
			
			if (result1)
			{
				if (Physics.Raycast(rayUp, out hitCenter, Mathf.Infinity, layerMask))
				{
					float distance = Vector3.Distance (m_Center, hitCenter.point);
					if (distance < Grid.main.PassableHeight)
					{
						Status = Const.TILE_Blocked;
						m_Buildable = false;
						return;
					}
				}
			}
			
			if (result2)
			{
				if (Physics.Raycast(rayUp2, out hitCenter, Mathf.Infinity, layerMask))
				{
					float distance = Vector3.Distance (m_Center, hitCenter.point);
					if (distance < Grid.main.PassableHeight)
					{
						Status = Const.TILE_Blocked;
						m_Buildable = false;
						return;
					}
				}
			}
			
			if (result3)
			{
				if (Physics.Raycast(rayUp3, out hitCenter, Mathf.Infinity, layerMask))
				{
					float distance = Vector3.Distance (m_Center, hitCenter.point);
					if (distance < Grid.main.PassableHeight)
					{
						Status = Const.TILE_Blocked;
						m_Buildable = false;
						return;
					}
				}
			}
			
			if (result4)
			{
				if (Physics.Raycast(rayUp4, out hitCenter, Mathf.Infinity, layerMask))
				{
					float distance = Vector3.Distance (m_Center, hitCenter.point);
					if (distance < Grid.main.PassableHeight)
					{
						Status = Const.TILE_Blocked;
						m_Buildable = false;
						return;
					}
				}
			}
			
			if (result5)
			{
				if (Physics.Raycast(rayUp5, out hitCenter, Mathf.Infinity, layerMask))
				{
					float distance = Vector3.Distance (m_Center, hitCenter.point);
					if (distance < Grid.main.PassableHeight)
					{
						Status = Const.TILE_Blocked;
						m_Buildable = false;
						return;
					}
				}
			}
		}
		else
		{
			//We haven't hit anything, so we're buildable and open
			m_Buildable = true;
		}
	}
	

	

	
	public void SetOccupied(RTSObject occupiedBy, bool occupiedStatic)
	{
		OccupiedBy = occupiedBy;
		m_OccupiedStatic = occupiedStatic;
	}
	
	public void NoLongerOccupied(RTSObject occupiedBy)
	{
		OccupiedBy = null;
		m_OccupiedStatic = false;
	}
}
