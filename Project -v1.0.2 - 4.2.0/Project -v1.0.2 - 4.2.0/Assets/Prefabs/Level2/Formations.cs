using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formations{


	public static List<Vector3> getFormation(int count)
	{
		List<Vector3> toReturn = new List<Vector3> ();

		if (count == 1) {
			return Odd.GetRange (0, 1);
		} 
		// Odd number of guys
		else {
			//Only two rows
			if (count < 9) {
				
				int remainder = count / 2;
				for (int n = 0; n < remainder; n++) {
					if (remainder % 2 == 1) {
						toReturn.Add ((Odd [n] - Vector3.forward * 5) * 3);
					} else {
						toReturn.Add ((Even [n] - Vector3.forward * 5) * 3);
					}
				}

				int RemainB = (count - remainder);
				for (int n = 0; n < RemainB; n++) {
					if (RemainB % 2 == 1) {
						toReturn.Add ((Odd [n] ) * 3);
					} else {
						toReturn.Add ((Even [n] ) * 3);
					}
				}
				//Three rows
			} else {

				int remainder = count / 3;
				for (int n = 0; n < remainder; n++) {
					if (remainder % 2 == 1) {
						toReturn.Add ((Odd [n] - Vector3.forward * 5) * 3);
					} else {
						toReturn.Add ((Even [n] - Vector3.forward * 5) * 3);
					}
				}

				int RemainB = (count - remainder) / 2;
				for (int n = 0; n < RemainB; n++) {
					if (RemainB % 2 == 1) {
						toReturn.Add ((Odd [n] ) * 3);
					} else {
						toReturn.Add ((Even [n] ) * 3);
					}
				}

				int RemainC= (count  - RemainB - remainder);
				for (int n = 0; n < RemainC; n++) {
					if (RemainC % 2 == 1) {
						toReturn.Add ((Odd [n] + Vector3.forward * 5) * 3);
					} else {
						toReturn.Add ((Even [n] + Vector3.forward * 5) * 3);
					}
				}

			
			
			
			}
		}

		return toReturn;
	}

	private static List<Vector3> Even = new List<Vector3> () {	
		new Vector3(-2.5f, 0, 0),
		new Vector3 (2.5f, 0, 0), 
		new Vector3(-7.5f,0,0),
		new Vector3 (7.5f, 0, 0), 
		new Vector3 (-12.5f, 0, 0), 
		new Vector3 (12.5f, 0, 0), 
		new Vector3 (-17.5f, 0, 0), 
		new Vector3 (17.5f, 0, 0), 
		new Vector3 (-22.5f, 0, 0), 
		new Vector3 (22.5f, 0, 0), 
		new Vector3 (-27.5f, 0, 0), 
		new Vector3 (27.5f, 0, 0), 
		new Vector3 (-32.5f, 0, 0), 
		new Vector3 (32.5f, 0, 0), 
		new Vector3 (-37.5f, 0, 0), 
		new Vector3 (37.5f, 0, 0), 
		new Vector3 (-42.5f, 0, 0), 
		new Vector3 (42.5f, 0, 0),
		new Vector3 (-47.5f, 0, 0), 
		new Vector3 (47.5f, 0, 0),
		new Vector3 (-52.5f, 0, 0), 
		new Vector3 (52.5f, 0, 0),
		new Vector3 (-57.5f, 0, 0), 
		new Vector3 (57.5f, 0, 0)

		};

	private static List<Vector3> Odd = new List<Vector3> () {	
		new Vector3(0, 0, 0),
		new Vector3 (5, 0, 0), 
		new Vector3(-5,0,0),
		new Vector3 (10, 0, 0), 
		new Vector3 (-10, 0, 0), 
		new Vector3 (15, 0, 0), 
		new Vector3 (-15, 0, 0), 
		new Vector3 (20, 0, 0), 
		new Vector3 (-20, 0, 0), 
		new Vector3 (25, 0, 0), 
		new Vector3 (-25, 0, 0), 
		new Vector3 (30, 0, 0), 
		new Vector3 (-30, 0, 0), 
		new Vector3 (35, 0, 0), 
		new Vector3 (-35, 0, 0), 
		new Vector3 (40, 0, 0), 
		new Vector3 (-40, 0, 0), 
		new Vector3 (45, 0, 0),
		new Vector3 (-45, 0, 0), 
		new Vector3 (50, 0, 0),
		new Vector3 (-50, 0, 0), 
		new Vector3 (55, 0, 0),
		new Vector3 (-55, 0, 0), 
		new Vector3 (60, 0, 0)

	};
		




}
