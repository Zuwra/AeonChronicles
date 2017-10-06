using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PageUIManager : MonoBehaviour {

	public Material grayscale;
	public Sprite blankSprite;
	private SelectedManager selectM;
	public List<Button> pageList = new List<Button> ();

	private int currentPage;

	// Use this for initialization
	void Start () {

		selectM = SelectedManager.main;
	}



	public void selectPage(int n)
	{
		foreach (Button b in pageList) {
			b.image.material = grayscale;
		}
		pageList [n].image.material = null;
		currentPage = n;
		selectM.setPage (n);

		for (int i = 0; i < pageList.Count; i++) {
			if (pageList [i].gameObject.activeSelf) {
				pageList [i].transform.SetSiblingIndex (pageList.Count -i -1);
			}
		}

		if (n < 4) {
			pageList [n].transform.SetAsLastSibling ();
		} else {
			pageList [n].transform.SetSiblingIndex(3);
		}

	}

	public void setPageCount(List<Page> units)
	{
		if (units.Count > 0) {
			for (int i = 0; i < pageList.Count; i++) {
				if (i < units.Count) {
					pageList [i].gameObject.SetActive (true);
					pageList [i].image.material = grayscale;
					pageList [i].transform.SetSiblingIndex (pageList.Count -i -1);
					Image[] array = pageList [i].GetComponentsInChildren<Image> ();

					string lastDude = ""; // Dont repeat icons if a guy takes mroe than one row.
					for (int n = 0; n < 3; n++) {
						try {//Weird math here because the getComponentsInChildren also gets the component in itself
							if(((units [i]).rows [n]) [0].getUnitManager ().UnitName != lastDude){
								array [n + (array.Length - 3)].sprite = ((units [i]).rows [n]) [0].getUnitManager ().getUnitStats ().Icon;
								lastDude = ((units [i]).rows [n]) [0].getUnitManager ().UnitName;
							}

						} catch {

							array [n + (array.Length - 3)].sprite = blankSprite;
						}
					}


				} else {
					pageList [i].gameObject.SetActive (false);
				}
			}
			pageList [0].image.material = null;
			pageList [0].transform.SetAsLastSibling ();
		}
	}

}