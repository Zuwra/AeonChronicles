using UnityEngine;
using System.Collections;

public interface IDeployable {
	
	bool Deploying { get; }
	void Deploy();
	void StopDeploy();
}
