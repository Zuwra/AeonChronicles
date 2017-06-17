using UnityEngine;
using System.Collections;

public class ImmortalityProtocol : MonoBehaviour,LethalDamageinterface {

	bool canRecall = true;
	public float cooldown;
	private float timer;
	private bool onCooldown= false;


	// Use this for initialization
	void Start () {
		GameManager.main.activePlayer.addDeathWatcher (this);
	
	}
	
	// Update is called once per frame
	void Update () {
	if (onCooldown) {
			timer -= Time.deltaTime;
			if(timer <= 0)
				{
				onCooldown = false;
			}
		}
	}

	public bool lethalDamageTrigger(UnitManager unit, GameObject deathsource)
	{if (canRecall && !onCooldown) {
			if(!unit.myStats.isUnitType(UnitTypes.UnitTypeTag.Structure) &&
			   unit.PlayerOwner == 1){
				EmergencyRecall(unit.gameObject);
				return false;
			}
		}
		return true;

	}

	public void EmergencyRecall(GameObject obj)
	{onCooldown = true;
		timer = cooldown;
		Debug.Log ("Recalling");
		Vector3 location = new Vector3(this.gameObject.transform.position.x ,this.gameObject.transform.position.y+15,this.gameObject.transform.position.z);
		obj.GetComponent<UnitStats> ().health = obj.GetComponent<UnitStats> ().Maxhealth / 10;
		obj.transform.position = location;


	}



}
