using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Selected))]
public class Unit : RTSObject, IOrderable {
	



	protected ISelectedManager selectedManager
	{
		get;
		set;
	}


	public override bool UseAbility (int n, bool queue)
	{
		return true;}


	public override bool UseTargetAbility (GameObject obj, Vector3 loc, int n, bool queue)
	{
		return true;}

	public override void autoCast (int n,bool offOn)
	{
		}

	public override UnitStats getUnitStats ()
	{
		return null;
	}

	public override UnitManager getUnitManager ()
	{
		return null;
	}

	public GameObject getObject(){return this.gameObject;}


	/*
	protected void Update()
	{
		if (GetComponent<Renderer>().isVisible && guiManager.Dragging)
		{
			if (guiManager.IsWithin (transform.position))
			{
				selectedManager.AddObject(this);
			}
			else
			{
				selectedManager.DeselectObject (this);				
			}
		}
	}
		*/
	public override void SetSelected ()
	{
		if (!GetComponent<Selected>().IsSelected)
		{
			GetComponent<Selected>().SetSelected ();
		}
	}

	public override void ToggleSelected ()
	{
		if (GetComponent<Selected> ().IsSelected) {
			GetComponent<Selected> ().SetDeselected ();
		} else {
			GetComponent<Selected> ().SetSelected();
		}
	}

	public override void SetDeselected ()
	{

		if (this) {
			GetComponent<Selected> ().SetDeselected ();
		}
	}

	public void GiveOrder(Order order){
	}

	public override void AssignToGroup (int groupNumber)
	{
		//GetComponent<Selected>().AssignGroupNumber (groupNumber);
	}

	public override void RemoveFromGroup ()
	{
		//GetComponent<Selected>().RemoveGroupNumber ();
	}
	





	
	void OnDestroy()
	{
		//Remove object from selected manager
		if(selectedManager!= null)
		selectedManager.DeselectObject(this);
	}
}
