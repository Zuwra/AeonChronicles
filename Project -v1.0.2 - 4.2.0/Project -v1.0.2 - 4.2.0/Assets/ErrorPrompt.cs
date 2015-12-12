using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ErrorPrompt : MonoBehaviour {

	Text errorPrompt;
	
	public void showError(string err)
	{this.gameObject.GetComponent<Text> ().text = err;
		this.gameObject.GetComponent<Text> ().enabled = true;


		StartCoroutine(MyCoroutine());
	}
	
	
	IEnumerator MyCoroutine ()
	{

		yield return new WaitForSeconds(3f);
		this.gameObject.GetComponent<Text> ().enabled = false;
		print("MyCoroutine is now finished.");
	}//
	
	
	
	
	
	
	
	


	// Use this for initialization
	void Start () {
	errorPrompt = this.gameObject.GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	


}
