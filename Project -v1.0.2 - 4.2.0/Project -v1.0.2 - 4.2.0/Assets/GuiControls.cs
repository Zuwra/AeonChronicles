using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GuiControls : MonoBehaviour {

	public Canvas commandPanel;
	public Text minimizeB;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void minimize()
	{if (commandPanel != null) {
			commandPanel.enabled = !commandPanel.enabled;
			if (commandPanel.enabled) {
				minimizeB.text = "-";
			} else {
				minimizeB.text = "+";
			}
		}
	}
}
