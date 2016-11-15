using UnityEngine;
using System.Collections;

public class blowUp : MonoBehaviour {
	public GameObject effect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void blowUpTrigger()
	{
		
		Instantiate(effect, this.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}




}
