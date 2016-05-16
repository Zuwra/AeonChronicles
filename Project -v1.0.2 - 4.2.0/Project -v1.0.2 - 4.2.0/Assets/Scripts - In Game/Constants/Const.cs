using UnityEngine;
using System.Collections;

public static class Const {

	public const int TEAM_GRI = 0;
	public const int TEAM_SALUS = 1;
	
	public const int ORDER_STOP = 0;
	public const int ORDER_MOVE_TO = 1;
	public const int ORDER_ATTACK = 2;
	public const int ORDER_DEPLOY = 3;
	public const int ORDER_AttackMove = 4;
	public const int ORDER_Follow = 5; 
	public const int ORDER_Interact = 6; 
	public const int Order_HoldGround = 7;
	public const int Order_Patrol = 8;


	
	public const int TYPE_Building = 0;
	public const int TYPE_Support = 1;
	public const int TYPE_Infantry = 2;
	public const int TYPE_Vehicle = 3;
	public const int TYPE_Air = 4;
	

	
	public const int TILE_Open = 1;
	public const int TILE_Closed = 2;
	public const int TILE_Unvisited = 3;
	public const int TILE_Blocked = 4;
	public const int TILE_OnRoute = 6;
	
	public const int LAYEREDTILE_NotLayered = 0;
	public const int LAYEREDTILE_Bridge = 1;
	public const int LAYEREDTILE_Tunnel = 2;
	
	public const float ASTAR_Costinc = 10.0f;
	public const float ASTAR_CostDinc = 14.0f;
	
	public const int BLOCKINGLEVEL_Normal = 0;
	public const int BLOCKINGLEVEL_Occupied = 1;
	public const int BLOCKINGLEVEL_OccupiedStatic = 2;
	
	public const int DIRECTION_Right = 0;
	public const int DIRECTION_Down = 1;
	public const int DIRECTION_Left = 2;
	public const int DIRECTION_Up = 3;
}
