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


	public void useAbility(int n)
	{
		if (rows [n / 4] [0] == null) {
			return;
		}

		int X = n - rows [n / 4] [0].AbilityStartingRow * 4;

		if ( rows [n / 4] [0].abilityList.Count <= X) {

		
				return;
			}




		foreach (RTSObject unit in rows[n/4]) {


					if (!unit.UseAbility (X)) {
						break;
					}
				}
			


	}



}
