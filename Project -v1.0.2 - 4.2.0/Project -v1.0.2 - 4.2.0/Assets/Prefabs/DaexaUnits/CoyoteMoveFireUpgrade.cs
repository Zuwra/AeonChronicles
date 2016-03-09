using UnityEngine;
using System.Collections;

public class CoyoteMoveFireUpgrade : Upgrade {

    override
    public void applyUpgrade(GameObject obj)
    {

        UnitManager manager = obj.GetComponent<UnitManager>();

        if (manager)
        {
            if (manager.UnitName == "Coyote")
            {
                obj.GetComponent<StandardInteract>().attackWhileMoving = true;
            }
        }
    }
}
