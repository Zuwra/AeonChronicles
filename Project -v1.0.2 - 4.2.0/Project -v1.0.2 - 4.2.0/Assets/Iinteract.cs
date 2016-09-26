using UnityEngine;
using System.Collections;

public interface Iinteract  {




	void computeInteractions (Order order);

	void initialize();

	UnitState computeState (UnitState state);



}
