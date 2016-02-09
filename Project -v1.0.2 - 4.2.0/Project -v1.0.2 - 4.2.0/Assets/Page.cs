using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Page  {

	public List<RTSObject>[] rows = new List<RTSObject>[3];


	public bool canBeAdded(List<RTSObject> obj)
	{

		if (rows [(int)obj [0].AbilityStartingRow] != null) {
			
			return false;
		}
		if (obj [0].abilityList.Count > 4 && rows [obj [0].AbilityStartingRow + 1] != null)
		{
			
			return false;
		}	

		if (obj [0].abilityList.Count > 8 && rows [obj [0].AbilityStartingRow + 2] !=null){
			{
				return false;
			}
		}
	
		return true;

	}

	public bool isTargetAbility(int n)
	{
		if (rows [n / 4] == null) {
			return false;
		}

		int X = n - rows [n / 4] [0].AbilityStartingRow * 4;
		if (rows [n / 4] [0].abilityList [X].myType == Ability.type.target) {
			return true;
		}
		return false;
	}

	public void fireAtTarget(GameObject obj , Vector3 loc,int n)
		{
		if (rows [n / 4] [0] == null) {
			return;
		}

		int X = n - rows [n / 4] [0].AbilityStartingRow * 4;

		if ( rows [n / 4] [0].abilityList.Count <= X) {


			return;
		}


		foreach (RTSObject unit in rows[n/4]) {

			continueOrder ord = unit.abilityList [X].canActivate ();
		
			if (ord.canCast) {
				unit.UseTargetAbility (obj, loc, X);

				}
			 if (!ord.nextUnitCast)
			{
				break;}

		}

	}

	public void addUnit(List<RTSObject> obj)
	{

		
		rows [obj[0].AbilityStartingRow] = obj;

		if (obj[0].abilityList.Count > 4) {
			rows [obj[0].AbilityStartingRow + 1] = obj;
		}
		if (obj[0].abilityList.Count > 8) {
			rows [obj[0].AbilityStartingRow+ 2] = obj;
		}

	}

	public Ability getAbility(int n)
	{	int X = n - rows [n / 4] [0].AbilityStartingRow * 4;
		return rows [n / 4] [0].abilityList [X];
	}


	public void useAbility(int n)
	{
		if (rows [n / 4] == null) {
			return;
		}

		if (rows [n / 4] [0] == null) {
			return;
		}

		int X = n - rows [n / 4] [0].AbilityStartingRow * 4;

		if ( rows [n / 4] [0].abilityList.Count <= X) {

		
				return;
			}


		// Tell the unit with the least number of units queued up to build the unit.
	
		if (rows [n / 4] [0].abilityList[X].GetType().IsSubclassOf(typeof(UnitProduction))) {

			int min = 1000;
			RTSObject best = null;
			foreach (RTSObject unit in rows[n/4]) {
				
				int man = unit.GetComponent<BuildManager> ().buildOrder.Count;
			
				if (man < min) {
					min = man;
					best = unit;
				}
			}
			if (best != null) {
				best.UseAbility (X);
			}


		}//Normal unit ability use
		else {
			
			foreach (RTSObject unit in rows[n/4]) {


				if (!unit.UseAbility (X)) {
					break;
				}

			}
		}
	}



	public void setAutoCast(int n)
	{
		if (rows [n / 4] [0] == null) {
			return;
		}

		int X = n - rows [n / 4] [0].AbilityStartingRow * 4;

		if ( rows [n / 4] [0].abilityList.Count <= X) {


			return;
		}




		foreach (RTSObject unit in rows[n/4]) {

			unit.autoCast (X);
		}



	}



}
