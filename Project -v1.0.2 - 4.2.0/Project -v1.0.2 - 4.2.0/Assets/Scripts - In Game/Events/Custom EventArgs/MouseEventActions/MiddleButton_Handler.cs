using UnityEngine;
using System.Collections;

public class MiddleButton_Handler : MouseEventArgs {

	public MiddleButton_Handler(int x, int y, int button) : base(x, y, button)
	{
		
	}
	
	public override void Command()
	{
		IUIManager uiManager = ManagerResolver.Resolve<IUIManager>();
		
		if (doubleClick)
		{
			uiManager.MiddleButton_DoubleClick (this);
		}
		else
		{
			uiManager.MiddleButton_SingleClick (this);
		}
	}
}
