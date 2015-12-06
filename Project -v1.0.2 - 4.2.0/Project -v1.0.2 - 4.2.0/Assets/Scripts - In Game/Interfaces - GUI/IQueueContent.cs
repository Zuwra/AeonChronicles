using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IQueueContent {

	void Execute();
	void UpdateContents(List<Item> newAvailableItems);
	void Resize(Rect area);
}
