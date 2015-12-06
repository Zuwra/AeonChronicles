using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISelectedManager {

	void AddObject(RTSObject obj);
	void DeselectObject(RTSObject obj);
	void DeselectAll();
	void AddUnitsToGroup(int number);
	void SelectGroup(int number);
	void GiveOrder(Order order);
	
	int ActiveObjectsCount();
	
	bool IsObjectSelected(GameObject obj);
	
	IOrderable FirstActiveObject();
	List<IOrderable>ActiveObjectList();
}
