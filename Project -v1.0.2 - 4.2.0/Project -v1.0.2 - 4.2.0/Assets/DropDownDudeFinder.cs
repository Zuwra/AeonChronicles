using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropDownDudeFinder : MonoBehaviour {

	private int currentIndex = 0;
	public List<GameObject> myProducer;


	public void onClick()
	{
		myProducer.RemoveAll (item=>item == null);
		currentIndex++;
		if (currentIndex >= myProducer.Count) {
			currentIndex = 0;
		}
	
		if (myProducer.Count > 0) {

			Vector3 location = myProducer[currentIndex].transform.position;
			location.z -= 70;

			MainCamera.main.Move (location);
		}



	}


}
