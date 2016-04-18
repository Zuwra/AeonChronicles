using UnityEngine;

public interface ICamera {

	void Zoom(object sender, ScrollWheelEventArgs e);
	void Pan(object sender, ScreenEdgeEventArgs e);
	void SetBoundries(float minX, float minY, float maxX, float maxY);
	void Move(Vector3 worldPos);

}
