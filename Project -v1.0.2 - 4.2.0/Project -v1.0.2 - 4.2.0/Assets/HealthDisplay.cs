using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {


	public List<Image> buffList = new List<Image>();
	public Material positive;
	public Material negative;

	private List<int> colorList = new List<int>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	//THIS METHOD RETURNS AN ID Reciept which the client class must keep so that it can turn it back in to turn of the buff icon
	public int addBuff(Color c, bool b)
	{

		foreach (Image i in buffList) {
			if (!i.enabled) {
				if (b) {
					i.material = positive;
				} else {
					i.material = negative;
				}
				i.color = c;
				break;
			}
		}

	
		int id;
		do {
			id = Random.Range (0, 50);
		} while(!colorList.Contains (id));


		colorList.Add (id);
			return id;
	}

	public void removeBuff(int id)
	{
		int n = colorList.IndexOf(id);

		for (int i = n; i < buffList.Count; i++) {
			if (i + 1 == buffList.Count || !buffList[i+1].enabled) {
			
				break;
			}

			buffList [i].color = buffList [i + 1].color;
			buffList [i].material = buffList [i + 1].material;
		}

		colorList.Remove (id);

	}


}
