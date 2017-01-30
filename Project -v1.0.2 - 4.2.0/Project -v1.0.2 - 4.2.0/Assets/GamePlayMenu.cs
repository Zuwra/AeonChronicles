using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayMenu : MonoBehaviour {


	public Dropdown healthList;
	private bool toggled= true;
	private MainCamera cam;
	public Slider scrollSpeed;
	public Text speedPercent;
	public Toggle showToolTip;
	public Toggle showAbility;

	// Use this for initialization
	void Start () {

		cam = GameObject.Find ("Main Camera").GetComponent<MainCamera> ();

		if (!GameSettings.getToolTips()) {

			toggleTools ();
		}

		if (!GameSettings.getAbility()) {
			toggleAbilities ();
		}
		healthBarList ();
	
	}
	

	public void toggleTools()
	{
		toggled = !toggled;
		GameSettings.setToolTips (toggled);
		if (showToolTip.isOn != toggled) {
			showToolTip.isOn = toggled;
		}
		foreach (ToolTip tool in Object.FindObjectsOfType<ToolTip> ()) {
			if (!tool.Ability) {
				tool.enabled = toggled;
			}
		}
	}

	public void toggleAbilities()
	{
		toggled = !toggled;
		GameSettings.setAbility (toggled);
		if (showAbility.isOn != toggled) {
			showAbility.isOn = toggled;
		}
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
				sel.setDisplayType( Selected.displayType.always);
			
			
	
			} else if (healthList.value == 1) {
				sel.setDisplayType(Selected.displayType.damaged);
			
	
			} else if (healthList.value == 2) {
				sel.setDisplayType(Selected.displayType.selected);

			} else {
			sel.setDisplayType( Selected.displayType.never);
			
			}
		
		}
	}

	public void setGameSpeed(Scrollbar theSlide)
	{
		GameSettings.gameSpeed = ((int)(theSlide.value/.2f))*.2f + .6f;
		speedPercent.text = "("+(int)(GameSettings.gameSpeed * 100) + ")%";


	}

	public void resetScrollSpeed()
	{cam.ScrollSpeed = scrollSpeed.value * 700 + 100;
	}


	public Selected.displayType getDisplayType()
	{if (healthList.value == 0) {
			return Selected.displayType.always;
		


	} else if (healthList.value == 1) {
		return Selected.displayType.damaged;


	} else if (healthList.value == 2) {
		return Selected.displayType.selected;

	} else {
		return Selected.displayType.never;
	
	}}

}
