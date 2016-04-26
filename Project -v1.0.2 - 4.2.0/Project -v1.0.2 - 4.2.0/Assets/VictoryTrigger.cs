using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class VictoryTrigger : MonoBehaviour {

	public Canvas VictoryScreen;
	public Canvas DefeatScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Win()
	{
		VictoryScreen.enabled = true;
		GameObject.FindObjectOfType<MainCamera> ().DisableScrolling ();
		StartCoroutine(WinLevel ());
	}


	public void Lose()
	{
		DefeatScreen.enabled = true;
		GameObject.FindObjectOfType<MainCamera> ().DisableScrolling ();
		StartCoroutine(LoseLevel ());
	}

	IEnumerator WinLevel ()
	{
		yield return new WaitForSeconds (6);
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (0);
	}

	IEnumerator LoseLevel ()
	{
		yield return new WaitForSeconds (6);
		GameObject.FindObjectOfType<MainCamera> ().EnableScrolling ();
		DefeatScreen.enabled = false;
		SceneManager.LoadScene (0);
	}



}
