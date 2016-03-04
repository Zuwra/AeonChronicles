using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class continueOrder  {

	public bool canCast = true;
	public bool nextUnitCast = true;

	public List<reason> reasonList = new List<reason>();

	public enum reason{resourceOne, resourceTwo, cooldown, requirement, energy, health, charge}


}
