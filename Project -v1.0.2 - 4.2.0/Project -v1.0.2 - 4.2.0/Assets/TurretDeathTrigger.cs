using UnityEngine;
using System.Collections;

public class TurretDeathTrigger : MonoBehaviour, Modifier{

	private UnitManager mymanager;
	//private IWeapon weapon;
	// Use this for initialization
	void Start () {
		//weapon = GetComponent<IWeapon> ();
		mymanager = GetComponent<UnitManager> ();
		mymanager.myStats.addDeathTrigger (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void place( )
	{



	}

	public void Dying()
	{
		GameObject.FindObjectOfType<GameManager> ().activePlayer.UnitDying (this.gameObject, null, true);

	}



	// If I die, I need to let my parent tank know that I am gone
	public float modify(float damage, GameObject source)
	{ 
		transform.GetComponentInParent<TurretMount> ().unPlaceTurret();


		//mymanager.myStats.kill (source);



		/*
		if (weapon.myManager.myWeapon.Contains( weapon)) {
			foreach (TurretMount turr in transform.GetComponentsInParent<TurretMount> ()) {

				if (turr.turret != null) {

					// What does this do again?
					//weapon.myManager.myWeapon = turr.turret.GetComponent<IWeapon> ();
					return 0 ;
				}

			}

		


			//weapon.myManager.changeState (new DefaultState ());
		}*/
		return 0 ;
	}
}
