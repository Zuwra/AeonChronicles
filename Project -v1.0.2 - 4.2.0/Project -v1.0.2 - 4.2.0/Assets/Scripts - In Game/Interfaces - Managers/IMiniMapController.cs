using UnityEngine;
using System.Collections;

public interface IMiniMapController {

	void LoadMiniMap(out float guiWidth, out Rect miniMapRect);
	void ReCalculateViewRect();
}
