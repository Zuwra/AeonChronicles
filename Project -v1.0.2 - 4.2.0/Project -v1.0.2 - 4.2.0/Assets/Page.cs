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
		if (rows [n / 4] [0].abilityList [X] == null) {
			return false;}
		if (rows [n / 4] [0].abilityList [X].getMyType() == Ability.type.target) {
			return true;
		}
		return false;
	}


	public bool isBuildingAbility(int n)
	{
		if (rows [n / 4] == null) {
			return false;
		}

		int X = n - rows [n / 4] [0].AbilityStartingRow * 4;
		if (rows [n / 4] [0].abilityList [X] == null) {
			return false;}
		if (rows [n / 4] [0].abilityList [X].getMyType() == Ability.type.building) {
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
		Vector3 targetLoc = loc;
		if (obj != null) {
			targetLoc = obj.transform.position;
		}

		RTSObject bestGuy = null;
		float bestDistance = 1000000;

		bool foundNonFollow = false;

		// This finds the nearest unit to the location and orders them to fire. if that person is already casting a spell/placing a building, it tries to find one that isn't.
		// these two parts in the if/else statement are identical except for the kind of state they are looking for.
		rows [n / 4].RemoveAll (item => item == null);
		if (rows [n / 4] [0].abilityList [X] is BuildStructure) {
			foreach (RTSObject unit in rows[n/4]) {
				if (unit) {
					continueOrder ord = unit.abilityList [X].canActivate (true);
					if (!ord.nextUnitCast) {

						if (ord.canCast) {
							if (((UnitManager)unit).getState () is PlaceBuildingState && foundNonFollow) {

								continue;
							}

							float tempDist = Vector3.Distance (unit.transform.position, targetLoc);
							if (tempDist < bestDistance || (!foundNonFollow && !(((UnitManager)unit).getState () is PlaceBuildingState))) {

								if (!(((UnitManager)unit).getState () is PlaceBuildingState)) {
									foundNonFollow = true;
								
								}
								bestGuy = unit;
								bestDistance = tempDist;
							}
								
						}		
					} else if (ord.canCast) {

						unit.UseTargetAbility (obj, loc, X);

					}

				}

			}
		} else {
			foreach (RTSObject unit in rows[n/4]) {
				if (unit) {
					continueOrder ord = unit.abilityList [X].canActivate (true);
					if (!ord.nextUnitCast) {

						if (ord.canCast) {
							if (((UnitManager)unit).getState () is AbilityFollowState && foundNonFollow) {

								continue;
							}

							float tempDist = Vector3.Distance (unit.transform.position, targetLoc);
							if (tempDist < bestDistance || (!foundNonFollow && !(((UnitManager)unit).getState () is AbilityFollowState))) {

								if (!(((UnitManager)unit).getState () is AbilityFollowState)) {
									foundNonFollow = true;

								}
								bestGuy = unit;
								bestDistance = tempDist;
							}

						}		
					} else if (ord.canCast) {

						unit.UseTargetAbility (obj, loc, X);

					}

				}

			}
		}

		if(bestGuy)
		{
			bestGuy.UseTargetAbility (obj, loc, X);
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


	public List<RTSObject> getUnitsFromAbilities(int n)
	{
		return rows [n / 4];
	}


	public bool validTarget(GameObject target, Vector3 location, int n)
	{
		
		int X = n - rows [n / 4] [0].AbilityStartingRow * 4;

		foreach (RTSObject rts in rows[n/4]) {
			if(((TargetAbility)rts.abilityList [X]).isValidTarget ( target,location))
			{
				return true;
			}
		
		}
		return false;
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
	
		if (rows [n / 4] [0].abilityList [X] == null) {
		
			return;}

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
		bool setToTrue = false;
		if (rows [n / 4].Count > 0) {
			
			setToTrue = !rows [n / 4] [0].abilityList [X].autocast;
		}


		foreach (RTSObject unit in rows[n/4]) {

			unit.autoCast (X, setToTrue);
		}



	}



}
