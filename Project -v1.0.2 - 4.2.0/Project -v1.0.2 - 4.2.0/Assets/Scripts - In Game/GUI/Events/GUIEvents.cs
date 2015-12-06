using UnityEngine;
using System.Collections;

public class GUIEvents 
{
	public delegate void TypeButton(object sender, TypeButtonEventArgs e);
	public delegate void QueueButton(object sender, QueueButtonEventArgs e);
	public delegate void MaintenanceButton(object sender, MaintenanceButtonEventArgs e);
	
	public delegate void ItemUpdate(float frameTime);
	
	public delegate void MenuWidthChange(float newWidth);
	
	public static event TypeButton TypeButtonChanged; 
	public static event QueueButton QueueButtonChanged;
	public static event MaintenanceButton MaintenanceButtonChanged;
	
	public static event ItemUpdate ItemUpdateTimer;
	
	public static event MenuWidthChange MenuWidthChanged;
	
	public static void TypeButtonPressed(object sender, ButtonType button)
	{
		if (TypeButtonChanged != null)
		{
			TypeButtonChanged(sender, new TypeButtonEventArgs(button));
		}
	}
	
	public static void QueueButtonPressed(object sender, int button)
	{
		if (QueueButtonChanged != null)
		{
			QueueButtonChanged(sender, new QueueButtonEventArgs(button));
		}
	}
	
	public static void MaintenanceButtonPreesed(object sender, int button)
	{
		if (MaintenanceButtonChanged != null)
		{
			MaintenanceButtonChanged(sender, new MaintenanceButtonEventArgs(button));
		}
	}
	
	public static void TellItemsToUpdate(float frameTime)
	{
		if (ItemUpdateTimer != null)
		{
			ItemUpdateTimer(frameTime);
		}
	}
	
	public static void MenuWidthHasChanged(float newWidth)
	{
		if (MenuWidthChanged != null)
		{
			MenuWidthChanged(newWidth);
		}
	}
}
