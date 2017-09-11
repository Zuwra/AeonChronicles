using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formations{


	public static List<Vector3> getFormation(int count, float sepDist)
	{
		List<Vector3> toReturn = new List<Vector3> ();

		if (count == 1) {
			return Odd.GetRange (0, 1);
		} else if (count == 2) {

			for (int n = 0; n < 2; n++) {
				toReturn.Add (Even [n]* sepDist);
			}

		}
		// Odd number of guys
		else {
			//Only two rows
			if (count < 9) {
				
				int remainder = count / 2;
				for (int n = 0; n < remainder; n++) {
					if (remainder % 2 == 1) {
						toReturn.Add ((Odd [n] - Vector3.forward * 7.5f) * sepDist );
					} else {
						toReturn.Add ((Even [n] - Vector3.forward * 7.5f)* sepDist );
					}
				}

				int RemainB = (count - remainder);
				for (int n = 0; n < RemainB; n++) {
					if (RemainB % 2 == 1) {
						toReturn.Add ((Odd [n] + Vector3.forward * 7.5f) * sepDist);
					} else {
						toReturn.Add ((Even [n] + Vector3.forward * 7.5f)* sepDist );
					}
				}
				//Three rows
			} else {

				int remainder = count / 3;
				for (int n = 0; n < remainder; n++) {
					if (remainder % 2 == 1) {
						toReturn.Add ((Odd [n] - Vector3.forward *15)* sepDist );
					} else {
						toReturn.Add ((Even [n] - Vector3.forward * 15)* sepDist );
					}
				}

				int RemainB = (count - remainder) / 2;
				for (int n = 0; n < RemainB; n++) {
					if (RemainB % 2 == 1) {
						toReturn.Add ((Odd [n] ) * sepDist);
					} else {
						toReturn.Add ((Even [n] * sepDist) );
					}
				}

				int RemainC= (count  - RemainB - remainder);
				for (int n = 0; n < RemainC; n++) {
					if (RemainC % 2 == 1) {
						toReturn.Add ((Odd [n] + Vector3.forward * 15) * sepDist);
					} else {
						toReturn.Add ((Even [n] + Vector3.forward * 15)* sepDist );
					}
				}

			
			
			
			}
		}

		return toReturn;
	}

	private static List<Vector3> Even = new List<Vector3> () {	
		new Vector3(-7.5f, 0, 0),
		new Vector3 (7.5f, 0, 0), 
		new Vector3(-22.5f,0,0),
		new Vector3 (22.5f, 0, 0), 
		new Vector3 (-37.5f, 0, 0), 
		new Vector3 (37.5f, 0, 0), 
		new Vector3 (-52.5f, 0, 0), 
		new Vector3 (52.5f, 0, 0), 
		new Vector3 (-67.5f, 0, 0), 
		new Vector3 (67.5f, 0, 0), 
		new Vector3 (-82.5f, 0, 0), 
		new Vector3 (82.5f, 0, 0), 
		new Vector3 (-96.5f, 0, 0), 
		new Vector3 (96.5f, 0, 0), 
		new Vector3 (-111.5f, 0, 0), 
		new Vector3 (111.5f, 0, 0), 
		new Vector3 (-127.5f, 0, 0), 
		new Vector3 (127.5f, 0, 0),
		new Vector3 (-142.5f, 0, 0), 
		new Vector3 (142.5f, 0, 0),
		new Vector3 (-157.5f, 0, 0), 
		new Vector3 (157.5f, 0, 0),
		new Vector3 (-172.5f, 0, 0), 
		new Vector3 (172.5f, 0, 0)

		};

	private static List<Vector3> Odd = new List<Vector3> () {	
		new Vector3(0, 0, 0),
		new Vector3 (15, 0, 0), 
		new Vector3(-15,0,0),
		new Vector3 (30, 0, 0), 
		new Vector3 (-30, 0, 0), 
		new Vector3 (45, 0, 0), 
		new Vector3 (-45, 0, 0), 
		new Vector3 (60, 0, 0), 
		new Vector3 (-60, 0, 0), 
		new Vector3 (75, 0, 0), 
		new Vector3 (-75, 0, 0), 
		new Vector3 (90, 0, 0), 
		new Vector3 (-90, 0, 0), 
		new Vector3 (105, 0, 0), 
		new Vector3 (-105, 0, 0), 
		new Vector3 (120, 0, 0), 
		new Vector3 (-120, 0, 0), 
		new Vector3 (135, 0, 0),
		new Vector3 (-135, 0, 0), 
		new Vector3 (150, 0, 0),
		new Vector3 (-150, 0, 0), 
		new Vector3 (165, 0, 0),
		new Vector3 (-165, 0, 0), 
		new Vector3 (180, 0, 0)

	};
		




}
