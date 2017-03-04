using UnityEngine;
using System.Collections;

public class CubeMapCreator : MonoBehaviour
{
	private GameObject cubeCamera;
	//private float delayRender = 0;
	//private Transform player;
	//нужно ли заморозить позицию рендера (рендер от себя или от игрока)
	public bool freezeX = false;
	public bool freezeY = false;
	public bool freezeZ = false;
	public Cubemap cubeMap;

	void Awake ()
	{
		cubeCamera = new GameObject("CubemapCamera");
		cubeCamera.AddComponent<Camera>();
		cubeCamera.transform.position = transform.position;
		cubeCamera.transform.rotation = Quaternion.identity;
		//cubeCamera.transform.rotation = Quaternion.AngleAxis(180, transform.forward);
		cubeCamera.GetComponent<Camera> ().farClipPlane = 50;
		//cubeCamera.GetComponent<Camera> ().cullingMask = 1;
		cubeCamera.SetActive (false);
	}

	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () 
	{		
		if (GameObject.FindGameObjectWithTag ("MainCamera")) 
		if (Vector3.Distance (GameObject.FindGameObjectWithTag ("MainCamera").transform.position, transform.position) < 50)
		{
			{
				float x, y, z;
				if (freezeX)
					x = transform.position.x;
				else
					x = GameObject.FindGameObjectWithTag ("MainCamera").transform.position.x;
				if (freezeY)
					y = transform.position.y;
				else
					y = GameObject.FindGameObjectWithTag ("MainCamera").transform.position.y;
				if (freezeZ)
					z = transform.position.z;
				else
					z = GameObject.FindGameObjectWithTag ("MainCamera").transform.position.z;	
			
				cubeCamera.transform.position = new Vector3 (x, y, z);			
			}	

			cubeCamera.GetComponent<Camera> ().RenderToCubemap (cubeMap);	
		}
	}
}
