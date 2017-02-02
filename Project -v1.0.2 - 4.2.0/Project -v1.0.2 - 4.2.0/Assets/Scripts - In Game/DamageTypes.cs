using UnityEngine;
using System.Collections;

public class DamageTypes  {

	public  enum DamageType{Regular, Penetrating, Wound, True, Energy};
	//Regular- triggers abilities and can be modified by armor
	//True - does not trigger onDamage triggers and cannot be modified
	//Penetrating- cannot be reduced by armor but does trigger onDamage triggers
	//Wound - Can be reduced by armor but does not trigger onDamage ability
	//Plasma triggers stuff like regular, just tagged for ability purposes.
}
