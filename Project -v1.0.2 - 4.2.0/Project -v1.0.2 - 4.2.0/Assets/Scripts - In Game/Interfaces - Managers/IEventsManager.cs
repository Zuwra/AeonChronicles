using UnityEngine;
using System.Collections;

public interface IEventsManager {

	event MouseActions MouseClick;
	event KeyBoardActions KeyAction;
	event ScrollActions MouseScrollWheel;
	event ScreenEdgeActions ScreenEdgeMousePosition;
}
