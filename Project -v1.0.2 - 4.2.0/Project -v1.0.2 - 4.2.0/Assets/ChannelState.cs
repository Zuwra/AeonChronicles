using UnityEngine;
using System.Collections;

public class ChannelState : UnitState{

	// Update is called once per frame



	public ChannelState()
	{

	}


	public override void initialize()
	{
	}

	override
	public void Update () {// change this later so t will only check for attackable enemies.

	}

	/*
	override
	public void attackResponse(UnitManager src, float amount)
	{/*
		UnitManager manage = src.GetComponent<UnitManager> ();
		if (manage.PlayerOwner != myManager.PlayerOwner) {



			if (myWeapon.isValidTarget (src)) {
				myManager.GiveOrder (Orders.CreateAttackMove (src.transform.position));
			}

			else {
				Vector3 spot = (myManager.transform.position + (myManager.transform.position - src.transform.position)* .4f);
				spot.y += 100;
				Ray ray = new Ray(spot, Vector3.down);

				RaycastHit hit;
				Vector3 dest = new Vector3();
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~8))
				{
					dest = hit.point;
				}

				myManager.GiveOrder(Orders.CreateAttackMove(dest));
			}
		}


		}*/

	override
	public void endState()
	{
	}

	}


