using UnityEngine;
using System.Collections;

public class FogOfWarUnit : MonoBehaviour
{
    public float radius = 5.0f;

    public float updateFrequency { get { return FogOfWar.current.updateFrequency; } }
   // float _nextUpdate = 0.0f;

    public LayerMask lineOfSightMask = 0;


	private bool hasMoved = true;

    void Start()
    {
		hasMoved = true;
		InvokeRepeating ("UpdateFog", Random.Range(0, updateFrequency), updateFrequency);
		Invoke( "clearFog", 1);
    }



    void UpdateFog()
    {
			if (hasMoved) {
		
				clearFog();			
			}

    }


	public void move (){hasMoved = true;}


	public void clearFog()
	{hasMoved = false;
		//_nextUpdate = updateFrequency;
		//FogOfWar.current.Unfog(_transform.position, 1);
		FogOfWar.current.Unfog(transform.position, radius, lineOfSightMask);
	}
}
