using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayMenu : MonoBehaviour {


	public Dropdown healthList;
	private bool toggled= true;
	private MainCamera camera;
	public Slider scrollSpeed;
	// Use this for initialization
	void Start () {

		camera = GameObject.Find ("Main Camera").GetComponent<MainCamera> ();
	
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


	public void healthBarList()
	{
		foreach (Selected sel in Object.FindObjectsOfType<Selected>()) {
			if (healthList.value == 0) {
				sel.mydisplayType = Selected.displayType.always;
			
			
	
			} else if (healthList.value == 1) {
				sel.mydisplayType = Selected.displayType.damaged;
			
	
			} else if (healthList.value == 2) {
				sel.mydisplayType = Selected.displayType.selected;

			} else if (healthList.value == 3) {
				sel.mydisplayType = Selected.displayType.damAndSel;


			} else {
				sel.mydisplayType = Selected.displayType.never;
			
			}
		
		}
	}


	public void resetScrollSpeed()
	{camera.ScrollSpeed = scrollSpeed.value * 700 + 100;
	}


	public Selected.displayType getDisplayType()
	{if (healthList.value == 0) {
			return Selected.displayType.always;
		


	} else if (healthList.value == 1) {
		return Selected.displayType.damaged;


	} else if (healthList.value == 2) {
		return Selected.displayType.selected;

	} else if (healthList.value == 3) {
		return Selected.displayType.damAndSel;


	} else {
		return Selected.displayType.never;
	
	}}

}
