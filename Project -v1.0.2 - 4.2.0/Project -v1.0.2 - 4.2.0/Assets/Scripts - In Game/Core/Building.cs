using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SelectedBuilding))]
public class Building : RTSObject {
	
	private bool sellable = false;
	
	protected void Start()
	{
		//Tell the manager this building has been added
		ManagerResolver.Resolve<IManager>().BuildingAdded(this);
	}
	
	public bool InteractWith(IOrderable obj)
	{
		return false;
	}
	
	public bool CanSell()
	{
		return sellable;
	}

	public override void SetSelected ()
	{
		GetComponent<SelectedBuilding>().SetSelected ();
	}

	public override void SetDeselected ()
	{
		GetComponent<SelectedBuilding>().SetDeselected ();
	}

	public override void AssignToGroup (int groupNumber)
	{
		
	}

	public override void RemoveFromGroup ()
	{
		
	}
	
	public override void ChangeTeams(int team)
	{
		switch (team)
		{
		case Const.TEAM_GRI:
			
			break;
			
		case Const.TEAM_SALUS:
			
			break;
		}
	}
}
