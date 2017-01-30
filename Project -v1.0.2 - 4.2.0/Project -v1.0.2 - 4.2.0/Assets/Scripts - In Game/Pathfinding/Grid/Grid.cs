using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class Grid : MonoBehaviour, IGrid 
{
	//Singleton

	private static Grid Main;

	public static Grid main
	{get{ 
			if (Main == null) {
				Main = (Grid)FindObjectOfType (typeof(Grid));
			}
			return Main;
		}
		
	}



	//Member variables
	public  bool m_ShowGrid = false;	
	public  bool m_ShowOpenTiles = true;
	public  bool m_ShowClosedTiles = true;

	
	public float TileSize = 7.5f;
	public int Width = 130;
	public int Length = 130;
	public float WidthOffset = -1170;
	public float LengthOffset = -280;
	public  float MaxSteepness = 2.5f;
	public  float PassableHeight = 2.0f;
	public  int BlockIndent = 5;
	
	private  Tile[,] m_Grid;
	
	private  List<Vector3> debugAlgo = new List<Vector3>();




	//Properties
	public  bool ShowGrid
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
	
	public  bool ShowOpenTiles
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
	
	public bool ShowClosedTiles
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



	
	void Start()
	{
		//main = this;
		if (Application.isPlaying)
		{
			StartCoroutine (InitialiseAsRoutine ());
		}
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
						Gizmos.DrawWireCube (tile.Center, new Vector3(TileSize, 0.1f, TileSize));
					}
				}
				else
				{
					if (ShowOpenTiles)
					{
						Gizmos.color = Color.white;
						Gizmos.DrawWireCube (tile.Center, new Vector3(TileSize, 0.1f, TileSize));
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
	
	public  IEnumerator InitialiseAsRoutine()
	{
		ILevelLoader levelLoader = null;//ManagerResolver.Resolve<ILevelLoader>();
		
		m_Grid = new Tile[ Width, Length];
		
		//Create tiles
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Length; j++)
			{
				float xCenter = main.WidthOffset + ((i*TileSize) + (TileSize/2.0f));
				float zCenter = main.LengthOffset + ((j*TileSize) + (TileSize/2.0f));
				Vector3 center = new Vector3(xCenter, 0, zCenter);				
				center.y = Terrain.activeTerrain.SampleHeight(center);
				
				m_Grid[i,j] = new Tile(i, j, center);
			}
		}

		if (levelLoader != null) levelLoader.ChangeText ("Evaluating tiles");
		yield return null;


		


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

	}
	
	public void Initialise()
	{//main = this;
		m_Grid = new Tile[Width, Length];
		
		//Create tiles
		for (int i=0; i<Width; i++)
		{
			for (int j=0; j<Length; j++)
			{
				float xCenter = main.WidthOffset + ((i*TileSize) + (TileSize/2.0f));
				float zCenter = main.LengthOffset + ((j*TileSize) + (TileSize/2.0f));
				Vector3 center = new Vector3(xCenter, 0, zCenter);				
				center.y = Terrain.activeTerrain.SampleHeight(center);
				
				m_Grid[i,j] = new Tile(i, j, center);
			}
		}



	}
	
	public Tile GetClosestTile(Vector3 position)
	{
		int iValue = (int)((position.x - main.WidthOffset)/TileSize);
		int jValue = (int)((position.z - main.LengthOffset)/TileSize);
		
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
	
	public  Tile GetClosestAvailableTile(Vector3 position)
	{
		debugAlgo.Clear ();
		int iValue = (int)((position.x - main.WidthOffset)/TileSize);
		int jValue = (int)((position.z - main.LengthOffset)/TileSize);
		
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





	public  Tile GetClosestRedTile(Vector3 position)
	{if (m_Grid == null) {
			Initialise ();
		}
		int iValue = (int)((position.x - main.WidthOffset)/TileSize);
		int jValue = (int)((position.z - main.LengthOffset)/TileSize);

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

		if (tileToReturn.Status == Const.TILE_Blocked){
			return tileToReturn;}
	


			//Need to iterate to find closest available tile
			int directionCounter = Const.DIRECTION_Right;
			int widthCounter = 1;
			int lengthCounter = 1;
			int IValue = tileToReturn.I;
			int JValue = tileToReturn.J;

			while (tileToReturn.Status != Const.TILE_Blocked)
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


		return tileToReturn;
	}






	
	public  Tile GetClosestAvailableFreeTile(Vector3 position)
	{
		int iValue = (int)((position.x -main.WidthOffset)/TileSize);
		int jValue = (int)((position.z - main.LengthOffset)/TileSize);
		
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
	
	public  Tile GetClosestArrivalTile(Vector3 position)
	{
		int iValue = (int)((position.x - main.WidthOffset)/TileSize);
		int jValue = (int)((position.z - main.LengthOffset)/TileSize);
		
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
	
	public  void FindAccessibleTiles(Tile tile)
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
	
	private  void CheckTileConnection(Tile currentTile, Tile tileToCheck)
	{
		float acceptableHeightDiff = 2.5f;
		if (tileToCheck.Status == Const.TILE_Unvisited && Mathf.Abs (tileToCheck.Center.y - currentTile.Center.y) < acceptableHeightDiff)
		{
			currentTile.AccessibleTiles.Add (tileToCheck);
		}
		
		//Check if any layered tiles are accessible
		foreach (Tile layeredTile in tileToCheck.LayeredTiles)
		{
			if (Mathf.Abs (currentTile.Center.y - layeredTile.Center.y) < acceptableHeightDiff )
			{
				currentTile.AccessibleTiles.Add (layeredTile);
			}
		}
		
		//Check if the current tile's layered tiles can access any of the other tiles
		foreach (Tile currentLayeredTile in currentTile.LayeredTiles)
		{
			if (Mathf.Abs (currentLayeredTile.Center.y - tileToCheck.Center.y) < acceptableHeightDiff && tileToCheck.Status == Const.TILE_Unvisited )
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



	public  void AddLayeredTile(int gridI, int gridJ, float height,  Collider collider)
	{
		Vector3 baseCenter = m_Grid[gridI, gridJ].Center;
		Vector3 centerPos = new Vector3(baseCenter.x, height, baseCenter.z);
		m_Grid[gridI, gridJ].LayeredTiles.Add (new Tile(gridI, gridJ, centerPos));
	}
	
	public  void AssignGrid(Tile[,] grid)
	{
		m_Grid = grid;
	}

}


