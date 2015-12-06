using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class VisionBase : MonoBehaviour {/*

	public VisionEnum VisionVariant = VisionEnum.Default;
	public Color DebugColor = Color.red;
	public bool DrawDebugVision = false;

	protected HashSet<GameObject> allInVision;
	protected HashSet<GameObject> allLastFrameVision;
	protected HashSet<GameObject> allLeftVision;
	void Start(){
		allInVision = new HashSet<GameObject>();
		allLeftVision = new HashSet<GameObject>();
		allLastFrameVision = new HashSet<GameObject>();
	}

	void OnDrawGizmosSelected(){
		if(DrawDebugVision){
			DrawGizmos();
		}
	}

	public GameObject PlayerInVisionV2(){
		ObjectsInVisionV2( allInVision);
		if (allInVision != null) {
						foreach (GameObject g in allInVision) {
								if (g.CompareTag ("Player")) {
										allInVision.Clear ();
										return g;
								}
						}
				
						allInVision.Clear ();
				}
		return null;
	}

	public GameObject PlayerInVision(){
		GameObject[] all = CharactersInVision();
	//	List<GameObject> retVal = new List<GameObject>();
		
		foreach(GameObject g in all){
			if(g.CompareTag("Player")){
				return g;
			}
		}
		return null;
	}

	public GameObject[] PlayersInVision(){
		GameObject[] all = CharactersInVision();
		List<GameObject> retVal = new List<GameObject>();
		
		foreach(GameObject g in all){
			//Debug.Log("I have a "+g+".");
			//if(g.GetComponentInParent<PlayerBase>() != null){
			if(g){
			if(g.CompareTag("Player")){
				retVal.Add(g);
				}}
		}
		return retVal.ToArray();
	}



	public GameObject[] MonstersInVision(){
		GameObject[] all = ObjectsInVision();
		List<GameObject> retVal = new List<GameObject>();
		
		foreach(GameObject g in all){
			if(g){
			if(g.GetComponentInParent<MonsterBase>() != null){
					retVal.Add(g);}
			}
		}
		return retVal.ToArray();
	}
	
	public GameObject[] EnvironmentsInVision(){
		GameObject[] all = ObjectsInVision();
		List<GameObject> retVal = new List<GameObject>();
		
		foreach(GameObject g in all){
			if(g.GetComponentInParent<EnvironmentBase>() != null){
				retVal.Add(g);
			}
		}
		return retVal.ToArray();
	}
	
	public static VisionBase GetVisionByVariant(VisionEnum variant, GameObject target){
		VisionBase[] visions = target.GetComponentsInChildren<VisionBase>();
		foreach(VisionBase v in visions){
			if(v.VisionVariant == variant){
				return v;
			}
		}
		return null;
	}

	public abstract void DrawGizmos();
	public abstract GameObject[] ObjectsInVision();
	public abstract void ObjectsInVisionV2( HashSet<GameObject> allInVision);
	public abstract HashSet<GameObject> ObjectsInVisionV3( );
	public abstract HashSet<GameObject> ObjectsInVisionV4();
	public abstract HashSet<GameObject> LastFrameObjectsInVision();
	public abstract HashSet<GameObject> ObjectsLeftVision();
	public abstract GameObject[] CharactersInVision();*/
}
