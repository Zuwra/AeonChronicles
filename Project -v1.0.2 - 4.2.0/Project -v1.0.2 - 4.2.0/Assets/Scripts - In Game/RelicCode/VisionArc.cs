using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class VisionArc : VisionBase {
/*
	public float Distance = 4;
	public float Direction = 0f;
	public float ArcSize = 40f;
	public bool CanSeeThroughWalls = false;
	public bool CanSeeTriggerColliders = true;
	public Vector3 eyeCenter = Vector3.up;

	public LayerMask VisibleLayers = Physics.AllLayers;
	public LayerMask VisionBlockedBy = Physics.AllLayers;

	private Collider[] colliders;

	#region implemented abstract members of VisionBase

	public override void DrawGizmos ()
	{
		Vector3 eyeWorld = transform.TransformPoint(eyeCenter);

		DebugDraw.DrawArc(eyeWorld, Direction + this.transform.rotation.eulerAngles.y, Distance, ArcSize, DebugColor);
		GameObject[] inVision = ObjectsInVision();
		foreach(GameObject obj in inVision){
			Debug.DrawLine(eyeWorld, obj.transform.position);
		}

		Vector3 visionForward = Quaternion.Euler(new Vector3(0, Direction, 0)) * transform.forward;

		Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, -(ArcSize/2f), 0));
		Quaternion rotateRight = Quaternion.Euler(new Vector3(0, ArcSize/2f, 0));		
		Vector3 leftV = rotateLeft * visionForward * Distance;
		Vector3 rightV = rotateRight * visionForward * Distance;
		Debug.DrawRay (eyeWorld, leftV, Color.blue);
		Debug.DrawRay (eyeWorld, rightV, Color.blue);
	}

	public override GameObject[] ObjectsInVision ()
	{
		Vector3 eyeWorld = transform.TransformPoint(eyeCenter);

		colliders = Physics.OverlapSphere (eyeWorld, Distance, VisibleLayers.value);

		//List<GameObject> 
			//retVal = new List<GameObject>();
		List<GameObject> retVal = new List<GameObject>();
		
		Vector3 visionForward = Quaternion.Euler(new Vector3(0, Direction, 0)) * transform.forward;
		float toleranceValue = Mathf.Cos (Mathf.Deg2Rad * ArcSize / 2f);

		Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, -(ArcSize/2f), 0));
		Quaternion rotateRight = Quaternion.Euler(new Vector3(0, ArcSize/2f, 0));
		
		Vector3 leftV = rotateLeft * visionForward * Distance;
		Vector3 rightV = rotateRight * visionForward * Distance;

		Ray leftRay = new Ray(eyeWorld, leftV);
		Ray rightRay = new Ray(eyeWorld, rightV);
		
		RaycastHit[] leftHits = Physics.RaycastAll(leftRay);
		RaycastHit[] rightHits = Physics.RaycastAll (rightRay);

		foreach(Collider col in colliders){
			if (((col.isTrigger && CanSeeTriggerColliders) || col.isTrigger == false) && (VisibleLayers == (VisibleLayers | (1 << col.gameObject.layer)))){
				Vector3 colliderPosition = col.bounds.center;

				Vector3 localPosition = colliderPosition - eyeWorld; 
				localPosition = new Vector3(localPosition.x, 0, localPosition.z);
				float dot = Vector3.Dot(visionForward, localPosition);
				dot /= (visionForward.magnitude * localPosition.magnitude);

				bool isHitByEdges = false;
				foreach(RaycastHit h in leftHits){
					if(h.collider == col){
						isHitByEdges = true;
						break;
					}
				}
				foreach(RaycastHit h in rightHits){
					if(h.collider == col){
						isHitByEdges = true;
						break;
					}
				}

				if(dot >= toleranceValue || isHitByEdges){
					RaycastHit hit;
					
					if(CanSeeThroughWalls){
						retVal.Add (col.gameObject);
					}
					else{
						if(!Physics.Raycast(eyeWorld, colliderPosition - eyeWorld, out hit, Distance, VisionBlockedBy.value) || 
						   hit.collider.gameObject == col.gameObject){
							retVal.Add (col.gameObject);
						}
//						else{
//							Debug.Log ("Weeded out: " + col.name + " - " + hit.collider.gameObject.name);
//						}
					}
				}
			}
		}
		return retVal.Distinct().ToArray();
	}





	public override GameObject[] CharactersInVision ()
	{
		Vector3 eyeWorld = transform.TransformPoint(eyeCenter);
		
		colliders = Physics.OverlapSphere (eyeWorld, Distance, VisibleLayers.value);
		
		//List<GameObject> 
		//retVal = new List<GameObject>();
		List<GameObject> retVal = new List<GameObject>();
		
		Vector3 visionForward = Quaternion.Euler(new Vector3(0, Direction, 0)) * transform.forward;
		float toleranceValue = Mathf.Cos (Mathf.Deg2Rad * ArcSize / 2f);
		
		Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, -(ArcSize/2f), 0));
		Quaternion rotateRight = Quaternion.Euler(new Vector3(0, ArcSize/2f, 0));
		
		Vector3 leftV = rotateLeft * visionForward * Distance;
		Vector3 rightV = rotateRight * visionForward * Distance;
		
		Ray leftRay = new Ray(eyeWorld, leftV);
		Ray rightRay = new Ray(eyeWorld, rightV);
		
		RaycastHit[] leftHits = Physics.RaycastAll(leftRay);
		RaycastHit[] rightHits = Physics.RaycastAll (rightRay);
		
		foreach(Collider col in colliders){

			if(col.gameObject.GetComponent<HealthComponent>() != null){
			if (((col.isTrigger && CanSeeTriggerColliders) || col.isTrigger == false) && (VisibleLayers == (VisibleLayers | (1 << col.gameObject.layer)))){
				Vector3 colliderPosition = col.bounds.center;
				
				Vector3 localPosition = colliderPosition - eyeWorld; 
				localPosition = new Vector3(localPosition.x, 0, localPosition.z);
				float dot = Vector3.Dot(visionForward, localPosition);
				dot /= (visionForward.magnitude * localPosition.magnitude);
				
				bool isHitByEdges = false;
				foreach(RaycastHit h in leftHits){
					if(h.collider == col){
						isHitByEdges = true;
						break;
					}
				}
				foreach(RaycastHit h in rightHits){
					if(h.collider == col){
						isHitByEdges = true;
						break;
					}
				}
				
				if(dot >= toleranceValue || isHitByEdges){
					RaycastHit hit;
					
					if(CanSeeThroughWalls){
						retVal.Add (col.gameObject);
					}
					else{
						if(!Physics.Raycast(eyeWorld, colliderPosition - eyeWorld, out hit, Distance, VisionBlockedBy.value) || 
						   hit.collider.gameObject == col.gameObject){
							retVal.Add (col.gameObject);
						}
						//						else{
						//							Debug.Log ("Weeded out: " + col.name + " - " + hit.collider.gameObject.name);
						//						}
					}
				}
			}
			}}
			return retVal.Distinct().ToArray();
	}








	public override HashSet<GameObject> ObjectsInVisionV3( ){
		Debug.Log("Unimplemented");
		return null;
	}

	public override void ObjectsInVisionV2 ( HashSet<GameObject> allInVision)
	{
		Vector3 eyeWorld = transform.TransformPoint(eyeCenter);
		
		colliders = Physics.OverlapSphere (eyeWorld, Distance, VisibleLayers.value);
		
		//List<GameObject> 
		//retVal = new List<GameObject>();
//		List<GameObject> retVal = new List<GameObject>();
		
		Vector3 visionForward = Quaternion.Euler(new Vector3(0, Direction, 0)) * transform.forward;
		float toleranceValue = Mathf.Cos (Mathf.Deg2Rad * ArcSize / 2f);
		
		Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, -(ArcSize/2f), 0));
		Quaternion rotateRight = Quaternion.Euler(new Vector3(0, ArcSize/2f, 0));
		
		Vector3 leftV = rotateLeft * visionForward * Distance;
		Vector3 rightV = rotateRight * visionForward * Distance;
		
		Ray leftRay = new Ray(eyeWorld, leftV);
		Ray rightRay = new Ray(eyeWorld, rightV);
		
		RaycastHit[] leftHits = Physics.RaycastAll(leftRay);
		RaycastHit[] rightHits = Physics.RaycastAll (rightRay);
		
		foreach(Collider col in colliders){
			if (((col.isTrigger && CanSeeTriggerColliders) || col.isTrigger == false) && (VisibleLayers == (VisibleLayers | (1 << col.gameObject.layer)))){
				Vector3 colliderPosition = col.bounds.center;
				
				Vector3 localPosition = colliderPosition - eyeWorld; 
				localPosition = new Vector3(localPosition.x, 0, localPosition.z);
				float dot = Vector3.Dot(visionForward, localPosition);
				dot /= (visionForward.magnitude * localPosition.magnitude);
				
				bool isHitByEdges = false;
				foreach(RaycastHit h in leftHits){
					if(h.collider == col){
						isHitByEdges = true;
						break;
					}
				}
				foreach(RaycastHit h in rightHits){
					if(h.collider == col){
						isHitByEdges = true;
						break;
					}
				}
				
				if(dot >= toleranceValue || isHitByEdges){
					RaycastHit hit;
					
					if(CanSeeThroughWalls){
						if(allInVision!=null){
							allInVision.Add (col.gameObject);}
					}
					else{
						if(!Physics.Raycast(eyeWorld, colliderPosition - eyeWorld, out hit, Distance, VisionBlockedBy.value) || 
						   hit.collider.gameObject == col.gameObject){
							allInVision.Add (col.gameObject);
						}
						//						else{
						//							Debug.Log ("Weeded out: " + col.name + " - " + hit.collider.gameObject.name);
						//						}
					}
				}
			}
		}
	}

	public override HashSet<GameObject> ObjectsInVisionV4 ()
	{
		return new HashSet<GameObject>();
	}

	public override HashSet<GameObject> LastFrameObjectsInVision ()
	{
		return new HashSet<GameObject>();
	}

	public override HashSet<GameObject> ObjectsLeftVision ()
	{
		return new HashSet<GameObject>();
	}
	#endregion

*/

}
