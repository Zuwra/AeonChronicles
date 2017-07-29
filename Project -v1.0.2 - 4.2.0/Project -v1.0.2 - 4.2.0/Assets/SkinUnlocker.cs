using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlocker : MonoBehaviour {

	public List<Skin> mySkins;

	void Start()
	{
		foreach (Skin s in mySkins) {
			foreach (GameObject obj in s.myPieces) {
				obj.SetActive (false);
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
		foreach (Skin s in mySkins) {
			if (name == s.name) {
			
				foreach (GameObject obj in s.myPieces) {
					obj.SetActive (true);
				}
			}

		}

	}



}
