using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MVPCalculator {

	public List<VeteranStats> myVeterans = new List<VeteranStats>();


	public void addVet(VeteranStats input)
	{if(!myVeterans.Contains(input))
		myVeterans.Add (input);
	}


	public VeteranStats getMVP()
	{VeteranStats best = null;
		float bestScore =0;
		foreach (VeteranStats stat in myVeterans) {
			float temp = stat.calculateScore ();

			if (temp > bestScore) {
				bestScore = temp;
				best = stat;
			}
		}
		return best;
	}



	public List<VeteranStats> UnitStats ()
	{
		return myVeterans;
	}
}
