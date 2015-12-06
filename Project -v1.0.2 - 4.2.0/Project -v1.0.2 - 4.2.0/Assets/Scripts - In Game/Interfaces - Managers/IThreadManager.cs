using UnityEngine;
using System.Collections;

public interface IThreadManager 
{
	void AddPathfindingThread(GetPathThread thread);
}
