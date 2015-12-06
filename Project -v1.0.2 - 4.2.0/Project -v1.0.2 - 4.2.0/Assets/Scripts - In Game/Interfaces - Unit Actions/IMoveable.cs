using UnityEngine;
using System.Collections;

public interface IMoveable {

	void MoveTo(Vector3 location);
	void Follow(Transform target);
	void Stop();
}
