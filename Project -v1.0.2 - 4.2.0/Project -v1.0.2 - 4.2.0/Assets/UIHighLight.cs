using UnityEngine;
using System.Collections;

public class UIHighLight : MonoBehaviour {

	public GameObject Square;
	public GameObject Rectangle;
	public GameObject wideRectangle;

	public static UIHighLight main;

	// Use this for initialization
	void Awake () {
		main = this;
	
	}


	

	public void highLight(GameObject input, int size)
	{//Debug.Log ("Activating Highlighter " + size);
		

		if (size == 0) {
			StartCoroutine (tempSHowObjective(Square));

		}
		else if (size == 1) {
			StartCoroutine (tempSHowObjective(Rectangle));
		}
		else if (size == 2) {
			StartCoroutine (tempSHowObjective(wideRectangle));
		}


	}

	IEnumerator tempSHowObjective(GameObject thingy)
	{
		if (thingy) {
			thingy.SetActive (true);
			yield return new WaitForSeconds (8);
		}
		if(thingy){
			thingy.SetActive (false);

		}
	}


}
