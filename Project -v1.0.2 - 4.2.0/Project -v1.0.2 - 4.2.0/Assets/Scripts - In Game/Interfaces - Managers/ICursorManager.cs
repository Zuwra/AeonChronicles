using UnityEngine;
using System.Collections;

public interface ICursorManager 
{
	void UpdateCursor(InteractionState interactionState);
	void HideCursor();
	void ShowCursor();
}
