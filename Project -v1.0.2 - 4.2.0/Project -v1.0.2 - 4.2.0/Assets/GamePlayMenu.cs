using UnityEngine;
using System.Collections;

public class GamePlayMenu : MonoBehaviour {



	private bool toggled= true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void toggleTools()
	{
		toggled = !toggled;
		foreach (ToolTip tool in Object.FindObjectsOfType<ToolTip> ()) {
			if (!tool.Ability) {
				tool.enabled = toggled;
			}
		}
	}

	public void toggleAbilities()
	{
		toggled = !toggled;
		foreach (ToolTip tool in Object.FindObjectsOfType<ToolTip> ()) {
			if (tool.Ability) {
				tool.enabled = toggled;
			}
		}
	}

}
