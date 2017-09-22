
	using UnityEngine;
	using System.Collections;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;

	public class UnitUIBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


		public Canvas toolbox;

		
		//private bool inBox;
	public int index;

	public Text Title;
	public Text Range;
	public Text Damage;
	public Text AttackSpeed;
	//public Text AttackNum;
	public Text CantAttack;
	public UnitCardCreater myUnit;


		public void OnPointerEnter(PointerEventData eventd)
			{//inBox = true;
			toolbox.enabled = true;

		initialize ();
			
			}

		public void OnPointerExit(PointerEventData eventd)
		{//inBox = false;
			toolbox.enabled = false;
		}


	public void initialize()
	{
		if (myUnit.currentUnit) {
			Title.text = myUnit.currentUnit.myWeapon [index].Title;
			Range.text = "Range: " +myUnit.currentUnit.myWeapon [index].range;
			AttackSpeed.text = "Att. Rate: " +   ((float)((int)(100 *myUnit.currentUnit.myWeapon [index].attackPeriod)))/100;

			//if (myUnit.currentUnit.myWeapon [index].numOfAttacks > 1) {
				//AttackNum.text = "Attacks: " + myUnit.currentUnit.myWeapon [index].numOfAttacks;
			//} else {
				//AttackNum.text = "";
			//}
			Damage.text = "Damage: " + ((float)((int)(100 *myUnit.currentUnit.myWeapon [index].baseDamage)))/100; // myUnit.currentUnit.myWeapon [index].baseDamage;

			//if (myUnit.currentUnit.myWeapon [index].extraDamage.Length > 0) {
				//Damage.text += " ( +" +myUnit.currentUnit.myWeapon [index].extraDamage [0].bonus + " vs " + myUnit.currentUnit.myWeapon [index].extraDamage [0].type + ")";
			//} 
			foreach (IWeapon.bonusDamage bd in myUnit.currentUnit.myWeapon [index].extraDamage) {
				Damage.text += " (+" +bd.bonus + " vs " + bd.type + ")";
			}
			if (myUnit.currentUnit.myWeapon [index].numOfAttacks > 1) {
				Damage.text += "  Attacks X " + myUnit.currentUnit.myWeapon [index].numOfAttacks;
			} 


			string n = "";
			if (myUnit.currentUnit.myWeapon [index].cantAttackTypes.Count > 0) {
				n+="Can't Attack: ";
			}
			foreach (UnitTypes.UnitTypeTag t in myUnit.currentUnit.myWeapon [index].cantAttackTypes) {
				n += t.ToString () + ",";
			}
			if (n.EndsWith (",")) {
				n = n.Substring (0, n.Length - 1);
			}
			CantAttack.text = n;
				
		}
	}



	}
