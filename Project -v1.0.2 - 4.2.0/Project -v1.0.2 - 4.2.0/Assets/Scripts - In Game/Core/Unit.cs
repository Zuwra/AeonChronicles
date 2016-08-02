using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Selected))]
public class Unit : RTSObject, IOrderable {
	
	//Member Variables
	protected bool m_IsMoveable = true;
	

	protected bool m_IsAttackable = true;
	protected bool m_IsInteractable = false;
	
	protected IGUIManager guiManager
	{
		get;
		private set;
	}
	
	protected ISelectedManager selectedManager
	{
		get;
		private set;
	}


	public override bool UseAbility (int n)
	{
		return true;}


	public override bool UseTargetAbility (GameObject obj, Vector3 loc, int n)
	{
		return true;}

	public override void autoCast (int n)
	{
		}

	public override UnitStats getUnitStats ()
	{
		return null;
	}

	public GameObject getObject(){return this.gameObject;}

	protected void Start()
	{
		guiManager = GameObject.FindObjectOfType<GUIManager> ();// ManagerResolver.Resolve<IGUIManager>();
		selectedManager = GameObject.FindObjectOfType<SelectedManager>();

		m_IsAttackable = this is IAttackable;
	
	}
	
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
	



	public bool IsAttackable ()
	{
		return m_IsAttackable;
	}

	public bool IsMoveable ()
	{
		return m_IsMoveable;
	}

	public bool IsInteractable ()
	{
		return m_IsInteractable;
	}




	
	void OnDestroy()
	{
		//Remove object from selected manager
		if(selectedManager!= null)
		selectedManager.DeselectObject(this);
	}
}
