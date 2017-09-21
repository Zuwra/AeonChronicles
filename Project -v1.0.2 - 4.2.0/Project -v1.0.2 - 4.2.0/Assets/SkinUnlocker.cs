using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlocker : MonoBehaviour {

	public List<Skin> mySkins;

	bool setFalse;
	void Awake()
	{	if (!setFalse) {
			setFalse = true;
		
			foreach (Skin s in mySkins) {
				foreach (GameObject obj in s.myPieces) {
					obj.SetActive (false);
				}
			}
		}
	}


	[System.Serializable]
	public class Skin
	{
		public string name;
		public List<GameObject> myPieces;
	
	}



	public void unlockSkin(string name)
	{
		if (!setFalse) {
			Awake ();
		}
		
		foreach (Skin s in mySkins) {
			if (name == s.name) {
			
				foreach (GameObject obj in s.myPieces) {
					obj.SetActive (true);
				}
			}

		}

	}



}
