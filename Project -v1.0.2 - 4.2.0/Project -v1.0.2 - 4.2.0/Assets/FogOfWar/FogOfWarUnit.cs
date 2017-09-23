using UnityEngine;
using System.Collections;

public class FogOfWarUnit : MonoBehaviour
{
    public float radius = 5.0f;

    public float updateFrequency { get { return FogOfWar.current.updateFrequency; } }
   // float _nextUpdate = 0.0f;

    public LayerMask lineOfSightMask = 0;


	private bool hasMoved = true;
	public bool autoUpdate;
    void Start()
    {
		//Debug.Log ("Fog");
		hasMoved = true;
		InvokeRepeating ("clearFog", Random.Range(0, updateFrequency), updateFrequency);
		//Invoke ("move", 1.9f);
		//Invoke ("clearFog", 2);
		if (autoUpdate) {
			InvokeRepeating ("AutoUpdate", Random.Range(0, updateFrequency), updateFrequency + .2f);
		}
    }



	public void move (){hasMoved = true;
		}


	public void clearFog()
	{  
		if (hasMoved) {
			hasMoved = false;
			FogOfWar.current.Unfog (transform.position, radius, lineOfSightMask);

		}
	}

	public void AutoUpdate ()
	{
		FogOfWar.current.Unfog (transform.position, radius, lineOfSightMask);
	}

}
