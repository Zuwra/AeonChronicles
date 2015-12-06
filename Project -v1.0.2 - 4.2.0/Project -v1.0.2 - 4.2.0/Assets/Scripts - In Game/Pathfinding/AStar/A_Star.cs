using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Star {
	
	//Member vairables
	private bool m_EndSearch = false;
	
	private  int m_BlockingLevel;
	
	private Tile m_CurrentTile;
	private Tile m_GoalTile;
	
	private List<Tile> m_OpenTiles = new List<Tile>();
	private List<Tile> m_ClosedTiles = new List<Tile>();
	
	//Constructor
	public A_Star()
	{
		
	}
	
	//Methods
	public List<Vector3> FindVectorPath(Tile currentTile, Tile targetTile, int blockingLevel)
	{
		Vector3 origPosition = currentTile.Center;
		m_CurrentTile = currentTile;
		m_GoalTile = targetTile;
		m_BlockingLevel = blockingLevel;
		
		//Check it goal tile and current tile match
		if (m_CurrentTile == m_GoalTile)
		{
			return new List<Vector3>
			{
				origPosition,
			};
		}
		
		m_EndSearch = false;
		m_CurrentTile.Status = Const.TILE_Open;
		m_CurrentTile.Cost = 0 ;
		m_CurrentTile.Start = true;	
		m_GoalTile.End = true;
		m_OpenTiles.Clear ();
		m_ClosedTiles.Clear ();
		m_OpenTiles.Add (m_CurrentTile);
		
		do
		{			
			FindCheapestTile();
			Walk();	
			
			//If we're out of open tiles then return
			if (m_OpenTiles.Count == 0 && !m_EndSearch)
			{
				ResetOpenClosedTiles();
				
				return new List<Vector3>
				{
					origPosition,
				};
			}
		} while (m_EndSearch == false);
		
		
		List<Vector3> valueToReturn = FindRoute ();
		valueToReturn.RemoveAt (0);
		
		ResetOpenClosedTiles();
		
		return valueToReturn;
	}
	
	private void ResetOpenClosedTiles()
	{
		foreach (Tile t in m_OpenTiles)
		{
			t.Status = Const.TILE_Unvisited;
			t.Start = false;
			t.End = false;
			t.Cost = 0;
			t.hCost = 0;
			t.fCost = 0;
			t.ParentTile = null;
		}
		
		foreach (Tile t in m_ClosedTiles)
		{
			t.Status = Const.TILE_Unvisited;
			t.Start = false;
			t.End = false;
			t.Cost = 0;
			t.hCost = 0;
			t.fCost = 0;
			t.ParentTile = null;
		}
	}
	
	private float Heuristic_Diagonal(int x, int y)
	{
		int dx = Mathf.Abs(m_GoalTile.I - x);
		int dy = Mathf.Abs(m_GoalTile.J - y);

		int min_d = Mathf.Min(dx, dy);

		int h_travel = dx + dy;

		return ((Const.ASTAR_CostDinc*min_d) + (Const.ASTAR_Costinc*(h_travel - (2*min_d))));
	}
	
	private float Heuristic_Manhatten(int x, int y)
	{
		int dx = Mathf.Abs(m_GoalTile.I - x);
		int dy = Mathf.Abs(m_GoalTile.J - y);
		
		return (Const.ASTAR_Costinc*(dx+dy));
	}
	
	private void FindCheapestTile()
	{
		float lowcost = 10000.0f; 
		
		foreach (Tile t in m_OpenTiles)
		{
			if (t.fCost < lowcost)
			{
				lowcost = t.fCost;  
				m_CurrentTile = t;
			}
		}
		
   		// have we reached the destination ?. If yes then generate the route
		if (m_CurrentTile == m_GoalTile)
		{
			m_EndSearch = true;
		}
	}
	
	private void UpdateTile(Tile tile, float cost)
	{
		//If we're updating an already open tile, don't re-add it to the open list
		if (tile.Status != Const.TILE_Open) 
		{
			m_OpenTiles.Add (tile);
		}
		tile.Status = Const.TILE_Open;
		tile.Cost = m_CurrentTile.Cost + cost;
		tile.hCost = Heuristic_Diagonal (tile.I, tile.J);
		tile.fCost = tile.Cost + tile.hCost;
		tile.ParentTile = m_CurrentTile;
	}
	
	private void Walk()
	{
		foreach (Tile tile in m_CurrentTile.AccessibleTiles)
		{
			GetConnection (m_CurrentTile, tile);
		}	

		m_CurrentTile.Status = Const.TILE_Closed;
		m_OpenTiles.Remove (m_CurrentTile);
		m_ClosedTiles.Add (m_CurrentTile);
	}
	
	private void GetConnection(Tile currentTile, Tile newTile)
	{
		float costinc;
		if (currentTile.I != newTile.I && currentTile.J != newTile.J)
		{
			costinc = newTile.CostDinc;
		}
		else
		{
			costinc = newTile.Costinc;
		}
		
		bool tileValidity = true;
		
		if (m_BlockingLevel == Const.BLOCKINGLEVEL_Normal)
		{
			tileValidity = true;
		}
		else if (m_BlockingLevel == Const.BLOCKINGLEVEL_OccupiedStatic)
		{
			tileValidity = !newTile.IsOccupiedStatic;
		}
		else if (m_BlockingLevel == Const.BLOCKINGLEVEL_Occupied)
		{
			tileValidity = !newTile.IsOccupied;
		}
		
		if (tileValidity)
		{
			switch(newTile.Status)
			{
			case Const.TILE_Unvisited:
				UpdateTile (newTile, costinc);
				break;
				
			case Const.TILE_Open:
				float newcost = currentTile.Cost + costinc ;
	 			if (newcost < newTile.Cost)
	  			{
					UpdateTile(newTile, costinc) ;
	  			}
				break;
			}
		}
	}
	
	private List<Vector3> FindRoute()
	{
		List<Vector3> path = new List<Vector3>();
		path.Insert(0, m_CurrentTile.Center);
		
		do
		{
			path.Insert (0, m_CurrentTile.ParentTile.Center);
			m_CurrentTile = m_CurrentTile.ParentTile;
		} while (m_CurrentTile.ParentTile != null);
		
		return path;
	}
}
