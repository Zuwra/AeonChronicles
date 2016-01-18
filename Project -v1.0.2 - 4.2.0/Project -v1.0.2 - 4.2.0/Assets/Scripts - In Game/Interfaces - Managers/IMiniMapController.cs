using UnityEngine;
using System.Collections;

public interface IMiniMapController {

	void LoadMiniMap(out float guiWidth);
	void ReCalculateViewRect();
}
