using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExistAchievement :Achievement {

	public string ObjectTag;
	public int MinNumber;
	public int MaxNumber;

	public override string GetDecription()
	{return Description ;
	}

	public override void CheckBeginning (){
	}

	public override void CheckEnd ()
	{
		if (!IsAccomplished ()) {
			int n = GameObject.FindGameObjectsWithTag (ObjectTag).Length;
			if (n >= MinNumber &&  n <= MaxNumber) {
				Accomplished ();
			}
		}

	}

}