﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GuiControls : MonoBehaviour {

	public Canvas commandPanel;
	public Sprite MaxSprite;
	public Sprite minSprite;
	Image myImage;

	void Start()
	{
		myImage = GetComponent<Image> ();
	}


	public void minimize()
	{if (commandPanel != null) {
			commandPanel.enabled = !commandPanel.enabled;

			if (commandPanel.enabled) {
				myImage.sprite = minSprite;
			} else {
				myImage.sprite = MaxSprite;
			}
	
		}
	}
}
