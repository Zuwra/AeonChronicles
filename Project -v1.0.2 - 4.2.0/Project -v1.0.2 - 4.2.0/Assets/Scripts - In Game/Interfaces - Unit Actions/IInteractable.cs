using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IInteractable {
	
	
		
	void Interact(RTSObject obj);
	
	bool InteractWith (GameObject obj);
	
}
