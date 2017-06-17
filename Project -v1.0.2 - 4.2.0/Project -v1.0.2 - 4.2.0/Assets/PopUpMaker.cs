using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpMaker : MonoBehaviour {

	public string mytext;
	public Color textColor;
	public Sprite mySprite;

	static GameObject PopUpThingy;
	static GameObject StunThingy;

	 GameObject PopUpThing;
	 GameObject StunThing;

	public void CreatePopUp(string input, Color c)
	{Vector3 location = this.transform.position;
		location.y += 5;

		if (!PopUpThing) {
			PopUpThing = Resources.Load<GameObject> ("PopUp");
		}

		GameObject obj = (GameObject)Instantiate (PopUpThing, location, Quaternion.identity);
		if (mySprite != null) {
		//	obj.GetComponent<TextMesh>().te.GetComponentInChildren<Image> ().enabled = true;
			obj.GetComponentInChildren<SpriteRenderer> ().sprite = mySprite;
		} else {
			obj.GetComponent<TextMesh> ().text = input;
			obj.GetComponent<TextMesh> ().color = c;
		}


	}

	public void CreatePopUp(string input, Color c, Vector3 loc)
	{Vector3 location = loc;
		location.y += 5;

		if (!PopUpThing) {
			PopUpThing = Resources.Load<GameObject> ("PopUp");
		}

		GameObject obj = (GameObject)Instantiate (PopUpThing, location, Quaternion.identity);
		if (mySprite != null) {
			obj.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			obj.GetComponentInChildren<SpriteRenderer> ().sprite = mySprite;
		} else {
			obj.GetComponent<TextMesh> ().text = input;
			obj.GetComponent<TextMesh> ().color = c;
		}


	}


	public void CreatePopUp()
	{Vector3 location = this.transform.position;
		location.y +=5;

		if (!PopUpThing) {
			PopUpThing = Resources.Load<GameObject> ("PopUp");
		}

		GameObject obj = (GameObject)Instantiate (PopUpThing, location, Quaternion.identity);
		if (mySprite != null) {
			obj.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			obj.GetComponentInChildren<SpriteRenderer> ().sprite = mySprite;
		} else {
			obj.GetComponent<TextMesh> ().text = mytext;
			obj.GetComponent<TextMesh> ().color = textColor;
		}

	}

	public void CreatePopUp(Vector3 location)
	{location.y += 5;
		if (!PopUpThing) {
			PopUpThing = Resources.Load<GameObject> ("PopUp");
		}

		GameObject obj = (GameObject)Instantiate (PopUpThing, location, Quaternion.identity);
		if (mySprite != null) {
			obj.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			obj.GetComponentInChildren<SpriteRenderer> ().sprite = mySprite;
		} else {
			obj.GetComponent<TextMesh> ().text = mytext;
			obj.GetComponent<TextMesh> ().color = textColor;
		}

	}




	public static GameObject CreateGlobalPopUp(string input, Color c, Vector3 loc)
	{Vector3 location = loc;
		location.y += 5;

		if (!PopUpThingy) {
			PopUpThingy = Resources.Load<GameObject> ("PopUp");
		}

		GameObject obj =Instantiate<GameObject> (PopUpThingy, location, Quaternion.identity);
		TextMesh childText = obj.GetComponent<TextMesh> ();

		childText.color = c;
		childText.text = input;

		return obj;
	}

	public static GameObject CreateGlobalPopUp(string input, Color c, Vector3 loc, float duration)
	{Vector3 location = loc;
		location.y += 5;
		if (!PopUpThingy) {
			PopUpThingy = Resources.Load<GameObject> ("PopUp");
		}

		GameObject obj = Instantiate<GameObject> (PopUpThingy, location, Quaternion.identity);
		TextMesh childText =obj.GetComponent<TextMesh> ();
		childText.color = c;
		childText.text = input;
		obj.GetComponent<selfDestructTimer> ().timer = duration;
		obj.GetComponent<PopUp> ().speed = duration / 5;

		return obj;
	}


	public static GameObject CreateStunIcon(GameObject obj)
	{
		if (StunThingy == null) {
			StunThingy = Resources.Load<GameObject> ("StunIcon");
		}

		GameObject toReturn = Instantiate<GameObject> (StunThingy, obj.transform.position + Vector3.up* 9, Quaternion.identity, obj.transform);


		return toReturn;
	}


}
