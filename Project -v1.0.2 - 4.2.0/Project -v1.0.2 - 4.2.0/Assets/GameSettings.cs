using UnityEngine;
using System.Collections;

public class GameSettings {

	//public static float musicVolume = -1;
	//public static float masterVolume = -1;
	public static float gameSpeed = -1;




	public static float getMusicVolume()
	{
		return PlayerPrefs.GetFloat ("MusicVolume", .5f);
	}

	public static void setMusicVolume(float amount)
	{
		PlayerPrefs.SetFloat ("MusicVolume", amount);
	}

	public static float getMasterVolume()
	{
		
			return PlayerPrefs.GetFloat ("MasterVolume", .5f);
	}

	public static void setMasterVolume(float amount)
	{PlayerPrefs.SetFloat ("MasterVolume", amount);
	}

	public static float inverseGameTime()
	{
		return 1 / gameSpeed;
	}


	public static void setToolTips(bool onOff)
	{
		if (onOff) {
			PlayerPrefs.SetInt ("ToolTip", 1);
		} else {
			PlayerPrefs.SetInt ("ToolTip", 0);
		}
	}

	public static bool getToolTips()
	{
		if (PlayerPrefs.GetInt("ToolTip", 1) == 1) {
			return true;
	
		} else {
			return false;
		}	
	}


	public static void setAbility(bool onOff)
	{
		if (onOff) {
			PlayerPrefs.SetInt ("AbilityTool", 1);
		} else {
			PlayerPrefs.SetInt ("AbilityTool", 0);
		}
	}

	public static bool getAbility()
	{
		if (PlayerPrefs.GetInt("AbilityTool", 1) == 1) {
			return true;

		} else {
			return false;
		}	
	}


}
