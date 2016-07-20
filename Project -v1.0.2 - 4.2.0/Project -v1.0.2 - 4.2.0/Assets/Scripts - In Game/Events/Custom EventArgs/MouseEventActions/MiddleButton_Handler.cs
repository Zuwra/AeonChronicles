using UnityEngine;
using System.Collections;

public class MiddleButton_Handler : MouseEventArgs {

	public MiddleButton_Handler(int x, int y, int button) : base(x, y, button)
	{
		
	}
	
	public override void Command()
	{
		IUIManager uiManager = GameObject.FindObjectOfType<UIManager> ();

			uiManager.MiddleButton_SingleClick (this);

	}
}
