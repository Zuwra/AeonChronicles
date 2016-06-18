using UnityEngine;
using System.Collections;

public abstract class Behavior: MonoBehaviour{



	public enum buffType{
		movement, damage, health, ability, range, bleed, energy
	}

	public bool BuffIsGood;
	public buffType BuffType;

	private int BuffUiId;

	private HealthDisplay displayer;
	//private Color myColor;
	private bool initialized = false;

	// Use this for initialization
void Start () {
		initialized = true;
		Debug.Log ("Setting displayer");
		displayer = GetComponentInChildren<HealthDisplay> ();
		setBuffStuff (BuffType, BuffIsGood);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void applyBuffUI()
	{if (!initialized) {
		
			setBuffStuff (BuffType, BuffIsGood);
		}

		if (displayer == null) {
			displayer = GetComponentInChildren<HealthDisplay> ();
		}

		//BuffUiId = displayer.addBuff (myColor, BuffIsGood);

	}


	public void unApplybuffUI()
	{//displayer.removeBuff (BuffUiId);
	}

	public void setBuffStuff(buffType b,bool s)
	{initialized = true;
		BuffType = b;
		BuffIsGood = s;



		switch (b) {
		case buffType.ability: 
			//myColor = Color.gray;
			break;

		case buffType.bleed: 
			//myColor = new Color(1,0,1);
			break;

		case buffType.movement: 
			//myColor = Color.yellow;
			break;

		case buffType.energy: 
			//myColor = Color.blue;
			break;

		case buffType.health: 
			//myColor = Color.green;
			break;

		case buffType.range: 
			//myColor = Color.black;
			break;

		case buffType.damage: 
			//myColor = Color.red;
			break;

		}


	}



}
