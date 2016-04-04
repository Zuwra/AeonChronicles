using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGUIManager {

	bool Dragging { get; set; }
	
	bool IsWithin(Vector3 worldPos);
	
	Rect DragArea { get; set; }
	

}
