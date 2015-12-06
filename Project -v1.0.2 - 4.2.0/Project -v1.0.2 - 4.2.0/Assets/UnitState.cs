using UnityEngine;
using System.Collections;

public abstract class  UnitState  {


	public UnitManager myManager;
	public customMover myMover;
	public IWeapon myWeapon;

	// Update is called once per frame
	public abstract void Update ();


}
