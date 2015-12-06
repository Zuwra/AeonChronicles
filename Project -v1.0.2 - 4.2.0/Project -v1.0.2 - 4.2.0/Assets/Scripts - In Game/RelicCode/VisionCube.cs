using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionCube : VisionBase
{/*
    PersistantTrigger pt;
    public Vector3 size = Vector3.one;
    public Vector3 center;
	public LayerMask ToIgnore;
    private LayerMask layer;
    private BoxCollider bc;
    private GameObject sight;
    private GameObject[] objects;

	private Vector3 _originalPosition;
	private List<Ray> _v4Rays;
	private Vector3 curSize;

    // Use this for initialization
    void Start()
    {
        allInVision = new HashSet<GameObject>();

        sight = new GameObject("CubeSight");
        sight.transform.parent = transform;
        sight.transform.localPosition = Vector3.zero;
        sight.transform.localRotation = Quaternion.identity;
        sight.transform.localScale = Vector3.one;

        //box collider and persistanttrigger
        bc = sight.AddComponent<BoxCollider>();
        bc.size = size;
        bc.center = center;
        bc.isTrigger = true;

		_originalPosition = Vector3.zero;
		_v4Rays = new List<Ray> ();
		constructV4Rays ();

        pt = GetComponent<PersistantTrigger>();
        pt = sight.AddComponent<PersistantTrigger>();
    }

	private void constructV4Rays(){
		_originalPosition = gameObject.transform.position;
		if (_v4Rays != null) {
						_v4Rays.Clear ();
				}
		curSize = new Vector3(size.x * transform.localScale.x, size.y * transform.localScale.y, size.z * transform.localScale.z);
		Vector3 pos = (transform.position + center) - (curSize / 2f);
		//
		float stepSize = 0.45f;
		for(float i = 0; i <= curSize.x; i += stepSize){
			
			Ray r = new Ray(pos + new Vector3(i, 0.5f, 0), new Vector3(0, 0, 1));
			if (_v4Rays != null) {
				_v4Rays.Add(r);}
		}
	}

    public void SetSize(Vector3 value)
    {
        size = value;
        ApplyChanges();
    }

    public Vector3 GetSize()
    {
        return size;
    }

    public void SetCenter(Vector3 value)
    {
        center = value;
        ApplyChanges();
    }

    public Vector3 GetCenter()
    {
        return center;
    }

    public void SetLayer(string s)
    {
        layer = 1 << LayerMask.NameToLayer(s);
		if (layer > 31) {
			layer =31;		
		}
        ApplyChanges();
    }

    private void ApplyChanges()
    {
        if (bc != null)
        {
            bc.size = size;
            bc.center = center;
        }
        if (sight != null)
        {
            sight.layer = layer;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region implemented abstract members of VisionBase

    public override void DrawGizmos()
    {
        Gizmos.DrawWireCube(transform.TransformPoint(center), transform.TransformDirection(new Vector3(size.x * transform.localScale.x, size.y * transform.localScale.y, size.z * transform.localScale.z))); // Might have bugs with rotated parent objects
    }

    public override GameObject[] ObjectsInVision()
    {
       // Debug.Log("Vision cube is using an old method");
        HashSet<Collider> collisions = pt.GetOverlappingColliders();
        objects = new GameObject[collisions.Count];
        int counter = 0;
        foreach (Collider c in collisions)
        {
            if (c)
            {
                objects[counter] = c.gameObject;
                counter++;
            }
        }
        return objects;
    }

    public override void ObjectsInVisionV2(HashSet<GameObject> allInVision)
    {

    }

    public override HashSet<GameObject> ObjectsInVisionV3()
    {

		if(allInVision == null){
			allInVision = new HashSet<GameObject>();
		}
		if(allLastFrameVision == null){
			allLastFrameVision = new HashSet<GameObject>();
		}
		if(allLeftVision == null){
			allLeftVision = new HashSet<GameObject>();
		}
		allLeftVision.Clear();
		allLastFrameVision.Clear();
		foreach(GameObject obj in allInVision){
			allLastFrameVision.Add (obj);
		}
		allInVision.Clear();

        if (pt != null)
        {
	        HashSet<Collider> collisions = pt.GetOverlappingColliders();
	        foreach (Collider c in collisions)
	        {
//				Debug.Log("VisionCube can see: " + c.gameObject.name);
				if (c != null){
	            	allInVision.Add(c.gameObject);
				}
	        }
        }

		foreach(GameObject obj in allLastFrameVision){
			if(!allInVision.Contains(obj)){
				allLeftVision.Add (obj);
			}
		}

        return allInVision;
    }

	public override HashSet<GameObject> ObjectsInVisionV4 ()
	{
		if(allInVision == null){
			allInVision = new HashSet<GameObject>();
		}
		if(allLastFrameVision == null){
			allLastFrameVision = new HashSet<GameObject>();
		}
		if(allLeftVision == null){
			allLeftVision = new HashSet<GameObject>();
		}
		allLeftVision.Clear();
		allLastFrameVision.Clear();
		foreach(GameObject obj in allInVision){
			allLastFrameVision.Add (obj);
		}
		allInVision.Clear();

		if (!_originalPosition.Equals (transform.position)) {
			constructV4Rays();
		}
		if (_v4Rays != null) {
						for (int i = 0; i < _v4Rays.Count; i++) {
								if (curSize.z > 0) {
										RaycastHit[] h = Physics.RaycastAll (_v4Rays [i], curSize.z, ToIgnore);
										foreach (RaycastHit hit in h) {
												allInVision.Add (hit.collider.gameObject);
										}
								}
						}
				}
		foreach(GameObject obj in allLastFrameVision){
			if(!allInVision.Contains(obj)){
				allLeftVision.Add (obj);
			}
		}

		return allInVision;
	}

	public override HashSet<GameObject> LastFrameObjectsInVision ()
	{
		return allLastFrameVision;
	}

	public override GameObject[] CharactersInVision ()
	{
		if(allInVision == null){
			allInVision = new HashSet<GameObject>();
		}
		if(allLastFrameVision == null){
			allLastFrameVision = new HashSet<GameObject>();
		}
		if(allLeftVision == null){
			allLeftVision = new HashSet<GameObject>();
		}
		allLeftVision.Clear();
		allLastFrameVision.Clear();
		foreach(GameObject obj in allInVision){
			if(obj)
			{if(obj.GetComponent<HealthComponent>() != null){
					allLastFrameVision.Add (obj);}}
		}
		allInVision.Clear();
		
		if (!_originalPosition.Equals (transform.position)) {
			constructV4Rays();
		}
		if (_v4Rays != null) {
			for (int i = 0; i < _v4Rays.Count; i++) {
				if (curSize.z > 0) {
					RaycastHit[] h = Physics.RaycastAll (_v4Rays [i], curSize.z, ToIgnore);
					foreach (RaycastHit hit in h) {
						if(hit.collider.gameObject.GetComponent<HealthComponent>() != null){
							allInVision.Add (hit.collider.gameObject);}
					}
				}
			}
		}
		foreach(GameObject obj in allLastFrameVision){
			if(!allInVision.Contains(obj)){
				if(obj.GetComponent<HealthComponent>() != null){
					allLeftVision.Add (obj);}
			}
		}
		GameObject[] objArray = new GameObject[allInVision.Count];
		allInVision.CopyTo(objArray);
		return objArray;
	} 

	public override HashSet<GameObject> ObjectsLeftVision ()
	{
		return allLeftVision;
	}

    #endregion
	  */
}
