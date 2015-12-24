using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GraphicsMenu : MonoBehaviour {

	public GameObject qualityDropObj;
	private Dropdown qualityDrop;
	private bool shadows = true; 
	// Use this for initialization
	void Start () {
		qualityDrop = qualityDropObj.GetComponent<Dropdown> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void toggleShadows()
	{ shadows = !shadows;
		
		foreach (GameObject light in GameObject.FindGameObjectsWithTag ("Light")) {
			if (shadows) {
				light.GetComponent<Light> ().shadows = LightShadows.Hard;

			} else {
				light.GetComponent<Light> ().shadows = LightShadows.None;
			}
		}

		}


	public void resetQuality()
		{
		QualitySettings.SetQualityLevel (qualityDrop.value);
		
	}
		


}
