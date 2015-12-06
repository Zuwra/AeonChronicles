using UnityEngine;
using System.Collections;

public class RightButton_Handler : MouseEventArgs {

	public RightButton_Handler(int x, int y, int button) : base(x, y, button)
	{
		
	}
	
	public RightButton_Handler(int x, int y, int button, Vector3 target) : base(x, y, button, target)
	{
		
	}
	
	public RightButton_Handler(int x, int y, int button, RTSObject target) : base(x, y, button, target)
	{
		
	}
	
	public override void Command()
	{
		IUIManager uiManager = ManagerResolver.Resolve<IUIManager>();
		
		if (doubleClick)
		{
			uiManager.RightButton_DoubleClick (this);
		}
		else
		{
			uiManager.RightButton_SingleClick (this);
		}
	}
}
