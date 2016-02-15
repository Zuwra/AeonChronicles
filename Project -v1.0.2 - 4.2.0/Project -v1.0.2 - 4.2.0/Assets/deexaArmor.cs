using UnityEngine;
using System.Collections;

public class deexaArmor : Upgrade
{


    public float defaultAmount = 1;

    override
    public void applyUpgrade(GameObject obj)
    {

        UnitManager manager = obj.GetComponent<UnitManager>();

        if (!manager.myStats.isUnitType(UnitTypes.UnitTypeTag.structure))
        {
            obj.GetComponent<UnitStats>().armor += 1;
        }
    }



}
