using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IManager {

	int GetUniqueID();	
	
	void AddMoney(float money);
	
	void AddMoneyInstant(float money);
	void RemoveMoneyInstant(float money);
	
	bool CostAcceptable(float cost);
	
	int Money { get; }
}
