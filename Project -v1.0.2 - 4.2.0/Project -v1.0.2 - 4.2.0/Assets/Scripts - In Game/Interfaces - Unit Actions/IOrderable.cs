using UnityEngine;
using System.Collections;

public interface IOrderable {
	

	bool IsAttackable();
	bool IsMoveable();
	bool IsInteractable();

	void GiveOrder(Order order);

	GameObject getObject();

	bool UseAbility (int n);


	bool ShouldInteract(HoverOver hoveringOver);
}
