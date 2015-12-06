using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IQueueButton : IButton
{
	int ID { get; }
	int BuildingID { get; }
	
	void UpdateRect(int Id);
	void UpdateQueueContents(List<Item> availableItems);
	void SetSelected();
	void Resize(Rect area);
}
