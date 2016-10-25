﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class  UnitState  {


	public UnitManager myManager;

	// Update is called once per frame
	public abstract void Update ();

	public abstract void attackResponse(UnitManager src, float amount);

	public abstract void initialize();

	public abstract void endState();

}
