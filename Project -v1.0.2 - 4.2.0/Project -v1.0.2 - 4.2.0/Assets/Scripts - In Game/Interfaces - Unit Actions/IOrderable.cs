using UnityEngine;
using System.Collections;

public interface IOrderable {
	

	void GiveOrder(Order order);

	GameObject getObject();


}
