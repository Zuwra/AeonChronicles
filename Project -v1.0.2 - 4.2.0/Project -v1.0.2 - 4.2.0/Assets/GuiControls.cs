using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GuiControls : MonoBehaviour {

	public Canvas commandPanel;
	public Sprite MaxSprite;
	public Sprite minSprite;
	Image myImage;

	void Awake()
	{
		myImage = GetComponent<Image> ();
	}


	public void minimize()
	{if (commandPanel != null) {
			commandPanel.enabled = !commandPanel.enabled;

			if (commandPanel.enabled) {
				if (minSprite) {
					myImage.sprite = minSprite;
				}
			} else {
				if (myImage == null) {
					//Debug.Log (this.gameObject.name + "   is null");
				}
				else if (MaxSprite) {
					myImage.sprite = MaxSprite;
				}
			}
	
		}
	}
}
