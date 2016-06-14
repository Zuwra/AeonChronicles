using UnityEngine;
using System.Collections;

public class FogOfWarUnit : MonoBehaviour
{
    public float radius = 5.0f;

    public float updateFrequency { get { return FogOfWar.current.updateFrequency; } }
   // float _nextUpdate = 0.0f;

    public LayerMask lineOfSightMask = 0;


	private bool hasMoved = true;
    Transform _transform;

	private float nextActionTime;

    void Start()
    {
		Initialize ();
    }

	public void Initialize()
	{  _transform = transform;
		nextActionTime = Random.Range(0, updateFrequency);
		//_nextUpdate = Random.Range(0, updateFrequency);
		
	}

    void Update()
    {
		
		if (Time.time > nextActionTime) {
			nextActionTime += updateFrequency;
			if (hasMoved) {
		
				clearFog();			
			}
		
		
		}
    }


	public void move (){hasMoved = true;}


	public void clearFog()
	{hasMoved = false;
		//_nextUpdate = updateFrequency;
		//FogOfWar.current.Unfog(_transform.position, 1);
		FogOfWar.current.Unfog(_transform.position, radius, lineOfSightMask);
	}
}
