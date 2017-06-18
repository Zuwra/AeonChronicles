using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
public class MegaMap : MonoBehaviour{


	public Image fogSprite;
	public Image UnitSprite;

	public Image OtherFogSprite;
	public Image OtherUnitSprite;
	public GameObject childParent;
	public MiniMapUIController miniController;

	bool active;

	void Start()
	{
		InvokeRepeating ("UpdateSprite", .1f, .1f);
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.M)) {
			ToggleMegamap ();
		}
	}

	public void ToggleMegamap()
	{active = !active;
		childParent.SetActive (active);
		
	}


	// Update is called once per frame
	void UpdateSprite () {
		if (active) {
			fogSprite.sprite = OtherFogSprite.sprite;
			UnitSprite.sprite = miniController.UnitSprite;// OtherUnitSprite;
		
		}
		
	}


}
