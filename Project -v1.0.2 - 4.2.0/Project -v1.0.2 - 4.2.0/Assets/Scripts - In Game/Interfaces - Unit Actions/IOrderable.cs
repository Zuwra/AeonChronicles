using UnityEngine;
using System.Collections;

public interface IOrderable {
	

	bool IsAttackable();
	bool IsMoveable();
	bool IsInteractable();

	void GiveOrder(Order order);

	GameObject getObject();

	bool UseQAbility();
	bool UseWAbility();
	bool UseEAbility();
	bool UseRAbility();
	
	bool ShouldInteract(HoverOver hoveringOver);
}
