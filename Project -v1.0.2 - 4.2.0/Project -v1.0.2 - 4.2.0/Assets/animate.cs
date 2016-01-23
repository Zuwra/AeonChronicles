using UnityEngine;
using System.Collections;

public abstract class animate: MonoBehaviour  {

	public  bool active;


	public void ActivateAnimation()
	{
		active = true;
	}


	public void DeactivateAnimation()
	{
		active = false;
	}



}
