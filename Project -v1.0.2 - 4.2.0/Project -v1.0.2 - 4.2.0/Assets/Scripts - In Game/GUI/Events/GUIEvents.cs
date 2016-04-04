using UnityEngine;
using System.Collections;

public class GUIEvents 
{
	
	public delegate void ItemUpdate(float frameTime);
	
	public delegate void MenuWidthChange(float newWidth);

	public static event ItemUpdate ItemUpdateTimer;
	

	
	public static void TellItemsToUpdate(float frameTime)
	{
		if (ItemUpdateTimer != null)
		{
			ItemUpdateTimer(frameTime);
		}
	}

}
