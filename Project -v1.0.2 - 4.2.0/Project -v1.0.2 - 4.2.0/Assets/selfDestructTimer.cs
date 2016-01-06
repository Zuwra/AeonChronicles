using UnityEngine;
using System.Collections;

public class selfDestructTimer : MonoBehaviour {
	public float timer;
	// Use this for initialization
	void Start () {
		StartCoroutine(MyCoroutine(timer));
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	IEnumerator MyCoroutine (float tim)
	{


		yield return new WaitForSeconds(tim);

		Destroy (this.gameObject);
	}

}
