using UnityEngine;
using System.Collections;

public interface Modifier {



	float modify(float damage, GameObject source, DamageTypes.DamageType theType);


}
