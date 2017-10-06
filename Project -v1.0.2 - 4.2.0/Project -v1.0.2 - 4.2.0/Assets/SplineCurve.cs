using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCurve : MonoBehaviour {

		public static Vector3 CatmullRom(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
		{
			return 0.5f*( (2f*p2) + (-p1 + p3)*t +
			(2f*p1 - 5f*p2 + 4f*p3 - p4)*Mathf.Pow(t, 2f) + 
			(-p1 + 3f*p2 - 3f*p3 + p4)*Mathf.Pow(t, 3f));
		}


}
