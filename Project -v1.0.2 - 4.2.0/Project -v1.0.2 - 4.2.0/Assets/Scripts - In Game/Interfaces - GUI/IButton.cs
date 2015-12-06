using UnityEngine;
using System.Collections;

public interface IButton 
{
	bool Selected { get; }
	
	void Execute();
	
}
