using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selected : MonoBehaviour {
		
	public bool IsSelected
	{
		get;
		private set;
	}


	private GameObject cam;
	public Slider healthslider;
	public Image healthFill;

	public Slider energySlider;
	public Image energyFill;

	public Slider coolDownSlider;
	public Image coolFill;

	public enum displayType
	{
		always,damaged,selected,damAndSel,never
	}


	public displayType mydisplayType = displayType.damaged;

	private GameObject decalCircle;
	private UnitStats myStats;
	private bool onCooldown = false;
	// Use this for initialization
	void Start () 
	{	cam = GameObject.FindObjectOfType<MainCamera> ().gameObject;
		myStats = this.gameObject.GetComponent<UnitStats> ();
		decalCircle = this.gameObject.transform.Find("DecalCircle").gameObject;

		IsSelected = false;

		//If this unit is land based subscribe to the path changed event
		mydisplayType = GameObject.Find("GamePlayMenu").GetComponent<GamePlayMenu>().getDisplayType();
		if (myStats.MaxEnergy == 0) {
			energySlider.gameObject.SetActive (false);
		} else {
			updateHealthBar (myStats.currentEnergy / myStats.MaxEnergy);

		}
		updateHealthBar (myStats.health / myStats.Maxhealth);
		coolDownSlider.gameObject.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () 
	{if (this.gameObject.gameObject.gameObject.name == "Swallow") {
			Debug.Log ("Looking at " + cam.transform.position);
		}
		Vector3 location = cam.transform.position;
		location.x = this.gameObject.transform.position.x;
		healthslider.gameObject.gameObject.transform.LookAt (location);
	}





	public void updateHealthBar(float ratio)
	{
		healthslider.value = ratio; 
		if (ratio > .5) {
			healthFill.color = Color.green;
		} else if (ratio > .2) {
			healthFill.color = Color.yellow;
		} else {
			healthFill.color = Color.red;
		}

	}

	public void updateEnergyBar(float ratio)
	{
		energySlider.value = ratio;

	}

	public void updateCoolDown(float ratio)
	{
		coolDownSlider.value = ratio;
		if (ratio <= 0) {
			onCooldown = false;
			coolDownSlider.gameObject.SetActive (false);
		} else {
			onCooldown = true;
			coolDownSlider.gameObject.SetActive (true);
		}

	}


	public void SetSelected()
	{
		IsSelected = true;
		decalCircle.GetComponent<MeshRenderer> ().enabled = true;
		//m_GLManager.AddItemToRender (m_GLItem);
	}
	
	public void SetDeselected()
	{
		IsSelected = false;

		decalCircle.GetComponent<MeshRenderer> ().enabled = false;
	//	m_GLManager.RemoveItemToRender (m_GLItem);
	}
	
	public void AssignGroupNumber(int number)
	{
		
	}
	
	public void RemoveGroupNumber()
	{
		
	}
	

	

}
