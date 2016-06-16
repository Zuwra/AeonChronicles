using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GLManager : MonoBehaviour, IGLManager {
	
	//Singleton
	public static GLManager main;
	
	//Member variables
	private List<GLItem> m_ItemsToRender = new List<GLItem>();
	
	void Awake()
	{
		//Assign singleton
		main = this;
	}
	
	void OnPostRender()
	{
		foreach (GLItem item in m_ItemsToRender)
		{Debug.Log ("Rendering stuff");
			item.ExecuteCommand();
		}
	}

	public void AddItemToRender (GLItem item)
	{
		if (!m_ItemsToRender.Contains (item))
		{
			m_ItemsToRender.Add (item);
		}
	}

	public void RemoveItemToRender (GLItem item)
	{
		m_ItemsToRender.Remove (item);
	}
}
