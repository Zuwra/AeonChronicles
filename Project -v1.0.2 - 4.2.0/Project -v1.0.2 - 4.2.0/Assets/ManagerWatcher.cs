using UnityEngine;
using System.Collections;

public interface ManagerWatcher {



	void updateResources(float resOne, float resTwo);
		

	void updateSupply( float current, float max);

	void updateUpgrades();


}
