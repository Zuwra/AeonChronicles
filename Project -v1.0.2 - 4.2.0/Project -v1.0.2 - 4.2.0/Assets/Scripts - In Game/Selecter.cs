using UnityEngine;
using System.Collections;

public static  class Selecter{




	public static GameObject getUnit()
	{GameObject currentObject;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;		
		
		
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {

			currentObject = hit.collider.gameObject;
			Debug.Log(currentObject.name);
			if(currentObject.layer == 9 || currentObject.layer == 10)
			{

				return currentObject;}
		}
		return null;
	}





	public static Vector3 getPosition()
	{Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;		
		
		
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 16))) {


			return hit.point;
		}
		return hit.point;
	}
}
