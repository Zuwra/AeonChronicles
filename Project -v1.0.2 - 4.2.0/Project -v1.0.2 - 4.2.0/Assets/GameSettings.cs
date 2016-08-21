using UnityEngine;
using System.Collections;

public class GameSettings {

	public static float musicVolume = -1;
	public static float masterVolume = -1;
	public static float gameSpeed = -1;

	public static float inverseGameTime()
	{
		return 1 / gameSpeed;
	}


}
