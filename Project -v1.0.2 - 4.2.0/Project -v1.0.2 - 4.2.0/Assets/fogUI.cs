using UnityEngine;
using System.Collections;

public class fogUI : MonoBehaviour {


	public float updateFrequency { get { return FogOfWar.current.updateFrequency; } }
	float _nextUpdate = 0.0f;

	public LayerMask lineOfSightMask = 0;

	//Transform _transform;

	void Start()
	{
		//_transform = transform;
		_nextUpdate = Random.Range(0.0f, updateFrequency)/4;
	}

	void Update()
	{
		_nextUpdate -= Time.deltaTime;
		if (_nextUpdate > 0)
			return;

		_nextUpdate = updateFrequency;
		FogOfWar.current.Unfog (this.GetComponent<RectTransform>().rect);//Unfog(_transform.position, radius, lineOfSightMask);
	}
}
