using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGUIManager {

	float MainMenuWidth	{ get; }
	
	int GetSupportSelected { get; }
	
	bool Dragging { get; set; }
	
	bool IsWithin(Vector3 worldPos);
	
	Rect DragArea { get; set; }
	
	void UpdateQueueContents(List<Item> avaialableItems);
	
	void AddConstructor(Building building);
	void RemoveConstructor(Building building);
}
