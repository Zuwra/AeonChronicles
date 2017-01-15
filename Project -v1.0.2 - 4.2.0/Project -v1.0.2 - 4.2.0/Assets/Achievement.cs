using UnityEngine;
using System.Collections;

public abstract class Achievement : MonoBehaviour {

	public string Title;
	public string Description;
	public Sprite myIcon;

	public abstract void CheckBeginning ();

	public abstract void CheckEnd ();

	public bool IsAccomplished()
	{
		if (PlayerPrefs.GetInt ("Title", 0) == 0) {
			return false;
		}
		return true;
	}

	protected void Accomplished()
	{
		PlayerPrefs.SetInt ("Title", 1);
	}

	public void Reset()
	{
		PlayerPrefs.SetInt ("Title", 0);
	}
}
