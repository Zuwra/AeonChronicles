using UnityEngine;
using System.Collections;

public class RapidArmsUpgrade : SpecificUpgrade {

	override
	public void applyUpgrade(GameObject obj)
	{

		if (confirmUnit (obj)) {

			foreach (buildTurret bt in obj.GetComponents<buildTurret>()) {
				bt.rapidArms = true;
			}

			foreach (TurretScreenDisplayer tsd in obj.GetComponents<TurretScreenDisplayer>()) {
				tsd.rapidArms = true;
			}

			foreach (TurretMount tm in obj.GetComponentsInChildren<TurretMount>()) {
				tm.rapidArms = true;
			}
		}

	}

	public override void unApplyUpgrade (GameObject obj){
		if (confirmUnit (obj)) {
			foreach (buildTurret bt in obj.GetComponents<buildTurret>()) {
				bt.rapidArms = false;
			}

			foreach (TurretScreenDisplayer tsd in obj.GetComponents<TurretScreenDisplayer>()) {
				tsd.rapidArms = false;
			}

			foreach (TurretMount tm in obj.GetComponentsInChildren<TurretMount>()) {
				tm.rapidArms = false;
			}
		}
	}
}
