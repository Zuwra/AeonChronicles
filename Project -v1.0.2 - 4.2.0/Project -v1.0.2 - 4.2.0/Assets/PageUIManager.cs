using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PageUIManager : MonoBehaviour {

	private SelectedManager selectM;
	public List<Button> pageList = new List<Button> ();

	private int currentPage;

	// Use this for initialization
	void Start () {

		selectM = GameObject.FindObjectOfType<SelectedManager> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void selectPage(int n)
	{pageList [currentPage].image.color = Color.white;
		currentPage = n;
		selectM.setPage (n);
		pageList [n].image.color = Color.red;
	
	}

	public void setPageCount(int n)
	{

		for (int i = 0; i < pageList.Count; i++) {
			if (i < n) {
				pageList [i].gameObject.SetActive (true);
			} else {
				pageList [i].gameObject.SetActive (false);
			}
		}
			
	}

}