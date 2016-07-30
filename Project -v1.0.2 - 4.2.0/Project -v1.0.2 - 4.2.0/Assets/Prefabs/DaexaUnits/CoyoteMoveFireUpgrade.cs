using UnityEngine;
using System.Collections;

public class CoyoteMoveFireUpgrade : Upgrade {

    override
    public void applyUpgrade(GameObject obj)
    {

        UnitManager manager = obj.GetComponent<UnitManager>();

        if (manager)
        {
            if (manager.UnitName == "Zephyr")
            {
                obj.GetComponent<StandardInteract>().attackWhileMoving = true;
            }
        }
    }

	public override void unApplyUpgrade (GameObject obj){
		UnitManager manager = obj.GetComponent<UnitManager>();

		if (manager)
		{
			if (manager.UnitName == "Zephyr")
			{
				obj.GetComponent<StandardInteract>().attackWhileMoving = false;
			}
		}
	}
}
