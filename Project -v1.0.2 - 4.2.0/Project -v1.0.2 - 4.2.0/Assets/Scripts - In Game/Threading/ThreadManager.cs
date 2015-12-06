using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;

public class ThreadManager : MonoBehaviour, IThreadManager {
	
	private List<GetPathThread> m_PathFindingThreads = new List<GetPathThread>();
	
	public static ThreadManager main;
	
	void Awake()
	{
		main = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Iterate backwards so we can remove items as we're iterating
		for (int i=m_PathFindingThreads.Count-1; i >= 0; i--)
		{
			if (m_PathFindingThreads[i].Update ())
			{
				m_PathFindingThreads.RemoveAt (i);
			}
		}
	}
	
	public void AddPathfindingThread(GetPathThread thread)
	{
		//Add the thread to the list and start it
		m_PathFindingThreads.Add (thread);
		thread.Start ();
	}
	
	void OnDestroy()
	{
		//Threads could be running when we quit, make sure to abort them
		foreach (GetPathThread thread in m_PathFindingThreads)
		{
			thread.Abort ();
		}
	}
}
