using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISelectedManager {

	void AddObject(RTSObject obj);
	void DeselectObject(RTSObject obj);
	void DeselectAll();
	void AddUnitsToGroup(int number, bool clearGroup);
	void SelectGroup(int number);
	void GiveOrder(Order order);
	
	int ActiveObjectsCount();
	
	bool IsObjectSelected(GameObject obj);
	
	RTSObject FirstActiveObject();
	List<RTSObject>ActiveObjectList();
}
