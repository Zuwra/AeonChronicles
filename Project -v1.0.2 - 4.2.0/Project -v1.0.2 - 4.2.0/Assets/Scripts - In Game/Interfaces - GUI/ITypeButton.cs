using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITypeButton : IButton {

	void AddNewQueue(Building building);
	void RemoveQueue(Building building);
	
	void UpdateQueueContents(List<Item> availableItems);
	void Resize(Rect area);
}
