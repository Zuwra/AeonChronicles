using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITypeButton : IButton {

	void UpdateQueueContents(List<Item> availableItems);
	void Resize(Rect area);
}
