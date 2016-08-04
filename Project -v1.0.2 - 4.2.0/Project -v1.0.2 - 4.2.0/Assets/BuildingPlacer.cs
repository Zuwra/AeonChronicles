using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingPlacer : MonoBehaviour {


	public List<GameObject> objects = new List<GameObject>();
	public GameObject building;
	public Material good;
	public Material bad;

	private SphereCollider coll;
	// Use this for initialization
	void Start () {
		coll = GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (building) {
			if (objects.Count == 0) {
				Tile t = Grid.main.GetClosestRedTile (this.gameObject.transform.position);
				if (FogOfWar.current.IsInCompleteFog (this.gameObject.transform.position)) {
					setRenderers (bad);
				} else {
					if (!t.Buildable) {
						float dist = Mathf.Pow (t.Center.x - transform.position.x, 2) + Mathf.Pow (t.Center.z - transform.position.z, 2);
						if (Mathf.Sqrt (dist) < coll.radius) {
							setRenderers (bad);
						} else {
							setRenderers (good);
						}
					} else {
						setRenderers (good);
					}
				}
			}
		}
	}

	public bool canBuild()
	{  

		if (objects.Count != 0) {
			
			foreach (GameObject obj in objects) {
				Debug.Log ("Has objects " + obj);
			}
			return false;
		}
			Tile t = Grid.main.GetClosestRedTile (this.gameObject.transform.position);
			if (FogOfWar.current.IsInCompleteFog (this.gameObject.transform.position)) {
				setRenderers (bad);
			Debug.Log ("In fogg" );
			return false;
			} else {
				if (!t.Buildable) {
					float dist = Mathf.Pow (t.Center.x - transform.position.x, 2) + Mathf.Pow (t.Center.z - transform.position.z, 2);
					if (Mathf.Sqrt (dist) < coll.radius) {
						setRenderers (bad);
					Debug.Log ("not buildable" );
					return false;
					} else {
						setRenderers (good);
					}
				} else {
					setRenderers (good);
				}

		}


		return true;
	}

	public void reset(GameObject b, Material g, Material ba)
	{

		FogOfWarUnit fow = b.GetComponent<FogOfWarUnit> ();
		fow.enabled = false;
		GetComponent<SphereCollider> ().enabled = true;
		GetComponent<SphereCollider> ().radius = 10;
		objects.Clear ();
		building = b;
		good = g;
		bad = ba;
		setRenderers (good);


		foreach (CharacterController c in b.GetComponents<CharacterController>()) {
			c.enabled = false;
		}
		foreach (SphereCollider co in b.GetComponents<SphereCollider>()) {
			co.enabled = false;
		}
		foreach (SphereCollider co in b.GetComponentsInChildren<SphereCollider>()) {
			co.enabled = false;
		}

		foreach (BoxCollider co in b.GetComponents<BoxCollider>()) {
			co.enabled = false;
		}
		foreach (BoxCollider co in b.GetComponentsInChildren<BoxCollider>()) {
			co.enabled = false;
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (objects.Contains (other.gameObject)) {
			objects.Remove (other.gameObject);

			if (objects.Count == 0) {
				setRenderers (good);
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		UnitManager manage = other.gameObject.GetComponent<UnitManager> ();
		if (manage) {
			//if (manage.PlayerOwner != 1 || manage.myStats.isUnitType (UnitTypes.UnitTypeTag.Structure)) {
				objects.Add (other.gameObject);

				setRenderers (bad);

			//}
		} else {
			objects.Add (other.gameObject);

			setRenderers (bad);
		}
	}


	public void setRenderers(Material m)
	{
		
		foreach (MeshRenderer mr in building.GetComponentsInChildren<MeshRenderer>()) {
			mr.material = m;
		}

	}
}
