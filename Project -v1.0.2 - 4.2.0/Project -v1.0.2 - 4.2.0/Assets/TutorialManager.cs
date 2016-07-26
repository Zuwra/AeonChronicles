using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {


	public List<TutorialPage> myPages;

	int currentPage = 0;
	// Use this for initialization
	void Start () {
		foreach (TutorialPage o in myPages) {
			o.gameObject.SetActive (false);
		}
		myPages [currentPage].gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextPage()
	{	
		foreach (TutorialPage o in myPages) {
			o.gameObject.SetActive (false);
		}


		currentPage++;
		if (currentPage == myPages.Count) {
			currentPage = 0;
		}
		myPages [currentPage].gameObject.SetActive (true);
	}

	public void previousPage()
	{	
		foreach (TutorialPage o in myPages) {
			o.gameObject.SetActive (false);
		}

		
		currentPage--;
		if (currentPage ==-1) {
			currentPage = myPages.Count -1;
		}

		myPages [currentPage].gameObject.SetActive (true);
	}

	public void showAdvanced()
	{
		foreach (GameObject obj in myPages[currentPage].advanced) {
			obj.SetActive (true);
		}
	}
}

