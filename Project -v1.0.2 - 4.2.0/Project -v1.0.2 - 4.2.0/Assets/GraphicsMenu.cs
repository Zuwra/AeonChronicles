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
		qualityDrop.value = QualitySettings.GetQualityLevel ();
	}





	public void toggleShadows()
	{ shadows = !shadows;
		
		foreach (Light light in GameObject.FindObjectsOfType<Light>()) {
			if (shadows) {
				light.shadows = LightShadows.Soft;

			} else {
				light.shadows = LightShadows.None;
			}
		}

		}


	public void resetQuality()
		{
		QualitySettings.SetQualityLevel (qualityDrop.value);
		
	}
		


}
