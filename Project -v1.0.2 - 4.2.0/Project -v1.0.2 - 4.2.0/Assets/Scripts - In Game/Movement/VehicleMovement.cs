using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RTSObject))]
public class VehicleMovement : LandMovement {
	
	private bool m_RequestingThread = false;

	public float RotationalSpeed
	{ get; set;}
	public float Acceleration
	{ get; set;}
	
	
	public override Vector3 TargetLocation 
	{
		get 
		{
			if (Path == null || Path.Count == 0)
			{
				return Vector3.zero;
			}
			else
			{
				return Path[Path.Count-1];
			}
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		m_Parent = GetComponent<RTSObject>();
		UpdateCurrentTile ();
		m_CurrentTile.SetOccupied(m_Parent, false);
		SetAngles ();
	}
	
	// Update is called once per frame
	protected new void Update () 
	{
		base.Update ();
		
		if (Application.isEditor)
		{
			if (Path != null)
			{
				for (int i=1; i<Path.Count; i++)
				{
					Debug.DrawLine (Path[i-1], Path[i]);
				}
			}
			else
			{
				Debug.DrawLine (transform.position, transform.position+Vector3.forward*10);
			}
		}
		
		if (Path != null && Path.Count > 0)
		{
			//We have a path, lets move!
			//Make sure we're pointing at the target
			if (PointingAtTarget() )
			{
				//We're pointing at the target, lets move
				MoveForward ();
			}
			
			//Update the current tile
			UpdateCurrentTile();
			
			//Set the units height, x angle and z angle to correct values
			SetAngles ();
		}
	}

	public override void MoveTo(Vector3 position)
	{
		if (m_ArrivalTile != null) m_ArrivalTile.ExpectingArrival = false;
		m_ArrivalTile = Grid.GetClosestArrivalTile(position);
		m_ArrivalTile.ExpectingArrival = true;
		
		Tile currentTile = Grid.GetClosestTile (transform.position);
		ManagerResolver.Resolve<IThreadManager>().AddPathfindingThread (new GetPathThread(m_Parent, currentTile, m_ArrivalTile, Const.BLOCKINGLEVEL_Normal));
	}
	
	public override void Stop()
	{
		if (Path != null && Path.Count > 0)
		{
			Vector3 nextPos = Path[0];
			Path.Clear ();
			Path.Add (nextPos);
		}
	}

	public override void Follow (Transform target)
	{
		
	}
	
	public override void AssignDetails(Item item)
	{
		Speed = item.Speed;
		CurrentSpeed = 0;
		RotationalSpeed = item.RotationSpeed;
		Acceleration = item.Acceleration;
	}
	
	private bool PointingAtTarget()
	{
		Vector3 forwardVector = transform.forward;
		Vector3 targetVector = Path[0]-transform.position;
		
		forwardVector.y = 0;
		targetVector.y = 0;
				
		float angle = Vector3.Angle (forwardVector, targetVector);
		Vector3 crossProduct = Vector3.Cross (forwardVector, targetVector);
		
		if (crossProduct.y < 0) angle *= -1;
		
		if (Mathf.Abs (angle) < 2.0f)
		{
			return true;
		}
		else
		{
			//We need to rotate
			int direction = 1;
			if (angle < 0)
			{
				direction = -1;
			}
			
			transform.Rotate (0, RotationalSpeed*Time.deltaTime*direction, 0);
		}
		
		return false;
	}
	
	private void UpdateCurrentTile()
	{
		//What sort of tile are we on
		Tile currentTile = m_CurrentTile;
		m_CurrentTile = Grid.GetClosestTile(transform.position);
		
		if (currentTile != null && currentTile != m_CurrentTile)
		{
			//We've changed tiles, make sure the old tile is no longer occupied
			currentTile.NoLongerOccupied(m_Parent);
			
			//Since we've changed, our next tile should equal the target tile
			if (m_CurrentTile != m_TargetTile)
			{
				//If it doesn't equal then we've drifted off the tiles slightly, set the current tile the target tile
				m_CurrentTile = m_TargetTile;
			}
			
			if (Path.Count == 1)
			{
				m_CurrentTile.SetOccupied(m_Parent, true);
			}
			else
			{
				m_CurrentTile.SetOccupied(m_Parent, false);
			}
		}
	}
	
	private void SetAngles()
	{
		//Update Height and x-z rotation
		
		
		Vector3 checkLeft = transform.position - transform.right;
		Vector3 checkRight = transform.position + transform.right;
		Vector3 checkForward = transform.position + transform.forward;
		Vector3 checkBack = transform.position - transform.forward;
		
		float heightValCenter, heightValLeft, heightValRight, heightValForward, heightValBack;
		
		if ((m_CurrentTile.IsBridge || m_CurrentTile.IsTunnel) && m_CurrentTile.BridgeOrTunnelCollider != null)
		{
			//We're on a bridge or in a tunnel
			Ray rayCenter = new Ray(transform.position+(Vector3.up*10), Vector3.down);
			Ray rayLeft = new Ray(checkLeft+(Vector3.up*10), Vector3.down);
			Ray rayRight = new Ray(checkRight+(Vector3.up*10), Vector3.down);
			Ray rayForward = new Ray(checkForward+(Vector3.up*10), Vector3.down);
			Ray rayBack = new Ray(checkBack+(Vector3.up*10), Vector3.down);
			
			RaycastHit hit;
			
			if (m_CurrentTile.BridgeOrTunnelCollider.Raycast (rayCenter, out hit, Mathf.Infinity))
			{
				heightValCenter = hit.point.y;
			}
			else
			{
				heightValCenter = Terrain.activeTerrain.SampleHeight (transform.position);
			}
			
			if (m_CurrentTile.BridgeOrTunnelCollider.Raycast (rayLeft, out hit, Mathf.Infinity))
			{
				heightValLeft = hit.point.y;
			}
			else
			{
				heightValLeft = Terrain.activeTerrain.SampleHeight (checkLeft);
			}
			
			if (m_CurrentTile.BridgeOrTunnelCollider.Raycast (rayRight, out hit, Mathf.Infinity))
			{
				heightValRight = hit.point.y;
			}
			else
			{
				heightValRight = Terrain.activeTerrain.SampleHeight (checkRight);
			}
			
			if (m_CurrentTile.BridgeOrTunnelCollider.Raycast (rayForward, out hit, Mathf.Infinity))
			{
				heightValForward = hit.point.y;
			}
			else
			{
				heightValForward = Terrain.activeTerrain.SampleHeight (checkForward);
			}
			
			if (m_CurrentTile.BridgeOrTunnelCollider.Raycast (rayBack, out hit, Mathf.Infinity))
			{
				heightValBack = hit.point.y;
			}
			else
			{
				heightValBack = Terrain.activeTerrain.SampleHeight (checkBack);
			}
		}
		else
		{
			//We're on terrain, sample heights
			heightValCenter = Terrain.activeTerrain.SampleHeight (transform.position);
			heightValLeft = Terrain.activeTerrain.SampleHeight (checkLeft);
			heightValRight = Terrain.activeTerrain.SampleHeight (checkRight);
			heightValForward = Terrain.activeTerrain.SampleHeight (checkForward);
			heightValBack = Terrain.activeTerrain.SampleHeight (checkBack);
		}
		
		//Now we have our height values, we need to set the height and calculate the desired rotation
		//Set height
		m_Position.x = transform.position.x;
		m_Position.y = heightValCenter;
		m_Position.z = transform.position.z;
		
		transform.position = m_Position;
		
		//Rotation along x axis
		float xHeight = heightValForward-heightValBack;
		float xDistance = Vector3.Distance (checkForward, checkBack);
		float xAngle = Mathf.Atan (xHeight/xDistance)*Mathf.Rad2Deg*-1;
		
		//along z axis
		float zHeight = heightValLeft - heightValRight;
		float zDistance = Vector3.Distance (checkLeft, checkRight);
		float zAngle = Mathf.Atan (zHeight/zDistance)*Mathf.Rad2Deg*-1;
		
		Vector3 rotation = new Vector3(xAngle, transform.rotation.eulerAngles.y, zAngle);
		
		//Set rotation
		transform.rotation = Quaternion.Euler (rotation);
	}
	
	private void MoveForward()
	{
		if (m_TargetTile != null)
		{
			if (!m_TargetTile.IsOccupied || m_TargetTile.OccupiedBy == m_Parent)
			{
				CurrentSpeed += Acceleration*Time.deltaTime;
				
				if (CurrentSpeed >= Speed)
				{
					CurrentSpeed = Speed;
				}
				
				transform.Translate (0, 0, CurrentSpeed*Time.deltaTime, Space.Self);
				
				if (Vector3.Distance (transform.position, Path[0]) < 1.0f)
				{
					Path.RemoveAt (0);
					
					if (Path.Count > 0)
					{
						m_TargetTile = Grid.GetClosestTile (Path[0]);
					}
					else
					{
						m_TargetTile = null;
					}
				}
			}
			else if (m_TargetTile.IsOccupiedStatic)
			{
				//We're occupied static, lets find a path without static obstacles
				if (!m_RequestingThread)
				{
					ManagerResolver.Resolve<IThreadManager>().AddPathfindingThread (new GetPathThread(m_Parent, m_CurrentTile, m_ArrivalTile, Const.BLOCKINGLEVEL_OccupiedStatic, ThreadCallBack));
					m_RequestingThread = true;
				}			
			}
			else if (m_TargetTile.IsOccupied)
			{
				//We want to wait, unless the unit on the occupied tile is waiting for this tile
				if (!m_RequestingThread && m_TargetTile.OccupiedBy.GetComponent<VehicleMovement>().TargetTile == m_CurrentTile)
				{
					ManagerResolver.Resolve<IThreadManager>().AddPathfindingThread (new GetPathThread(m_Parent, m_CurrentTile, m_ArrivalTile, Const.BLOCKINGLEVEL_Occupied, ThreadCallBack));
					m_RequestingThread = true;
					
					//Set target tile to null, this will stop the other unit from also trtying to find a dynamic path, but it will get updated once the thread finishes
					m_TargetTile = null;
				}
			}
		}
	}
	
	private void ThreadCallBack()
	{
		m_RequestingThread = false;
	}
}