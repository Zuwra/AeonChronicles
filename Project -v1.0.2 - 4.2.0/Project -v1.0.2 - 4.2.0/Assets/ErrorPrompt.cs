using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ErrorPrompt : MonoBehaviour {


	public static ErrorPrompt instance;

	public void showError(string err)
	{this.gameObject.GetComponent<Text> ().text = err;
		this.gameObject.GetComponent<Text> ().enabled = true;

		StopCoroutine (MyCoroutine ());
		StartCoroutine(MyCoroutine());
	}
	
	
	IEnumerator MyCoroutine ()
	{

		yield return new WaitForSeconds(3f);
		this.gameObject.GetComponent<Text> ().enabled = false;

	}//
	
	
	
	
	
	



	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	


}
