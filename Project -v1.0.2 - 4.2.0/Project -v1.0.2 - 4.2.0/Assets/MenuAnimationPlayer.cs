using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuAnimationPlayer : MonoBehaviour {


	float lastAnimationTime;
	int totalClickCount;
	Animator myANim;

	public bool tank;

	bool activated = false;

	public List<GameObject> myObjs;
	int currentIndex;

	void Start()
	{
		myANim = GetComponent<Animator> ();
	}

	public void ClickOn()
	{if (tank) {
			totalClickCount++;
	
			if (totalClickCount > 15 && !activated) {
				activated = true;
				myANim.SetInteger ("State", 2);
				StartCoroutine (DelayState ());

			} else if (totalClickCount < 15 && Time.time > lastAnimationTime + 1) {

				int n = Random.Range (0, 2);

				myANim.SetInteger ("State", n);
				lastAnimationTime = Time.time;
				StartCoroutine (DelayState ());
			}
		} else {

			myObjs [currentIndex].SetActive (false);
			currentIndex++;
			if (currentIndex > myObjs.Count - 1) {
				currentIndex = 0;
		}
		myObjs [currentIndex].SetActive (true);
	}
		}

	IEnumerator DelayState()
	{
		yield return new WaitForSeconds (.5f);
		myANim.SetInteger ("State", -1);
	}


}
