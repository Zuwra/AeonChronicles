using UnityEngine;
using System.Collections;

public class ZephyrInteract : StandardInteract{


	public override void Interact(Order order)
	{
		UnitManager manage = order.Target.GetComponent<UnitManager> ();
		if (!manage) {
			manage = order.Target.GetComponentInParent<UnitManager> ();
		}

		if (manage != null) {

			if (manage.PlayerOwner != this.gameObject.GetComponent<UnitManager>().PlayerOwner  ) {
				if (this.gameObject.GetComponent<UnitManager> ().myWeapon == null) {
					myManager.changeState (new FollowState (order.Target.gameObject, myManager));
				} else {
					//Debug.Log ("Ordering to interact " + manage.gameObject);
					myManager.changeState (new InteractState (manage.gameObject, myManager));
				}
			} else {
				RepairTurret RT = myManager.GetComponentInChildren<RepairTurret> ();
				if (RT) {
					RT.setTarget (order.Target);


				} else {
					myManager.changeState (new FollowState (order.Target.gameObject, myManager));
				}
			}
		}
	}

	public override void  Follow(Order order){
		RepairTurret RT = myManager.GetComponentInChildren<RepairTurret> ();

		if (RT) {
			RT.setTarget (order.Target);

		
		}

		else if (order.Target == this.gameObject) {
			return;
		}

		else if (myManager.myWeapon.Count > 0) {

			myManager.changeState (new AttackMoveState (null, order.OrderLocation, AttackMoveState.MoveType.command, myManager, myManager.gameObject.transform.position));
		}else {
			myManager.changeState (new MoveState (order.OrderLocation, myManager));
		}
	}

}
