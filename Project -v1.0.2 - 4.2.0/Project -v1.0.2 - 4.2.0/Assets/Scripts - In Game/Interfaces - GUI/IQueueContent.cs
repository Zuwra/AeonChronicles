using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IQueueContent {

	void Execute();

	void Resize(Rect area);
}
