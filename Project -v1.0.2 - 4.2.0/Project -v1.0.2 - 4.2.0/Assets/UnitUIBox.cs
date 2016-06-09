
	using UnityEngine;
	using System.Collections;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;

	public class UnitUIBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


		public Canvas toolbox;

		
		private bool inBox;
	public int index;

	public Text Title;
	public Text Range;
	public Text Damage;
	public Text AttackSpeed;
	public Text AttackNum;
	public UnitCardCreater myUnit;


		public void OnPointerEnter(PointerEventData eventd)
			{inBox = true;
			toolbox.enabled = true;

		initialize ();
			
			}

		public void OnPointerExit(PointerEventData eventd)
		{inBox = false;
			toolbox.enabled = false;
		}


	public void initialize()
	{
		if (myUnit.currentUnit) {
			Title.text = myUnit.currentUnit.myWeapon [index].Title;
			Range.text = "Range: " +myUnit.currentUnit.myWeapon [index].range;
			AttackSpeed.text = "Attack Speed: " + myUnit.currentUnit.myWeapon [index].attackPeriod;

			if (myUnit.currentUnit.myWeapon [index].numOfAttacks > 1) {
				AttackNum.text = "Number of Attacks: " + myUnit.currentUnit.myWeapon [index].numOfAttacks;
			} else {
				AttackNum.text = "";
			}
			Damage.text = "Damage: " + myUnit.currentUnit.myWeapon [index].baseDamage;

			if (myUnit.currentUnit.myWeapon [index].extraDamage.Length > 0) {
				Damage.text += " ( +" +myUnit.currentUnit.myWeapon [index].extraDamage [0].bonus + " v " + myUnit.currentUnit.myWeapon [index].extraDamage [0].type + ")";
			} 
		}
	}

		// Use this for initialization
		void Start () {
		
		}

		// Update is called once per frame
		void Update () {

		}



	}
