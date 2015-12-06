using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IManager {
	
	void BuildingAdded(Building building);
	void BuildingRemoved(Building building);
	void UnitAdded(Unit unit);
	void UnitRemoved(Unit unit);
	int GetUniqueID();	
	
	void AddMoney(float money);
	
	void AddMoneyInstant(float money);
	void RemoveMoneyInstant(float money);
	
	bool CostAcceptable(float cost);
	
	int Money { get; }
}
