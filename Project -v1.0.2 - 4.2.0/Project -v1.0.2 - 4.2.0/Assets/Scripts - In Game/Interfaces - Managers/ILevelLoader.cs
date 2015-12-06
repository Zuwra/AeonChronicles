using UnityEngine;
using System.Collections;

public interface ILevelLoader 
{
	void LoadLevel(int level);
	void FinishLoading();
	void ChangeText (string text);
}
