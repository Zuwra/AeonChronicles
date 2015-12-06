using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Grid : MonoBehaviour, IGrid 
{
	//Singleton
	public static Grid main;
	
	//Member variables
	private static bool m_ShowGrid = false;	
	private static bool m_ShowOpenTiles = true;
	private static bool m_ShowClosedTiles = true;
	private static bool m_ShowBridgeTiles = true;
	private static bool m_ShowTunnelTiles = true;
	
	private static float m_TileSize = 7.5f;
	private static int m_Width = 200;
	private static int m_Length = 200;
	private static float m_WidthOffset = 400;
	private static float m_LengthOffset = 400;
	private static float m_MaxSteepness = 2.5f;
	private static float m_PassableHeight = 2.0f;
	private static int m_BlockIndent = 5;
	
	private static Tile[,] m_Grid;
	
	private static List<Vector3> debugAlgo = new List<Vector3>();
	
	//Properties
	public static bool ShowGrid
	{
		get
		{
			return m_ShowGrid;
		}
		set
		{
			if (m_Grid == null)
			{
				Initialise ();
			}
			
			m_ShowGrid = value;
		}
	}
	
	public static bool ShowOpenTiles
	{
		get
		{
			return m_ShowOpenTiles;
		}
		set
		{
			m_ShowOpenTiles = value;
		}
	}
	
	public static bool ShowClosedTiles
	{
		get
		{
			return m_ShowClosedTiles;
		}
		set
		{
			m_ShowClosedTiles = value;
		}
	}
	
	public static bool ShowBridgeTiles
	{
		get
		{
			return m_ShowBridgeTiles;
		}
		set
		{
			m_ShowBridgeTiles = value;
		}
	}
	
	public static bool ShowTunnelTiles
	{
		get
		{
			return m_ShowTunnelTiles;
		}
		set
		{
			m_ShowTunnelTiles = value;
		}
	}
	
	public static float TileSize
	{
		get
		{
			return m_TileSize;
		}
		set
		{
			if (Equals (m_TileSize, value))
			{
				return;
			}
			
			m_TileSize = value;
			
			Initialise ();
		}
	}
	
	public static int Width
	{
		get
		{
			return m_Width;
		}
		set
		{
			if (Equals (m_Width, value))
			{
				return;
			}
		
			m_Width = value;
			
			Initialise ();
		}
	}
	
	public static int Length
	{
		get
		{
			return m_Length;
		}
		set
		{
			if (Equals (m_Length, value))
			{
				return;
			}
			
			m_Length = value;
			
			Initialise ();
		}
	}
	
	public static float WidthOffset
	{
		get
		{
			return m_WidthOffset;
		}
		set
		{
			if (Equals (m_WidthOffset, value))
			{
				return;
			}
			
			m_WidthOffset = value;
			
			Initialise ();
		}
	}
	
	public static float LengthOffset
	{
		get
		{
			return m_LengthOffset;
		}
		set
		{
			if (Equals (m_LengthOffset, value))
			{
				return;
			}
			
			m_LengthOffset = value;
			
			Initialise ();
		}
	}
	
	public static float MaxSteepness
	{
		get
		{
			return m_MaxSteepness;
		}
		set
		{
			if (Equals (m_MaxSteepness, value))
			{
				return;
			}
			
			m_MaxSteepness = value;
			
			Initialise ();
		}
	}
	
	public static float PassableHeight
	{
		get
		{
			return m_PassableHeight;
		}
		set
		{
			if (Equals (m_PassableHeight, value))
			{
				return;
			}
			
			m_PassableHeight = value;
			
			Initialise ();
		}
	}
	
	public static int BlockIndent
	{
		get
		{
			return m_BlockIndent;
		}
		set
		{
			if (Equals (m_BlockIndent, value))
			{
				return;
			}
			
			if (value > 0)
			{
				m_BlockIndent = value;
			}
			else
			{
				m_BlockIndent = 1;
			}
			
			Initialise ();
		}
	}
	
	void Awake()
	{
		main = this;
	}
	
	void Start()
	{
		if (Application.isPlaying)
		{
			StartCoroutine (InitialiseAsRoutine ());
		}
	}
	
	void Update()
	{
		
	}
	
	void OnDrawGizmos()
	{
		if (m_ShowGrid && Application.isEditor && m_Grid != null)
		{
			foreach (Tile tile in m_Grid)
			{
				if (tile.Status == Const.TILE_Blocked) 
				{
					if (ShowClosedTiles)
					{
						Gizmos.color = Color.red;
						Gizmos.DrawWireCube (tile.Center, new Vector3(m_TileSize, 0.1f, m_TileSize));
					}
				}
				else
				{
					if (ShowOpenTiles)
					{
						Gizmos.color = Color.white;
						Gizmos.DrawWireCube (tile.Center, new Vector3(m_TileSize, 0.1f, m_TileSize));
					}
				}
				
				
				foreach (Tile t in tile.LayeredTiles)
				{
					if (t.IsBridge)
					{
						if (ShowBridgeTiles)
						{
							Gizmos.color = Color.green;
							Gizmos.DrawWireCube (t.Center, new Vector3(m_TileSize, 0.1f, m_TileSize));
						}
					}
					else
					{
						if (ShowTunnelTiles)
						{
							Gizmos.color = Color.cyan;
							Gizmos.DrawWireCube (t.Center, new Vector3(m_TileSize, 0.1f, m_TileSize));
						}
					}
				}
			}
		}
		
		for (int i = 1; i < debugAlgo.Count; i++)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (debugAlgo[i-1], debugAlgo[i]);
		}
	}
	
	public static IEnumerator InitialiseAsRoutine()
	{
		ILevelLoader levelLoader = ManagerResolver.Resolve<ILevelLoader>();
		
		m_Grid = new Tile[Width, Length];
		
		//Create tiles
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Length; j++)
			{
				float xCenter = m_WidthOffset + ((i*m_TileSize) + (m_TileSize/2.0f));
				float zCenter = m_LengthOffset + ((j*m_TileSize) + (m_TileSize/2.0f));
				Vector3 center = new Vector3(xCenter, 0, zCenter);				
				center.y = Terrain.activeTerrain.SampleHeight(center);
				
				m_Grid[i,j] = new Tile(i, j, center);
			}
		}
		
		if (levelLoader != null) levelLoader.ChangeText ("Evaluating tiles");
		yield return null;
		
		List<Collider> bridgeList = new List<Collider>();
		List<Collider> tunnelList = new List<Collider>();
		
		//Evaluate
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Length; j++)
			{
				m_Grid[i,j].Evaluate (bridgeList, tunnelList);
			}
		}
		
		if (levelLoader != null) levelLoader.ChangeText ("Evaluating bridges");
		yield return null;
		
		//Create the bridges
		foreach (Collider collider in bridgeList)
		{
			BuildBridge(collider);
		}
		
		if (levelLoader != null) levelLoader.ChangeText ("Evaluating Tunnels");
		yield return null;
		
		//Create the tunnels
		foreach (Collider collider in tunnelList)
		{
			BuildTunnel(collider);
		}
		
		if (levelLoader != null) levelLoader.ChangeText ("Populating internal array");
		yield return null;
		
		//Now all the tiles have been initialised, we need to populate the tiles internal array with accessible tiles
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Length; j++)
			{
				FindAccessibleTiles (m_Grid[i,j]);
			}
		}
		
		if (levelLoader != null) levelLoader.FinishLoading ();
		ManagerResolver.Resolve<ICursorManager>().ShowCursor ();
	}
	
	public static void Initialise()
	{
		m_Grid = new Tile[Width, Length];
		
		//Create tiles
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Length; j++)
			{
				float xCenter = m_WidthOffset + ((i*m_TileSize) + (m_TileSize/2.0f));
				float zCenter = m_LengthOffset + ((j*m_TileSize) + (m_TileSize/2.0f));
				Vector3 center = new Vector3(xCenter, 0, zCenter);				
				center.y = Terrain.activeTerrain.SampleHeight(center);
				
				m_Grid[i,j] = new Tile(i, j, center);
			}
		}
		
		List<Collider> bridgeList = new List<Collider>();
		List<Collider> tunnelList = new List<Collider>();
		
		//Evaluate
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Length; j++)
			{
				m_Grid[i,j].Evaluate (bridgeList, tunnelList);
			}
		}
		
		//Create the bridges
		foreach (Collider collider in bridgeList)
		{
			BuildBridge(collider);
		}
		
		//Create the tunnels
		foreach (Collider collider in tunnelList)
		{
			BuildTunnel(collider);
		}
	}
	
	public static Tile GetClosestTile(Vector3 position)
	{
		int iValue = (int)((position.x - m_WidthOffset)/m_TileSize);
		int jValue = (int)((position.z - m_LengthOffset)/m_TileSize);
		
		if (iValue < 0) iValue = 0;
		else if (iValue >= Width) iValue = Width-1;
		
		if (jValue < 0) jValue = 0;
		else if (jValue >= Length) jValue = Length-1;
		
		Tile tileToReturn = m_Grid[iValue, jValue];
		
		float distance = Mathf.Abs (tileToReturn.Center.y - position.y);
		Tile lTile = null;
		foreach (Tile tile in tileToReturn.LayeredTiles)
		{
			if (Mathf.Abs (tile.Center.y - position.y) < distance)
			{
				lTile = tile;
				distance = Mathf.Abs (tile.Center.y - position.y);
			}
		}
		
		if (lTile != null)
		{
			tileToReturn = lTile;
		}
		
		return tileToReturn;
	}
	
	public static Tile GetClosestAvailableTile(Vector3 position)
	{
		debugAlgo.Clear ();
		int iValue = (int)((position.x - m_WidthOffset)/m_TileSize);
		int jValue = (int)((position.z - m_LengthOffset)/m_TileSize);
		
		if (iValue < 0) iValue = 0;
		else if (iValue >= Width) iValue = Width-1;
		
		if (jValue < 0) jValue = 0;
		else if (jValue >= Length) jValue = Length-1;
		
		Tile tileToReturn = m_Grid[iValue, jValue];
		
		if (tileToReturn.LayeredTiles.Count > 0)
		{
			if (Mathf.Abs (tileToReturn.Center.y - position.y) > Mathf.Abs (tileToReturn.LayeredTiles[0].Center.y - position.y))
			{
				tileToReturn = tileToReturn.LayeredTiles[0];
			}
		}
		
		if (tileToReturn.Status == Const.TILE_Blocked)
		{
			//Need to iterate to find closest available tile
			int directionCounter = Const.DIRECTION_Right;
			int widthCounter = 1;
			int lengthCounter = 1;
			int IValue = tileToReturn.I;
			int JValue = tileToReturn.J;
			
		    while (tileToReturn.Status == Const.TILE_Blocked)
			{
				int counter;
				
				//If we're travelling left or right use the width counter, up or down use the length counter
				if (directionCounter == Const.DIRECTION_Right || directionCounter == Const.DIRECTION_Left)
				{
					counter = widthCounter;
				}
				else
				{
					counter = lengthCounter;
				}
				
				for (int i=0; i<counter; i++)
				{
					switch (directionCounter)
					{
					case Const.DIRECTION_Right:
						//Increase I value (go right)
						IValue++;
						
						//Check if we're at the width so we don't get an exception
						if (IValue >= Width)
						{
							//We're past the width, decrease I value
							IValue = Width-1;
							
							//Set JValue to whatever it is minus lengthcounter (no point checking tiles we've already checked!)
							JValue = JValue - lengthCounter;
							
							//Since we've skipped all the downward tiles, go left
							directionCounter = Const.DIRECTION_Left;
							
							//Update the length counter as we're skipping it out
							lengthCounter++;
						}
						else
						{
							//Have we travelled far enough?
							if (i == widthCounter - 1)
							{
								//We've travelled as far as we want to, change the direction and increase the width counter
								directionCounter = Const.DIRECTION_Down;
								widthCounter++;
							}
						}
						
						break;
						
					case Const.DIRECTION_Down:
						
						JValue--;
						if (JValue < 0)
						{
							JValue = 0;
							
							IValue = IValue - widthCounter;
							
							directionCounter = Const.DIRECTION_Up;
							
							widthCounter++;
						}
						else
						{
							if (i == lengthCounter - 1)
							{
								directionCounter = Const.DIRECTION_Left;
								lengthCounter++;
							}
						}
						
						break;
						
					case Const.DIRECTION_Left:
						
						IValue--;
						if (IValue < 0)
						{
							IValue = 0;
							
							JValue = JValue + lengthCounter;
							
							directionCounter = Const.DIRECTION_Right;
							
							lengthCounter++;
						}
						else
						{
							if (i == widthCounter - 1)
							{
								directionCounter = Const.DIRECTION_Up;
								widthCounter++;
							}
						}
						
						break;
						
					case Const.DIRECTION_Up:
						
						JValue++;
						if (JValue >= Length)
						{
							JValue = Length-1;
							
							IValue = IValue + widthCounter;
							
							directionCounter = Const.DIRECTION_Down;
							
							widthCounter++;
						}
						else
						{
							if (i == lengthCounter - 1)
							{
								directionCounter = Const.DIRECTION_Right;
								lengthCounter++;
							}
						}
						
						break;
					}
					
					tileToReturn = m_Grid[IValue, JValue];
					debugAlgo.Add (tileToReturn.Center);
				}
			}
		}
		
		return tileToReturn;
	}
	
	public static Tile GetClosestAvailableFreeTile(Vector3 position)
	{
		int iValue = (int)((position.x - m_WidthOffset)/m_TileSize);
		int jValue = (int)((position.z - m_LengthOffset)/m_TileSize);
		
		if (iValue < 0) iValue = 0;
		else if (iValue >= Width) iValue = Width-1;
		
		if (jValue < 0) jValue = 0;
		else if (jValue >= Length) jValue = Length-1;
		
		Tile tileToReturn = m_Grid[iValue, jValue];
		
		float yVal = Mathf.Abs (tileToReturn.Center.y - position.y);
		foreach (Tile tile in tileToReturn.LayeredTiles)
		{
			if (Mathf.Abs (tile.Center.y - position.y) < yVal)
			{
				yVal = Mathf.Abs (tile.Center.y - position.y);
				tileToReturn = tile;
			}
		}
		
		if (tileToReturn.Status == Const.TILE_Blocked || tileToReturn.ExpectingArrival)
		{
			//Need to iterate to find closest available tile
			int directionCounter = Const.DIRECTION_Right;
			int widthCounter = 1;
			int lengthCounter = 1;
			int IValue = tileToReturn.I;
			int JValue = tileToReturn.J;
			
		    while (tileToReturn.Status == Const.TILE_Blocked || tileToReturn.ExpectingArrival)
			{
				int counter;
				
				//If we're travelling left or right use the width counter, up or down use the length counter
				if (directionCounter == Const.DIRECTION_Right || directionCounter == Const.DIRECTION_Left)
				{
					counter = widthCounter;
				}
				else
				{
					counter = lengthCounter;
				}
				
				for (int i=0; i<counter; i++)
				{
					switch (directionCounter)
					{
					case Const.DIRECTION_Right:
						//Increase I value (go right)
						IValue++;
						
						//Check if we're at the width so we don't get an exception
						if (IValue >= Width)
						{
							//We're past the width, decrease I value
							IValue = Width-1;
							
							//Set JValue to whatever it is minus lengthcounter (no point checking tiles we've already checked!)
							JValue = JValue - lengthCounter;
							
							//Since we've skipped all the downward tiles, go left
							directionCounter = Const.DIRECTION_Left;
							
							//Update the length counter as we're skipping it out
							lengthCounter++;
						}
						else
						{
							//Have we travelled far enough?
							if (i == widthCounter - 1)
							{
								//We've travelled as far as we want to, change the direction and increase the width counter
								directionCounter = Const.DIRECTION_Down;
								widthCounter++;
							}
						}
						
						break;
						
					case Const.DIRECTION_Down:
						
						JValue--;
						if (JValue < 0)
						{
							JValue = 0;
							
							IValue = IValue - widthCounter;
							
							directionCounter = Const.DIRECTION_Up;
							
							widthCounter++;
						}
						else
						{
							if (i == lengthCounter - 1)
							{
								directionCounter = Const.DIRECTION_Left;
								lengthCounter++;
							}
						}
						
						break;
						
					case Const.DIRECTION_Left:
						
						IValue--;
						if (IValue < 0)
						{
							IValue = 0;
							
							JValue = JValue + lengthCounter;
							
							directionCounter = Const.DIRECTION_Right;
							
							lengthCounter++;
						}
						else
						{
							if (i == widthCounter - 1)
							{
								directionCounter = Const.DIRECTION_Up;
								widthCounter++;
							}
						}
						
						break;
						
					case Const.DIRECTION_Up:
						
						JValue++;
						if (JValue >= Length)
						{
							JValue = Length-1;
							
							IValue = IValue + widthCounter;
							
							directionCounter = Const.DIRECTION_Down;
							
							widthCounter++;
						}
						else
						{
							if (i == lengthCounter - 1)
							{
								directionCounter = Const.DIRECTION_Right;
								lengthCounter++;
							}
						}
						
						break;
					}
					
					tileToReturn = m_Grid[IValue, JValue];
					
					yVal = Mathf.Abs (tileToReturn.Center.y - position.y);
					foreach (Tile tile in tileToReturn.LayeredTiles)
					{
						if (Mathf.Abs (tile.Center.y - position.y) < yVal)
						{
							yVal = Mathf.Abs (tile.Center.y - position.y);
							tileToReturn = tile;
						}
					}
				}
			}
		}
		
		return tileToReturn;
	}
	
	public static Tile GetClosestArrivalTile(Vector3 position)
	{
		int iValue = (int)((position.x - m_WidthOffset)/m_TileSize);
		int jValue = (int)((position.z - m_LengthOffset)/m_TileSize);
		
		if (iValue < 0) iValue = 0;
		else if (iValue >= Width) iValue = Width-1;
		
		if (jValue < 0) jValue = 0;
		else if (jValue >= Length) jValue = Length-1;
		
		Tile tileToReturn = m_Grid[iValue, jValue];
		
		float yVal = Mathf.Abs (tileToReturn.Center.y - position.y);
		foreach (Tile tile in tileToReturn.LayeredTiles)
		{
			if (Mathf.Abs (tile.Center.y - position.y) < yVal)
			{
				yVal = Mathf.Abs (tile.Center.y - position.y);
				tileToReturn = tile;
			}
		}
		
		if (tileToReturn.Status == Const.TILE_Blocked || tileToReturn.ExpectingArrival)
		{
			Queue<Tile> tilesToCheck = new Queue<Tile>();
			tilesToCheck.Enqueue (tileToReturn);
			
			while (tilesToCheck.Count > 0)
			{
				tileToReturn = tilesToCheck.Dequeue ();
				
				foreach (Tile tile in tileToReturn.AccessibleTiles)
				{
					if (tile.Status != Const.TILE_Blocked && !tile.ExpectingArrival)
					{
						return tile;
					}
					
					if (!tilesToCheck.Contains (tile))
					{
						tilesToCheck.Enqueue (tile);
					}
				}
			}
			
			tileToReturn = null;
		}
		
		return tileToReturn;
	}
	
	public static void FindAccessibleTiles(Tile tile)
	{
		//Need to find which tiles this tile can travel to
		try
		{
			Tile tileLeft = m_Grid[tile.I-1, tile.J];
			Tile tileRight = m_Grid[tile.I+1, tile.J];
			Tile tileUp = m_Grid[tile.I, tile.J+1];
			Tile tileDown = m_Grid[tile.I, tile.J-1];
			
			Tile topLeft = m_Grid[tile.I-1, tile.J+1];
			Tile topRight = m_Grid[tile.I+1, tile.J+1];
			Tile bottomRight = m_Grid[tile.I+1, tile.J-1];
			Tile bottomLeft = m_Grid[tile.I-1, tile.J-1];
			
			CheckTileConnection (tile, tileLeft);
			CheckTileConnection (tile, tileRight);
			CheckTileConnection (tile, tileUp);
			CheckTileConnection (tile, tileDown);
			CheckTileConnection (tile, topLeft);
			CheckTileConnection (tile, topRight);
			CheckTileConnection (tile, bottomRight);
			CheckTileConnection (tile, bottomLeft);
		}
		catch 
		{
			//Silently ignored, bad coder!
		}
	}
	
	private static void CheckTileConnection(Tile currentTile, Tile tileToCheck)
	{
		float acceptableHeightDiff = 2.5f;
		if (tileToCheck.Status == Const.TILE_Unvisited && Mathf.Abs (tileToCheck.Center.y - currentTile.Center.y) < acceptableHeightDiff)
		{
			currentTile.AccessibleTiles.Add (tileToCheck);
		}
		
		//Check if any layered tiles are accessible
		foreach (Tile layeredTile in tileToCheck.LayeredTiles)
		{
			if (Mathf.Abs (currentTile.Center.y - layeredTile.Center.y) < acceptableHeightDiff && layeredTile.BridgeTunnelEntrance)
			{
				currentTile.AccessibleTiles.Add (layeredTile);
			}
		}
		
		//Check if the current tile's layered tiles can access any of the other tiles
		foreach (Tile currentLayeredTile in currentTile.LayeredTiles)
		{
			if (Mathf.Abs (currentLayeredTile.Center.y - tileToCheck.Center.y) < acceptableHeightDiff && tileToCheck.Status == Const.TILE_Unvisited && currentLayeredTile.BridgeTunnelEntrance)
			{
				currentLayeredTile.AccessibleTiles.Add (tileToCheck);
			}
			
			foreach (Tile layeredTile in tileToCheck.LayeredTiles)
			{
				if (Mathf.Abs (currentLayeredTile.Center.y - layeredTile.Center.y) < acceptableHeightDiff)
				{
					currentLayeredTile.AccessibleTiles.Add (layeredTile);
				}
			}
		}
	}
	
	public static void SetTunnelSideTileToBlocked(int I, int J)
	{
		m_Grid[I, J].Status = Const.TILE_Blocked;
	}
	
	public static void AddLayeredTile(int gridI, int gridJ, float height, bool isBridge, Collider collider)
	{
		Vector3 baseCenter = m_Grid[gridI, gridJ].Center;
		Vector3 centerPos = new Vector3(baseCenter.x, height, baseCenter.z);
		m_Grid[gridI, gridJ].LayeredTiles.Add (new Tile(gridI, gridJ, centerPos, isBridge, !isBridge));
	}
	
	public static void AssignGrid(Tile[,] grid)
	{
		m_Grid = grid;
	}
	
	private static void BuildBridge(Collider bridgeCollider)
	{
		//Find min and max tiles
		Tile maxTile = GetClosestTile (bridgeCollider.bounds.max);
		Tile minTile = GetClosestTile (bridgeCollider.bounds.min);
		
		bool leftToRight = bridgeCollider.bounds.size.x > bridgeCollider.bounds.size.z;
		
		int firstMinNumber, firstMaxNumber, secondMinNumber, secondMaxNumber;
		
		if (leftToRight)
		{
			firstMinNumber = minTile.I;
			firstMaxNumber = maxTile.I;
			
			secondMinNumber = minTile.J;
			secondMaxNumber = maxTile.J;
		}
		else
		{
			firstMinNumber = minTile.J;
			firstMaxNumber = maxTile.J;
			
			secondMinNumber = minTile.I;
			secondMaxNumber = maxTile.I;
		}
		
		for (int i = firstMinNumber; i<= firstMaxNumber; i++)
		{
			for (int j = secondMinNumber; j <= secondMaxNumber; j++)
			{
				Tile terrainTile;
				
				if (leftToRight)
				{
					terrainTile = m_Grid[i,j];
				}
				else
				{
					terrainTile = m_Grid[j,i];
				}
				
				if (j != secondMinNumber && j != secondMaxNumber)
				{
					//Create tile and check if tile underneath is passable, we're raycasting as ClosestPointOnBounds wouldn't return the value we wanted if the bridge is sloped
					Tile tileToAdd;
					
					if (leftToRight)
					{
						Ray yValueRay = new Ray(m_Grid[i,j].Center+(Vector3.up*1000), Vector3.down);
						RaycastHit hitInfo;
						if (bridgeCollider.Raycast (yValueRay, out hitInfo, Mathf.Infinity))
						{
							tileToAdd = new Tile(i, j, new Vector3(m_Grid[i,j].Center.x, hitInfo.point.y, m_Grid[i,j].Center.z), true, false, bridgeCollider);
						}
						else
						{
							//We didn't hit the collider, that means it's either an entrance or exit, now we have to use ClosestPointOnBounds instead
							tileToAdd = new Tile(i, j, new Vector3(m_Grid[i,j].Center.x, bridgeCollider.ClosestPointOnBounds (m_Grid[i,j].Center+(Vector3.up*10)).y, m_Grid[i,j].Center.z), true, false, bridgeCollider);
						}
					}
					else
					{
						Ray yValueRay = new Ray(m_Grid[j,i].Center+(Vector3.up*1000), Vector3.down);
						RaycastHit hitInfo;
						if (bridgeCollider.Raycast (yValueRay, out hitInfo, Mathf.Infinity))
						{
							tileToAdd = new Tile(j, i, new Vector3(m_Grid[j,i].Center.x, hitInfo.point.y, m_Grid[j,i].Center.z), true, false, bridgeCollider);
						}
						else
						{
							tileToAdd = new Tile(j, i, new Vector3(m_Grid[j,i].Center.x, bridgeCollider.ClosestPointOnBounds (m_Grid[j,i].Center+(Vector3.up*10)).y, m_Grid[j,i].Center.z), true, false, bridgeCollider);
						}
					}
					
					if (i == firstMinNumber || i == firstMaxNumber)
					{
						tileToAdd.BridgeTunnelEntrance = true;
					}
					
					terrainTile.LayeredTiles.Add(tileToAdd);
				}
				
				//Raycast to find height (but beware the bridge might not cover the center point)
				//Since the bridge might not be over the center point, we'll raycast in 3 places
				Vector3 startPoint = terrainTile.Center+(Vector3.down*5);
				Ray rayCenter = new Ray(startPoint, Vector3.up);
				Ray ray1;
				Ray ray2;
				RaycastHit hit;
				
				if (leftToRight)
				{
					//ray1 wants to be above, ray2 below
					Vector3 zHalfTileSize = new Vector3(0, 0, TileSize/2.0f);
					ray1 = new Ray(startPoint + zHalfTileSize, Vector3.up);
					ray2 = new Ray(startPoint - zHalfTileSize, Vector3.up);
				}
				else
				{
					//ray1 wants to be to the left, ray2 to the right
					Vector3 xHalfTileSize = new Vector3(TileSize/2.0f, 0, 0);
					ray1 = new Ray(startPoint - xHalfTileSize, Vector3.up);
					ray2 = new Ray(startPoint + xHalfTileSize, Vector3.up);
				}
				
				float heightVal;
				if (bridgeCollider.Raycast (rayCenter, out hit, Mathf.Infinity))
				{
					heightVal = hit.point.y;
				}
				else if (bridgeCollider.Raycast (ray1, out hit, Mathf.Infinity))
				{
					heightVal = hit.point.y;
				}
				else if (bridgeCollider.Raycast (ray2, out hit, Mathf.Infinity))
				{
					heightVal = hit.point.y;
				}
				else
				{
					//We haven't hit any of our points, so we must an entrance/exit tile, set it to blocked
					terrainTile.Status = Const.TILE_Blocked;
					continue;
				}
				
				if (Mathf.Abs (heightVal - terrainTile.Center.y) < PassableHeight)
				{
					terrainTile.Status = Const.TILE_Blocked;
				}
			}
		}
	}
	
	private static void BuildTunnel(Collider tunnelCollider)
	{
		//Find min and max tiles
		Tile maxTile = GetClosestTile (tunnelCollider.bounds.max);
		Tile minTile = GetClosestTile (tunnelCollider.bounds.min);
		
		bool leftToRight = tunnelCollider.bounds.size.x > tunnelCollider.bounds.size.z;
		
		int firstMinNumber, firstMaxNumber, secondMinNumber, secondMaxNumber;
		
		if (leftToRight)
		{
			firstMinNumber = minTile.I;
			firstMaxNumber = maxTile.I;
			
			secondMinNumber = minTile.J;
			secondMaxNumber = maxTile.J;
		}
		else
		{
			firstMinNumber = minTile.J;
			firstMaxNumber = maxTile.J;
			
			secondMinNumber = minTile.I;
			secondMaxNumber = maxTile.I;
		}
		
		for (int i = firstMinNumber; i<= firstMaxNumber; i++)
		{
			for (int j = secondMinNumber; j <= secondMaxNumber; j++)
			{
				Tile terrainTile;
				
				if (leftToRight)
				{
					terrainTile = m_Grid[i,j];
				}
				else
				{
					terrainTile = m_Grid[j,i];
				}
				
				if (j != secondMinNumber && j != secondMaxNumber)
				{
					//Create tile and check if tile underneath is passable
					Tile tileToAdd;
					
					if (leftToRight)
					{
						tileToAdd = new Tile(i, j, new Vector3(m_Grid[i,j].Center.x, tunnelCollider.bounds.max.y, m_Grid[i,j].Center.z), false, true, tunnelCollider);
					}
					else
					{
						tileToAdd = new Tile(j, i, new Vector3(m_Grid[j,i].Center.x, tunnelCollider.bounds.max.y, m_Grid[j,i].Center.z), false, true, tunnelCollider);
					}
					
					if (i == firstMinNumber || i == firstMaxNumber)
					{
						tileToAdd.BridgeTunnelEntrance = true;
					}
					
					terrainTile.LayeredTiles.Add(tileToAdd);
				}
				
				//Check if tile underneath is passable
				if (Mathf.Abs (tunnelCollider.bounds.max.y - terrainTile.Center.y) < PassableHeight)
				//if (terrainTile.Center.y < tunnelCollider.bounds.max.y)
				{
					terrainTile.Status = Const.TILE_Blocked;
				}
			}
		}
	}
}
