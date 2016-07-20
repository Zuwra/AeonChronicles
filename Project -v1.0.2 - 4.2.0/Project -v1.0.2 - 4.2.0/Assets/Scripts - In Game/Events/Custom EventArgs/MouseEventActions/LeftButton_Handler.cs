using UnityEngine;
using System.Collections;

public class LeftButton_Handler : MouseEventArgs {

	public LeftButton_Handler(int x, int y, int button) : base(x, y, button)
	{
		
	}
	
	public override void Command()
	{
		IUIManager uiManager = GameObject.FindObjectOfType<UIManager> ();
		
		if (!buttonUp)
		{
			if (doubleClick)
			{
				uiManager.LeftButton_DoubleClickDown(this);
			}
			else
			{
				uiManager.LeftButton_SingleClickDown(this);
			}
		}
		else
		{

	
			uiManager.LeftButton_SingleClickUp(this);
		}
	}
}
